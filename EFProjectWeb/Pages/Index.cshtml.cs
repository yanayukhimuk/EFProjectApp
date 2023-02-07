using EFDataAccessLibrary.DataAccess;
using EFDataAccessLibrary.Extensions;
using EFDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EFProjectWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyDbContext _db;

        public IndexModel(ILogger<IndexModel> logger, MyDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            LoadData();
        }

        public void LoadData()
        {
            if (_db.Products.Any()) { return; }

            List<EFDataAccessLibrary.JsonModels.Order> jsonOrders = new List<EFDataAccessLibrary.JsonModels.Order>();
            using (StreamReader r = new StreamReader("data2.json"))
            {
                string json = r.ReadToEnd();
                jsonOrders = JsonSerializer.Deserialize<List<EFDataAccessLibrary.JsonModels.Order>>(json);
            }

            foreach (var jsonOrder in jsonOrders)
            {
                _db.AddRange(
                        new Order()
                        {
                            CreatedDate = jsonOrder.CreatedDate,
                            UpdatedDate = jsonOrder.UpdatedDate,
                            Status= jsonOrder.Status,
                            Products = jsonOrder.Products.ConvertToEntity()
                        });
            }
            _db.SaveChanges();
        }
    }
}