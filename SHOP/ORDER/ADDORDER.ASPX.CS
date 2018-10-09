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

public partial class Order_AddOrder : System.Web.UI.Page
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
	{   ///获取购物车的商品
		ShoppingCart shoppingCart = new ShoppingCart(Session);
		///绑定数据并显示商品
		gvProduct.DataSource = shoppingCart.ShoppingCartList;
		gvProduct.DataBind();
	}
	protected void btnCommit_Click(object sender,EventArgs e)
	{
		Order order = new Order();
		///获取当天最近的订单编号
		string orderNo = order.GetOrderLastOrderNo();
		///创建下一个订单编号的基数
		if(string.IsNullOrEmpty(orderNo) == true)
		{   ///下一个订单号的基数为1
			orderNo = "0001";
		}
		else
		{   ///创建下一个订单号的基数
			orderNo = (Int32.Parse(orderNo.Substring(8)) + 1).ToString();
		}
		///创建下一个订单编号
		orderNo = AjaxEBusinessSystem.CreaterOrderNo(orderNo);

		///获取购物车的商品
		ShoppingCart shoppingCart = new ShoppingCart(Session);		
		///计算购物车中的商品总数量和总金额
		int totalNumber = 0;
		decimal totalMoney = 0.0m;
		foreach(ShoppingCartItem item in shoppingCart.ShoppingCartList)
		{   ///数量和金额累加
			totalNumber += item.Number;
			totalMoney += item.Number * item.Price;
		}
		///创建订单
		int orderID = order.AddOrder(orderNo,userID,totalNumber,totalMoney);
		if(orderID > 0)
		{   ///创建订单的商品项
			foreach(ShoppingCartItem item in shoppingCart.ShoppingCartList)
			{
				order.AddOrderItem(orderID,item.ProductID,item.Number);
			}
		}
		///清空购物车中的商品
		shoppingCart.ClearShoppingCart();
		///重定向到预览订单的页面
		Response.Redirect("~/Order/ViewOrder.aspx?OrderID=" + orderID.ToString());
	}
	protected void btnAdd_Click(object sender,EventArgs e)
	{
		Response.Redirect("~/Product.aspx?CategoryID=27");
	}
}
