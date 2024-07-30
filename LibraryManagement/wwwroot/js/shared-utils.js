function loadAndInitializeSelect(selectElement, url, isMultiple, onChangeCallback = null, createNewOptions = null, additionalOptions = {}) {
    $.getJSON(url)
        .done(function (allData) {
            const options = allData.map(item => ({
                id: item.id,
                text: item.name || item.title || item.roomNumber
            }));

            const select2Options = {
                data: options,
                placeholder: additionalOptions.placeholder || "Select an option",
                allowClear: additionalOptions.allowClear !== undefined ? additionalOptions.allowClear : true,
                multiple: isMultiple,
                escapeMarkup: function (markup) {
                    return markup;
                },
                ...additionalOptions
            };

            if (createNewOptions) {
                select2Options.language = setupSelect2Language(createNewOptions.createUrl, createNewOptions.entityName);
            }

            $(selectElement).select2(select2Options);

            if (onChangeCallback) {
                $(selectElement).on('change', onChangeCallback);
            }

            if (createNewOptions) {
                addCreateNewOption(selectElement, createNewOptions.entityName);
            }

            $(selectElement).val(null).trigger('change');
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error(`Error loading data for ${selectElement}:`, textStatus, errorThrown);
        });
}

function initializeSelect(selectElement, allDataUrl, selectedDataUrl = null, createNewOptions = null) {

    const dataPromises = [$.getJSON(allDataUrl)];

    if (selectedDataUrl) {
        dataPromises.push($.getJSON(selectedDataUrl));
    }

    $.when(...dataPromises)
        .done(function (allDataResponse, selectedDataResponse) {
            const allData = allDataResponse[0];
            const selectedIds = selectedDataResponse ? selectedDataResponse[0] : [];

            const options = allData.map(item => ({
                id: item.id,
                text: item.name,
                selected: selectedIds.includes(item.id)
            }));

            const select2Options = {
                data: options,
                placeholder: $(selectElement).data('placeholder'),
                allowClear: true,
                multiple: true,
                escapeMarkup: function (markup) {
                    return markup;
                }
            };

            if (createNewOptions) {
                select2Options.language = setupSelect2Language(createNewOptions.createUrl, createNewOptions.entityName);
            }

            $(selectElement).select2(select2Options);

            if (createNewOptions) {
                addCreateNewOption(selectElement, createNewOptions.entityName);
            }

            $(selectElement).trigger('change');
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.error("Error fetching data:", textStatus, errorThrown);
        });
}

function setupSelect2Language(createUrl, entityName) {
    return {
        noResults: function () {
            return `No results found. <a href="${createUrl}" class="create-new-${entityName.toLowerCase()}">Create new ${entityName}</a>`;
        }
    };
}

function addCreateNewOption(selectElement, entityName) {
    const $select = $(selectElement);
    const createUrl = $select.data('create-url');

    $select.on('select2:open', function () {
        if (!$(".select2-results__options").find('.create-new-option').length) {
            $(".select2-results__options").append(`<li class="select2-results__option create-new-option">
                <a href="${createUrl}" class="create-new-${entityName.toLowerCase()}">Create new ${entityName}</a>
            </li>`);
        }
    });
}