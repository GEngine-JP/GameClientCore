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

public partial class Toolbar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {   ///动态载入用户登录控件或者显示用户信息的控件
		pLogin.Controls.Clear();
		if(Session["UserID"] == null)
		{			
			pLogin.Controls.Add(Page.LoadControl("~/Login/LoginUC.ascx"));
		}
		else
		{
			pLogin.Controls.Add(Page.LoadControl("~/Login/LogoffUC.ascx"));
		}
    }
}
