using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace eCommerceReloaded.Controllers
{
    public class WishlistController : Controller
    {
        private eCommerceReloadedContext _context;

        public WishlistController(eCommerceReloadedContext context)
        {
            _context = context;
        }

        //Get: wishlist content page
        [HttpGet]
        [Route("/wishlist/content")]
        public IActionResult Wishlist()  
        {
            string pidincart=Request.Cookies["wishlist"];
            if(pidincart!=null)
            {
                List<string> cookielist = new List<string>();
                cookielist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                List<Product> wishlistcontent=GetWishlistContent(cookielist);
                ViewBag.items=wishlistcontent;
            }
            return View("wishlistcontent");
        }      

        //Post: add to wishlist (save to cookie)
        [HttpPost]
        [Route("/wishlist/addtowishlist")]
        public JsonResult AddtoWishlist(string productId)
        {
            string pidincart=Request.Cookies["wishlist"];
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(365);
            if(pidincart==null)
            {
                Response.Cookies.Append("wishlist", productId,options);
                return Json(new {result="success"});
            }
            else
            {
                List<string> cookielist = new List<string>();
                cookielist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                if(cookielist.FindIndex(i=>i==productId)!=-1)
                {
                    return Json(new {result="exist"});
                }
                else
                {
                    string newcookie=pidincart+","+productId;
                    Response.Cookies.Append("wishlist", newcookie,options);
                    return Json(new {result="success"});
                }           
            }     
        }
        //Post: delete item from wishlist (remove item to cookie)
        [HttpPost]
        [Route("/wishlist/deletewishlistitem")]
        public JsonResult DeleteItemFromWishlist(string productId)
        {
            string pidincart=Request.Cookies["cart"];
            if(pidincart==null)
            {
                return Json(new {result="fail"});
            }
            else
            {
                List<string> cookielist = new List<string>();
                cookielist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                int index=cookielist.FindIndex(i=>i==productId);
                if(index  <0)
                {
                    return Json(new {result="fail"});
                }
                else
                {

                    cookielist.Remove(productId);
                    string cookiestring=cookielist.Aggregate((a,b)=>a=a+","+b);
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(365);   
                    Response.Cookies.Append("wishlist", cookiestring,options);
                    return Json(new {result="success"});
                }
            }    
        }

        //Post: add wishlist item to cart
        // [HttpPost]
        // [Route("/wishlist/addlistitemtocart")]
        // public JsonResult AddWishlistItemToCart(string productId)
        // {     
        //     string pidincart=Request.Cookies["cart"];
        //     CookieOptions options = new CookieOptions();
        //     options.Expires = DateTime.Now.AddDays(365);
        //     if(pidincart==null)
        //     {
        //         Response.Cookies.Append("cart", productId,options);
        //     }
        //     else
        //     {
        //         string newcookie=pidincart+","+productId;
        //         Response.Cookies.Append("cart", newcookie,options);          
        //     }
        //     return Json(new {result="success"});
        // }   
        public List<Product> GetWishlistContent(List<String> cookielist)
        {
          List<Product> wishlistcontent=new List<Product>();
          foreach(string pid in cookielist)
            {
                int id=Convert.ToInt32(pid);
                Product p=_context.products
                        .SingleOrDefault(product=>product.productId==id);
                wishlistcontent.Add(p);
            }
            return wishlistcontent;
        }
    }
}