using bitirmee.DTO;
using Neo4jClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var client = new BoltGraphClient(new Uri("bolt://localhost:7687"), "admin", "admin123");
var adminDto = new AdminDto();
client.ConnectAsync();
builder.Services.AddSingleton<IGraphClient>(client);
builder.Services.AddSingleton<AdminDto>(adminDto);
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
