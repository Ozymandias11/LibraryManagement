function initializeAddQuantityModal() {
    $('#addQuantityModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var modal = $(this);
        modal.find('#modifyQuantityModalLabel').text('Modify Book Copies: ' + button.data('book-title'));
        modal.find('#OriginalBookId').val(button.data('book-id'));
        modal.find('#PublisherId').val(button.data('publisher-id'));
        modal.find('#Edition').val(button.data('edition'));
        modal.find("#RoomId").val(button.data('room-id'));
        modal.find("#ShelfId").val(button.data("shelf-id"));
        modal.find("#BookCopyShelfId").val(button.data("book-shelf-id"));

        modal.find('#State').val('Added');
        modal.find('#QuantityModified').val(1);
        modal.find('#Message').val('');
    });
}