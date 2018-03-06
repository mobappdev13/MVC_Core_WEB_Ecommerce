using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using Ecommerce01.Models;

namespace Ecommerce01.Classes
{
    public class DbHelper
    {
        public static Response SaveChanges(Ecommerce01Context db)
        {
            try
            {
                db.SaveChanges();
                return new Response() { Succeeded = true };
            }
            catch (Exception exception)
            {
                var response = new Response() { Succeeded = false };
                if (exception.InnerException?.InnerException != null && exception.InnerException.InnerException.Message.Contains("_Index"))
                {
                    response.Message = "Esiste già un Registro con lo stesso valore !";
                }
                else if (exception.InnerException?.InnerException != null &&
                         exception.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    response.Message = "Questo registro non può essere eliminato, ha dei registri collegati";

                }
                else
                {
                    response.Message = exception.Message;
                }
                return response;
            }
        }


        public static int GetState(string description, Ecommerce01Context db)
        {
            var state = db.States.FirstOrDefault(s => s.Description == description);
            if (state != null)
                return state.StateId;

            state = new State()
            {
                Description = description
            };

            db.States.Add(state);
            db.SaveChanges();
            return state.StateId;
        }
    }
}