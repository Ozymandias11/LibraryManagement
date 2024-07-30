$(document).ready(function () {
    initializeSelects();
});

function initializeSelects() {
    loadAndInitializeSelect('#bookSelect', '/Book/GetBooksForDropDown', false, populatePublishers);
    loadAndInitializeSelect('#roomSelect', '/Room/GetRoomsForDropDown', false, populateShelves);
    $('#publisherSelect, #shelfSelect').prop('disabled', true);
}

function populatePublishers() {
    var bookId = $('#bookSelect').val();
    console.log(`Book selected: ${bookId}`);
    if (bookId) {
        loadAndInitializeSelect('#publisherSelect', `/Publisher/GetBookPublishersForSelect2/${bookId}`, false);
        $('#publisherSelect').prop('disabled', false);
    } else {
        $('#publisherSelect').empty().prop('disabled', true).append($('<option></option>').val('').text('Select a publisher'));
    }
}

function populateShelves() {
    var roomId = $('#roomSelect').val();
    console.log(`Room selected: ${roomId}`);
    if (roomId) {
        loadAndInitializeSelect('#shelfSelect', `/Shelf/GetRoomShelvesForSelect2/${roomId}`, false);
        $('#shelfSelect').prop('disabled', false);
    } else {
        $('#shelfSelect').empty().prop('disabled', true).append($('<option></option>').val('').text('Select a shelf'));
    }
}