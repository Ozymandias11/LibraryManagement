$(document).ready(function () {
    initializeSelectFields();
    setupEventHandlers();
});

function initializeSelectFields() {
    loadAndInitializeSelect('#authors', '/Author/GetAuthorsForDropDown', null, {
        createUrl: '/Author/Create',
        entityName: 'Author'
    });
    loadAndInitializeSelect('#publishers', '/Publisher/GetPublishersForDropDown', null, {
        createUrl: '/Publisher/Create',
        entityName: 'Publisher'
    });
    loadAndInitializeSelect('#categories', '/Category/GetCategoriesForDropDown', null, {
        createUrl: '/Category/Create',
        entityName: 'Category'
    });
}

function setupEventHandlers() {
    $(document).on('click', '[class^="create-new-"]', function (e) {
        e.preventDefault();
        e.stopPropagation();
        window.location.href = $(this).attr('href');
    });
}