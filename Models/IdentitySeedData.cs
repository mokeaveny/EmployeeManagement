using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Models
{
    public class IdentitySeedData
    {
        // Creates the department data to be saved to the database.
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<IdentityContext>();
            context.Database.EnsureCreated();
            if (!context.Departments.Any())
            {
                Department finance = new Department
                {
                    DepartmentID = 1,
                    Name = "Finance",
                    HeadOfDepartment = "John Smith",
                    NumberOfEmployees = 100
                };

                context.Departments.Add(finance);
                context.SaveChanges();
            }

        }

        public static void CreateAdminAccount(IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            CreateAdminAccountAsync(serviceProvider, configuration).Wait();
        }

        public static async Task CreateAdminAccountAsync(IServiceProvider
            serviceProvider, IConfiguration configuration)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;

            UserManager<Employee> userManager =
                serviceProvider.GetRequiredService<UserManager<Employee>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:AdminUser:Name"] ?? "admin";
            string email = configuration["Data:AdminUser:Email"] ?? "admin@example.com";
            string password = configuration["Data:AdminUser:Password"] ?? "Password123$";
            string role = configuration["Data:AdminUser:Role"] ?? "Admin";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                Employee employee = new Employee
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(employee, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, role);
                }
            }
        }
    }
}
