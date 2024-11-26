$(document).ready(function () {
    // Xử lý switch Ẩn/Hiện
    $('#hideSwitch').change(function() {
        var isChecked = $(this).is(':checked');
        $('#hideStatus').text(isChecked ? "Đang ẩn" : "Đang hiện");
        // Gán giá trị 0/1 cho input hidden
        $(this).val(isChecked ? 1 : 0);
    });

    // Tự động tạo Meta từ Name
    $("#Name").on('input', function () {
        var name = $(this).val();
        var meta = name.toLowerCase()
            .replace(/[^\w\s-]/g, '')
            .replace(/\s+/g, '-')
            .replace(/--+/g, '-');
        $("#Meta").val(meta);
    });
});