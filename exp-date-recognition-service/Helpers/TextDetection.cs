using System.Globalization;
using System.Text.RegularExpressions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;

namespace Exp.Date.Recognition.Helpers;

public class TextDetection
{
  public static DateTime Run(string image)
  {
    // build the credential thing
    GoogleCredential credential;

    using (var stream = new FileStream("./serviceaccount.json", FileMode.Open, FileAccess.Read))
    {
      string[] scopes = { VisionService.Scope.CloudPlatform };
      credential = GoogleCredential.FromStream(stream);
      credential = credential.CreateScoped(scopes);
    }

    // instantiate the vision service
    VisionService visionService = new VisionService(new BaseClientService.Initializer()
    {
      HttpClientInitializer = credential,
      ApplicationName = "test-app",
      GZipEnabled = true,
    });

    // create image request
    BatchAnnotateImagesRequest batchRequest = new BatchAnnotateImagesRequest();
    batchRequest.Requests = new List<AnnotateImageRequest>();
    batchRequest.Requests.Add(new AnnotateImageRequest()
    {
      Features = new List<Feature>() { new Feature() { Type = "TEXT_DETECTION", MaxResults = 1 }, },
      // ImageContext = new ImageContext() { LanguageHints = new List<string>() { language } },
      Image = new Image()
      {
        Content = image,
      }
    });

    var annotate = visionService.Images.Annotate(batchRequest);
    BatchAnnotateImagesResponse batchAnnotateImagesResponse = annotate.Execute();

    string text = batchAnnotateImagesResponse.Responses[0].TextAnnotations[0].Description;

    // attempt match xxxx-xx-xxx first (dd-MM-yyyy, yyyy-MM-dd, etc)
    Regex regex = new Regex(@"\d{1,4}.\d{1,2}.\d{2,4}");
    Match match = regex.Match(text);
    string dateTime = "";

    if (!match.Success)
    {
      // attempt to match dd-MM (used for milk)
      regex = new Regex(@"\d{2}.\d{2}");
      match = regex.Match(text);

      dateTime = match.Value.Replace('.', '-').Replace('/', '-');
      return DateTime.ParseExact(dateTime, "dd-MM", CultureInfo.InvariantCulture);
    }

    dateTime = match.Value.Replace('.', '-').Replace('/', '-');

    if (dateTime.Length == 8)
    {
      return DateTime.ParseExact(dateTime, "dd-MM-yy", CultureInfo.InvariantCulture);
    }

    return DateTime.ParseExact(dateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);
  }
}