let bookCopies = [];

$(document).ready(function () {

    $('.customer-select').select2({
        placeholder: "Search for a customer",
        allowClear: true
    });

    $('#modalOriginalBookId').select2({
        dropdownParent: $('#addBookCopyModal')
    });

    $('#modalPublisherId').select2({
        dropdownParent: $('#addBookCopyModal')
    });

    $('#modalOriginalBookId').on('change', function () {
        populatePublishers();
    });

});
function checkAvailability() {
    const originalBookId = document.getElementById('modalOriginalBookId').value;
    const edition = document.getElementById('modalEdition').value;
    const publisherId = document.getElementById('modalPublisherId').value;
    const quantity = document.getElementById('modalQuantity').value;

    if (originalBookId && edition && publisherId && quantity) {
        $.ajax({
            url: '/Reservation/CheckBookCopyAvailability',
            type: 'GET',
            data: {
                originalBookId: originalBookId,
                edition: edition,
                publisherId: publisherId,
                quantity: quantity
            },
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
    const statusElement = document.getElementById('availabilityStatus');
    statusElement.textContent = message;
    statusElement.className = isAvailable ? 'text-success' : 'text-danger';
}


$('#modalOriginalBookId, #modalEdition, #modalPublisherId, #modalQuantity').on('change', checkAvailability);



function populatePublishers() {
    var selectedBookId = $("#modalOriginalBookId").val();
    if (selectedBookId) {
        $.ajax({
            url: '/BookCopy/GetPublishersUrl',
            type: 'GET',
            success: function (getPublishersUrl) {
                $.ajax({
                    url: getPublishersUrl,
                    data: { bookId: selectedBookId },
                    type: 'GET',
                    success: function (data) {
                        var publisherSelect = $("#modalPublisherId");
                        publisherSelect.empty();
                        publisherSelect.prop("disabled", false);
                        publisherSelect.append($("<option>").val("").text("Select a publisher"));
                        $.each(data, function (index, publisher) {
                            publisherSelect.append($("<option>").val(publisher.publisherId).text(publisher.publisherName));
                        });
                    },
                    error: function () {
                        alert("Error occurred while fetching publishers.");
                    }
                });
            },
            error: function () {
                alert("Error occurred while fetching the publishers URL.");
            }
        });
    } else {

    }
}

function addBookCopy() {
    const bookCopy = {
        OriginalBookId: document.getElementById('modalOriginalBookId').value,
        OriginalBookTitle: $("#modalOriginalBookId option:selected").text(),
        Edition: document.getElementById('modalEdition').value,
        PublisherId: document.getElementById('modalPublisherId').value,
        PublisherName: $("#modalPublisherId option:selected").text(),
        Quantity: document.getElementById('modalQuantity').value
    };

    bookCopies.push(bookCopy);
    updateBookCopySummary();
    $('#addBookCopyModal').modal('hide');
    clearModalInputs();
}

function updateBookCopySummary() {
    const summary = document.getElementById('bookCopySummary');
    summary.innerHTML = '';
    bookCopies.forEach((copy, index) => {
        const copyElement = document.createElement('div');
        copyElement.className = 'border p-2 mb-2';
        copyElement.innerHTML = `
                    <strong>Book ${index + 1}:</strong> ${copy.OriginalBookTitle}, Edition: ${copy.Edition}, Publisher: ${copy.PublisherName}, Quantity: ${copy.Quantity}
                    <button type="button" class="btn btn-danger btn-sm float-right" onclick="removeBookCopy(${index})">Remove</button>
                    <input type="hidden" name="BookCopyReservations[${index}].OriginalBookId" value="${copy.OriginalBookId}" />
                    <input type="hidden" name="BookCopyReservations[${index}].Edition" value="${copy.Edition}" />
                    <input type="hidden" name="BookCopyReservations[${index}].PublisherId" value="${copy.PublisherId}" />
                    <input type="hidden" name="BookCopyReservations[${index}].Quantity" value="${copy.Quantity}" />
                `;
        summary.appendChild(copyElement);
    });
}

function removeBookCopy(index) {
    bookCopies.splice(index, 1);
    updateBookCopySummary();
}

function clearModalInputs() {
    $('#modalOriginalBookId').val('').trigger('change');
    document.getElementById('modalEdition').value = '';
    $('#modalPublisherId').val('').prop('disabled', true);
    document.getElementById('modalQuantity').value = '';
}