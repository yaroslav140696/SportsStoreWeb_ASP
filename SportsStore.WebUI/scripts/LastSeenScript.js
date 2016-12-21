$(document).ready(function () {
   var id = $('#productID').val();
   $.ajax({
       url: 'http://localhost:18375/Product/AddToLastSeenProduct',
       data: { itemID: id },
       success: function () {
           alert('success');
       },
       error: function (xhr) {
           alert(xhr.statusText);
       }
   })
})