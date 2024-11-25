$(document).ready(function () {
    // Handle login form submission
    $("#loginForm").on("submit", function (e) {
        e.preventDefault();
        
        // Show loading spinner and disable button
        const $button = $("#loginButton");
        const $spinner = $button.find(".spinner");
        const $buttonText = $button.find(".button-text");
        const $errorMessage = $(".error-message");
        
        $button.prop('disabled', true);
        $spinner.show();
        $errorMessage.hide();
        
        var loginData = {
            username: $("#username").val(),
            password: $("#password").val(),
            rememberMe: $("#remember").is(":checked")
        };

        $.ajax({
            url: "/Account/Login",
            type: "POST",
            data: loginData,
            success: function (response) {
                if (response.success) {
                    // Close login popup
                    $("#login-content").parents('.overlay').removeClass("openform");
                    
                    // Reload current page after successful login
                    window.location.reload();
                } else {
                    // Show error message
                    $errorMessage.text(response.message || "Login failed. Please try again.").show();
                }
            },
            error: function () {
                $errorMessage.text("An error occurred. Please try again.").show();
            },
            complete: function() {
                // Hide loading spinner and enable button
                $button.prop('disabled', false);
                $spinner.hide();
            }
        });
    });

    // Handle logout
    $(document).on("click", "#logoutBtn", function (e) {
        e.preventDefault();
        
        $.ajax({
            url: "/Account/Logout",
            type: "POST",
            success: function (response) {
                if (response.success) {
                    // Reload page after successful logout
                    window.location.reload();
                }
            }
        });
    });
});