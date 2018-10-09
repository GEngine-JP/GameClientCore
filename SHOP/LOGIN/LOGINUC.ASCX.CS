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

public partial class Login_LoginUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {	
    }
	protected void btnLogin_Click(object sender,EventArgs e)
	{   ///用户登录操作
		///【注意】：下面的程序代码仅仅是模拟登录，
		///实际的登录过程应该是从数据库中获取数据，
		///并判断用户信息是否正确，然后保存用户的信息。
		Session["UserID"] = 1;
		Session["Username"] = "admin";
		///刷新工具栏页面
		Response.Redirect("~/Daohang.aspx");
	}
}
