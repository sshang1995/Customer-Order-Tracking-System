using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Remoting.Messaging;

namespace TropicalServer.DAL
{
	public class ReportsDAL
	{   /* valid login*/
		string connString = Convert.ToString(ConfigurationManager.AppSettings["TropicalServerConnectionString"]);
		public DataSet IsValid(string UserID, String Password)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{

				DataSet ds = new DataSet();
				SqlDataAdapter sda = new SqlDataAdapter();
				string queryString = "select * from dbo.tblUserLogin where UserID = '" + UserID + "' and Password ='" + Password + "'";
				sda.SelectCommand = new SqlCommand(queryString, conn);
				sda.Fill(ds);

				return ds;

			}

		}
		 /* order filtered data */
		public DataSet FilteredData(string OrderDate, string CustomerID,
			string CustomerName, string SalesManager)
		{
			using (SqlConnection conn = new SqlConnection(connString))
			{
				DataSet ds = new DataSet();
				SqlDataAdapter sda = new SqlDataAdapter();
				string querystring = "select distinct OrderTrackingNumber, OrderDate,CustID, CustName, CustBillingAddress1, c.CustRouteNumber " +
									 " from[dbo].[tblOrder] o inner join dbo.tblCustomer c on o.OrderCustomerNumber = c.CustNumber" +
									 " inner join dbo.tblCustRoute r on c.CustRouteNumber=r.CustRouteNumber";

				if (OrderDate == "Today")
				{
					querystring += " where o.OrderDate = '2012-02-28' ";
				}
				else if (OrderDate == "Last 7 Days")
				{
					querystring += " where o.OrderDate between Dateadd(day,-7,'2012-02-28') and '2012-02-28' ";
				}
				else if (OrderDate == "Last 1 Month")
				{
					querystring += " where o.OrderDate between Dateadd(month,-1,'2012-02-28') and '2012-02-28' ";
				}
				else if (OrderDate == "Last 6 Months")
				{
					querystring += " where o.OrderDate between Dateadd(month,-6,'2012-02-28') and '2012-02-28' ";
				}


				if (CustomerID != "")
				{
					querystring += " And CustID =" + CustomerID;
				}

				if (CustomerName != "")
				{
					querystring += " And CustName ='" + CustomerName+"'";
				}

				if (SalesManager != "")
				{
					querystring += " And r.CustRouteRep ='" + SalesManager+"'";
				}

				Console.WriteLine(querystring);

				sda.SelectCommand = new SqlCommand(querystring, conn);
				sda.Fill(ds);

				return ds;
			}
		}
		
   
	
