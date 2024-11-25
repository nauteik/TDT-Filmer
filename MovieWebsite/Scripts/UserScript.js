$(document).ready(function() {
    // Tab switching
    $('.tab-link').on('click', function(e) {
        e.preventDefault();
        
        // Remove active class from all tabs
        $('.tab-link').parent().removeClass('active');
        // Add active class to current tab
        $(this).parent().addClass('active');
        
        // Hide all tab contents
        $('.tab-content').hide();
        // Show selected tab content
        $('#' + $(this).data('tab')).show();
    });

    // Profile form submission
    $('#profileForm').on('submit', function(e) {
        e.preventDefault();
        
        var $form = $(this);
        var data = {
            Email: $form.find('input[name="Email"]').val(),
            FirstName: $form.find('input[name="FirstName"]').val(),
            LastName: $form.find('input[name="LastName"]').val(),
            Country: $form.find('input[name="Country"]').val()
        };
        
        $.ajax({
            url: '/Account/UpdateProfile',
            type: 'POST',
            data: data,
            success: function(response) {
                if (response.success) {
                    toastr.success('Profile updated successfully');
                } else {
                    toastr.error(response.message || 'Failed to update profile');
                }
            },
            error: function() {
                toastr.error('An error occurred while updating profile');
            }
        });
    });

    // Password form submission
    $('#passwordForm').on('submit', function(e) {
        e.preventDefault();
        
        var $form = $(this);
        var newPassword = $form.find('input[name="newPassword"]').val();
        var confirmPassword = $form.find('input[name="confirmPassword"]').val();
        
        // Validate password match
        if (newPassword !== confirmPassword) {
            toastr.error('New passwords do not match');
            return;
        }
        
        var data = {
            oldPassword: $form.find('input[name="oldPassword"]').val(),
            newPassword: newPassword,
            confirmPassword: confirmPassword
        };
        
        // Debug: Log request data
        console.log('Sending password change request:', data);
        
        $.ajax({
            url: '/Account/ChangePassword',
            type: 'POST',
            data: data,
            success: function(response) {
                // Debug: Log response
                console.log('Password change response:', response);
                
                if (response.success) {
                    toastr.success('Password changed successfully');
                    $form[0].reset(); // Clear form
                } else {
                    toastr.error(response.message || 'Failed to change password');
                }
            },
            error: function(xhr, status, error) {
                // Debug: Log error details
                console.log('Error details:', {
                    status: status,
                    error: error,
                    response: xhr.responseText
                });
                toastr.error('An error occurred while changing password');
            }
        });
    });

    // Set initial active tab based on URL hash or default to profile
    var initialTab = window.location.hash.substring(1) || 'profile-tab';
    $('.tab-link[data-tab="' + initialTab + '"]').click();
});