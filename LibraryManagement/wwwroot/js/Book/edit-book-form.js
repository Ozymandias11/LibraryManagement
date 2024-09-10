$(document).ready(function () {
    initializeSelectFields();
    setupCreateNewHandler();
});

function initializeSelectFields() {
    const bookId = $('#bookId').val();

    const selectFields = [
        {
            selector: '#authors',
            generalEndpoint: '/Author/GetAuthorsForDropDown',
            bookSpecificEndpoint: `/Author/GetBookAuthors/${bookId}`,
            createOptions: {
                createUrl: '/Author/Create',
                entityName: 'Author'
            }
        },
        {
            selector: '#publishers',
            generalEndpoint: '/Publisher/GetPublishersForDropDown',
            bookSpecificEndpoint: `/Publisher/GetBookPublishers/${bookId}`,
            createOptions: {
                createUrl: '/Publisher/Create',
                entityName: 'Publisher'
            }
        },
        {
            selector: '#categories',
            generalEndpoint: '/Category/GetCategoriesForDropDown',
            bookSpecificEndpoint: `/Category/GetBookCategories/${bookId}`,
            createOptions: {
                createUrl: '/Category/Create',
                entityName: 'Category'
            }
        }
    ];

    selectFields.forEach(field => {
        initializeSelect(
            field.selector,
            createLocalizedUrl(field.generalEndpoint),
            createLocalizedUrl(field.bookSpecificEndpoint),
            {
                createUrl: createLocalizedUrl(field.createOptions.createUrl),
                entityName: field.createOptions.entityName
            }
        );
    });
}