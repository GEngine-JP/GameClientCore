<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchByPrice.aspx.cs" Inherits="Search_SearchByPrice" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>按价格搜索商品</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <table class="Table" border="0" cellpadding="2" bgcolor="Black" cellspacing="1">
		<tr bgcolor="white">
			<td colspan="2"><hr /><a name="message"></a></td>
		</tr>
		<tr bgcolor="white">
			<td>最低价格：</td>
			<td width="90%"><asp:TextBox ID="tbMinPrice" runat="server" SkinID="tbSkin" Width="60%" MaxLength="100"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfMinPrice" runat="server" ControlToValidate="tbMinPrice" Display="none" ErrorMessage="价格不能为空！"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="revMinPrice" runat="server" ControlToValidate="tbMinPrice" Display="none" ErrorMessage="价格只能为整数或实数，请重新输入。" ValidationExpression="\d+(.\d+)?"></asp:RegularExpressionValidator>
				<ajaxToolkit:TextBoxWatermarkExtender ID="tweMinPrice" runat="server" TargetControlID="tbMinPrice" WatermarkText="请输入最低价格" WatermarkCssClass="Watermark"></ajaxToolkit:TextBoxWatermarkExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceMinPriceBlank" runat="server" TargetControlID="rfMinPrice" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceMinPriceRegex" runat="server" TargetControlID="revMinPrice" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>				
			</td>
		</tr>
		<tr bgcolor="white">
			<td>最高价格：</td>
			<td width="90%"><asp:TextBox ID="tbMaxPrice" runat="server" SkinID="tbSkin" Width="60%" MaxLength="100"></asp:TextBox>
				<asp:RequiredFieldValidator ID="rfMaxPrice" runat="server" ControlToValidate="tbMaxPrice" Display="none" ErrorMessage="价格不能为空！"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="revMaxPrice" runat="server" ControlToValidate="tbMaxPrice" Display="none" ErrorMessage="价格只能为整数或实数，请重新输入。" ValidationExpression="\d+(.\d+)?"></asp:RegularExpressionValidator>
				<ajaxToolkit:TextBoxWatermarkExtender ID="tweMaxPrice" runat="server" TargetControlID="tbMaxPrice" WatermarkText="请输入最高价格" WatermarkCssClass="Watermark"></ajaxToolkit:TextBoxWatermarkExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceMaxPriceBlank" runat="server" TargetControlID="rfMaxPrice" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>
				<ajaxToolkit:ValidatorCalloutExtender ID="vceMaxPriceRegex" runat="server" TargetControlID="revMaxPrice" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>				
			</td>
		</tr>
		<tr bgcolor="white">						
			<td>验 证 码：</td>
			<td>
				<asp:TextBox ID="tbCode" runat="server" SkinID="tbSkin" Width="80px"></asp:TextBox>
				<asp:Image ID="imgCode" runat="server" ImageUrl = "~/Yanzheng.aspx" />
				&nbsp;<asp:Label ID="lbMessage" runat="server" CssClass="Text" ForeColor="Red"></asp:Label>										
			</td>
		</tr>
	</table>
	<asp:UpdatePanel ID="up" runat="server">
		<ContentTemplate>
			<table class="Table" border="0" cellpadding="2" bgcolor="Black" cellspacing="1">
				<tr bgcolor="white">
					<td>&nbsp;</td>
					<td width="90%">				
						<asp:Button ID="btnCommit" runat="server" Text="提交" SkinID="btnSkin" Width="100px" OnClick="btnCommit_Click" />&nbsp;&nbsp;&nbsp;
						<asp:UpdateProgress ID="upProgress" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="up">
							<ProgressTemplate>
								<font color="red">正在搜索商品，请等待……</font> 
							</ProgressTemplate>
						</asp:UpdateProgress>
					</td>			
				</tr>
				<tr bgcolor="white">
					<td>搜索结果：&nbsp;</td>
					<td width="90%">
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
															<td colspan="2"><a href='../ShowProduct.aspx?ProductID=<%# Eval("ID") %>' target="_blank"><asp:Label ID="lbName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></a></td>
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
					</td>
				</tr>
			</table>  
		</ContentTemplate>					
	</asp:UpdatePanel>	
    </form>
</body>
</html>

