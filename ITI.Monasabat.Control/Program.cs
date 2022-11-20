using Microsoft.AspNetCore.Identity;
using Monasapat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentity<User,IdentityRole>
           ().AddEntityFrameworkStores<MonasabatContext>(); 
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(MonasabatContext));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/SignIn";
    options.AccessDeniedPath = "/User/AccessDenied";
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{id2?}/{id3?}");

app.Run();
