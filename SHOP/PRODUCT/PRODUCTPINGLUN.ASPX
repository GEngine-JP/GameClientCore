<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPinglun.aspx.cs" Inherits="Product_ProductComment" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理商品的评论</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up">
		<ContentTemplate>
			<table width="800" border="0" cellpadding="2" align="center" cellspacing="0" class="Text">
				<tr>					
					<td colspan="2">				
						<asp:GridView ID="gvComment" runat="server" Width="100%" EmptyDataText="暂无商品评论。" AutoGenerateColumns="False" SkinID="gvSkin" ShowHeader="False" OnRowCommand="gvComment_RowCommand" OnRowDataBound="gvComment_RowDataBound">
							<Columns>
								<asp:TemplateField>
									<ItemTemplate>
										<table class="Table" cellpadding="3" cellspacing="0">
											<tr>
												<td>作者：<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a> 于[<%# Eval("IP") %>]、[<%# Eval("CreateDate") %>] 评论&nbsp;<asp:ImageButton ID="ibtDelete" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="del" ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/delete.PNG" /></td>
											</tr>
											<tr>
												<td><hr size="1" /></td>
											</tr>
											<tr>
												<td class="Title">　<%# Eval("Title") %></td>
											</tr>
											<tr>
												<td>　<%# Eval("Body") %></td>
											</tr>
										</table>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>							
					</td>
				</tr>    
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>	
    </form>
</body>
</html>

