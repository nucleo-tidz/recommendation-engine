$(document).ready(function () {
    // Handle the login form submission
    $("#login-form").submit(function (event) {
        event.preventDefault(); // Prevent default form submission

        // Get the email value from the input field
        var email = $("#email").val();

        // Save the email in localStorage
        localStorage.setItem("userEmail", email);

        // Redirect to the product grid page
        window.location.href = "Home.html";
    });
});