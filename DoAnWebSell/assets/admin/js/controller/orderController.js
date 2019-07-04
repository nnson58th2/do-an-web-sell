var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-changeStatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/dondathang/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == 2) {
                        btn.text("Đã xác nhận");
                    }
                }
            })
        });
    }
}

user.init();