using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace eCommerceReloaded.Controllers
{
    public class CartController : Controller
    {
        private eCommerceReloadedContext _context;

        public CartController(eCommerceReloadedContext context)
        {
            _context = context;
        }

        //Get: Cart content page
        [HttpGet]
        [Route("/cartcontent")]
        public IActionResult Cart()  
        {
            string pidincart=Request.Cookies["cart"];
            List<string> cookielist = new List<string>();
            cookielist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            CartContent cartcontent=GetCartContent(cookielist);
            ViewBag.items=cartcontent.items;
            ViewBag.total=cartcontent.total;
            ViewBag.quantity=cartcontent.quantity;
            return View("cartcontent");
        }      

        //Post: add to cart (save to cookie)
        [HttpPost]
        [Route("/cart/addtocart")]
        public JsonResult AddtoCart(string productId)
        {
            string pidincart=Request.Cookies["cart"];
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(365);
            int cartnumber;
            if(pidincart==null)
            {
                Response.Cookies.Append("cart", productId,options);
                cartnumber=1;
            }
            else
            {
                string newcookie=pidincart+","+productId;
                Response.Cookies.Append("cart", newcookie,options);
                List<string> cookielist = new List<string>();
                cookielist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                cartnumber=cookielist.Count+1;             
            }
            return Json(new {number=cartnumber});      
        }
        //Post: delete item from cart (remove item to cookie)
        [HttpPost]
        [Route("/cart/deletecartitem")]
        public JsonResult DeleteItemFromCart(string productId)
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
                    while(cookielist.FindIndex(i=>i==productId)!=-1)
                    {
                            cookielist.Remove(productId);
                    }
                    string cookiestring=cookielist.Aggregate((a,b)=>a=a+","+b);
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(365);                    
                    Response.Cookies.Append("cart", cookiestring,options);
                    CartContent cartcontent=GetCartContent(cookielist);
                    return Json(new {result="success",total=cartcontent.total,quantity=cartcontent.quantity});
                }
            }    
        }
        //Post: change item  quantity in cart (add or remove item to cookie)
        [HttpPost]
        [Route("/cart/changequantity")]
        public JsonResult ChangeItemQuantityInCart(string productId,int quantity)
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
                    int currentcount=0;
                    for(int i=0;i<cookielist.Count;i++)
                    {
                        if(cookielist[i]==productId)
                        {
                            currentcount++;
                        }
                    }
                    int difference=quantity-currentcount;
                    if(difference==0)
                    {
                       return Json(new {result="fail"}); 
                    }
                    else
                    {
                        if(difference>0)  //need add to cookie
                        {
                            while(difference>0)
                            {
                                cookielist.Add(productId);
                                difference--;
                            }
                        }
                        else     //remove from cookie
                        {
                            while(difference<0)
                            {
                                cookielist.Remove(productId);
                                difference++;
                            }
                        }
                        string cookiestring=cookielist.Aggregate((a,b)=>a=a+","+b);
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddDays(365);                    
                        Response.Cookies.Append("cart", cookiestring,options);
                        CartContent cartcontent=GetCartContent(cookielist);
                        return Json(new {result="success",total=cartcontent.total,quantity=cartcontent.quantity});
                    }   
                }
            }    
        }
        //Post: clear cookie for dev
        [HttpPost]
        [Route("/clearcookie")]
        public IActionResult ClearCookie()
        {
            Response.Cookies.Delete("cart");
            return RedirectToAction("ProductAdmin2","Product");
        }
        
        public CartContent GetCartContent(List<String> cookielist)
        {
            CartContent cartcontent=new CartContent();
            // List<CartItem> itemsincart =new List<CartItem>();
            Dictionary<string,int> cartitem=new Dictionary<string,int>();
            foreach(string id in cookielist)
            {
                if(!cartitem.ContainsKey(id))
                {
                    cartitem.Add(id,1);
                }
                else
                {
                    cartitem[id]+=1;
                }
            }
            // int total=0;
            // int quantity=0;
            foreach(KeyValuePair<string,int> entry in cartitem)
            {
                int id=Convert.ToInt32(entry.Key);
                Product p=_context.products
                        .SingleOrDefault(product=>product.productId==id);
                cartcontent.total+=p.price*entry.Value;
                cartcontent.quantity+=entry.Value;
                CartItem newcontent= new CartItem();
                newcontent.product=p;
                newcontent.quantity=entry.Value;
                cartcontent.items.Add(newcontent);
            }
            return cartcontent;
        }
    }
}