	/*
	 * Insert item description to get the #, description, 
	 * pre-price and size of the item           
	 */
	public DataSet GetProductByProductCategory_DAL(string newItemDescription)
		{
			SqlParameter[] parameters = new SqlParameter[1];
			DataSet ds = new DataSet();

			parameters[0] = new SqlParameter("@itemDescription", SqlDbType.NVarChar);

			if (newItemDescription.Trim() != string.Empty)
				parameters[0].Value = newItemDescription.Trim();

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("GetProductByProductCategory", connection);
					command.CommandType = CommandType.StoredProcedure;
					if (parameters != null)
					{
						SqlParameter p = null;
						foreach (SqlParameter sqlP in parameters)
						{
							p = sqlP;
							if (p != null)
							{
								if (p.Direction == ParameterDirection.InputOutput ||
								   p.Direction == ParameterDirection.Input && p.Value == null)
								{
									p.Value = DBNull.Value;
								}
								command.Parameters.Add(p);
							}
						}
					}
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving Product Categories - " + ex.Message.ToString());
			}
		}//End of GetProductByProductCategory_DAL method...

		/*
		 *Enter a number to populate 
		 * the CustSalesRepNumber
		 */
		public DataSet GetCustSalesRepNumber_DAL(int newCustSaleRepNum)
		{
			SqlParameter[] parameters = new SqlParameter[1];
			DataSet ds = new DataSet();

			parameters[0] = new SqlParameter("@custSaleRepNum", SqlDbType.Int);

			if (newCustSaleRepNum != 0)
				parameters[0].Value = newCustSaleRepNum;

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("GetCustSalesRepNumber", connection);
					command.CommandType = CommandType.StoredProcedure;

					if (parameters != null)
					{
						SqlParameter p = null;
						foreach (SqlParameter sqlP in parameters)
						{
							p = sqlP;
							if (p != null)
							{
								if (p.Direction == ParameterDirection.InputOutput ||
								   p.Direction == ParameterDirection.Input && p.Value == null)
								{
									p.Value = DBNull.Value;
								}
								command.Parameters.Add(p);
							}
						}
					}
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving Sales Representative Number - " + ex.Message.ToString());
			}
		}// End of GetCustSalesRepNumber_DAL method...

		/*
		 * Select custSalesRepNum on the 
		 * side bar to get the route info.
		 */
		public DataSet GetRouteInfo_DAL(int newRouteID)
		{
			SqlParameter[] parameters = new SqlParameter[1];
			DataSet ds = new DataSet();

			parameters[0] = new SqlParameter("@roleID", SqlDbType.Int);

			parameters[0].Value = newRouteID;

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("GetRouteInfo", connection);
					command.CommandType = CommandType.StoredProcedure;

					if (parameters != null)
					{
						SqlParameter p = null;
						foreach (SqlParameter sqlP in parameters)
						{
							p = sqlP;
							if (p != null)
							{
								if (p.Direction == ParameterDirection.InputOutput ||
								   p.Direction == ParameterDirection.Input && p.Value == null)
								{
									p.Value = DBNull.Value;
								}
								command.Parameters.Add(p);
							}
						}
					}
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
			}

		}// End of GetRouteInfo_DAL method...

		/*
		 * Get the Name,LoginID, password, Role Description
		 * of the User who are active(true).
		 */
		public DataSet GetUsersSetting_DAL()
		{
			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("GetUsersSetting", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
			}
		}// End of GetRouteInfo_DAL method...

		/*
		 * Get the Customers for Setting page.
		 */
		public DataSet GetCustomersSetting_DAL()
		{
			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("GetCustomersSetting", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
			}
		}// End of GetCustomersSetting_DAL method...

		/*
		 * Get the Price Group Info for Setting page.
		 */
		public DataSet GetPriceGroupSetting_DAL()
		{
			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("GetPriceGroupSetting", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving Route Info - " + ex.Message.ToString());
			}
		}// End of GetPriceGroup_DAL method...

		public DataSet GetItemTypeID_DAL()
		{
			//[ItemTypeID]

			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("select ItemTypeDescription from tblItemType", connection);
				 //   command.CommandType = CommandType.StoredProcedure;
				  //  command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving item Type - " + ex.Message.ToString());
			}
		}//////////

		public DataSet GetItemsDetail_DAL(string itemType)
		{
			SqlParameter[] parameters = new SqlParameter[1];
			DataSet ds = new DataSet();

		  //  parameters[0] = new SqlParameter("@itemID", SqlDbType.VarChar);

			//parameters[0].Value = itemType;

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("getItemsDetails", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@itemID", itemType);

					if (parameters != null)
					{
						SqlParameter p = null;
						foreach (SqlParameter sqlP in parameters)
						{
							p = sqlP;
							if (p != null)
							{
								if (p.Direction == ParameterDirection.InputOutput ||
								   p.Direction == ParameterDirection.Input && p.Value == null)
								{
									p.Value = DBNull.Value;
								}
								
							}
						}
					}
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving items - " + ex.Message.ToString());
			}

		}

		public DataSet Sample_DAL()
		{
			DataSet ds = new DataSet();

			//  parameters[0] = new SqlParameter("@itemID", SqlDbType.VarChar);

			//parameters[0].Value = itemType;

			try
			{
				using (SqlConnection connection = new SqlConnection(connString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand("Sample_procedure", connection);
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 6000;
					SqlDataAdapter adp = new SqlDataAdapter(command);
					adp.Fill(ds);
					connection.Close();
				}
				return ds;
			}
			catch (Exception ex)
			{
				throw new Exception("Error occured while retrieving items - " + ex.Message.ToString());
			}

		}

	}
}
