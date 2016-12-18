$(document).ready(function () {
    $(".orderDetails").click(function () {
        var id = parseInt($(this).val());
        $.ajax({
            url: 'http://localhost:18375/Order/DetailInfoAboutOrder',
            dataType: 'html',
            type: 'GET',
            data: { orderID : id },
            success: function (data) {
                $('#orderExtension').html(data);
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        })
    })
})