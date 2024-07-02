$(document).ready(function () {
    $('#authors').select2({
        placeholder: 'Select authors',
        multiple: true,
        allowClear: true,
        language: {
            noResults: function () {
                return 'No results found. <a href="/Author/CreateAuthor" class="create-new-author">Create new author</a>';
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });

    $('#publishers').select2({
        placeholder: 'Select Publishers',
        multiple: true,
        allowClear: true,
        language: {
            noResults: function () {
                return 'No results found. <a href="/Publisher/CreatePublisher" class="create-new-publisher">Create new Publisher</a>';
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });

    $('#Categories').select2({
        placeholder: 'Select Categories',
        multiple: true,
        allowClear: true,
        language: {
            noResults: function () {
                return 'No results found. <a href="/Category/CreateCategory" class="create-new-category">Create new Category</a>';
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });

    // Handle click on "Create new" links
    $(document).on('click', '.create-new-author, .create-new-publisher, .create-new-category', function (e) {
        e.preventDefault();
        e.stopPropagation();
        window.location.href = $(this).attr('href');
    });
});