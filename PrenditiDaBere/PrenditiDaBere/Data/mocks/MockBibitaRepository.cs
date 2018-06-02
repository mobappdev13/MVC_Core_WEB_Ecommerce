using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;


namespace PrenditiDaBere.Data.mocks
{
    public class MockBibitaRepository : IBibitaRepository
    {
        private readonly ICategoriaRepository _categoriaRepository = new MockCategoriaRepository();

        public IEnumerable<Bibita> Bibite {
            get
            {
                return new List<Bibita>
                {
                    new Bibita
                    {
                      Nome = "Birra",
                      Prezzo = 7.95M,
                      LungaDescrizione ="La seconda bevanda più popolare al mondo è nata in una collisione tra Stati Uniti e Spagna. Accadde durante la guerra ispano-americana alla fine del secolo, quando Teddy Roosevelt, i cavalieri rudi e gli americani in gran numero arrivarono a Cuba. Un pomeriggio, un gruppo di soldati fuori servizio del Corpo dei Segnali degli Stati Uniti si è riunito in un bar dell'Avana Vecchia. Fausto Rodriguez, un giovane messaggero, in seguito ha ricordato che il Capitano Russell è entrato e ha ordinato il rum Bacardi (Oro) e la Coca-Cola sul ghiaccio con un cuneo di lime. Il capitano bevve la mistura con tale piacere che suscitò l'interesse dei soldati intorno a lui. Avevano il barista preparare un giro della bevanda del capitano per loro. Il rum Bacardi e Coca-Cola fu un successo immediato. Come fa ancora oggi, la bevanda ha unito la folla in uno spirito di divertimento e buona compagnia. Quando ordinarono un altro round, un soldato suggerì di brindare a Cuba Libre! per celebrare la Cuba appena liberata.",
                      Categoria = _categoriaRepository.Categorie.First(),
                      UrlImmagine = "https://imgh.us/beerL_2.jpg",
                      Disponibile  = true,
                      BibitaPreferita = true,
                      MiniaturaImmagine = "https://imgh.us/beerS_1.jpeg"
                    },
                     new Bibita
                    {
                      Nome = "Cuba Libre",
                      Prezzo = 13.95M,
                      LungaDescrizione ="La seconda bevanda più popolare al mondo è nata in una collisione tra Stati Uniti e Spagna. Accadde durante la guerra ispano-americana alla fine del secolo, quando Teddy Roosevelt, i cavalieri rudi e gli americani in gran numero arrivarono a Cuba. Un pomeriggio, un gruppo di soldati fuori servizio del Corpo dei Segnali degli Stati Uniti si è riunito in un bar dell'Avana Vecchia. Fausto Rodriguez, un giovane messaggero, in seguito ha ricordato che il Capitano Russell è entrato e ha ordinato il rum Bacardi (Oro) e la Coca-Cola sul ghiaccio con un cuneo di lime. Il capitano bevve la mistura con tale piacere che suscitò l'interesse dei soldati intorno a lui. Avevano il barista preparare un giro della bevanda del capitano per loro. Il rum Bacardi e Coca-Cola fu un successo immediato. Come fa ancora oggi, la bevanda ha unito la folla in uno spirito di divertimento e buona compagnia. Quando ordinarono un altro round, un soldato suggerì di brindare a Cuba Libre! per celebrare la Cuba appena liberata.",
                      Categoria = _categoriaRepository.Categorie.First(),
                      UrlImmagine = "https://imgh.us/beerL_2.jpg",
                      Disponibile  = true,
                      BibitaPreferita = true,
                      MiniaturaImmagine = "https://imgh.us/beerS_1.jpeg"
                    },
                       new Bibita
                    {
                      Nome = "Tequila",
                      Prezzo = 5.95M,
                      LungaDescrizione ="La seconda bevanda più popolare al mondo è nata in una collisione tra Stati Uniti e Spagna. Accadde durante la guerra ispano-americana alla fine del secolo, quando Teddy Roosevelt, i cavalieri rudi e gli americani in gran numero arrivarono a Cuba. Un pomeriggio, un gruppo di soldati fuori servizio del Corpo dei Segnali degli Stati Uniti si è riunito in un bar dell'Avana Vecchia. Fausto Rodriguez, un giovane messaggero, in seguito ha ricordato che il Capitano Russell è entrato e ha ordinato il rum Bacardi (Oro) e la Coca-Cola sul ghiaccio con un cuneo di lime. Il capitano bevve la mistura con tale piacere che suscitò l'interesse dei soldati intorno a lui. Avevano il barista preparare un giro della bevanda del capitano per loro. Il rum Bacardi e Coca-Cola fu un successo immediato. Come fa ancora oggi, la bevanda ha unito la folla in uno spirito di divertimento e buona compagnia. Quando ordinarono un altro round, un soldato suggerì di brindare a Cuba Libre! per celebrare la Cuba appena liberata.",
                      Categoria = _categoriaRepository.Categorie.First(),
                      UrlImmagine = "",
                      Disponibile  = true,
                      BibitaPreferita = true,
                      MiniaturaImmagine = "https://imgh.us/beerS_1.jpeg"
                    },
                         new Bibita
                    {
                      Nome = "Succhi di Frutta",
                      Prezzo = 7.95M,
                      LungaDescrizione ="La seconda bevanda più popolare al mondo è nata in una collisione tra Stati Uniti e Spagna. Accadde durante la guerra ispano-americana alla fine del secolo, quando Teddy Roosevelt, i cavalieri rudi e gli americani in gran numero arrivarono a Cuba. Un pomeriggio, un gruppo di soldati fuori servizio del Corpo dei Segnali degli Stati Uniti si è riunito in un bar dell'Avana Vecchia. Fausto Rodriguez, un giovane messaggero, in seguito ha ricordato che il Capitano Russell è entrato e ha ordinato il rum Bacardi (Oro) e la Coca-Cola sul ghiaccio con un cuneo di lime. Il capitano bevve la mistura con tale piacere che suscitò l'interesse dei soldati intorno a lui. Avevano il barista preparare un giro della bevanda del capitano per loro. Il rum Bacardi e Coca-Cola fu un successo immediato. Come fa ancora oggi, la bevanda ha unito la folla in uno spirito di divertimento e buona compagnia. Quando ordinarono un altro round, un soldato suggerì di brindare a Cuba Libre! per celebrare la Cuba appena liberata.",
                      Categoria = _categoriaRepository.Categorie.First(),
                      UrlImmagine = "https://imgh.us/beerL_2.jpg",
                      Disponibile  = true,
                      BibitaPreferita = true,
                      MiniaturaImmagine = "https://imgh.us/beerS_1.jpeg"
                    },
                          new Bibita
                    {
                      Nome = "Succhi di Mixti",
                      Prezzo = 7.95M,
                      LungaDescrizione ="La seconda bevanda più popolare al mondo è nata in una collisione tra Stati Uniti e Spagna. Accadde durante la guerra ispano-americana alla fine del secolo, quando Teddy Roosevelt, i cavalieri rudi e gli americani in gran numero arrivarono a Cuba. Un pomeriggio, un gruppo di soldati fuori servizio del Corpo dei Segnali degli Stati Uniti si è riunito in un bar dell'Avana Vecchia. Fausto Rodriguez, un giovane messaggero, in seguito ha ricordato che il Capitano Russell è entrato e ha ordinato il rum Bacardi (Oro) e la Coca-Cola sul ghiaccio con un cuneo di lime. Il capitano bevve la mistura con tale piacere che suscitò l'interesse dei soldati intorno a lui. Avevano il barista preparare un giro della bevanda del capitano per loro. Il rum Bacardi e Coca-Cola fu un successo immediato. Come fa ancora oggi, la bevanda ha unito la folla in uno spirito di divertimento e buona compagnia. Quando ordinarono un altro round, un soldato suggerì di brindare a Cuba Libre! per celebrare la Cuba appena liberata.",
                      Categoria = _categoriaRepository.Categorie.First(),
                      UrlImmagine = "https://imgh.us/beerL_2.jpg",
                      Disponibile  = true,
                      BibitaPreferita = true,
                      MiniaturaImmagine = "https://imgh.us/beerS_1.jpeg"
                    }
                };
            }
        }

        public IEnumerable<Bibita> BibitePreferite { get; }

        public Bibita GetBibitaById(int bibtaId)
        {
            throw new NotImplementedException();
        }
    }
}