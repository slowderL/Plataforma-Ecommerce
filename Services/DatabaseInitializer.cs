using Microsoft.AspNetCore.Identity;
using BestStoreMVC.Models;

namespace BestStoreMVC.Services
{
    public class DatabaseInitializer
    {
        public static async Task SeedDataAsync(UserManager<ApplicationUser>? userManager,
            RoleManager<IdentityRole>? roleManager)
        {
            if (userManager == null || roleManager ==null)
            {
                Console.WriteLine("UserManager or RoleManager is null => exit");
                return;
            }

            //checa se é um administrador ou não
            var exists = await roleManager.RoleExistsAsync("Admin");
            if (!exists)
            {
                Console.WriteLine("A rota de admin não estava definida e vamos criar");
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //checa se é um vendedor ou não
             exists = await roleManager.RoleExistsAsync("Vendedor");
            if (!exists)
            {
                Console.WriteLine("A rota de Vendedor não estava definida e vamos criar");
                await roleManager.CreateAsync(new IdentityRole("Vendedor"));
            }

            //checa se é um cliente ou não
            exists = await roleManager.RoleExistsAsync("Cliente");
            if (!exists)
            {
                Console.WriteLine("A rota de Cliente não estava definida e vamos criar");
                await roleManager.CreateAsync(new IdentityRole("Cliente"));
            }

            //verifica se temos pelo menos um usuario administrador 
            var AdminUsers = await userManager.GetUsersInRoleAsync("Admin");
            if (AdminUsers.Any())
            {
                Console.WriteLine("usuario admin já existe => exit");
                return;
            }

            var user = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "Admin", 
                UserName = "admin@admin.com",
                Email ="admin@admin.com",
                CreatedAt = DateTime.Now,
            };

            string initialPassword = "Admin123";

            var result = await userManager.CreateAsync(user, initialPassword);
            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                Console.WriteLine("Usuario admin criado com sucesso! Cadastre uma nova senha");
                Console.WriteLine("Email: " + user.Email);
                Console.WriteLine("Senha: " + initialPassword);
            }

        }
    }
}
