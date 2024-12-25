using Microsoft.AspNetCore.Authentication.JwtBearer;

using MyApp.Api;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppDI(builder.Configuration);

builder.Services.AddSwagger();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");


app.UseAuthentication();  
app.UseAuthorization();   

app.MapControllers();
//app.Urls.Add("https://localhost:7012");


app.Run();
