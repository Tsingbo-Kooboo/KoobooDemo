$(function () {
    var $window = $(window)
    var $body = $(document.body)
    //$(".blog-sidebar").height($(".blog-main").height());
    //$("#catalog").pin();
    //$(".breadcrumb").pin();

    //固定右侧导航栏
    var $header = $(".blog-header");
    $('#catalog').affix({
        offset: {
            top: $header.outerHeight()
        }
    });
  
    $('.banner').unslider({
        complete: true,
        keys: true,
        dots: false,
        fluid: false
    });

    $('.dropdown-menu a').tooltip({
        placement: 'auto',
        container: 'body',
        delay: {
            show: 200,
            hide: 100
        }
    });
});
