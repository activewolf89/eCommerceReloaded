using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace eCommerceReloaded.Controllers
{
   public class ProductController : Controller
    {
        private eCommerceReloadedContext _context;
        private readonly IHostingEnvironment _env;
        public ProductController(eCommerceReloadedContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
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
        //*************************** below content added by Jason
       // GET: /product admin page
        [HttpGet]
        [Route("/admin")]
        public IActionResult ProductAdmin2()
        {
            List<Product> products =_context.products
                    .OrderByDescending(c=>c.created_At)
                    .ToList();
            List<Category> categories =_context.categories.ToList();
            List<Event> events= _context.events
                    .OrderBy(e=>e.month)
                    .ToList();
            ViewBag.categories=categories;
            ViewBag.events=events;
            ViewBag.products=products;
            if(TempData["error"]!=null)
            {
                ViewData["error"]=TempData["error"];
            }
            return View("productadmin2");
        }
        // Post: add a new category
        [HttpPost]
        [Route("/admin/category/new")]
        public IActionResult NewCategory(string name)
        {
            Category newcategory=new Category();
            newcategory.name=name;
            _context.Add(newcategory);
            _context.SaveChanges();
            return RedirectToAction("ProductAdmin2");          
        }
        // Post: add a new event
        [HttpPost]
        [Route("/admin/event/new")]
        public IActionResult NewEvent(string name,int month)
        {
            Event newevent=new Event();
            newevent.name=name;
            newevent.month=month;
            _context.Add(newevent);
            _context.SaveChanges();
            return RedirectToAction("ProductAdmin2");          
        }
        // Post : add a new product
        [HttpPost]
        [Route("/admin/product/new")]
        public IActionResult NewProduct(string name,int inventory,string description, int category, IList<IFormFile> image, int[]  Event )
        {
            if(ModelState.IsValid)
            {
                Product product = _context.products
                    .SingleOrDefault(c=>c.name==name);
                if(product !=null)
                {
                    TempData["error"]="Product name has existed!";
                }
                else
                {
                    long size = 0;
                    string imageUrl="";
                    foreach(var file in image)
                        {
                            var fileName = ContentDispositionHeaderValue
                                            .Parse(file.ContentDisposition)
                                            .FileName
                                            .Trim('"');
                            imageUrl=Convert.ToString(fileName);
                            string filename = _env.WebRootPath + $@"\images\{fileName}";
                            size += file.Length;
                            using (FileStream fs = System.IO.File.Create(filename))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                        }
                    Product newproduct=new Product();
                    newproduct.created_At=DateTime.Now;
                    newproduct.name=name;
                    newproduct.inventory=inventory;
                    newproduct.description=description;
                    newproduct.categoryId=category;
                    newproduct.imageUrl="/images/"+imageUrl;
                    _context.Add(newproduct);
                    _context.SaveChanges();
                    int pid=newproduct.productId;
                    for(int i=0;i<Event.Length;i++)
                    {
                        ProductEvent newpie =new ProductEvent();
                        newpie.productId=pid;
                        newpie.eventId= Event[i];
                        _context.Add(newpie);
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("ProductAdmin2");
            }       
            List<Product> products =_context.products
                    .OrderByDescending(c=>c.created_At)
                    .ToList();
            List<Category> categories =_context.categories.ToList();
            List<Event> events= _context.events.ToList();
            ViewBag.categories=categories;
            ViewBag.events=events;
            ViewBag.products=products;
            return View("productadmin2");
        }
    }
}
