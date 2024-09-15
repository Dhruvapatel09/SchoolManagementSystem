<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="SchoolManagementSyatem.AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        .login,
        .image {
            min-height: 100vh;
        }
        .bg-image {
            background-image: url('../Image/login1.jpg');
            background-size: cover;
        }
        
        .register-link {
            position: absolute;
            top: 20px;
            right: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid" style="height: 100vh;">
            <div class="row no-gutter">
                <!-- The image half -->
                <div class="col-md-6 d-none d-md-flex bg-image"></div>
                <!-- The content half -->
                <div class="col-md-6 bg-light position-relative">
                    <div class="login d-flex align-items-center py-3">
                    <a href="AdminRegistration.aspx" class="btn btn-outline-secondary register-link">Register here</a> 
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-10 col-xl-7 mx-auto">
                                <div class="login-form">
                                    <h3 class="display-4 pb-3">Admin Sign In</h3>
                                    <p class="text-muted mb-3">Login page for Admins.</p>
                                    <div class="form-group mb-3">
                                        <input id="inputEmail" runat="server" type="text" placeholder="Email address" required autofocus class="form-control rounded-pill" />
                                    </div>
                                    <div class="form-group mb-3">
                                        <input id="inputPassword" runat="server" type="password" placeholder="Password" required class="form-control rounded-pill border-0" />
                                    </div>
                                    <asp:Button ID="btnLogin" runat="server" Text="Sign in" CssClass="btn btn-primary btn-block text-uppercase mb-2 rounded-pill shadow-sm" OnClick="btnLogin_Click" />
                                    <div class="text-center d-flex justify-content-between mt-4">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
