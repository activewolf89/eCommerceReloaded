using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using System.Linq;

namespace eCommerceReloaded.Controllers
{
 public class CommonFunctions : Controller
    {
        private eCommerceReloadedContext _context;
        public CommonFunctions(eCommerceReloadedContext context)
        {
            _context = context;
        }
       
        public void SaveCartContent(List<string> cartitemlist,int userId)
        {
            User curuser=_context.users.SingleOrDefault(u=>u.userId==userId);
            Cart curcart=_context.carts.SingleOrDefault(c=>c.user==curuser);
            int cartid=-1;
            if(curcart==null)
            {
                Cart newcart =new Cart();
                newcart.user=curuser;
                _context.Add(newcart);
                _context.SaveChanges();
                cartid=newcart.cartId;
            }
            else
            {
                cartid=curcart.cartId;
                _context.productInCarts.RemoveRange(_context.productInCarts.Where(p=>p.cartId==curcart.cartId));
            }
            Dictionary<string,int> cartitem=new Dictionary<string,int>();
            foreach(string id in cartitemlist)
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
            foreach(KeyValuePair<string,int> entry in cartitem)
            {
                ProductInCart newitem=new ProductInCart();
                newitem.productId=Convert.ToInt32(entry.Key);
                newitem.cartId=cartid;
                newitem.quantity=entry.Value;
                newitem.created_At=DateTime.Now;
                _context.productInCarts.Add(newitem);
            }
            _context.SaveChanges();
        }
        public void SaveWishlistContent(List<string> wishlistitemlist,int userId)
        {
            User curuser=_context.users.SingleOrDefault(u=>u.userId==userId);
            Wishlist curwishlist=_context.wishlists.SingleOrDefault(c=>c.user==curuser);
            int wishlistid=-1;
            if(curwishlist==null)
            {
                Wishlist newwishlist =new Wishlist();
                newwishlist.user=curuser;
                _context.Add(newwishlist);
                _context.SaveChanges();
                wishlistid=newwishlist.wishlistId;
            }
            else
            {
                wishlistid=curwishlist.wishlistId;
                _context.productInWishlists.RemoveRange(_context.productInWishlists.Where(p=>p.wishlistId==curwishlist.wishlistId));
            }
            foreach(string id in wishlistitemlist)
            {
                ProductInWishlist newitem=new ProductInWishlist();
                newitem.productId=Convert.ToInt32(id);
                newitem.wishlistId=wishlistid;
                newitem.created_At=DateTime.Now;
                _context.productInWishlists.Add(newitem);
            }
            _context.SaveChanges();

        }
    }
}
