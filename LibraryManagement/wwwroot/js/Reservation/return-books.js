$(document).ready(function () {
    initializeCustomerSelect();
    populateCustomers();
    setupEventListeners();
});

function initializeCustomerSelect() {
    $('#customerSelect').select2({
        placeholder: "Search for a customer",
        allowClear: true,
        dropdownParent: $('#returnBookModal'),
        language: {
            noResults: function () {
                return "No results found. <a href='/Customer/CreateCustomer' class='create-customer-link'>Create New Customer</a>";
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    }).on('select2:open', function () {
        $('.select2-results__options').on('click', '.create-customer-link', function (e) {
            e.preventDefault();
            e.stopPropagation();
            window.location.href = $(this).attr('href');
        });
    });
}

function populateCustomers() {
    $.ajax({
        url: '/Reservation/GetAllCustomers',
        type: 'GET',
        success: function (data) {
            var customerSelect = $("#customerSelect");
            customerSelect.empty().append($("<option>").val("").text("Select a customer"));
            $.each(data, function (index, customer) {
                customerSelect.append($("<option>")
                    .val(customer.id)
                    .text(`${customer.customerPersonalId} - ${customer.firstName} ${customer.lastName}`));
            });
            customerSelect.trigger('change');
        },
        error: function () {
            alert("Error occurred while fetching customers.");
        }
    });
}

function setupEventListeners() {
    $('.return-book').click(handleReturnBookClick);
    $('#addReturnAction').click(addReturnActionRow);
    $(document).on('click', '.remove-action', removeActionRow);
    $(document).on('change', '.return-quantity', validateReturnQuantity);
    $('#submitReturn').click(submitReturn);
}

function handleReturnBookClick() {
    var button = $(this);
    $('#reservationItemId').val(button.data('reservation-item-id'));
    $('#bookTitle').val(button.data('book-title'));
    $('#edition').val(button.data('edition'));
    $('#publisherName').val(button.data('publisher-name'));
    $('#remainingQuantity').val(button.data('remaining-quantity'));

    // Reset form
    $('#customerSelect').val(null).trigger('change');
    $('#returnActionsTable tbody').empty();
    addReturnActionRow();
}

function addReturnActionRow() {
    var row = `
        <tr>
            <td>
                <select class="form-control return-status">
                    <option value="Safe">Safe</option>
                    <option value="Damaged">Damaged</option>
                    <option value="Lost">Lost</option>
                </select>
            </td>
            <td>
                <input type="number" class="form-control return-quantity" min="1" max="${$('#remainingQuantity').val()}" value="1">
            </td>
            <td>
                <button type="button" class="btn btn-danger btn-sm remove-action">Remove</button>
            </td>
        </tr>
    `;
    $('#returnActionsTable tbody').append(row);
}

function removeActionRow() {
    $(this).closest('tr').remove();
}

function validateReturnQuantity() {
    var totalQuantity = 0;
    $('.return-quantity').each(function () {
        totalQuantity += parseInt($(this).val()) || 0;
    });
    if (totalQuantity > parseInt($('#remainingQuantity').val())) {
        alert('Total quantity exceeds remaining quantity');
        $(this).val(1);
    }
}

function submitReturn() {
    var returnItems = [];
    $('#returnActionsTable tbody tr').each(function () {
        returnItems.push({
            ReturnStatus: $(this).find('.return-status').val(),
            Quantity: parseInt($(this).find('.return-quantity').val())
        });
    });

    var model = {
        ReservationId: $('#reservationId').val(),
        CustomerId: $('#customerSelect').val(),
        ReturnItems: returnItems
    };

    $.ajax({
        url: '/Reservation/ReturnBook',
        type: 'POST',
        data: JSON.stringify(model),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $('#returnBookModal').modal('hide');
                location.reload();
            } else {
                alert('Error: ' + response.message);
            }
        },
        error: function () {
            alert('An error occurred while returning the book.');
        }
    });
}