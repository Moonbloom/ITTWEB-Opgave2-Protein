using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ITTWEB_Opgave2_Protein.Controllers.Utility
{
    public static class ModelErrorChecker
    {
        //Created to remove duplicated error message generation when a bad model is passed to a service.  
        //This method recieves the modelState, loops through all the erros, and builds them into a list.
        //An empty list means no errors.
        public static List<string> Check(ModelStateDictionary modelState)
        {
            var modelStateErrors = modelState.Values.ToList();
            var errors = new List<string>();
            foreach (var s in modelStateErrors)
            {
                foreach (var e in s.Errors)
                {
                    if (e.ErrorMessage != null && e.ErrorMessage.Trim() != "")
                    {
                        errors.Add(e.ErrorMessage);
                    }
                }
            }

            return errors;
        }
    }
}