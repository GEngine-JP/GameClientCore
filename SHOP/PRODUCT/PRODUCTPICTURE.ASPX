<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPicture.aspx.cs" Inherits="Product_ProductPicture" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品的图片管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
	<table class="Table" border="0" cellpadding="2" bgcolor="Black" cellspacing="1">
		<tr bgcolor="white">
			<td colspan="2"><hr /><a name="message"></a></td>
		</tr>
		<tr bgcolor="white">
			<td>商品名称：</td>
			<td width="90%"><asp:Label ID="lbProductName" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>				
			</td>
		</tr>
		<tr bgcolor="white">
			<td valign="top">现有图片：</td>
			<td width="90%"><asp:Image ID="imgProduct" runat="server" Width="240" Height="180" />
			</td>
		</tr>				
		<tr bgcolor="white">
			<td>选择图片：</td>
			<td width="90%">
				<asp:FileUpload ID="fuPicture" runat="server" CssClass="Button" Width="400px" />
				<asp:Label ID="lbMessage" runat="server" CssClass="Text" ForeColor="Red"></asp:Label>
			</td>
		</tr>	
		<tr bgcolor="white">
			<td>&nbsp;</td>
			<td width="90%">
				<asp:Button ID="btnCommit" runat="server" Text="提交" SkinID="btnSkin" Width="100px" OnClick="btnCommit_Click" />&nbsp;							
			</td>			
		</tr>
	</table>
    </form>
</body>
</html>
