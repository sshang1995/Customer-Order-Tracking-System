using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TropicalServer.BLL;

namespace TropicalServerApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public string  FilterData(object sender, EventArgs e)
        {
            // note: this is called via an AJAX post() triggered by clicking a button

            Filters obj = new Filters();
            Console.WriteLine(obj.ToString());
            obj.OrderDate = Request.Form["OrderDate"];
            obj.CustomerID = Request.Form["CustomerID"];
            obj.CustomerName = Request.Form["CustomerName"];
            obj.SalesManager = Request.Form["SalesManager"];

            DataSet dataResult = obj.FinalTable();

            // create container (list of dictionary<string columnName, string obj>)
            List<Dictionary<string, string>> resultsList = new List<Dictionary<string, string>>();
            Dictionary<string, string> rowResult;

            foreach (DataTable table in dataResult.Tables)
            {

                foreach (DataRow row in table.Rows)
                {
                    // create a Dictionary for a single row
                    rowResult = new Dictionary<string, string>();

                    foreach (DataColumn col in table.Columns)
                    {

                        // fill the dictionary for each column
                        rowResult.Add(col.ColumnName, row[col].ToString());

                    }

                    resultsList.Add(rowResult);
                    // add the new Dictionary as a row in the container list

                }
            }

            // turn the container (which is a list of dictionaries, one for each row)
            // into JSON and return that as the response
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(resultsList);

            // note: on the JavaScript side, do JSON.parse() and then turn into html
            
            //Response.Write(resultsList);

        }



            public ActionResult Route()
        {
            return View();
        }

        public ActionResult Stop()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }
    }
}