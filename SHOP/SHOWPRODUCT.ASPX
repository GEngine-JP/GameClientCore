<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowProduct.aspx.cs" Inherits="ShowProduct" StylesheetTheme="ASPNETAjaxWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>查看商品：<%= ProductName %></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="up">
		<ContentTemplate>
			<table width="800" border="0" cellpadding="2" align="center" cellspacing="0" class="Text">
				<tr>
					<td width="200"><asp:Image ID="imgProduct" runat="server" Width="160" Height="120" /></td>
					<td width="600">
						<table width="400" border="0" cellpadding="2" cellspacing="0" class="Table">
							<tr>
								<td colspan="2"><font class="Title"><%= ProductName %></font></td>
							</tr>
							<tr>
								<td>商品价格：</td>
								<td><asp:Label ID="lbPrice" ForeColor="red" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td>访问次数：</td>
								<td width="80%"><%= ViewCount %></td>
							</tr>
							<tr>
								<td>发布日期：</td>
								<td><%= CreateDate %></td>
							</tr>
							<tr>
								<td>发 布 人：</td>
								<td><%= Username %></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td><asp:Button ID="btnBuy" runat="server" Text="加入购物车" OnClick="btnBuy_Click" CausesValidation="False" /></td>
							</tr>
						</table>										
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:GridView ID="gvAttribute" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="gvSkin">
							<Columns>
								<asp:TemplateField HeaderText="属性名称">
									<ItemTemplate>
										<%# Eval("Text") %>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="40%" />
									<HeaderStyle HorizontalAlign="left" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="属性值">
									<ItemTemplate>
										<%# Eval("Value") %>										
									</ItemTemplate>
									<ItemStyle HorizontalAlign="left" Width="60%" />
									<HeaderStyle HorizontalAlign="left" />
								</asp:TemplateField>
							</Columns>							
						</asp:GridView>
					</td>
				</tr>	
				<tr>
					<td colspan="2"><%= Remark %></td>
				</tr>
				<tr bgcolor="white">
					<td colspan="2"><hr /></td>
				</tr>
				<tr>
					<td colspan="2">				
						<asp:GridView ID="gvComment" runat="server" Width="100%" EmptyDataText="暂无商品评论。" AutoGenerateColumns="False" SkinID="gvSkin" ShowHeader="False">
							<Columns>
								<asp:TemplateField>
									<ItemTemplate>
										<table class="Table" cellpadding="3" cellspacing="0">
											<tr>
												<td>作者：<a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a> 于[<%# Eval("IP") %>]、[<%# Eval("CreateDate") %>] 评论</td>
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
				<tr bgcolor="white">
					<td colspan="2"><hr /></td>
				</tr>
				<tr bgcolor="white">
					<td>评论标题：</td>
					<td width="90%"><asp:TextBox ID="tbTitle" runat="server" SkinID="tbSkin" Width="80%"></asp:TextBox>
						<asp:RequiredFieldValidator ID="rfTitle" runat="server" ControlToValidate="tbTitle"	ErrorMessage="标题不能为空！"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator ID="revTitle" runat="server" ControlToValidate="tbTitle" Display="Dynamic" ErrorMessage="标题不能为空！" ValidationExpression=".+"></asp:RegularExpressionValidator>
						<ajaxToolkit:TextBoxWatermarkExtender ID="tweTitle" runat="server" TargetControlID="tbTitle" WatermarkText="请输入评论标题" WatermarkCssClass="Watermark"></ajaxToolkit:TextBoxWatermarkExtender>
					</td>
				</tr>
				<tr bgcolor="white">
					<td>IP地址：</td>
					<td width="90%"><asp:TextBox ID="tbIP" runat="server" Enabled="false" SkinID="tbSkin" Width="40%"></asp:TextBox></td>
				</tr>
				<tr bgcolor="white">
					<td>电子邮件：</td>
					<td width="90%"><asp:TextBox ID="tbEmail" runat="server" SkinID="tbSkin" Width="40%"></asp:TextBox>
						<asp:RequiredFieldValidator ID="rfEmail" runat="server" ErrorMessage="不能为空！" ControlToValidate="tbEmail" Display="Dynamic"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail" Display="None" ErrorMessage="电子邮件格式不正确，请输入如下形式的电子邮件：<br />aaa@bbb.com" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
						<ajaxToolkit:TextBoxWatermarkExtender ID="tweEmail" runat="server" TargetControlID="tbEmail" WatermarkText="请输入电子邮件" WatermarkCssClass="Watermark"></ajaxToolkit:TextBoxWatermarkExtender>
						<ajaxToolkit:ValidatorCalloutExtender ID="vceEmail" runat="server" TargetControlID="revEmail" HighlightCssClass="Validator"></ajaxToolkit:ValidatorCalloutExtender>
					</td>
				</tr>
				
				<tr bgcolor="white">
					<td valign="top">评论内容：</td>
					<td width="90%">
						<asp:TextBox ID="tbBody" runat="server" Height="100px" SkinID="tbSkin" TextMode="MultiLine" Width="98%"></asp:TextBox>
						<ajaxToolkit:TextBoxWatermarkExtender ID="wmeMessage" runat="server" TargetControlID="tbBody" WatermarkText="请输入评论内容" WatermarkCssClass="Watermark"></ajaxToolkit:TextBoxWatermarkExtender>
					</td>
				</tr>
				<tr bgcolor="white">
					<td>&nbsp;</td>
					<td width="90%">
						<asp:Button ID="btnCommit" runat="server" Text="提交" SkinID="btnSkin" Width="100px" OnClick="btnCommit_Click" />							
					</td>			
				</tr>							
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>	
    </form>
</body>
</html>
