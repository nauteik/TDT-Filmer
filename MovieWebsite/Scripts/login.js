$(document).ready(function () {
    // Handle login form submission
    $("#loginForm").on("submit", function (e) {
        e.preventDefault();
        
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
                    $("#login-content").removeClass("show");
                    
                    // Update UI for logged in user
                    $(".btn-login").hide();
                    $(".btn-signup").hide();
                    $(".user-info").html(`
                        <li class="dropdown">
                            <a href="#">${response.username}</a>
                            <ul class="dropdown-menu">
                                <li><a href="/Account/Profile">Profile</a></li>
                                <li><a href="#" id="logoutBtn">Logout</a></li>
                            </ul>
                        </li>
                    `).show();

                    // Show success message
                    alert("Login successful!");
                } else {
                    alert(response.message || "Login failed. Please try again.");
                }
            },
            error: function () {
                alert("An error occurred. Please try again.");
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
                    // Update UI for logged out user
                    $(".user-info").hide();
                    $(".btn-login").show();
                    $(".btn-signup").show();
                    
                    // Show success message
                    alert("Logged out successfully!");
                }
            }
        });
    });
});