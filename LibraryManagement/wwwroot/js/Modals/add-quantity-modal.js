function initializeAddQuantityModal() {
    $('#addQuantityModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var modal = $(this);

        modal.find('#bookTitle').text(button.data('book-title'));
        modal.find('#SelectedBookId').val(button.data('book-id'));
        modal.find('#SelectedPublisherId').val(button.data('publisher-id'));
        modal.find('#SelectedRoomId').val(button.data('room-id'));
        modal.find('#SelectedShelfId').val(button.data('shelf-id'));
        modal.find('#NumberOfPages').val(button.data('number-of-pages'));
        modal.find('#Edition').val(button.data('edition'));
        modal.find('#Status').val(button.data('status'));

        // Reset quantity field
        modal.find('#Quantity').val(1);
    });
}