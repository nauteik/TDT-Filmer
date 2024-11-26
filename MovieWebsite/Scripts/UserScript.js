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
        
        // Get tab name and show corresponding content
        var tabId = $(this).data('tab');
        $('#' + tabId).show();

        // Update hero content based on tab
        var heroTitle = '';
        var breadcrumb = '';
        
        switch(tabId) {
            case 'profile-tab':
                heroTitle = "Profile";
                breadcrumb = "Profile";
                break;
            case 'password-tab':
                heroTitle = "Change Password";
                breadcrumb = "Change Password";
                break;
            // Add more cases for other tabs if needed
        }

        // Update hero title and breadcrumb
        $('.hero-ct h1').text(heroTitle);
        $('.breadcumb li:last-child').html('<span class="ion-ios-arrow-right"></span>' + breadcrumb);
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

    // Click handler for the Change Avatar button
    $('#changeAvatarBtn').click(function(e) {
        e.preventDefault();
        $('#avatarFile').click();
    });

    // File input change handler
    $('#avatarFile').change(function() {
        var file = this.files[0];
        if (file) {
            // Validate file type
            if (!file.type.match('image.*')) {
                toastr.error('Please select an image file');
                return;
            }

            // Validate file size (max 5MB)
            if (file.size > 5 * 1024 * 1024) {
                toastr.error('File size should not exceed 5MB');
                return;
            }

            // Show loading state
            $('#changeAvatarBtn').text('Uploading...');

            // Create FormData and append file
            var formData = new FormData();
            formData.append('avatarFile', file);

            // Show preview immediately
            var reader = new FileReader();
            reader.onload = function(e) {
                $('#avatarPreview').attr('src', e.target.result);
            };
            reader.readAsDataURL(file);

            // Upload file
            $.ajax({
                url: '/Account/ChangeAvatar',
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function(response) {
                    if (response.success) {
                        toastr.success('Avatar updated successfully');
                        // Update avatar in header if exists
                        $('.user-avatar').attr('src', '/Content/images/uploads/' + response.avatarFileName);
                    } else {
                        toastr.error(response.message || 'Failed to update avatar');
                        // Revert preview if failed
                        $('#avatarPreview').attr('src', '/Content/images/uploads/@(Model.Avatar ?? "defaultavatar.png")');
                    }
                },
                error: function() {
                    toastr.error('An error occurred while updating avatar');
                    // Revert preview
                    $('#avatarPreview').attr('src', '/Content/images/uploads/@(Model.Avatar ?? "defaultavatar.png")');
                },
                complete: function() {
                    // Reset button text
                    $('#changeAvatarBtn').text('Change avatar');
                }
            });
        }
    });
});