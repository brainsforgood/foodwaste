var builder = WebApplication.CreateBuilder(args);


// Exp.Date.Recognition.Helpers.TextDetection.Test("https://i0.wp.com/www.optimavita.nl/wp-content/uploads/2021/04/20210413_083634-scaled.jpg?ssl=1");
// Exp.Date.Recognition.Helpers.TextDetection.Test("https://dekoningh.nl/wp-content/uploads/2020/11/Nieuws_uitgelichte-afbeelding_Verschil-THT-TGT.jpg");

// Exp.Date.Recognition.Helpers.TextDetection.Test();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
