<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AJAX电子商务应用</title>
</head>
<frameset id="Default" rows="100,50,*" frameborder="0" border="0" framespacing="0">
	<frame id="Header" name="Header" src="Header.aspx" scrolling="auto" noresize></frame>
	<frame id="Toolbar" name="Toolbar" src="Daohang.aspx" scrolling="no" noresize></frame>
	<frameset id="Main" cols="170,*" rows="*" border="0" framespacing="0">
		<frame name="MenuTree" src="Classes.aspx" scrolling="auto" frameborder="0" noresize></frame>
		<frame name="Product" src="Product.aspx?CategoryID=27" scrolling="auto" frameborder="0"></frame>
	</frameset>	
</frameset>
</html>
