<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="AEPTexasUserMngmnt.CreateUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Table ID="Table01" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Literal ID="Literal1" runat="server" Text="Email Address" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tbxEmailAddress" runat="server" AutoPostBack="true" OnTextChanged="tbxEmailAddress_TextChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Literal ID="Literal2" runat="server" Text="First Name" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tbxFirstName" runat="server" AutoPostBack="true" OnTextChanged="tbxFirstName_TextChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Literal ID="Literal3" runat="server" Text="Last Name" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tbxLastName" runat="server" AutoPostBack="true" OnTextChanged="tbxLastName_TextChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Literal ID="Literal4" runat="server" Text="Password" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tbxPassword1" runat="server" AutoPostBack="true" OnTextChanged="tbxPassword1_TextChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Literal ID="Literal5" runat="server" Text="Re-enter Password" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="tbxPassword2" runat="server" AutoPostBack="true" OnTextChanged="tbxPassword2_TextChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Literal ID="Literal6" runat="server" Text="Role" />
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="roleList" runat="server" OnTextChanged="roleList_TextChanged" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnCreateUser" runat="server" OnClick="btnCreateUser_Click" Text="Create User" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
