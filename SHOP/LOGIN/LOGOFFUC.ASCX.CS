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

public partial class Login_LogoffUC : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(Session["Username"] != null)
		{   ///显示登录后的用户名称
			lbUsername.Text = Session["Username"].ToString();
		}		
    }
	protected void btnLogoff_Click(object sender,EventArgs e)
	{   ///退出登录
		Session["UserID"] = null;
		Session["Username"] = null;
		Session.Clear();
		Session.Abandon();
		///刷新工具栏页面
		Response.Redirect("~/Daohang.aspx");
	}
}
