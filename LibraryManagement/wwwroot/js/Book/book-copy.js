$(document).ready(function () {
    initializeSelects();
});

function initializeSelects() {
    const selectFields = [
        { selector: '#bookSelect', endpoint: '/Book/GetBooksForDropDown', callback: populatePublishers },
        { selector: '#roomSelect', endpoint: '/Room/GetRoomsForDropDown', callback: populateShelves }
    ];

    selectFields.forEach(field => {
        loadAndInitializeSelect(field.selector, createLocalizedUrl(field.endpoint), false, field.callback);
    });

    $('#publisherSelect, #shelfSelect').prop('disabled', true);
}

function populatePublishers() {
    var bookId = $('#bookSelect').val();
    console.log(`Book selected: ${bookId}`);
    if (bookId) {
        loadAndInitializeSelect('#publisherSelect', createLocalizedUrl(`/Publisher/GetBookPublishersForSelect2/${bookId}`), false);
        $('#publisherSelect').prop('disabled', false);
    } else {
        $('#publisherSelect').empty().prop('disabled', true).append($('<option></option>').val('').text('Select a publisher'));
    }
}

function populateShelves() {
    var roomId = $('#roomSelect').val();
    console.log(`Room selected: ${roomId}`);
    if (roomId) {
        loadAndInitializeSelect('#shelfSelect', createLocalizedUrl(`/Shelf/GetRoomShelvesForSelect2/${roomId}`), false);
        $('#shelfSelect').prop('disabled', false);
    } else {
        $('#shelfSelect').empty().prop('disabled', true).append($('<option></option>').val('').text('Select a shelf'));
    }
}