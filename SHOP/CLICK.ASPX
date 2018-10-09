<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Click.aspx.cs" Inherits="ViewProductByCategoryCount" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>按点击查看商品</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up">
		<ContentTemplate>
			<asp:GridView ID="gvProduct" runat="server" Width="100%" EmptyDataText="暂无商品！" AutoGenerateColumns="False" GridLines="Horizontal" ShowHeader="False" SkinID="gvSkin" OnRowCommand="gvProduct_RowCommand" OnRowDataBound="gvProduct_RowDataBound" DataKeyNames="ID">
				<EmptyDataTemplate>暂无商品！</EmptyDataTemplate>
				<Columns>					
					<asp:TemplateField>
						<ItemTemplate>
							<table width="100%" border="0" cellpadding="2" cellspacing="0" class="Table">
								<tr>
									<td width="200"><asp:Image ID="imgProduct" runat="server" Width="160" Height="120" ImageUrl='<%# "~/" + Eval("PictureUrl") %>' /></td>
									<td width="400">
										<table width="400" border="0" cellpadding="2" cellspacing="0" class="Table">
											<tr>
												<td colspan="2"><a href='ShowProduct.aspx?ProductID=<%# Eval("ID") %>' target="_blank"><asp:Label ID="lbName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></a></td>
											</tr>
											<tr>
												<td>商品价格：</td>
												<td><asp:Label ID="lbPrice" ForeColor="red" runat="server" Text='<%# Eval("Price","{0:f2}") %>'></asp:Label></td>
											</tr>
											<tr>
												<td>访问次数：</td>
												<td width="80%"><%# Eval("ViewCount") %></td>
											</tr>
											<tr>
												<td>发布日期：</td>
												<td><%# Eval("CreateDate","{0:d}") %></td>
											</tr>
											<tr>
												<td>发 布 人：</td>
												<td><%# Eval("Username") %></td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td><asp:Button ID="btnBuy" runat="server" Text="加入购物车" CommandArgument='<%# Eval("ID") %>' CommandName="buy" /></td>
											</tr>
										</table>										
									</td>
								</tr>	
								<tr>
									<td colspan="2"><%# Eval("Remark") %></td>
								</tr>
								<tr>
									<td colspan="2"><hr /></td>
								</tr>													
							</table>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
		</ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
