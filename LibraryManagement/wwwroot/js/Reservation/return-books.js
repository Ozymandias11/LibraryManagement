$(document).ready(function () {
    initializeReturnBook();
});

function initializeReturnBook() {
    initializeCustomerSelect();
    populateCustomers();
    setupEventListeners();
}

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
        $('.select2-results__options').off('click', '.create-customer-link').on('click', '.create-customer-link', function (e) {
            e.preventDefault();
            e.stopPropagation();
            window.location.href = $(this).attr('href');
        });
    });
}

function populateCustomers() {
    $.ajax({
        url: '/Customer/GetCustomersForDropDown',
        type: 'GET',
        success: function (data) {
            var customerSelect = $("#customerSelect");
            customerSelect.empty();

            // Add placeholder option
            customerSelect.append($("<option>").val("").text("Search for a customer"));

            $.each(data, function (index, customer) {
                customerSelect.append($("<option>")
                    .val(customer.id)
                    .text(customer.name));
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
    $('#returnActionsTable').on('click', '.remove-action', removeActionRow);
    $('#returnActionsTable').on('change', '.return-quantity', validateReturnQuantity);
    $('#submitReturn').click(submitReturn);
}

function handleReturnBookClick() {
    var button = $(this);
    var data = button.data();
    $('#reservationItemId').val(data.reservationItemId);
    $('#bookTitle').val(data.bookTitle);
    $('#edition').val(data.edition);
    $('#publisherName').val(data.publisherName);
    $('#remainingQuantity').val(data.remainingQuantity);

    // Reset form
    $('#customerSelect').val(null).trigger('change');
    $('#returnActionsTable tbody').empty();
    addReturnActionRow();
}

function addReturnActionRow() {
    var remainingQuantity = $('#remainingQuantity').val();
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
                <input type="number" class="form-control return-quantity" min="1" max="${remainingQuantity}" value="1">
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
    var remainingQuantity = parseInt($('#remainingQuantity').val());
    $('.return-quantity').each(function () {
        totalQuantity += parseInt($(this).val()) || 0;
    });
    if (totalQuantity > remainingQuantity) {
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