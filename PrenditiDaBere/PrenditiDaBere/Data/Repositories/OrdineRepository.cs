using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PrenditiDaBere.Data.Repositories
{
    public class OrdineRepository : IOrdineRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;


        public OrdineRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreaOrdine(Ordine ordine)
        {
            ordine.DataOrdine = DateTime.Now;

            _appDbContext.Ordini.Add(ordine);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var dettagliOrdine = new DettagliOrdine
                {
                    Quantita = shoppingCartItem.Quantita,
                    BibitaId = shoppingCartItem.Bibita.BibitaId,
                    OrdineId = ordine.OrdineId,
                    Prezzo = shoppingCartItem.Bibita.Prezzo
                };

                _appDbContext.DettagliOrdini.Add(dettagliOrdine);
            }

            _appDbContext.SaveChanges();
        }
    }    
}
