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

public partial class Order_ViewOrder : System.Web.UI.Page
{
	int orderID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {   ///获取订单信息
		if(Request.Params["OrderID"] != null)
		{
			orderID = Int32.Parse(Request.Params["OrderID"].ToString());
		}
		if(!Page.IsPostBack && orderID > 0)
		{
			BindPageData(orderID);
		}
    }

	private void BindPageData(int orderID)
	{
		Order order = new Order();
		///获取订单信息
		SqlDataReader dr = order.GetSingleOrder(orderID);
		if(dr == null) return;
		if(dr.Read())
		{   ///显示订单信息
			lbOrderNo.Text = dr["OrderNo"].ToString();
			lbCreateDate.Text = dr["CreateDate"].ToString();
			///格式化为货币格式
			lbTotalMoney.Text = string.Format("{0:C}",dr["TotalMoney"]);
			lbTotalNumber.Text = dr["TotalNumber"].ToString();
		}
		dr.Close();
		///显示订单的详细商品信息
		gvProduct.DataSource = order.GetOrderItemByOrder(orderID);
		gvProduct.DataBind();
	}
}
