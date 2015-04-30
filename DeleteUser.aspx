<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteUser.aspx.cs" Inherits="AEPTexasUserMngmnt.DeleteUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell5" runat="server" ColumnSpan="2">
                    Enter Username of User to delete:
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server">&nbsp;</asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server">
                    <asp:TextBox ID="tbxUserDelete" runat="server" />
                </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell3" runat="server">&nbsp;</asp:TableCell><asp:TableCell ID="TableCell4" runat="server">
                    <asp:Button ID="btnDeleteUser" runat="server" OnClick="btnDeleteUser_Click" Text="Delete User" />
                </asp:TableCell></asp:TableRow></asp:Table></div></form></body></html>