using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using System.Linq;
<<<<<<< HEAD
=======
using Newtonsoft.Json;
>>>>>>> Reed

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
            //aProduct is the validation instantiated object, if valid add to Product Model 
            if(ModelState.IsValid)
            {
                //categoryID starts at 0 which means that if it stays at 0, 
                //there is no category with that name after the check(see check below)
                int categoryId = 0;
                
                foreach(Category aCategory in _context.categories.ToList())
                {
                    //this is the check
                    if(aCategory.name == aProduct.name)
                    {
                        categoryId = aCategory.categoryId;
                    }
                }
                //if nothing is found, add new category
                if(categoryId == 0)
                {
                    Category newCategory = new Category
                    {
                        name = aProduct.name,
                        created_At = DateTime.Now,

                    };
                    _context.categories.Add(newCategory);
                    //updated parent variable categoryId with new categoryID
                    categoryId = newCategory.categoryId;
                    
                }
                else 
                {
                    //if the category name already exists pull up the category when adding inventory
                    Category existingCategory = _context.categories 
                    .SingleOrDefault(category => category.categoryId == categoryId);
                    categoryId = existingCategory.categoryId;
                }

                Product newProduct = new Product 
                {
                    //created new product 
                 name = aProduct.name,
                 image = aProduct.image,
                 inventory = aProduct.quantity,
                 cost = aProduct.cost,
                 price = aProduct.price,
                 created_At = DateTime.Now,
                 categoryId = categoryId,
                };
                _context.Add(newProduct);
            }
            else 
            {
          
              return View("productadmin");

            }
            //finally save changes to Database
            _context.SaveChanges();
            return View("productadmin");
            // return View("productadmin");
        }
    }
}
