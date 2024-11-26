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
        
        // Show confirmation dialog using SweetAlert2
        Swal.fire({
            title: 'Chắc chắn?',
            text: "Bạn đang đăng xuất khỏi tài khoản",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Đăng xuất',
            cancelButtonText: 'Hủy'
        }).then((result) => {
            if (result.isConfirmed) {
                // Proceed with logout
                $.ajax({
                    url: "/Account/Logout",
                    type: "POST",
                    success: function(response) {
                        if (response.success) {
                            toastr.success('Đăng xuất thành công');
                            // Redirect sau 
                            window.location.href = '/';
                        } else {
                            toastr.error('Đăng xuất thất bại');
                        }
                    },
                    error: function() {
                        toastr.error('Đã xảy ra lỗi khi đăng xuất');
                    }
                });
            }
        });
    });
});