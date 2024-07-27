﻿$(document).ready(function () {
    const bookId = $('#bookId').val();

    loadAndInitializeSelect('#authors', '/Author/GetAuthorsForDropDown', '/Author/GetBookAuthors/' + bookId);
    loadAndInitializeSelect('#publishers', '/Publisher/GetPublishersForDropDown', '/Publisher/GetBookPublishers/' + bookId);
    loadAndInitializeSelect('#categories', '/Category/GetCategoriesForDropDown', '/Category/GetBookCategories/' + bookId);

    
    $(document).on('click', '[class^="create-new-"]', function (e) {
        e.preventDefault();
        e.stopPropagation();
        window.location.href = $(this).attr('href');
    });
});

function loadAndInitializeSelect(selectElement, allDataUrl, selectedDataUrl) {
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
    });
}