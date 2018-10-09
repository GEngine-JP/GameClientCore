<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Classes.aspx.cs" Inherits="Category_Category" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>商品分类管理</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" ></asp:ScriptManager>
    <table class="Table" border="0" cellpadding="5" cellspacing="0">
		<tr>
			<td colspan="2">
				<asp:UpdatePanel runat="server" ID="up">
					<ContentTemplate>
						<asp:GridView ID="gvCategory" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="gvSkin" OnRowCommand="gvCategory_RowCommand" OnRowDataBound="gvCategory_RowDataBound">
							<Columns>
								<asp:TemplateField HeaderText="商品分类">
									<ItemTemplate>
										<%# Eval("Name") %>(<%# Eval("SubCount") %>)</a>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="20%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="上级分类">
									<ItemTemplate>
										<%# Eval("ParentName") %>										
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Center" Width="18%" />
									<HeaderStyle HorizontalAlign="Center" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="分类简介">
									<ItemTemplate>
										<%# Eval("Remark") %>										
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="50%" />
									<HeaderStyle HorizontalAlign="left" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="操作">
									<ItemTemplate>
										<asp:ImageButton ID="imgUpdate" runat="server" CommandArgument='<%# Eval("ID") %>' Visible='<%# (int)Eval("ID") > 1 ? true : false %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/edit.PNG" CommandName="update" />&nbsp;
										<asp:ImageButton ID="imgUp" runat="server" CommandName="up" Visible='<%# (int)Eval("ShowOrder") > 1 ? true : false %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/up.png" AlternateText="上移" CommandArgument='<%# Eval("ID") %>' />
										<asp:ImageButton ID="imgDown" runat="server" CommandName="down" Visible='<%# (int)Eval("ShowOrder") < (int)Eval("SiblingCount") ? true : false %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/down.png" AlternateText="下移" CommandArgument='<%# Eval("ID") %>' />
										<asp:ImageButton ID="imgDelete" runat="server" Visible='<%# (int)Eval("SubCount") > 0 ? false : true %>' CommandArgument='<%# Eval("ID") %>' ImageUrl="~/App_Themes/ASPNETAjaxWeb/Images/delete.PNG" CommandName="del" />								
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="12%" />
									<HeaderStyle HorizontalAlign="left" />
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
				<asp:Button ID="btnAdd" runat="server" Text="添加新的商品分类" OnClick="btnAdd_Click" SkinID="btnSkin" />
			</td>
		</tr>
	</table>
    </form>
</body>
</html>

