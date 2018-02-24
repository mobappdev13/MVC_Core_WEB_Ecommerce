using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Ecommerce01.Models;

namespace Ecommerce01
{
    public partial class Startup
    {
        // Per altre informazioni su come configurare l'autenticazione, vedere https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configurare il contesto di database, la gestione utenti e la gestione accessi in modo da usare un'unica istanza per richiesta
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Consentire all'applicazione di utilizzare un cookie per memorizzare informazioni relative all'utente connesso
            // e per memorizzare temporaneamente le informazioni relative a un utente che accede tramite un provider di accesso di terze parti
            // Configurare il cookie di accesso
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Consente all'applicazione di convalidare l'indicatore di sicurezza quando l'utente esegue l'accesso.
                    // Questa funzionalità di sicurezza è utile quando si cambia una password o si aggiungono i dati di un account di accesso esterno all'account personale.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Consente all'applicazione di memorizzare temporaneamente le informazioni dell'utente durante la verifica del secondo fattore nel processo di autenticazione a due fattori.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Consente all'applicazione di memorizzare il secondo fattore di verifica dell'accesso, ad esempio il numero di telefono o l'indirizzo e-mail.
            // Una volta selezionata questa opzione, il secondo passaggio di verifica durante la procedura di accesso viene memorizzato sul dispositivo usato per accedere.
            // È simile all'opzione RememberMe disponibile durante l'accesso.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Rimuovere il commento dalle seguenti righe per abilitare l'accesso con provider di accesso di terze parti
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}