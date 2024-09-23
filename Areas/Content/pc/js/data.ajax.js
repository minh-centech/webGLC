var page = 1;
function paging_product(url) {
    $('.loading').show();

    //atr
    var checkbox = $('.right-property li input[type="checkbox"]:checked').map(function () {
        return this.value;
    }).get();
    var atr = checkbox.join('-');

    var sort = $('.filterTopCtgr').find('a.active').eq(0).data('value');
    url = url + '?page=' + page + '&sort=' + sort + '&atr=' + atr;
    console.log('paging:' + url);

    $.ajax({
        url: url,
        type: 'get',
        dataType: 'html',
        success: function (data) {
            data = $(data).find('.product-fs').html();
            data = $.trim(data);

            if (data == '')
                $('.pagination a').hide();
            
            $('.product-fs').append(data);
            $('.product-fs li').click(function () {
                location.href = $(this).data('url');
            })

            $('.loading').hide();
        },
        error: function () { }
    });
}

function checkout() {
    $('.loading').show();
    $.ajax({
        url: '/ajax/QuerySCF.html',
        type: 'POST',
        data: $("#query_form").serialize(),
        success: function (data) {
            var content = data.Html;
            var param = data.Params;

            if (content != '')
                location.href = '/dat-hang-thanh-cong.html?code=' + content;

            if (param != '')
                zebra_alert('Thông báo !', param);

            $('.loading').hide();
        },
        error: function () { }
    });
}

function search(keyword) {
    $.ajax({
        url: '/ajax/GetSearch.html',
        type: 'GET',
        data: 'Keyword=' + keyword,
        dataType: 'json',
        success: function (data) {
            var content = data.Html;

            if (content != '') {
                $('.resuiltSearch').html(content);
                $('.resuiltSearch').show();

                $(document).on('click', function (e) {
                    if ($(e.target).closest('.resuiltSearch').length === 0) {
                        $('.resuiltSearch').hide();
                    }
                });
            }
        },
        error: function () { }
    });
}