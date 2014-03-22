//var navigation = responsiveNav(".blog-nav");
$(function () {
    $(".blog-sidebar").height($(".blog-main").height());
    $("#catalog").pin();

    $('.banner').unslider({
        complete: true, // when a slide's finished
        keys: true, // keyboard shortcuts - disable if it breaks things
        dots: false, // display ••••o• pagination
        fluid: false // is it a percentage width?,
    });
});