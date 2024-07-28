$(document).ready(function () {
    initializeSelectFields();
});

function initializeSelectFields() {
    const bookId = $('#bookId').val();

    initializeSelect('#authors', '/Author/GetAuthorsForDropDown', `/Author/GetBookAuthors/${bookId}`);
    initializeSelect('#publishers', '/Publisher/GetPublishersForDropDown', `/Publisher/GetBookPublishers/${bookId}`);
    initializeSelect('#categories', '/Category/GetCategoriesForDropDown', `/Category/GetBookCategories/${bookId}`);

    $(document).on('click', '[class^="create-new-"]', function (e) {
        e.preventDefault();
        e.stopPropagation();
        window.location.href = $(this).attr('href');
    });
}

function initializeSelect(selectElement, allDataUrl, selectedDataUrl) {
    $.when(
        $.getJSON(allDataUrl),
        $.getJSON(selectedDataUrl)
    ).done(function (allDataResponse, selectedDataResponse) {

        const allData = allDataResponse[0];
        const selectedIds = selectedDataResponse[0];
        const options = allData.map(item => ({
            id: item.id,
            text: item.name,
            selected: selectedIds.includes(item.id)
        }));

        $(selectElement).select2({
            data: options,
            placeholder: $(selectElement).data('placeholder'),
            allowClear: true,
            multiple: true,
            escapeMarkup: function (markup) {
                return markup;
            },
            language: {
                noResults: function () {
                    const createUrl = $(selectElement).data('create-url');
                    const entityName = $(selectElement).data('entity-name');
                    return `No results found. <a href="${createUrl}" class="create-new-${entityName.toLowerCase()}">Create new ${entityName}</a>`;
                }
            }
        });
        $(selectElement).trigger('change');
    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.error("Error fetching data:", textStatus, errorThrown);
    });
}