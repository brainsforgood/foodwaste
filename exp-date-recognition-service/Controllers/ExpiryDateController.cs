using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Exp.Date.Recognition.Controllers;

public class FileFormData
{
  public IFormFile? File { get; set; }
}

public class GetExpiryDateResponse
{
  public bool Success { get; set; }
  public DateTime ExpDate { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class ExpiryDateController : ControllerBase
{
  [HttpPost(Name = "GetExpiryDate")]
  public async Task<GetExpiryDateResponse> GetExpiryDate([FromForm] FileFormData formData)
  {
    if (formData == null || formData.File == null)
    {
      return new GetExpiryDateResponse
      {
        Success = false,
      };
    }

    using (Stream stream = formData.File.OpenReadStream())
    {
      using (BinaryReader binaryReader = new BinaryReader(stream))
      {
        byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
        DateTime expDate = Helpers.TextDetection.Run(System.Convert.ToBase64String(bytes));

        return new GetExpiryDateResponse
        {
          Success = true,
          ExpDate = expDate,
        };
      }
    }
  }
}