using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
namespace eCommerceReloaded.Controllers
{
    public class OrderController : Controller
    {
        private eCommerceReloadedContext _context;

        public OrderController(eCommerceReloadedContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("/orders")]
        public IActionResult Index()
        {
            return View();
        }
        
    }
}