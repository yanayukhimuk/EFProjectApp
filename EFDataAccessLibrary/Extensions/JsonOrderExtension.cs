using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDataAccessLibrary.Extensions
{
    public static class JsonOrderExtension
    {
        public static List<Models.Product> ConvertToEntity(this List<JsonModels.Product> product)
        {
            List<Models.Product> results = (from p in product
                                             select new Models.Product()
                                             {
                                                 ProductId = p.ProductId,
                                                 Name = p.Name,
                                                 Description = p.Description,
                                                 Weight = p.Weight,
                                                 Height = p.Height,
                                                 Width = p.Width,
                                                 Length = p.Length
                                             }).ToList();
            return results;
        }
    }
}
