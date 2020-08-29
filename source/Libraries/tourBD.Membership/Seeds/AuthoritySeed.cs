using System.Threading.Tasks;
using tourBD.Core.Seeds;
using tourBD.Membership.Contexts;
using tourBD.Membership.Entities;
using tourBD.Membership.Services;

namespace tourBD.Membership.Seeds
{
    public class AuthoritySeed : DataSeed
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        private readonly ApplicationUser _adminUser, _supportUser, _marketingUser;
        private readonly Role _adminRole, _supportRole, _marketingRole;

        public AuthoritySeed(UserManager _userManager, RoleManager _roleManager, ApplicationDbContext dbContext) 
            : base(dbContext)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;

            _adminUser = new ApplicationUser("kshuvo96@gmail.com", "Kawsarul Alam", "01764206806", "kshuvo96@gmail.com", @"\img\no-profile.png");
            _supportUser = new ApplicationUser("mahin@gmail.com", "Mahin Hosen", "01789577996", "mahin@gmail.com", @"\img\no-profile.png");
            _marketingUser = new ApplicationUser("arif@gmail.com", "Ariful Islam", "01688032774", "arif@gmail.com", @"\img\no-profile.png");

            _adminRole = new Role("Admin");
            _supportRole = new Role("Support");
            _marketingRole = new Role("Marketing");
        }

        private async Task<bool> CheckAndCreateRoleAsync(Role role)
        {
            if ((await _roleManager.FindByNameAsync(role.Name)) == null)
            {
                var result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }
            return true;
        }

        public override async Task SeedAsync()
        {
            if ((await _userManager.FindByNameAsync(_adminUser.UserName.ToUpper())) == null)
            {
                var result = await _userManager.CreateAsync(_adminUser, "Admin$2020");
                if (result.Succeeded)
                {
                    if (await CheckAndCreateRoleAsync(_adminRole))
                        await _userManager.AddToRoleAsync(_adminUser, _adminRole.Name);
                    if (await CheckAndCreateRoleAsync(_supportRole))
                        await _userManager.AddToRoleAsync(_adminUser, _supportRole.Name);
                }
            }

            if ((await _userManager.FindByNameAsync(_supportUser.UserName.ToUpper())) == null)
            {
                var result = await _userManager.CreateAsync(_supportUser, "Support$2020");
                if (result.Succeeded)
                {
                    if (await CheckAndCreateRoleAsync(_supportRole))
                        await _userManager.AddToRoleAsync(_supportUser, _supportRole.Name);
                }
            }

            if ((await _userManager.FindByNameAsync(_marketingUser.UserName.ToUpper())) == null)
            {
                var result = await _userManager.CreateAsync(_marketingUser, "Marketing$2020");
                if (result.Succeeded)
                {
                    if (await CheckAndCreateRoleAsync(_marketingRole))
                        await _userManager.AddToRoleAsync(_marketingUser, _marketingRole.Name);
                }
            }
        }
    }
}
