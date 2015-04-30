<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AEPTexasUserMngmnt.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager01" runat="server"></asp:ScriptManager>
        <div>
            <asp:LoginView ID="LoginView01" runat="server">
                <AnonymousTemplate>
                    <asp:Login ID="Login01" 
                        runat="server" 
                        CreateUserText="Create New User" 
                        CreateUserUrl="~/Login.aspx" 
                        DisplayRememberMe="false" 
                        OnAuthenticate="Login01_OnAuthenticate" 
                        OnLoggedIn="Login01_OnLoggedIn" 
                        OnLoggingIn="Login01_OnLoggingIn" 
                        OnLoginError="Login01_OnLoginError" 
                        PasswordRecoveryText="Lost Password?" 
                        PasswordRecoveryUrl="~/Login.aspx" 
                        TextBoxStyle-Width="190px" 
                        TitleText="" 
                        ValidateRequestMode="Enabled">
                    </asp:Login>
                    <asp:Label ID="failLabel" runat="server" />
                </AnonymousTemplate>
                <LoggedInTemplate>
                    <asp:Label ID="LoggedInLabel" runat="server" />
                </LoggedInTemplate>
                <RoleGroups>
                    <asp:RoleGroup Roles="UserManager">
                        <ContentTemplate>
                            <asp:Literal ID="ManagerLiteral" runat="server" Text="Role: UserManager" />
                        </ContentTemplate>
                    </asp:RoleGroup>
                    <asp:RoleGroup Roles="AppUser">
                        <ContentTemplate>
                            <asp:Literal ID="AppUserLiteral" runat="server" Text="Role: AppUser" />
                        </ContentTemplate>
                    </asp:RoleGroup>
                    <asp:RoleGroup Roles="Administrator">
                        <ContentTemplate>
                            <asp:Literal ID="AdminLiteral" runat="server" Text="Role: Administrator" />
                        </ContentTemplate>
                    </asp:RoleGroup>
                </RoleGroups>
            </asp:LoginView>
        </div>
    </form>
</body>
</html>
