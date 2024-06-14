//$(document).ready(function () {
//    $('#deleteModal').on('show.bs.modal', function (event) {
//        var button = $(event.relatedTarget); // Button that triggered the modal
//        var userId = button.data('user-id'); // Extract info from data-* attributes
//        var userEmail = button.data('user-email'); // Extract info from data-* attributes
//        var modal = $(this);
//        modal.find('#userId').val(userId);
//        modal.find('#userEmail').text(userEmail);
//    });
//});


// wwwroot/authorModal.js
$(document).ready(function () {
    $('#deleteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget)
        var authorId = button.data('author-id')
        var authorName = button.data('author-name')
        var modal = $(this)
        modal.find('#authorId').val(authorId)
        modal.find('#authorName').text(authorName)
    })
});