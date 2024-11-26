$(document).ready(function () {
    // Xử lý preview hình ảnh
    $("#wallpaper").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#preview').attr('src', e.target.result);
            }
            reader.readAsDataURL(this.files[0]);
        }
    });

    // Tự động tạo Meta từ Name
    $("#Name").on('input', function () {
        var name = $(this).val();
        var meta = name.toLowerCase()
            .replace(/\s+/g, '-')           // Thay khoảng trắng bằng dấu gạch
            .replace(/[áàảãạâấầẩẫậăắằẳẵặ]/g, 'a')
            .replace(/[éèẻẽẹêếềểễệ]/g, 'e')
            .replace(/[íìỉĩị]/g, 'i')
            .replace(/[óòỏõọôốồổỗộơớờởỡợ]/g, 'o')
            .replace(/[úùủũụưứừửữự]/g, 'u')
            .replace(/[ýỳỷỹỵ]/g, 'y')
            .replace(/đ/g, 'd')
            .replace(/[^a-z0-9-]/g, '')     // Xóa ký tự đặc biệt
            .replace(/-+/g, '-');           // Xóa dấu gạch liên tiếp
        $("#Meta").val(meta);
    });
});
