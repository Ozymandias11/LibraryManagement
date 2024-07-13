$(document).ready(function () {
    initializeSelect2('#authors', 'Select authors', '/Author/CreateAuthor', 'Author');
    initializeSelect2('#publishers', 'Select Publishers', '/Publisher/CreatePublisher', 'Publisher');
    initializeSelect2('#Categories', 'Select Categories', '/Category/CreateCategory', 'Category');

    // Handle click on "Create new" links
    $(document).on('click', '[class^="create-new-"]', function (e) {
        e.preventDefault();
        e.stopPropagation();
        window.location.href = $(this).attr('href');
    });
});


function initializeSelect2(selector, placeholder, createUrl, entityName) {
    $(selector).select2({
        placeholder: placeholder,
        multiple: true,
        allowClear: true,
        language: {
            noResults: function () {
                return `No results found. <a href="${createUrl}" class="create-new-${entityName.toLowerCase()}">Create new ${entityName}</a>`;
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });
}