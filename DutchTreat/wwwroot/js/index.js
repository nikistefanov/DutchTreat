$(document).ready(function() {
    var $theForm = $("#theForm");
    var $buyButton = $("#buyButton");
    var $productInfo = $(".product-props li");

    $theForm.hide();

    $buyButton.on("click", function () {
        $theForm.fadeToggle(400);
    });

    $productInfo.on("click", function () {
        console.log($(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $("#popupForm");

    $loginToggle.on("click", function () {
        $popupForm.slideToggle(400);
    });
});