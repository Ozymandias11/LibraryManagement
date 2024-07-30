let bookCopies = [];

$(document).ready(function () {
    initializeSelects();
    setupEventHandlers();
    updateBookCopySummary();
});

function initializeSelects() {
    loadAndInitializeSelect('#CustomerID', '/Customer/GetCustomersForDropDown', false, null, {
        createUrl: '/Customer/CreateCustomer',
        entityName: 'Customer'
    });

    loadAndInitializeSelect('#modalOriginalBookId', '/Book/GetBooksForDropDown', false, populatePublishers, null, {
        placeholder: "Select a book",
        allowClear: true
    });

    $('#modalPublisherId').select2({
        placeholder: "Select a publisher",
        allowClear: true
    }).prop('disabled', true);
}

function populatePublishers() {
    var bookId = $('#modalOriginalBookId').val();
    console.log(`Book selected: ${bookId}`);
    if (bookId) {
        loadAndInitializeSelect('#modalPublisherId', `/Publisher/GetBookPublishersForSelect2/${bookId}`, false);
        $('#modalPublisherId').prop('disabled', false);
    } else {
        $('#modalPublisherId').empty().prop('disabled', true).append($('<option></option>').val('').text('Select a publisher'));
    }
}

function setupEventHandlers() {
    $('#reservationForm').on('submit', validateForm);
    $('#modalOriginalBookId, #modalEdition, #modalPublisherId, #modalQuantity').on('change', checkAvailability);
    $('#addBookCopyBtn').on('click', addBookCopy);
}

function validateForm(e) {
    if (!hasBookCopies()) {
        e.preventDefault();
        alert('Please add at least one book copy to the reservation before submitting.');
        return false;
    }
    return true;
}

function hasBookCopies() {
    return bookCopies.length > 0;
}

function checkAvailability() {
    const originalBookId = $('#modalOriginalBookId').val();
    const edition = $('#modalEdition').val();
    const publisherId = $('#modalPublisherId').val();
    const quantity = $('#modalQuantity').val();

    if (originalBookId && edition && publisherId && quantity) {
        $.ajax({
            url: '/Reservation/CheckBookCopyAvailability',
            type: 'GET',
            data: { originalBookId, edition, publisherId, quantity },
            success: function (response) {
                updateAvailabilityStatus(response.isAvailable, response.message);
            },
            error: function () {
                alert("Error occurred while checking availability.");
            }
        });
    }
}

function updateAvailabilityStatus(isAvailable, message) {
    const statusElement = $('#availabilityStatus');
    statusElement.text(message);
    statusElement.removeClass('text-success text-danger').addClass(isAvailable ? 'text-success' : 'text-danger');
}

function addBookCopy() {
    const originalBookId = $('#modalOriginalBookId').val();
    const edition = $('#modalEdition').val();
    const publisherId = $('#modalPublisherId').val();
    const quantity = $('#modalQuantity').val();

    if (!originalBookId || !edition || !publisherId || !quantity) {
        alert('Please fill in all fields before adding a book copy.');
        return;
    }

    const newBookCopy = {
        OriginalBookId: originalBookId,
        OriginalBookTitle: $("#modalOriginalBookId option:selected").text(),
        Edition: edition,
        PublisherId: publisherId,
        PublisherName: $("#modalPublisherId option:selected").text(),
        Quantity: parseInt(quantity)
    };

    bookCopies.push(newBookCopy);
    updateBookCopySummary();
    clearModalInputs();
    $('#addBookCopyModal').modal('hide');
}

function updateBookCopySummary() {
    const summary = $('#bookCopySummary');
    summary.empty();
    bookCopies.forEach((copy, index) => {
        const copyElement = $('<div>').addClass('border p-2 mb-2').html(`
            <strong>Book ${index + 1}:</strong> ${copy.OriginalBookTitle}, Edition: ${copy.Edition}, Publisher: ${copy.PublisherName}, Quantity: ${copy.Quantity}
            <button type="button" class="btn btn-danger btn-sm float-right" onclick="removeBookCopy(${index})">Remove</button>
            <input type="hidden" name="BookCopyReservations[${index}].OriginalBookId" value="${copy.OriginalBookId}" />
            <input type="hidden" name="BookCopyReservations[${index}].Edition" value="${copy.Edition}" />
            <input type="hidden" name="BookCopyReservations[${index}].PublisherId" value="${copy.PublisherId}" />
            <input type="hidden" name="BookCopyReservations[${index}].Quantity" value="${copy.Quantity}" />
        `);
        summary.append(copyElement);
    });
}

function removeBookCopy(index) {
    bookCopies.splice(index, 1);
    updateBookCopySummary();
}

function clearModalInputs() {
    $('#modalOriginalBookId').val(null).trigger('change');
    $('#modalEdition').val('');
    $('#modalPublisherId').val(null).trigger('change').prop('disabled', true);
    $('#modalQuantity').val('');
    $('#availabilityStatus').text('').removeClass('text-success text-danger');
}