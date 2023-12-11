/* Set values + misc */
var promoCode; // mã giảm giá nếu có
var promoPrice; // giá trị giảm giá
var fadeTime = 300; // thời gian hiệu ứng

/* Thực thi */
/*Sự kiện change được gán cho các trường nhập số lượng (input) của sản phẩm trong giỏ hàng, và khi có sự thay đổi,
 chúng ta gọi hàm updateQuantity() để cập nhật số lượng và tính toán lại tổng giá trị giỏ hàng.*/
$('.quantity input').change(function () {
    updateQuantity(this);
});
/*Sự kiện click được gán cho các nút "remove" trong giỏ hàng, 
và khi được nhấp vào, chúng ta gọi hàm removeItem() 
để xóa sản phẩm khỏi giỏ hàng và tính toán lại tổng giá trị giỏ hàng.*/
$('.remove button').click(function () {
    removeItem(this);
});
/*Sự kiện ready được gán cho tài liệu (document), và khi trang web được tải hoàn chỉnh, chúng ta gọi hàm updateSumItems() để cập nhật tổng số sản phẩm trong giỏ hàng.
 */
$(document).ready(function () {
    updateSumItems();
});
/*Sự kiện click được gán cho nút "promo-code-cta" (button kích hoạt mã giảm giá), và khi được nhấp vào, chúng ta xử lý mã giảm giá.*/
$('.promo-code-cta').click(function () {
    promoCode = $('#promo-code').val();

    if (promoCode == '10off' || promoCode == '10OFF') {
        if (!promoPrice) {
            promoPrice = 10;
        } else if (promoCode) {
            promoPrice = promoPrice * 1;
        }
    } else if (promoCode != '') {
        alert('Invalid Promo Code');
        promoPrice = 0;
    }
    if (promoPrice) {
        $('.summary-promo').removeClass('hide');
        $('.promo-value').text(promoPrice.toFixed(2));
        recalculateCart(true);
    }
});

function recalculateCart(onlyTotal) {
    var subtotal = 0;

    $('.basket-product').each(function () {
        subtotal += parseFloat($(this).children('.subtotal').text());
    });

    /* Calculate totals */
    var total = subtotal;

    var promoPrice = parseFloat($('.promo-value').text());
    if (promoPrice) {
        if (subtotal >= 10) {
            total -= promoPrice;
        } else {
            alert('Đơn đặt hàng phải trên $10 để áp dụng Mã khuyến mại.');
            $('.summary-promo').addClass('hide');
        }
    }

    if (onlyTotal) {
        //Cập nhật tổng tiền hiển thị
        $('.total-value').fadeOut(fadeTime, function () {
            $('#basket-total').html(total.toFixed(2));
            $('.total-value').fadeIn(fadeTime);
        });
    } else {
        //Cập nhật tóm tắt
        $('.final-value').fadeOut(fadeTime, function () {
            $('#basket-subtotal').html(subtotal.toFixed(2));
            $('#basket-total').html(total.toFixed(2));
            if (total == 0) {
                $('.checkout-cta').fadeOut(fadeTime);
            } else {
                $('.checkout-cta').fadeIn(fadeTime);
            }
            $('.final-value').fadeIn(fadeTime);
        });
    }
}

//Cập nhật số lượng
function updateQuantity(quantityInput) {
    //Tính toán giá trị trên dòng
    var productRow = $(quantityInput).parent().parent();
    var price = parseFloat(productRow.children('.price').text());
    var quantity = $(quantityInput).val();
    var linePrice = price * quantity;

    //Cập nhật hiển thị dòng giá trị trên dòng và tính toán lại tổng giỏ hàng
    productRow.children('.subtotal').each(function () {
        $(this).fadeOut(fadeTime, function () {
            $(this).text(linePrice.toFixed(2));
            recalculateCart();
            $(this).fadeIn(fadeTime);
        });
    });

    productRow.find('.item-quantity').text(quantity);
    updateSumItems();
}
// Cập nhật tổng số sản phẩm trong giỏ hàng
function updateSumItems() {
    var sumItems = 0;
    $('.quantity input').each(function () {
        sumItems += parseInt($(this).val());
    });
    $('.total-items').text(sumItems);
}

// Xóa sản phẩm khỏi giỏ hàng
function removeItem(removeButton) {
    /*Xóa khỏi hàng DOM và tính lại tổng giỏ hàng */
    var productRow = $(removeButton).parent().parent();
    productRow.slideUp(fadeTime, function () {
        productRow.remove();
        recalculateCart();
        updateSumItems();
    });
}
