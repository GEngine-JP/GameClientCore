<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductAttribute.ascx.cs" Inherits="Product_ProductUC_ProductAttribute" %>
<asp:UpdatePanel runat="server" ID="up">
	<ContentTemplate>
		<asp:DropDownList ID="ddlCategory" runat="server" SkinID="ddlSkin"  AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="300px"></asp:DropDownList><br />
		<asp:DropDownList ID="ddlCategoryS" runat="server" SkinID="ddlSkin" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryS_SelectedIndexChanged" Width="300px"></asp:DropDownList><br />
		<asp:DropDownList ID="ddlCategoryT" runat="server" SkinID="ddlSkin" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryT_SelectedIndexChanged" Width="300px"></asp:DropDownList><br />
		<asp:Panel ID="pAttribute" CssClass="Text" GroupingText="属性：" runat="server" Width="100%"></asp:Panel>
	</ContentTemplate>
</asp:UpdatePanel>
