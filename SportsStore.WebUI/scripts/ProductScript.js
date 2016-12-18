$(document).ready(function () {
        $(".productQuantity").each(function () {
            
            var count = parseInt($(this).text());
            var prodQParant = $(this).parents(".prodQ");
            var button = prodQParant.next().find(".addToCart").css({ "color": "red" });

            if (count > 0) {
                $(this).prev().text("На складе ");
                button.removeClass("disabled").prop('disable', false).attr("type", "submit");
                prodQParant.css("color", "green");
            }
            else {
                prodQParant.css("color", "orange");
                $(this).prev().text("Нет в наличии ");
                button.addClass("disabled").prop('disable', true).attr("type", "button");
            }
        })


})