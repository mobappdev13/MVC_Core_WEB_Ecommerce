using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using Ecommerce01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce01.Classes
{
    public class UsersHelper : IDisposable
    {
        private static readonly ApplicationDbContext UserContext = new ApplicationDbContext();
        private static readonly Ecommerce01Context db = new Ecommerce01Context();

        public static bool DeleteUser(string userName, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var userAsp = userManager.FindByEmail(userName);
            if (userAsp == null)
            {
                return false;
            }
            var response = userManager.RemoveFromRole(userAsp.Id, roleName);
            return response.Succeeded;
        }

        public static bool UpdateUserName(string currentUserName, string newUserName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var userAsp = userManager.FindByEmail(currentUserName);
            if (userAsp == null)
            {
                return false;
            }
            //two methods
            userAsp.UserName = newUserName;
            userAsp.Email = newUserName;
            //
            var response = userManager.Update(userAsp);
            return response.Succeeded;
        }

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(UserContext));

            // Check to see if Role Exists, if not create it
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        //see web.config
        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var email = WebConfigurationManager.AppSettings["AdminUser"];
            var password = WebConfigurationManager.AppSettings["AdminPassWord"];
            var userASP = userManager.FindByName(email);
            //superuser and Admin
            if (userASP == null)
            {
                CreateUserAsp(email, "Admin", password);
                return;
            }

            userManager.AddToRole(userASP.Id, "Admin");
        }

        //overload two arguments
        public static void CreateUserAsp(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var userAsp = userManager.FindByEmail(email);
            if (userAsp == null)
            {
                userAsp = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                };
                userManager.Create(userAsp, email);
            }
            userManager.AddToRole(userAsp.Id, roleName);
        }

        //overload three arguments
        public static void CreateUserAsp(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));

            var userAsp = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userAsp, password);
            userManager.AddToRole(userAsp.Id, roleName);
        }

        //psw
        public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(UserContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                return;
            }

            var user = db.Users.FirstOrDefault(tp => tp.UserName == email);
            if (user == null)
            {
                return;
            }

            var random = new Random();
            var newPassword = string.Format("{0}{1}{2:04}*",
                user.FirstName.Trim().ToUpper().Substring(0, 1),
                user.LastName.Trim().ToLower(),
                random.Next(10000));

            userManager.RemovePassword(userASP.Id);
            userManager.AddPassword(userASP.Id, newPassword);

            var subject = "Mvc Core Recupero della Password";
            var body = string.Format(@"
                <h1>Mvc Core Recupero della Password</h1>
                <p>Su nuova password è: <strong>{0}</strong></p>
                <p>Per cortesia, cambie la sua password per una che possa ricordare facilmente",
                newPassword);

            await MailHelper.SendMail(email, subject, body);
        }

        public void Dispose()
        {
            UserContext.Dispose();
            db.Dispose();
        }
    }
}