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
using System.IO;

public partial class Product_ProductPicture : System.Web.UI.Page
{
	private int productID = -1;

	protected void Page_Load(object sender,EventArgs e)
	{   ///获取商品的ID值
		if(Request.Params["ProductID"] != null)
		{
			productID = Int32.Parse(Request.Params["ProductID"].ToString());
		}
		///显示商品信息
		if(!Page.IsPostBack && productID > 0)
		{
			BindPageData(productID);
		}
	}

	private void BindPageData(int productID)
	{   ///获取商品信息
		Product product = new Product();
		SqlDataReader dr = product.GetSingleProduct(productID);
		if(dr == null) return;
		///显示商品信息
		if(dr.Read())
		{
			lbProductName.Text = dr["Name"].ToString();
			imgProduct.ImageUrl = "~/" + dr["PictureUrl"].ToString();
		}
		dr.Close();
	}
	protected void btnCommit_Click(object sender,EventArgs e)
	{
		if(productID <= -1) return;
		///判断上载文件的内容是否为空
		if(fuPicture.HasFile == false || fuPicture.PostedFile.ContentLength <= 0)
		{
			lbMessage.Text = "上载文件的内容为空，请重新选择文件！";
			return;
		}
		///获取上载文件的属性，如类型、大小、名称等
		string type = fuPicture.PostedFile.ContentType;
		int size = fuPicture.PostedFile.ContentLength;
		///创建基于时间的文件名称
		string fileName = AjaxEBusinessSystem.CreateDateTimeString();
		string extension = Path.GetExtension(fuPicture.PostedFile.FileName);
		///判断文件是否合法
		bool isAllow = false;
		foreach(string ext in AjaxEBusinessSystem.ALLOWPICTRUEFILELIST)
		{
			if(ext == extension.ToLower())
			{
				isAllow = true;
				break;
			}
		}
		if(isAllow == false) return;
		///构建保存文件位置的路径
		string url = "img/" + fileName + extension;
		///映射为物理路径
		string fullPath = Server.MapPath("~/" + url);
		///判断文件是否存在
		if(File.Exists(fullPath) == true)
		{
			lbMessage.Text = "上载文件的已经存在，请重新选择文件！";
			return;
		}

		try
		{   ///上载文件
			fuPicture.SaveAs(fullPath);
			Product product = new Product();
			///添加到数据库中
			if(product.UpdateProductPicture(productID,url) > 0)
			{	///显示操作结果
				lbMessage.Text = "恭喜您，上载商品图片成功。";
				imgProduct.ImageUrl = "~/" + url;
			}
		}
		catch(Exception ex)
		{   ///显示错误信息
			lbMessage.Text = "上载文件错误，错误原因为：" + ex.Message;
			return;
		}
	}
}
