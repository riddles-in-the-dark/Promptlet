using Bodie.Promptlet.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddPrompletContext(builder.Configuration.GetConnectionString("PrompletContext") ?? throw new ArgumentNullException("Connection string not found."));
builder.Services.AddHttpClient();

var app = builder.Build();

;// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();

}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PromptletContext>();
    context.Database.EnsureCreated();
      DbInitializer.Initialize(context);
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
