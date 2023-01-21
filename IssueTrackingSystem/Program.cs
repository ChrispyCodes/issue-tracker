using AspNetCoreHero.ToastNotification;
using IssueTrackingSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DevConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
    //options.Conventions.AddPageRoute("/Dashboard", "Index")


builder.Services.AddNotyf(config => { config.DurationInSeconds = 5; config.IsDismissable = true; config.Position = NotyfPosition.TopRight;  });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IssueTrackingSystem.Areas.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
    await IssueTrackingSystem.Areas.Identity.Seeds.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
    await IssueTrackingSystem.Areas.Identity.Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

    
//var scope = app.Services.CreateScope();
//var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
//var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//await IssueTrackingSystem.Areas.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
//await IssueTrackingSystem.Areas.Identity.Seeds.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
//await IssueTrackingSystem.Areas.Identity.Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
    
app.Run();
