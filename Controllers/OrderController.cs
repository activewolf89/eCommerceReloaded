using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace eCommerceReloaded.Controllers
{
    public class OrderController : Controller
    {
        private eCommerceReloadedContext _context;

        public OrderController(eCommerceReloadedContext context)
        {
            _context = context;
        }

        // Post: /Display order page
        [HttpPost]
        [Route("/checkout")]
        public IActionResult Checkout()
        {
            int? Uid = HttpContext.Session.GetInt32("UserId");
            if(Uid==null){
                Response.Cookies.Append("fromcheckout", "1");
                return RedirectToAction("Index","User");
            }
            else
            {
                int userid=(int)Uid;
                return View();
            }
        }

     
    }
}