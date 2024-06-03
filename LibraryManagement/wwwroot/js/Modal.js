   $('#deleteModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var userId = button.data('user-id'); // Extract info from data-* attributes
        var userEmail = button.data('user-email'); // Extract info from data-* attributes

        var modal = $(this);
        modal.find('#userId').val(userId);
        modal.find('#userEmail').text(userEmail);
    });

