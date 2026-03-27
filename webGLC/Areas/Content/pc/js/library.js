$(function () {
    if ($('#video-element').length) {
        play_video('video-element', $('#video-element').data('file'), $('#video-element').data('image'), '100%', 312, true);
    }

    $('[data-toggle="datepicker"]').datetimepicker({
        timepicker: false,
        //format: 'm/d/Y H:i',
        format: 'd/m/Y',
        //startDate: new Date()
    });

    $('#slide img').height($(window).height() - $('#header').height());

    $('.slide-home').owlCarousel({
        animateOut: 'slideOutDown',
        animateIn: 'flipInX',
        autoplay: true,
        autoplayTimeout: 5000,
        autoplayHoverPause: true,
        items: 1,
        lazyLoad: true,
        loop: true,
        margin: 0
    });
    $('.slide-kh').owlCarousel({
        animateOut: 'slideOutDown',
        animateIn: 'flipInX',
        loop: true,
        margin: 10,
        nav: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            }
        }
    });
    $('.slide-gt').owlCarousel({
        animateOut: 'slideOutDown',
        animateIn: 'flipInX',
        loop: true,
        margin: 10,
        nav: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            }
        }
    });

    //$('.slide-kh').owlCarousel({
    //    items: 4,
    //    itemsDesktop: [1199, 4],
    //    itemsDesktopSmall: [979, 3],
    //    itemsTablet: [768, 2],
    //    itemsTabletSmall: false,
    //    itemsMobile: [479, 1],
    //    autoPlay: true,
    //    responsive: true,
    //    loop: true
    //});
    //$('.slide-gt').owlCarousel({
    //    items: 4,
    //    itemsDesktop: [1199, 4],
    //    itemsDesktopSmall: [979, 3],
    //    itemsTablet: [768, 2],
    //    itemsTabletSmall: false,
    //    itemsMobile: [479, 1],
    //    autoPlay: true,
    //    responsive: true,
    //    loop: true
    //});
})

function del_scf(id) {
    location.href = '/lenh-online/DelSCFPOST.html?id=' + id;
}

function add_cart(id, returnpath) {
    location.href = '/gio-hang/Add.html?ProductID=' + id + '&Quantity=1&returnpath=' + returnpath;
}

function update_cart(index, quantity, returnpath) {
    location.href = '/gio-hang/Update.html?Index=' + index + '&Quantity=' + quantity + '&returnpath=' + returnpath;
}

function delete_cart(index) {
    location.href = '/gio-hang/Delete.html?Index=' + index;
}

function change_captcha() {
    var e = Math.floor(Math.random() * 999999); document.getElementById("imgValidCode").src = "/ajax/Security.html?Code=" + e
}