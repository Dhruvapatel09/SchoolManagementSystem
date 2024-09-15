<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>School Management System</title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
<style>
    body {
        background-color: #f8f9fa;
        font-family: Arial, sans-serif;
    }
    .container {
        margin-top: 50px;
    }
    .btn {
        margin-bottom: 20px;
    }
    .navbar {
        background-color: #343a40; /* dark gray */
    }
    .navbar-brand, .navbar-nav .nav-link {
        color: #ffffff !important; /* white */
    }
    .navbar-brand:hover, .navbar-nav .nav-link:hover {
        color: #cccccc !important; /* light gray */
    }
    .carousel-item img {
        width: 100%;
        height: 400px;
        object-fit: cover;
    }
    .testimonial {
        background-color: #f8f9fa;
        padding: 20px;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        margin-bottom: 20px;
    }
    .testimonial p {
        font-style: italic;
    }
</style>
</head>
<body>

<nav class="navbar navbar-expand-lg navbar-dark">
    <div class="container">
        <a class="navbar-brand" href="#">School Management System</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <a class="nav-link" href="AdminLogin.aspx">Admin Login</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="TeacherLogin.aspx">Teacher Login</a>
                </li>
            </ul>
        </div>
    </div>
</nav>

<div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
    <ol class="carousel-indicators">
        <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
        <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
        <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
    </ol>
    <div class="carousel-inner">
        <div class="carousel-item active">
            <img src="https://via.placeholder.com/1200x400?text=School+Activity" class="d-block w-100" alt="...">
        </div>
        <div class="carousel-item">
            <img src="https://via.placeholder.com/1200x400?text=Students+Learning" class="d-block w-100" alt="...">
        </div>
        <div class="carousel-item">
            <img src="https://via.placeholder.com/1200x400?text=School+Events" class="d-block w-100" alt="...">
        </div>
    </div>
</div>

<div class="container text-center">
    <h1>Welcome to School Management System</h1>
    <p>A comprehensive system for managing schools, including administration, teaching staff, and students.</p>
    <h3>Features:</h3>
    <ul class="list-unstyled">
        <li>Admin panel for managing school resources, staff, and students.</li>
        <li>Teacher panel for managing classes, assignments, and grades.</li>
    </ul>
</div>

<div class="container">
    <div class="row">
        <div class="col-md-6 mx-auto">
            <div class="testimonial">
                <p>"The School Management System has made our school operations more efficient and streamlined. It's easy to use and has all the features we need."</p>
                <p>- John Doe, Principal</p>
            </div>
        </div>
    </div>
</div>

<div class="container">
    <div class="row">
        <div class="col-md-6 mx-auto">
            <div class="testimonial">
                <p>"I love how the Teacher panel allows me to manage my classes and assignments effectively. It saves me a lot of time!"</p>
                <p>- Jane Smith, Teacher</p>
            </div>
        </div>
    </div>
</div>

<footer class="bg-dark text-white text-center py-4 mt-5">
    <div class="container">
        <p>&copy; 2024 School Management System. All rights reserved.</p>
        <p><a href="HomePage.aspx">Go to Home Page</a></p>
    </div>
</footer>

<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.3/dist/umd/popper.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</body>
</html>
