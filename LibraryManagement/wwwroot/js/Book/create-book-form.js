$(document).ready(function () {
    initializeSelectFields();
    setupEventHandlers();
});

function initializeSelectFields() {
    const culture = window.location.pathname.split('/')[1];
    loadAndInitializeSelect('#authors', `/${culture}/Author/GetAuthorsForDropDown`, null, { createUrl: `/${culture}/Author/Create`, entityName: 'Author' });
    loadAndInitializeSelect('#publishers', `/${culture}/Publisher/GetPublishersForDropDown`, null, { createUrl: `/${culture}/Publisher/Create`, entityName: 'Publisher' });
    loadAndInitializeSelect('#categories', `/${culture}/Category/GetCategoriesForDropDown`, null, { createUrl: `/${culture}/Category/Create`, entityName: 'Category' });;
}

function setupEventHandlers() {
    $(document).on('click', '[class^="create-new-"]', function (e) {
        e.preventDefault();
        e.stopPropagation();
        const culture = window.location.pathname.split('/')[1];
        const href = $(this).attr('href');
        window.location.href = `/${culture}${href}`;
    });
}