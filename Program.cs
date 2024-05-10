using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Api Consume Ayarlar�
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options
        => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//JSON Serializer Ayarlar�
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ContractResolver
    = new DefaultContractResolver());


var app = builder.Build();
//Api Consume Ayarlar�
app.UseCors(options
        => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Photo Ayarlar�
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider
    (Path.Combine(Directory.GetCurrentDirectory(),"Photos")),
    RequestPath = "/Photos"
});

app.Run();
