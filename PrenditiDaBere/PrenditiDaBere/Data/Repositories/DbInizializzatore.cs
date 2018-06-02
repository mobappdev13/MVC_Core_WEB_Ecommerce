using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PrenditiDaBere.Data.Models;
using PrenditiDaBere.Data.Repositories;

namespace PrenditiDaBere.Data
{
    public static class DbInizializzatore
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                context.Database.EnsureCreated();

                // Look for any Bibite.
                if (context.Bibite.Any())
                {
                    return;   // DB has been seeded
                }

                if (!context.Categorie.Any())
                {
                    context.Categorie.AddRange(Categorie.Select(c => c.Value));
                }

                if (!context.Bibite.Any())
                {
                    context.AddRange
                    (
                        new Bibita
                        {
                            Nome = "Birra",
                            Prezzo = 7.95M,
                            PiccolaDescrizione = "Il secondo alcol più consumato al mondo",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Birra.jpg",
                            Disponibile = true,
                            BibitaPreferita = true,
                            MiniaturaImmagine = "Birra.jpg",
                        },
                        new Bibita
                        {
                            Nome = "Cuba Libre",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = "Cocktail a base di cola, lime e rum.",
                            LungaDescrizione = "bla bla bla 1267 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "CubaLibre.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "CubaLibre.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Tequila ",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = "Bevanda ricavata dalla pianta di agave blu.",
                            LungaDescrizione = "bla bla bla 1298 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Tequila.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Tequila.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Vino ",
                            Prezzo = 16.75M,
                            PiccolaDescrizione = "La bibita alcolica più consumata al mondo, Unaelegantissima Bibita",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Wine.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Wine.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Margarita",
                            Prezzo = 17.95M,
                            PiccolaDescrizione = "Un cocktail con Sec (tipo grapa alla arancia), tequila e lime",
                            Categoria = Categorie["Alcolica"],
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            UrlImmagine = "Margarita.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Margarita.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Whisky",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Il modo migliore per assaggiare il Whisky",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Whisky.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Whisky.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Jagermeister",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Un digestivo Tedesco fatto con 56 erbe.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Jagermeister.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Jagermeister.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Champagne",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "È così che si può chiamare lo spumante",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Champagne.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Champagne.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Piña Colada ",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Un dolce cocktail fatto con rum con sapore di ananas.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "PiñaColada.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "PiñaColada.jpg"
                        },
                        new Bibita
                        {
                            Nome = "White Russian",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "un cocktail a base di vodka, liquore al caffè e latte servito sul ghiaccio. ",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "WhiteRussian.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "WhiteRussian.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Long Island Iced Tea- Tè freddo",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Aa mixed Bibita made with tequila.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "LongIslandIceTea.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "LongIslandIceTea.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Vodka",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Una bevanda distillata con acqua ed etanolo.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Vodka.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Vodka.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Gin e tonico",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Fatta con Gin Vodka e acqua Tonica.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "GinTonic.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "GinTonic.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Cosmopolitan",
                            Prezzo = 15.95M,
                            PiccolaDescrizione = "Realizzato con vodka, triple sec, succo di mirtillo.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Alcolica"],
                            UrlImmagine = "Cosmopolitan.jpg",
                            Disponibile = false,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Cosmopolitan.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Tè",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = "Prodotto dalle foglie della pianta del tè in acqua calda.",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Analcolica"],
                            UrlImmagine = "Tea.jpg",
                            Disponibile = true,
                            BibitaPreferita = true,
                            MiniaturaImmagine = "Tea.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Acqua ",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = " Più della metà del tuo peso corporeo",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Analcolica"],
                            UrlImmagine = "Acqua.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Acqua.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Caffe",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = " Una bevanda meravigliosa !",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Analcolica"],
                            UrlImmagine = "Coffe.jpg",
                            Disponibile = true,
                            BibitaPreferita = true,
                            MiniaturaImmagine = "Coffe.jpg",
                        },
                        new Bibita
                        {
                            Nome = "Kvass",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = "Una bevanda Russa molto rinfrescante",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Analcolica"],
                            UrlImmagine = "Kvass.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "Kvass.jpg"
                        },
                        new Bibita
                        {
                            Nome = "Succo di Fruta ",
                            Prezzo = 12.95M,
                            PiccolaDescrizione = "Succo naturale fatto con della frutta naturali",
                            LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                            Categoria = Categorie["Analcolica"],
                            UrlImmagine = "SuccoF.jpg",
                            Disponibile = true,
                            BibitaPreferita = false,
                            MiniaturaImmagine = "SuccoF.jpg"
                        },
                         new Bibita
                         {
                             Nome = "Succo Misto ",
                             Prezzo = 12.95M,
                             PiccolaDescrizione = "Succo naturale, che contiene frutta e verdura.",
                             LungaDescrizione = "bla bla bla 123 si si si Sempre ",
                             Categoria = Categorie["Analcolica"],
                             UrlImmagine = "SuccoM.jpg",
                             Disponibile = true,
                             BibitaPreferita = false,
                             MiniaturaImmagine = "SuccoM.jpg"
                         }
                    );
                }

                context.SaveChanges();
            }
        }
   

    private static Dictionary<string, Categoria> categorie;

    public static Dictionary<string, Categoria> Categorie
    {
        get
        {
            if (categorie == null)
            {
                var genresList = new Categoria[]
                {
                        new Categoria { NomeCategoria = "Alcolica", Descrizione="Tutte le bibite alcoliche" },
                        new Categoria { NomeCategoria = "Analcolica", Descrizione="Tutte le bibite analcoliche" }
                };

                categorie = new Dictionary<string, Categoria>();

                foreach (Categoria genre in genresList)
                {
                    categorie.Add(genre.NomeCategoria, genre);
                }
            }

            return categorie;
         }
        }
    }
}
    