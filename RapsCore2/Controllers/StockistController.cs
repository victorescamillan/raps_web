using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RapsCore2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RapsCore2.Controllers
{
    public class StockistController : Controller
    {
        // GET: /<controller>/
        private readonly DB_A1A56D_RapsFinalContext _context;

        public StockistController(DB_A1A56D_RapsFinalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        //[Route("api/stockist/GetStockists")]
        //public IActionResult GetStockists()
        //{
            

        //    return null;
        //    //return new ObjectResult(products);
        //}
    }
}
