using BlogWeb.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.LoadConfiguration();
builder.ConfigureAuthentication();
builder.ConfigureMvc();
builder.ConfigureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication(); // O que você é
app.UseAuthorization(); // o que pode fazer
app.MapControllers();
app.UseStaticFiles();
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();