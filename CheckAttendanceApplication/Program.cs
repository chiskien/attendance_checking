var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var app = builder.Build();


app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}/{isCheckAttendace?}");
app.Run();