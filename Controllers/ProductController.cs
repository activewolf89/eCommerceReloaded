using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
namespace eCommerceReloaded.Controllers
{
    public class ProductController : Controller
    {
        private eCommerceReloadedContext _context;

        public ProductController(eCommerceReloadedContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("productadmin")]
        public IActionResult productadmin()
        {
           
            return View();
        }
        [HttpPost]
        [Route("addproduct")]
        public IActionResult addproduct(ProductValidation aProduct)
        {
            //aProduct is the validation instantiated object, if valid add to Product Model 

            return View("productadmin");
        }
    }
}
