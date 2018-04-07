<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Input.aspx.cs" Inherits="TestServiceWebAppFE.Input" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Enter Test Row: "></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Enter Test Column: "></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Enter Test Name: "></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Enter Test Length: "></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Enter Test Unit: "></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label12" runat="server" Text="Status Result : "></asp:Label>
        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Fetch from DB" OnClick="Button2_Click" />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Display Test ID: "></asp:Label>
        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Display Test Row: "></asp:Label>
        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label8" runat="server" Text="Display Test Column: "></asp:Label>
        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label9" runat="server" Text="Display Test Name: "></asp:Label>
        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label10" runat="server" Text="Display Test Length: "></asp:Label>
        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label11" runat="server" Text="Display Test Unit: "></asp:Label>
        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
        
    </div>
    </form>
</body>
</html>
