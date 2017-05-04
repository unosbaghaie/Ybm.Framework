using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ybm.UI.Infrastructure.Authorization;
using Ybm.UI.Models;

namespace Ybm.UI.Controllers
{
    //[TokenCategory(CategoryTitle = "Home Page", CategoryName = "HomePage")]
    public class HomeController : Controller
    {
        //[ClaimBasedAuthorzation(TokenName = "View Home Page", ClaimType = "UserRight")]
        public ActionResult Index()
        {
            return View();
        }

        //[ClaimBasedAuthorzation(TokenName = "Do Something", ClaimType = "UserRight")]
        public ActionResult DoSomething()
        {
            return View();
        }



        public ActionResult Products()
        {
            var products = new List<Product>
            {
               new Product
               {
                    ProductId= 2,
                    ProductName= "Garden Cart",
                    ProductCode= "GDN-0023",
                    ReleaseDate= "March 18, 2016",
                    Description= "15 gallon capacity rolling garden cart",
                    Price= (decimal) 32.99,
                    StarRating= 4.2,
                    ImageUrl= "app/assets/images/garden_cart.png"
               },
               new Product
               {
                    ProductId= 5,
                    ProductName= "Hammer",
                    ProductCode= "TBX-0048",
                    ReleaseDate= "May 21, 2016",
                    Description= "Curved claw steel hammer",
                    Price= (decimal) 8.9,
                    StarRating= 4.8,
                    ImageUrl= "app/assets/images/rejon_Hammer.png"
               }
            };

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(products, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }),
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8
            };
        }


    }
}