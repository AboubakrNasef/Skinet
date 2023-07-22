


using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {

        public static  async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Aboubakr"
                    ,UserName="Aboubakr",
                    Email = "Admin@admin",
                    Address = new Address()
                    {
                        FirstName = "Aboubakr",
                        LastName="Nasef"
                        ,
                        Street = "st1",
                        City="ct",
                        State="state",
                        ZipCode="22",
                        

                    }
                };
             var res =    await userManager.CreateAsync(user, "@Admin123");
            }
        }
    }
}
