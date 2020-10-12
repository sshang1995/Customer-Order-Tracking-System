using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalServer.BLL;

namespace TropicalServerApp.Controllers
{
    public class LoginController : Controller
    {
        // static user object 
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Email()
        {
            return View();
        }
        public void LoginClick(object sender, EventArgs e)
        {

            UserBO obj = new UserBO();
            Console.WriteLine(obj.ToString());
            obj.UserID = Request.Form["userid"];
            obj.Password = Request.Form["password"];
           

            if (obj.getUser())
            {
                //if(rememberme.Checked == true)
                //{
                //    Response.Cookies["userid"].Value = textUserID.Text;
                //}

                var result = new { redirect = "/Product/Orders", status = "success" };
                Response.Write(System.Web.Helpers.Json.Encode(result));
                

            }
            else
            {
                var result = new { redirect = "", status = "failure"};
                Response.Write(System.Web.Helpers.Json.Encode(result));
            }
        }


    }
}