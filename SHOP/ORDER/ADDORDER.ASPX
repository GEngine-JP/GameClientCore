<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddOrder.aspx.cs" Inherits="Order_AddOrder" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>创建订单</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <table class="Table" border="0" cellpadding="5" cellspacing="0">
		<tr>
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
		<tr>
			<td>
				<br />
				<asp:Button ID="btnCommit" runat="server" Text="提交并创建订单" SkinID="btnSkin" OnClick="btnCommit_Click" />&nbsp;&nbsp;<asp:Button ID="btnAdd" runat="server" Text="我要购买商品" SkinID="btnSkin" OnClick="btnAdd_Click" />
			</td>
		</tr>
	</table>
    </form>
</body>
</html>

