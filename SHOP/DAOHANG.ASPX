<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Daohang.aspx.cs" Inherits="Toolbar" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>工具栏</title>
</head>
<body style="margin:0;" bgcolor="#184073" class="Body">
    <form id="form1" runat="server">
    <table class="Table" border="0" cellpadding="2" bgcolor="Black" cellspacing="1">
		<tr bgcolor="white">
			<td bgcolor="white"><asp:Panel ID="pLogin" runat="server"></asp:Panel></td>
		  <td align="center" bgcolor="white"><a href="ShoppingCart/ViewShoppingCart.aspx" target="Product">查看购物车</a></td>
		  <td align="center" bgcolor="white"><a href="Order/OrderList.aspx" target="Product">查看订单</a></td>
		  <td align="center" bgcolor="white"><a href="Order/OrderManage.aspx" target="Product">订单处理</a></td>
		  <td align="center" bgcolor="white"><a href="Search/SearchByKey.aspx" target="Product">按照关键字搜索</a></td>
			<td align="center"><a href="Search/SearchByPrice.aspx" target="Product">按照价格搜索</a></td>
		  <td align="center" bgcolor="white"><a href="Search/SearchByTime.aspx" target="Product">按照时间搜索</a></td>
	  </tr>		
	</table>
    </form>
</body>
</html>
