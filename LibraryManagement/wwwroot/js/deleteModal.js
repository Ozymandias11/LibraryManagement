

function initializeDeleteModal(idAttr, nameAttr) {
    console.log("initializeDeleteModal called");
    $('#deleteModal').on('show.bs.modal', function (event) {
        console.log("Modal show event triggered");
        var button = $(event.relatedTarget); // Button that triggered the modal
        var itemId = button.data(idAttr); // Extract info from data-* attributes
        var itemName = button.data(nameAttr);

        var modal = $(this);
        modal.find('.modal-body #itemName').text(itemName);
        modal.find('.modal-footer #itemId').val(itemId);
    });
}