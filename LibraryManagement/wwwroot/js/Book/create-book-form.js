$(document).ready(function () {
    initializeSelectFields();
    setupEventHandlers();
});

function initializeSelectFields() {
    const selectFields = [
        { selector: '#authors', endpoint: '/Author/GetAuthorsForDropDown', createUrl: '/Author/Create', entityName: 'Author' },
        { selector: '#publishers', endpoint: '/Publisher/GetPublishersForDropDown', createUrl: '/Publisher/Create', entityName: 'Publisher' },
        { selector: '#categories', endpoint: '/Category/GetCategoriesForDropDown', createUrl: '/Category/Create', entityName: 'Category' }
    ];

    selectFields.forEach(field => {
        loadAndInitializeSelect(
            field.selector,
            createLocalizedUrl(field.endpoint),
            null,
            {
                createUrl: createLocalizedUrl(field.createUrl),
                entityName: field.entityName
            }
        );
    });
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