<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Product_Product" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品管理</title>
</head>
<body>
    <form id="form1" runat="server">
	<asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up">
		<ContentTemplate>
			 <table class="Table" border="0" cellpadding="2" bgcolor="Black" cellspacing="1">
				<tr bgcolor="white">
					<td>上级分类：</td>
					<td width="90%">
						<asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddlSkin" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
						</asp:DropDownList>
					</td>
				</tr>
				<tr bgcolor="white">
					<td colspan="2">				
						<asp:GridView ID="gvProduct" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="gvSkin" OnRowCommand="gvProduct_RowCommand" OnRowDataBound="gvProduct_RowDataBound">
							<Columns>
								<asp:TemplateField HeaderText="商品名称">
									<ItemTemplate>
										<a href='../ShowProduct.aspx?ProductID=<%# Eval("ID") %>'><%# Eval("Name") %></a>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="20%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="查看次数">
									<ItemTemplate>
										<%# Eval("ViewCount") %>										
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="12%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="商品简介">
									<ItemTemplate>
										<%# Eval("Remark") %>										
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="56%" />
									<HeaderStyle HorizontalAlign="left" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="操作">
									<ItemTemplate>
										<asp:ImageButton ID="imgUpdate" runat="server" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/edit.PNG" CommandName="update" />&nbsp;
										<asp:ImageButton ID="imgPicture" runat="server" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/pic.gif" CommandName="picture" />&nbsp;
										<asp:ImageButton ID="imgComment" runat="server" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/comment.png" CommandName="comment" />&nbsp;
										<asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("ID") %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/delete.PNG" CommandName="del" />								
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="12%" />
									<HeaderStyle HorizontalAlign="left" />
								</asp:TemplateField>
							</Columns>							
						</asp:GridView>								
					</td>
				</tr>
				<tr bgcolor="white">
					<td colspan="2">
						<asp:Button ID="btnAdd" runat="server" Text="添加新的商品" OnClick="btnAdd_Click" SkinID="btnSkin" />
					</td>
				</tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
    </form>
</body>
</html>
