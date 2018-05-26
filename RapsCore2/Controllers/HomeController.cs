using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RapsCore2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace RapsCore2.Controllers
{
    public class HomeController : Controller
    {
        private DB_A1A56D_RapsFinalContext _context;
        public HomeController(DB_A1A56D_RapsFinalContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            if (id.HasValue)
                HttpContext.Session.SetInt32("member_id", (int)id);
          
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Stockistship()
        {
            return View();
        }
        public IActionResult CommPlan()
        {
            return View();
        }
        public IActionResult ReferralBonus()
        {
            return View();
        }
        public IActionResult BusinessBuildUp()
        {
            return View();
        }
        public IActionResult RepeatPurchase()
        {
            return View();
        }
        public IActionResult MaintenancePurchase()
        {
            return View();
        }
        public IActionResult PepetualBonus()
        {
            return View();
        }
        public IActionResult LoyaltyBonus()
        {
            return View();
        }
        public IActionResult RetailProfit()
        {
            return View();
        }
        public IActionResult TranferableBusiness()
        {
            return View();
        }
        public IActionResult EliteRanking()
        {
            return View();
        }
    }
}
