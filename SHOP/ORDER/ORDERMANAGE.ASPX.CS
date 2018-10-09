using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

///引入新的命名空间
using ASPNETAJAXWeb.AjaxEBusiness;
using System.Data.SqlClient;

public partial class Order_OrderManage : System.Web.UI.Page
{
	int userID = -1;
	protected void Page_Load(object sender,EventArgs e)
	{   ///判断用户是否登录
		if(Session["UserID"] == null)
		{
			Response.Redirect("~/Default.aspx");
			return;
		}
		///获取用户信息
		userID = Int32.Parse(Session["UserID"].ToString());
		if(!Page.IsPostBack)
		{
			BindPageData();
		}
	}

	private void BindPageData()
	{   ///获取历史订单
		Order order = new Order();
		///绑定数据并显示订单
		gvOrder.DataSource = order.GetOrderByUser(userID);
		gvOrder.DataBind();
	}
	protected void gvOrder_RowCommand(object sender,GridViewCommandEventArgs e)
	{
		if(e.CommandName.ToString() == "sale")
		{   ///检查库存
			int orderID = Int32.Parse(e.CommandArgument.ToString());
			if(CheckStockAndSale(orderID) == false)
			{   
				AjaxEBusinessSystem.ShowAjaxDialog((Button)e.CommandSource,"库存不够，不能处理该订单");
				return;
			}
			AjaxEBusinessSystem.ShowAjaxDialog((Button)e.CommandSource,"恭喜您，处理订单成功。");
		}
	}

	private bool CheckStockAndSale(int orderID)
	{   ///获取订单信息
		Order order = new Order();
		DataSet ds = order.GetOrderItemByOrder(orderID);
		if(ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return false;
		///判断库存是否足够
		Product product = new Product();
		bool isAllowSale = true;
		foreach(DataRow row in ds.Tables[0].Rows)
		{   ///读取商品信息
			SqlDataReader dr = product.GetSingleProduct(Int32.Parse(row["ProductID"].ToString()));
			if(dr == null)
			{
				isAllowSale = false;break;
			}
			if(dr.Read())
			{   ///判断库存数量是否足够，如果不够，则不能卖该商品
				if(Int32.Parse(dr["Stock"].ToString()) < Int32.Parse(row["Number"].ToString()))
				{
					isAllowSale = false; break;
				}				
			}
			dr.Close();
		}
		if(isAllowSale == false) return false;
		///修改此次交易商品的库存和销售数量
		foreach(DataRow row in ds.Tables[0].Rows)
		{   ///修改库存信息和销售数量
			if(product.UpdateProductStock(
				Int32.Parse(row["ProductID"].ToString()),
				Int32.Parse(row["Number"].ToString())) <= 0)
			{
				isAllowSale = false; break;
			}			
		}
		if(isAllowSale == false) return false;
		///提交该订单，并重新显示数据
		if(order.UpdateOrderStatus(orderID,1) > 0)
		{
			BindPageData();
		}
		return isAllowSale;
	}	
}
