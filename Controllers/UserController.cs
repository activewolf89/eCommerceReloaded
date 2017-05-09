using System;
using System.Collections.Generic;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceReloaded.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eCommerceReloaded.Controllers
{
 public class UserController : Controller
    {
        private eCommerceReloadedContext _context;
        private readonly IHostingEnvironment _env;
        private readonly MSApiKeyOption _msoptions;
        public UserController(eCommerceReloadedContext context,IHostingEnvironment env,IOptions<MSApiKeyOption> optionsAccessor)
        {
            _context = context;
            _env = env;
            _msoptions = optionsAccessor.Value;

        }
        // GET: /Login and reg page
        [HttpGet]
        [Route("/regloginpage")]
        public IActionResult Index()
        {
           int? Uid = HttpContext.Session.GetInt32("UserId");
            if(Uid!=null){
                int userid=(int)Uid;
                string ReturnUrl=HttpContext.Session.GetString("ReturnUrl");
                if(ReturnUrl!=null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    string referer = Request.Headers["Referer"].ToString();
                    if(referer!=null)
                    {
                        return Redirect(referer);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
            }
            else
            {
                string uid=Request.Cookies["uid"];
                if(uid!=null)
                {
                    int userid = Int32.Parse(uid);
                    User user =_context.users 
                                .SingleOrDefault(u=>u.userId==userid);
                    if(user!=null && user.imgUrl!=null)
                    {
                        ViewBag.ifhasimage=true;
                    }
                    ViewBag.user=user;  
                }
                return View();
            }            
        }
        
        // Post : Register
        [HttpPost]
        [Route("/register")]
        public IActionResult Register(RegisterViewModel regmodel)
        {
            if(ModelState.IsValid)
            {
                List<User> all=_context.users.ToList();
                List<User> users=_context.users.Where(user => user.email==regmodel.Email).ToList();
                if(users.Count!=0)
                {
                    ViewBag.emailexist="Email has been registered!";
                    return View("Index");
                }
                else
                {
                    User newuser = new User
                    {
                        firstName = regmodel.FirstName,
                        lastName = regmodel.LastName,
                        password = regmodel.Password,
                        email = regmodel.Email,
                        shipToAddress = regmodel.shipToAddress,
                        city=regmodel.city,
                        state=regmodel.state,
                        zipcode=regmodel.zipcode,
                        created_At = DateTime.Now
                    };
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newuser.password = Hasher.HashPassword(newuser, newuser.password);
                    _context.Add(newuser);
                    _context.SaveChanges();
                    User curuser= _context.users.SingleOrDefault(user => user.email == newuser.email);              
                    HttpContext.Session.SetInt32("UserId", curuser.userId);
                    //save cookie content of cart and wishlist into database
                    string pidincart=Request.Cookies["cart"];
                    string pidinwishlist=Request.Cookies["wishlist"];
                    if(pidincart!=null)
                    {
                        List<string> cartitemlist = new List<string>();
                        cartitemlist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                        // SaveCartContent(cartitemlist,curuser.userId);
                        CommonFunctions common=new CommonFunctions(_context);
                        common.SaveCartContent(cartitemlist,curuser.userId);
                    }
                    if(pidinwishlist!=null)
                    {
                        List<string> wishlistitemlist = new List<string>();
                        wishlistitemlist.AddRange(pidinwishlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                        // SaveWishlistContent(wishlistitemlist,curuser.userId);
                        CommonFunctions common=new CommonFunctions(_context);
                        common.SaveWishlistContent(wishlistitemlist,curuser.userId);
                    }
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(3650);
                    Response.Cookies.Append("uid", curuser.userId.ToString(),options);
                    string ReturnUrl=HttpContext.Session.GetString("ReturnUrl");
                    if(ReturnUrl!=null)
                    {
                        HttpContext.Session.Remove("ReturnUrl");
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        string referer = Request.Headers["Referer"].ToString();
                        Uri url=new Uri(referer);
                        string path=url.AbsolutePath;
                        if(referer!=null&&path!="/regloginpage")
                        {
                            return Redirect(referer);
                        }
                        else
                        {
                            return Redirect("/");
                        }
                    }
                }

            }
            return View("Index");
        }

        // Post: Login
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginViewModel logmodel)
        {
            if(ModelState.IsValid){
                User curuser= _context.users.SingleOrDefault(user => user.email == logmodel.LEmail);      
                if(curuser == null)
                {
                    ViewBag.loginemailexist="Email do not exist!";
                    return View("Index");
                }
                else
                {
                    var Hasher = new PasswordHasher<User>();
                    // if(curuser.Password != logmodel.LPassword){
                    if(0 == Hasher.VerifyHashedPassword(curuser, curuser.password, logmodel.LPassword))
                    {
                        ViewBag.loginpassword="Password error";
                        return View("Index");
                    }
                    else{
                        HttpContext.Session.SetInt32("UserId", curuser.userId);
                        string uid=Request.Cookies["uid"];
                        if(uid!=null && Int32.Parse(uid)!=curuser.userId)
                        {
                            int userid = Int32.Parse(uid);
                          //remove cookie for cart and wishlist if different user login in
                            Response.Cookies.Delete("cart");
                            Response.Cookies.Delete("wishlist");
                        }
                        else
                        {
                            //save cookie content of cart and wishlist into database
                            string pidincart=Request.Cookies["cart"];
                            string pidinwishlist=Request.Cookies["wishlist"];
                            if(pidincart!=null)
                            {
                                List<string> cartitemlist = new List<string>();
                                cartitemlist.AddRange(pidincart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                                // SaveCartContent(cartitemlist,curuser.userId);
                                CommonFunctions common=new CommonFunctions(_context);
                                common.SaveCartContent(cartitemlist,curuser.userId);
                            }
                            if(pidinwishlist!=null)
                            {
                                List<string> wishlistitemlist = new List<string>();
                                wishlistitemlist.AddRange(pidinwishlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                                // SaveWishlistContent(wishlistitemlist,curuser.userId);
                                CommonFunctions common=new CommonFunctions(_context);
                                common.SaveWishlistContent(wishlistitemlist,curuser.userId);
                            }
                        }
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddDays(3650);
                        Response.Cookies.Append("uid", curuser.userId.ToString(),options);
                        string ReturnUrl=HttpContext.Session.GetString("ReturnUrl");
                        if(ReturnUrl!=null)
                        {
                            HttpContext.Session.Remove("ReturnUrl");
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            string referer = Request.Headers["Referer"].ToString();
                            Uri url=new Uri(referer);
                            string path=url.AbsolutePath;
                            if(referer!=null&&path!="/regloginpage")
                            {
                                return Redirect(referer);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View("Index");
        }

        //Get: Logout
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        [HttpPost]
        [Route("/uploadregimg")]
        public JsonResult UploadRegImg(string img)
        {
            int? Uid = HttpContext.Session.GetInt32("UserId");
            if(Uid==null)
            {
                return Json(new {result="fail"});
            }
            else
            {
                int userid=(int)Uid;
                var parts = img.Split(new char[] { ',' }, 2);
                var bytes = Convert.FromBase64String(parts[1]);
                // DateTime now = DateTime.Now;
                // var filename =string.Format("images/{0}-reg-{1}.png", userid.ToString(),now.ToString("MM-dd-yyyy-HH-mm-ss"));
                var filename =string.Format("images/{0}-reg.png", userid.ToString());
                var webRoot = _env.WebRootPath;
                var file = System.IO.Path.Combine(webRoot, filename);
                // System.IO.File.WriteAllText(file, "Hello World!");
                System.IO.File.WriteAllBytes(file, bytes); 
                Byte[] imgcontent = System.IO.File.ReadAllBytes(file);
                User curuser= _context.users.SingleOrDefault(user => user.userId == userid);
                curuser.imgUrl=Convert.ToString(filename);
                _context.SaveChanges(); 
                string base64string = Convert.ToBase64String(imgcontent, 0, imgcontent.Length);
                string imageurl = @"data:image/png;base64," + base64string;
                return Json(new {result="success",imageurl=imageurl});   
            }                 
        }
        [HttpPost]
        [Route("/imglogin")]
        public JsonResult ImgLogin(string img)
        {
            string uid=Request.Cookies["uid"];
            if(uid==null || uid=="")
            {
               return Json(new {result="fail"});
            }
            else
            { 
                int userid = Int32.Parse(uid);
                var parts = img.Split(new char[] { ',' }, 2);
                Byte[] newbytes = Convert.FromBase64String(parts[1]);
                // string filename =string.Format("images/{0}-reg.png", userid.ToString());
                User curuser= _context.users.SingleOrDefault(user => user.userId == userid);
                string filename=curuser.imgUrl;
                var webRoot = _env.WebRootPath;
                var regfile = System.IO.Path.Combine(webRoot, filename);
                Byte[] regimgcontent = System.IO.File.ReadAllBytes(regfile);
                string msapikey=_msoptions.MSApiKey;
                string faceid1;
                string faceid2;
                faceid1=MSFaceApiRequest.FaceDetect(msapikey,newbytes).Result;
                faceid2=MSFaceApiRequest.FaceDetect(msapikey,regimgcontent).Result;
                var VerifyResult = new Dictionary<string, object>();
                VerifyResult=MSFaceApiRequest.FaceVerify(msapikey,faceid1,faceid2).Result;
                Boolean ifidentical=Convert.ToBoolean(VerifyResult["isIdentical"]);
                double confidence=Convert.ToDouble(VerifyResult["confidence"]);
                if(ifidentical==true &&confidence>0.7)
                {
                    HttpContext.Session.SetInt32("UserId", curuser.userId);
                    HttpContext.Session.SetString("isIdentical",ifidentical.ToString());
                    HttpContext.Session.SetString("confidence",confidence.ToString());
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(3650);
                    Response.Cookies.Append("uid", curuser.userId.ToString(),options);
                    return Json(new {result="success"});;                         
                }
                else
                {
                    return Json(new {result="fail"}); 
                }  
            }                 
        }

        // public void SaveCartContent(List<string> cartitemlist,int userId)
        // {
        //     User curuser=_context.users.SingleOrDefault(u=>u.userId==userId);
        //     Cart curcart=_context.carts.SingleOrDefault(c=>c.user==curuser);
        //     int cartid=-1;
        //     if(curcart==null)
        //     {
        //         Cart newcart =new Cart();
        //         newcart.user=curuser;
        //         _context.Add(newcart);
        //         _context.SaveChanges();
        //         cartid=newcart.cartId;
        //     }
        //     else
        //     {
        //         cartid=curcart.cartId;
        //         _context.productInCarts.RemoveRange(_context.productInCarts.Where(p=>p.cartId==curcart.cartId));
        //     }
        //     Dictionary<string,int> cartitem=new Dictionary<string,int>();
        //     foreach(string id in cartitemlist)
        //     {
        //         if(!cartitem.ContainsKey(id))
        //         {
        //             cartitem.Add(id,1);
        //         }
        //         else
        //         {
        //             cartitem[id]+=1;
        //         }
        //     }
        //     foreach(KeyValuePair<string,int> entry in cartitem)
        //     {
        //         ProductInCart newitem=new ProductInCart();
        //         newitem.productId=Convert.ToInt32(entry.Key);
        //         newitem.cartId=cartid;
        //         newitem.quantity=entry.Value;
        //         newitem.created_At=DateTime.Now;
        //         _context.productInCarts.Add(newitem);
        //     }
        //     _context.SaveChanges();
        // }
        // public void SaveWishlistContent(List<string> wishlistitemlist,int userId)
        // {
        //     User curuser=_context.users.SingleOrDefault(u=>u.userId==userId);
        //     Wishlist curwishlist=_context.wishlists.SingleOrDefault(c=>c.user==curuser);
        //     int wishlistid=-1;
        //     if(curwishlist==null)
        //     {
        //         Wishlist newwishlist =new Wishlist();
        //         newwishlist.user=curuser;
        //         _context.Add(newwishlist);
        //         _context.SaveChanges();
        //         wishlistid=newwishlist.wishlistId;
        //     }
        //     else
        //     {
        //         wishlistid=curwishlist.wishlistId;
        //         _context.productInWishlists.RemoveRange(_context.productInWishlists.Where(p=>p.wishlistId==curwishlist.wishlistId));
        //     }
        //     foreach(string id in wishlistitemlist)
        //     {
        //         ProductInWishlist newitem=new ProductInWishlist();
        //         newitem.productId=Convert.ToInt32(id);
        //         newitem.wishlistId=wishlistid;
        //         newitem.created_At=DateTime.Now;
        //         _context.productInWishlists.Add(newitem);
        //     }
        //     _context.SaveChanges();

        // }
    }
}
