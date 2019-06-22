var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        // Tiếp tục mua hàng
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        // Cập nhập giỏ hàng
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        MaSP: $(item).data('id')
                    }
                });
            });

            $.ajax({
                url: '/cart/update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true)
                    {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        // Xóa giỏ hàng
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        // Thanh toán
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });

        // Xóa từng sản phẩm trong đơn hàng
        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/cart/delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
    }
}

cart.init();