$(document).ready(function() {
    // Cấu hình Toastr
    

    $('#commentForm').on('submit', function(e) {
        e.preventDefault();
        
        var $form = $(this);
        var data = {
            newId: $form.find('input[name="newId"]').val(),
            userId: $form.find('input[name="userId"]').val(),
            content: $form.find('textarea[name="content"]').val()
        };

        // Validate content
        if (!data.content.trim()) {
            toastr.warning('Please enter your comment');
            return;
        }
        
        $.ajax({
            url: '/New/AddComment',
            type: 'POST',
            data: data,
            success: function(response) {
                if (response.success) {
                    // Thêm comment mới vào UI
                    var newComment = `
                        <div class="cmt-item flex-it">
                            <img style="width: 70px; height: 70px;" src="/Content/images/uploads/${response.comment.userAvatar}" alt="">
                            <div class="author-infor">
                                <div class="flex-it2">
                                    <h6><a href="#">${response.comment.userFullName}</a></h6>
                                    <span class="time"> - ${response.comment.initDate}</span>
                                </div>
                                <p class="comment-content">${response.comment.content}</p>
                                <p><a class="rep-btn" href="#">+ Reply</a></p>
                            </div>
                        </div>
                    `;
                    
                    // Thêm comment vào cuối danh sách comments
                    $('.comments').append(newComment);
                    
                    // Cập nhật số lượng comments
                    var currentCount = parseInt($('.comments h4').text());
                    $('.comments h4').text((currentCount + 1).toString().padStart(2, '0') + ' Comments');
                    
                    // Clear form
                    $form.find('textarea[name="content"]').val('');

                    // Hiển thị toast success
                    toastr.success('Your comment has been added successfully');
                } else {
                    // Hiển thị toast error với message từ server
                    toastr.error(response.message || 'Failed to add comment');
                }
            },
            error: function(xhr, status, error) {
                console.log('Error details:', {
                    status: status,
                    error: error,
                    response: xhr.responseText
                });
                // Hiển thị toast error
                toastr.error('An error occurred. Please try again.');
            }
        });
    });
}); 