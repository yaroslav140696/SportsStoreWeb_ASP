
$(document).ready(function () {
    $(".incrementQuantity").click(function () {
        var current = $(this);
        var button = $(this).find("button");
        var productID = parseInt(button.val());

        $.ajax({
            url: 'http://localhost:18375/Cart/IncrementQuantity',
            type: "GET",
            cashe: "false",
            dataType: 'json',
            data: {
                productID: productID
            },
            success: function (data) {

                current.nextAll(".productSummary:first").text(data.productsValue);

                $("#cartTotalCash").text(data.totalValue);
                
                var text = parseInt(current.next().text());

                if (data.quantity >= 0) {
                    text += 1;
                    current.next().text(text);
                    $("#totalQuantity").text(data.totalQuantity);
                }

                if (data.quantity === 0) {
                    button.addClass("disabled");
                    button.prop('disable', true);
                }

                current.nextAll(".decrementQuantity:first").find("button").removeClass("disabled").prop('disable', false);

            },
            error: function (xhr) {
                alert("error");
            }
        })
    })

    $(".decrementQuantity").click(function () {
        var current = $(this);
        var button = $(this).find("button");
        var productID = parseInt(button.val());
        var text = parseInt(current.prev().text());

        if (text === 1) {
            button.addClass("disabled").prop('disable', true);
        }
        else {
            $.ajax({
                url: 'http://localhost:18375/Cart/DecrementQuantity',
                type: "GET",
                cashe: "false",
                dataType: 'json',
                data: {
                    productID: productID
                },
                success: function (data) {
                    current.nextAll(".productSummary:first").text(data.productsValue);
                    $("#cartTotalCash").text(data.totalValue);
                    $("#totalQuantity").text(data.totalQuantity);
                    text -= 1;
                    current.prev().text(text);
                    current.prevAll(".incrementQuantity:first").find("button").removeClass("disabled").prop('disable', false);
                    button.removeClass("disabled").prop('disable', false);
                },
                error: function (xhr) {
                    alert("error");
                }
            })
        }
    })
})