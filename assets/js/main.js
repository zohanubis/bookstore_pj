$('#header').prepend('<div id="menu-icon"><span class="first"></span><span class="second"></span><span class="third"></span></div>');

$('#menu-icon').on('click', function () {
    $('nav').slideToggle();
    $(this).toggleClass('active');
});

// Menu
// Lấy tất cả các nav-item trong navbar
var navItems = document.querySelectorAll('.navbar-nav .nav-item');

// Duyệt qua tất cả các nav-item
for (var i = 0; i < navItems.length; i++) {
    // Tìm đến menu-book trong nav-item hiện tại
    var menuBook = navItems[i].querySelector('.menu-book');

    // Nếu có menu-book trong nav-item hiện tại
    if (menuBook) {
        // Thêm sự kiện click vào nav-item hiện tại
        navItems[i].addEventListener('click', function (event) {
            // Ngăn chặn hành động mặc định của sự kiện click (điều hướng đến URL)
            event.preventDefault();

            // Toggle hiển thị của menu-book trong nav-item hiện tại
            menuBook.classList.toggle('show');
        });

        // Thêm sự kiện click vào trang để ẩn menu-book
        document.addEventListener('click', function (event) {
            // Nếu click không phải vào nav-item hoặc menu-book hiện tại
            if (!event.target.closest('.nav-item') || !event.target.closest('.menu-book')) {
                // Ẩn menu-book
                menuBook.classList.remove('show');
            }
        });
    }
}
const sr = ScrollReveal({
    distance: '40px',
    duration: 2050,
    delay: 200,
    reset: true,
});
sr.reveal('.container', { origin: 'top' });
