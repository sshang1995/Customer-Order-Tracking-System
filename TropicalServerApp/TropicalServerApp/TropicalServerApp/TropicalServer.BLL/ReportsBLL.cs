using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TropicalServer.DAL;
using System.Runtime.Remoting.Messaging;

namespace TropicalServer.BLL
{
    public class UserBO
    {
        private string _UserID = "";
        private string _Password = "";

        public string UserID { get => _UserID; set => _UserID = value; }
        public string Password { get => _Password; set => _Password = value; }

        ReportsDAL da = new ReportsDAL();

        public bool getUser()
        {
            if (da.IsValid(UserID, Password).Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    public class Filters
    {
        private string _OrderDate = "";
        private string _CustomerID = "";
        private string _CustomerName = "";
        private string _SalesManager = "";

        public string OrderDate { get => _OrderDate; set => _OrderDate = value; }
        public string CustomerID { get => _CustomerID; set => _CustomerID = value; }
        public string CustomerName { get => _CustomerName; set => _CustomerName = value; }
        public string SalesManager { get => _SalesManager; set => _SalesManager = value; }

        //ReportsDAL da = new ReportsDAL();

        public DataSet FinalTable()
        {
            var dataResult = new ReportsDAL().FilteredData(OrderDate, CustomerID, CustomerName, SalesManager);
            return (dataResult);
           
            
        }
       

    } 


    public class ReportsBLL
    {
        
        public DataSet GetProductByProductCategory_BLL(string newItemDescription)
        {
            return (new ReportsDAL().GetProductByProductCategory_DAL(newItemDescription));
        }

        public DataSet GetCustSalesRepNumber_BLL(int newCustSaleRepNum)
        {
            return (new ReportsDAL().GetCustSalesRepNumber_DAL(newCustSaleRepNum));
        }

        public DataSet GetUsersSetting_BLL()
        {
            return (new ReportsDAL().GetUsersSetting_DAL());
        }

        public DataSet GetCustomersSetting_BLL()
        {
            return (new ReportsDAL().GetCustomersSetting_DAL());
        }

        public DataSet GetPriceGroupSetting_BLL()
        {
            return (new ReportsDAL().GetPriceGroupSetting_DAL());
        }

        public DataSet GetRouteInfo_BLL(int newRouteID)
        {
            return (new ReportsDAL().GetRouteInfo_DAL(newRouteID));
        }
        public DataSet GetItemTypeID_BLL()
        {
            return (new ReportsDAL().GetItemTypeID_DAL());
        }

        public DataSet GetItemsDetail_BLL(string itemType)
        {
            return (new ReportsDAL().GetItemsDetail_DAL(itemType));
        }

        public DataSet Sample_BLL()
        {
            return (new ReportsDAL().Sample_DAL());
        }
    }
}
