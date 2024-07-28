$(document).ready(function () {
    initializeSelectFields();

    $(document).on('click', '[class^="create-new-"]', function (e) {
        e.preventDefault();
        e.stopPropagation();
        window.location.href = $(this).attr('href');
    });
});

function initializeSelectFields() {
    initializeSelect('#authors', '/Author/GetAuthorsForDropDown');
    initializeSelect('#publishers', '/Publisher/GetPublishersForDropDown');
    initializeSelect('#categories', '/Category/GetCategoriesForDropDown');
}

function initializeSelect(selectElement, dataUrl) {
    $.getJSON(dataUrl)
         .done(function (data) {
            const options = data.map(item => ({
                id: item.id,
                text: item.name
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
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error(`Error fetching data for ${selectElement}:`, textStatus, errorThrown);
        });
}