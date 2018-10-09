<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewShoppingCart.aspx.cs" Inherits="ShoppingCart_ViewShoppingCart" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我的购物车</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <table class="Table" border="0" cellpadding="5" cellspacing="0">
		<tr>
			<td colspan="2">
				<asp:UpdatePanel runat="server" ID="up">
					<ContentTemplate>
						<asp:GridView ID="gvProduct" runat="server" Width="100%" EmptyDataText="您还没有添加商品到购物车。" AutoGenerateColumns="False" SkinID="gvSkin" OnRowCommand="gvProduct_RowCommand" OnRowDataBound="gvProduct_RowDataBound">
							<Columns>
								<asp:TemplateField HeaderText="商品名称">
									<ItemTemplate>
										<a href='../ShowProduct.aspx?ProductID=<%# Eval("ProductID") %>' target="_blank"><%# Eval("Name") %></a>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Left" Width="66%" />
									<HeaderStyle HorizontalAlign="Left" />
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
										<asp:TextBox ID="tbNumber" runat="server" Text='<%# Eval("Number") %>' Width="95%"></asp:TextBox>
										<asp:RequiredFieldValidator ID="rfNumber" runat="server" ControlToValidate="tbNumber" Display="dynamic" ErrorMessage="不能为空！"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="revNumber" runat="server" ControlToValidate="tbNumber" ValidationExpression="([1-9]\d*)|\d" Display="dynamic" ErrorMessage="只能为整数！"></asp:RegularExpressionValidator>																	
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="12%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="操作">
									<ItemTemplate>
										<asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("ProductID") %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/delete.PNG" CommandName="del" />								
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="6%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
							</Columns>							
						</asp:GridView>
					</ContentTemplate>
				</asp:UpdatePanel>				
			</td>
		</tr>
		<tr>
			<td>
				<br />
				<asp:Button ID="btnStore" runat="server" Text="保存修改" SkinID="btnSkin" OnClick="btnStore_Click" />&nbsp;&nbsp;<asp:Button ID="btnCommit" runat="server" Text="我要创建订单" SkinID="btnSkin" OnClick="btnCommit_Click" />&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="我要购买商品" SkinID="btnSkin" OnClick="btnAdd_Click" />
			</td>
		</tr>
	</table>
    </form>
</body>
</html>
