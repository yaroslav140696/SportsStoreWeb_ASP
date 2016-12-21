$(document).ready(function () {
   
    $('.removeWishListCart').click(function () {
        var id = $(this).val();
        var rem = $(this).parent().parent();
        $.ajax({
            url: 'http://localhost:18375/WishList/DeleteItem',
            data: {
                itemID: id
            },
            success: function () {
                alert('Товар удален из списка желаемых');
                rem.remove();
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        })
        
    })
})