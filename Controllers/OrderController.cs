using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace eCommerceReloaded.Controllers
{
    public class OrderController : Controller
    {
        private eCommerceReloadedContext _context;

        public OrderController(eCommerceReloadedContext context)
        {
            _context = context;
        }

        // Post: handle checkout request
        [HttpPost]
        [Route("/orders")]
        public IActionResult Checkout()
        {
            int? Uid = HttpContext.Session.GetInt32("UserId");
            if(Uid==null){
                // Response.Cookies.Append("fromcheckout", "1");
                // TempData["ReturnUrl"]=HttpContext.Request.Path;
                HttpContext.Session.SetString("ReturnUrl",HttpContext.Request.Path);
                return RedirectToAction("Index","User");  //redirect to login page
            }
            else
            {
                int userid=(int)Uid;
                //save cookie content of cart and wishlist into database
                string pidincart=Request.Cookies["cart"];
                string pidinwishlist=Request.Cookies["wishlist"];
                if(pidincart!=null)
                {
                    List<string> cartitemlist = new List<string>();
                    cartitemlist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    CommonFunctions common=new CommonFunctions(_context);
                    common.SaveCartContent(cartitemlist,userid);
                }
                if(pidinwishlist!=null)
                {
                    List<string> wishlistitemlist = new List<string>();
                    wishlistitemlist.AddRange(pidinwishlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                    CommonFunctions common=new CommonFunctions(_context);
                    common.SaveWishlistContent(wishlistitemlist,userid);
                }               
                return RedirectToAction("Orders");
            }
        }
        //Get:Display order page
        [HttpGet]
        [Route("/orders")]
        public IActionResult Orders()
        {
            int? Uid = HttpContext.Session.GetInt32("UserId");
            if(Uid==null)
            {
                return Redirect("/");
            }
            else
            {
                 int userid=(int)Uid;
                 User curuser= _context.users.SingleOrDefault(user => user.userId == userid);
                 ViewBag.user=curuser;
                 ViewBag.shipto=curuser.shipToAddress+"  "+curuser.city+"  "+curuser.state+"  "+curuser.zipcode;
                Cart curcart=_context.carts.SingleOrDefault(c=>c.user==curuser);
                List<ProductInCart> cartitems=_context.productInCarts
                    .Include(item=>item.product)
                    .Where(i=>i.cartId==curcart.cartId).ToList();
                ViewBag.items=cartitems;
                int total=0,quantity=0;
                foreach(ProductInCart item in cartitems)
                {
                    total+=item.product.price*item.quantity;
                    quantity+=item.quantity;
                }
                ViewBag.total=total;
                ViewBag.quantity=quantity;
                
                 return View("orders");
            }
        }
        [HttpPost]
        [Route("/orderreview")]

        public IActionResult orderreview(int quantity, int total, int customer)
        {
            User checkOutCustomer = _context.users.SingleOrDefault(c=> c.userId == customer);
            ViewBag.quantity = quantity; 
            ViewBag.total = total; 
            
            ViewBag.customer = checkOutCustomer; 
            return View();
        }
 [HttpPost]
    [Route("charge")]
    public IActionResult orderstatus( string stripeToken, string address, string cardholdername, double amount, string description)
    {
        	var myCharge = new StripeChargeCreateOptions();

                        
   // when the customers trial ends (overrides the plan if applicable)   
	// always set these properties
	myCharge.Amount = (int)(amount * 100);
	myCharge.Currency = "usd";
	// set this if you want to
	myCharge.Description = description;
    


	myCharge.SourceTokenOrExistingSourceId = stripeToken;
    
	// set this property if using a customer - this MUST be set if you are using an existing source!

	// (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
	myCharge.Capture = true;


	var chargeService = new StripeChargeService();
	StripeCharge stripeCharge = chargeService.Create(myCharge);
    if(stripeCharge.Status == "succeeded")
        {
            return View();
        }
        return RedirectToAction("orders");
    }
    }
     
}