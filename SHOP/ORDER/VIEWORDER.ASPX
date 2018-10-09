<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewOrder.aspx.cs" Inherits="Order_ViewOrder" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>查看订单</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <table class="Table" border="0" cellpadding="5" bgcolor="Black" cellspacing="0">
		<tr bgcolor="white">
			<td>订单编号：<asp:Label ID="lbOrderNo" runat="server" ForeColor="red" Font-Bold="true"></asp:Label></td>
			<td>创建时间：<asp:Label ID="lbCreateDate" runat="server" ForeColor="Blue"></asp:Label></td>
		</tr>
		<tr bgcolor="white">
			<td>商品总数量：<asp:Label ID="lbTotalNumber" runat="server" ForeColor="red" Font-Bold="true"></asp:Label></td>
			<td>商品总金额：<asp:Label ID="lbTotalMoney" runat="server" ForeColor="red" Font-Bold="true"></asp:Label></td>
		</tr>
		<tr bgcolor="white">
			<td colspan="2">
				<asp:UpdatePanel runat="server" ID="up">
					<ContentTemplate>
						<asp:GridView ID="gvProduct" runat="server" Width="100%" EmptyDataText="您还没有添加商品到购物车。" AutoGenerateColumns="False" SkinID="gvSkin">
							<Columns>
								<asp:TemplateField HeaderText="商品名称">
									<ItemTemplate>
										<a href='../ShowProduct.aspx?ProductID=<%# Eval("ProductID") %>' target="_blank"><%# Eval("Name") %></a>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="72%" />
									<HeaderStyle HorizontalAlign="left" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="商品价格">
									<ItemTemplate>
										<%# Eval("Price") %>										
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="16%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="商品数量">
									<ItemTemplate>
										<%# Eval("Number") %>																				
									</ItemTemplate>
									<ItemStyle HorizontalAlign="center" Width="12%" />
									<HeaderStyle HorizontalAlign="center" />
								</asp:TemplateField>								
							</Columns>							
						</asp:GridView>
					</ContentTemplate>
				</asp:UpdatePanel>				
			</td>
		</tr>		
	</table>
    </form>
</body>
</html>
