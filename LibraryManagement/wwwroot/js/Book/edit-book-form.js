$(document).ready(function () {
    initializeSelectFields();
    setupCreateNewHandler();
});

function initializeSelectFields() {
    const bookId = $('#bookId').val();

    initializeSelect('#authors', '/Author/GetAuthorsForDropDown', `/Author/GetBookAuthors/${bookId}`, {
        createUrl: '/Author/Create',
        entityName: 'Author'
    });

    initializeSelect('#publishers', '/Publisher/GetPublishersForDropDown', `/Publisher/GetBookPublishers/${bookId}`, {
        createUrl: '/Publisher/Create',
        entityName: 'Publisher'
    });

    initializeSelect('#categories', '/Category/GetCategoriesForDropDown', `/Category/GetBookCategories/${bookId}`, {
        createUrl: '/Category/Create',
        entityName: 'Category'
    });
}