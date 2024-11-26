$(document).ready(function () {
    // Lưu text gốc của button
    const originalText = $('.btn-login').html();

    // Xử lý sự kiện submit form
    $('form').on('submit', function () {
        const $loginBtn = $('.btn-login');
        
        // Kiểm tra form hợp lệ
        if ($(this).valid()) {
            // Disable button và hiện loading
            $loginBtn.prop('disabled', true)
                    .html('<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>Đang xử lý...');
        }
        
        // Sau 30 giây nếu không có phản hồi, reset button
        setTimeout(function() {
            $loginBtn.prop('disabled', false).html(originalText);
        }, 30000);
    });
});