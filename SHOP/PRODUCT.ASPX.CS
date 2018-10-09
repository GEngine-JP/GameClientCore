using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ProductPage:System.Web.UI.Page 
{
    protected int categoryID = -1;

	protected void Page_Load(object sender,EventArgs e)
	{	///获取商品种类的ID
		if(Request.Params["CategoryID"] != null)
		{
			categoryID = Int32.Parse(Request.Params["CategoryID"].ToString());
		}
	}
}