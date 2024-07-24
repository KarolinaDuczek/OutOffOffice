using Microsoft.AspNetCore.Identity;

namespace OutOfOffice_web.Data;

public class UserInitializer()
{
    public static async void Initialize(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var admin = await userManager.FindByEmailAsync("admin@admin.com");
            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Administrator");
            }

            var hrManager = await userManager.FindByEmailAsync("hrmanager@example.com");
            if (hrManager == null)
            {
                hrManager = new IdentityUser
                {
                    UserName = "hrmanager@example.com",
                    Email = "hrmanager@example.com",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(hrManager, "Hrmanager123!");
                await userManager.AddToRoleAsync(hrManager, "HRManager");
            }

            var projectManager = await userManager.FindByEmailAsync("projectmanager@example.com");
            if (projectManager == null)
            {
                projectManager = new IdentityUser
                {
                    UserName = "projectmanager@example.com",
                    Email = "projectmanager@example.com",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(projectManager, "Projectmanager123!");
                await userManager.AddToRoleAsync(projectManager, "ProjectManager");
            }

            var employee = await userManager.FindByEmailAsync("employee@example.com");
            if (employee == null)
            {
                employee = new IdentityUser
                {
                    UserName = "employee@example.com",
                    Email = "employee@example.com",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(employee, "Employee123!");
                await userManager.AddToRoleAsync(employee, "Employee");
            }
        }
    }
}
