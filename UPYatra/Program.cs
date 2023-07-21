using Microsoft.EntityFrameworkCore;
using UPYatra.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogDataContext>(options => 
{
    var conn = builder.Configuration.GetConnectionString("BlogDataContext");
    options.UseSqlServer(conn); 
});
builder.Services.AddDbContext<IdentityDataContext>(options => 
{
    var conn = builder.Configuration.GetConnectionString("IdentityDataContext");
    options.UseSqlServer(conn); 
});
builder.Services.AddTransient<FormattingService>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDataContext>()
            .AddDefaultTokenProviders(); ;
//builder.Services.AddTransient<FeatureToggle>();
//builder.services.AddControllersWithViews().AddRazorRuntimeCompilation();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error.html");
}

//app.UseFileServer();
app.UseStaticFiles();
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("invalid"))
        throw new Exception("new exception");
    await next();
}
);
app.UseAuthentication();
app.UseMvc(routes =>
{
    routes.MapRoute("Default", "{controller=Home}/{action=Index}/{Id?}"
        );
});
//app.MapGet("/", () => "Hello World!");

app.Run();
