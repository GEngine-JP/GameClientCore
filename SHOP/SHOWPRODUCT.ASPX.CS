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

///引入新的名字空间
using ASPNETAJAXWeb.AjaxEBusiness;
using System.Data.SqlClient;

public partial class ShowProduct : System.Web.UI.Page
{
	protected string ProductName = string.Empty;
	protected string ViewCount = string.Empty;
	protected string CreateDate = string.Empty;
	protected string Username = string.Empty;
	protected string Remark = string.Empty;
	private int productID = -1;

	protected void Page_Load(object sender,EventArgs e)
	{   ///获取客户端的IP地址
		tbIP.Text = Request.UserHostAddress;
		if(Request.Params["ProductID"] != null)
		{
			productID = Int32.Parse(Request.Params["ProductID"].ToString());			
		}
		if(productID > 0)
		{
			BindPageData(productID);
			if(!Page.IsPostBack)
			{   ///更新商品被访问的次数
				Product product = new Product();
				product.UpdateProductViewCount(productID);
			}
		}
	}

	private void BindPageData(int productID)
	{   ///获取商品评论
		Product product = new Product();
		SqlDataReader dr = product.GetSingleProduct(productID);
		if(dr == null) return;

		if(dr.Read())
		{   ///读取商品信息
			ProductName = dr["Name"].ToString();
			ViewCount = dr["ViewCount"].ToString();
			CreateDate = dr["CreateDate"].ToString();
			Username = dr["Username"].ToString();
			Remark = dr["Remark"].ToString();
			lbPrice.Text = string.Format("{0:f2}",dr["Price"]);
			imgProduct.ImageUrl = "~/" + dr["PictureUrl"].ToString();

			///获取属性的数据
			DataSet ds = product.GetAttributeByProduct(productID);
			///绑定并显示属性的数据
			gvAttribute.DataSource = ds;
			gvAttribute.DataBind();
		}
		dr.Close();
		///显示商品的评论
		gvComment.DataSource = product.GetCommentByProdcut(productID);
		gvComment.DataBind();
	}

	protected void btnBuy_Click(object sender,EventArgs e)
	{   ///设置商品的属性
		ShoppingCartItem item = new ShoppingCartItem();
		item.ProductID = productID;
		item.Name = ProductName;
		item.Price = Decimal.Parse(lbPrice.Text);
		item.Number = 1;
		///添加到购物车
		ShoppingCart shoppingCart = new ShoppingCart(Session);
		if(shoppingCart.AddProductToShoppingCart(item) > -1)
		{
			AjaxEBusinessSystem.ShowAjaxDialog((Button)sender,"恭喜您，添加商品到购物车成功。");
		}
	}
	protected void btnCommit_Click(object sender,EventArgs e)
	{
		Product product = new Product();
		///发表评论
		if(product.AddProductComment(tbTitle.Text,tbBody.Text,Request.UserHostAddress,tbEmail.Text,productID) > 0)
		{	///重新显示数据		
			BindPageData(productID);
		}
	}
}
