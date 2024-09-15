<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminRegistration.aspx.cs" Inherits="SchoolManagementSyatem.AdminRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        /* Adjustments for full-page layout */
        html, body, form, .container-fluid, .row, .col-md-6 {
            height: 100%;
            margin: 0;
            padding: 0;
        }

        /* Background image styles */
        .bg-image {
            background-image: url('../Image/login1.jpg');
            background-size: cover;
            height: 100%;
        }

        /* Styling for registration form */
        .login-form {
            margin-top: 50px;
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
        <div class="container-fluid">
            <div class="row">
                <!-- The image half -->
                <div class="col-md-6 d-none d-md-flex bg-image"></div>
                <!-- The content half -->
                <div class="col-md-6 bg-light position-relative">
                    <div class="login d-flex align-items-center py-3">
                        <a href="AdminLogin.aspx" class="btn btn-outline-secondary register-link">Login here</a>
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-10 col-xl-7 mx-auto">
                                    <div class="login-form">
                                        <h3 class="display-4 pb-3">Admin Sign Up</h3>
                                        <p class="text-muted mb-3">Sign page for Admins.</p>
                                        <div class="form-group mb-3">
                                            <input id="txtName" runat="server" type="text" placeholder="Name" required autofocus class="form-control rounded-pill" />
                                        </div>
                                        <div class="form-group mb-3">
                                            <input id="txtEmail" runat="server" type="email" placeholder="Email address" required class="form-control rounded-pill" />
                                        </div>
                                        <div class="form-group mb-3">
                                            <input id="txtPassword" runat="server" type="password" placeholder="Password" required class="form-control rounded-pill" />
                                        </div>
                                        <div class="form-group mb-3">
                                            <input id="txtConfirmPassword" runat="server" type="password" placeholder="Confirm Password" required class="form-control rounded-pill" />
                                        </div>
                                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary btn-block text-uppercase mb-2 rounded-pill shadow-sm" OnClick="btnRegister_Click" />
                                        <div class="text-center d-flex justify-content-between mt-4">
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>
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
