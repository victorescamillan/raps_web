using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RapsCore2.Models;

namespace RapsCore2.Controllers
{
    public class ProductController : Controller
    {
        private readonly DB_A1A56D_RapsFinalContext _context;
        public ProductController(DB_A1A56D_RapsFinalContext context)
        {
            _context = context;
        }
        public IActionResult MXG()
        {
            var product = _context.Product.SingleOrDefault( d => d.Id == 2);
            if (product != null)
                return View(product);
            else
                return View();
        }
        public IActionResult MXGMobile()
        {
            var product = _context.Product.SingleOrDefault(d => d.Id == 2);
            if (product != null)
                return View(product);
            else
                return View();
        }
        public IActionResult Spiru()
        {
            var product = _context.Product.SingleOrDefault(d => d.Id == 3);
            if (product != null)
                return View(product);
            else
                return View();
        }
        public IActionResult SpiruMobile()
        {
            var product = _context.Product.SingleOrDefault(d => d.Id == 3);
            if (product != null)
                return View(product);
            else
                return View();
        }
        public IActionResult Xtrim()
        {
            var product = _context.Product.SingleOrDefault(d => d.Id == 4);
            if (product != null)
                return View(product);
            else
                return View();
        }
        public IActionResult XtrimMobile()
        {
            var product = _context.Product.SingleOrDefault(d => d.Id == 4);
            if (product != null)
                return View(product);
            else
                return View();
        }
        [Route("api/product/GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _context.Product.ToList();
            
            return new ObjectResult(products);
        }
    }
}