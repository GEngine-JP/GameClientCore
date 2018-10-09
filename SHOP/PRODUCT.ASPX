<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="ProductPage" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server" />
        <ajaxToolkit:TabContainer ID="tcProduct" ActiveTabIndex="0" runat="server" ScrollBars="Auto">
			<ajaxToolkit:TabPanel ID="tbOrder" runat="server" HeaderText="按商品销量浏览">
				<ContentTemplate>
					<iframe id="iProductOrder" frameborder="0" marginheight="2" marginwidth="2" width="100%" height="524" scrolling="auto" src='Sales.aspx?CategoryID=<%= categoryID %>'></iframe>
				</ContentTemplate>
			</ajaxToolkit:TabPanel>
			<ajaxToolkit:TabPanel ID="tbName" runat="server" HeaderText="按商品名称浏览">
				<ContentTemplate>
					<iframe id="iProductName" frameborder="0" marginheight="2" marginwidth="2" width="100%" height="524" scrolling="auto" src='Name.aspx?CategoryID=<%= categoryID %>'></iframe>
				</ContentTemplate>
			</ajaxToolkit:TabPanel>
			<ajaxToolkit:TabPanel ID="tbTime" runat="server" HeaderText="按上架时间浏览">
				<ContentTemplate>
					<iframe id="iProductTime" frameborder="0" marginheight="2" marginwidth="2" width="100%" height="524" scrolling="auto" src='Time.aspx?CategoryID=<%= categoryID %>'></iframe>
				</ContentTemplate>
			</ajaxToolkit:TabPanel>
			<ajaxToolkit:TabPanel ID="tbPrice" runat="server" HeaderText="按商品价格浏览">
				<ContentTemplate>
					<iframe id="iProductPrice" frameborder="0" marginheight="2" marginwidth="2" width="100%" height="524" scrolling="auto" src='Charge.aspx?CategoryID=<%= categoryID %>'></iframe>
				</ContentTemplate>
			</ajaxToolkit:TabPanel>
			<ajaxToolkit:TabPanel ID="tbCount" runat="server" HeaderText="按浏览次数浏览">
				<ContentTemplate>
					<iframe id="iProductCount" frameborder="0" marginheight="2" marginwidth="2" width="100%" height="524" scrolling="auto" src='Click.aspx?CategoryID=<%= categoryID %>'></iframe>
				</ContentTemplate>
			</ajaxToolkit:TabPanel>			
        </ajaxToolkit:TabContainer>
        <div>
        </div>
    </form>
</body>
</html>
