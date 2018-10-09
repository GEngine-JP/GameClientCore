<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderManage.aspx.cs" Inherits="Order_OrderManage" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>管理员处理订单</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up">
		<ContentTemplate>
			<table class="Table" border="0" cellpadding="5" cellspacing="0">
				<tr>
					<td colspan="2">
						<asp:UpdatePanel runat="server" ID="UpdatePanel1">
							<ContentTemplate>
								<asp:GridView ID="gvOrder" runat="server" Width="100%" EmptyDataText="您还没有订单。" AutoGenerateColumns="False" SkinID="gvSkin" OnRowCommand="gvOrder_RowCommand">
									<Columns>
										<asp:TemplateField HeaderText="订单编号">
											<ItemTemplate>
												<a href='ViewOrder.aspx?OrderID=<%# Eval("ID") %>' target="Product"><%# Eval("OrderNo") %></a>
											</ItemTemplate>
											<ItemStyle HorizontalAlign="left" Width="28%" />
											<HeaderStyle HorizontalAlign="left" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="商品总金额">
											<ItemTemplate>
												<%# Eval("TotalMoney","{0:C2}") %>										
											</ItemTemplate>
											<ItemStyle HorizontalAlign="Center" Width="20%" Font-Bold="true" ForeColor="red" />
											<HeaderStyle HorizontalAlign="Center" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="商品总数量">
											<ItemTemplate>
												<%# Eval("TotalNumber") %>																				
											</ItemTemplate>
											<ItemStyle HorizontalAlign="center" Width="16%" />
											<HeaderStyle HorizontalAlign="center" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="创建时间">
											<ItemTemplate>
												<%# Eval("CreateDate") %>																				
											</ItemTemplate>
											<ItemStyle HorizontalAlign="center" Width="18%" />
											<HeaderStyle HorizontalAlign="center" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="订单状态">
											<ItemTemplate>
												<%# Eval("Status").ToString() == "0" ? "未处理" : "已处理" %>																				
											</ItemTemplate>
											<ItemStyle HorizontalAlign="center" Width="10%" ForeColor="red" />
											<HeaderStyle HorizontalAlign="center" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="处理订单">
											<ItemTemplate>
												<asp:Button ID="btnCommit" runat="server" Text="提交" CommandName="sale" CommandArgument='<%# Eval("ID") %>' />											
											</ItemTemplate>
											<ItemStyle HorizontalAlign="center" Width="8%" />
											<HeaderStyle HorizontalAlign="center" />
										</asp:TemplateField>													
									</Columns>							
								</asp:GridView>
							</ContentTemplate>
						</asp:UpdatePanel>				
					</td>
				</tr>
			</table>
		</ContentTemplate>
    </asp:UpdatePanel>    
    </form>
</body>
</html>
