


var originalContents = {}; // Object to store original content of placeholders

function makeEditable(id) {
    var element = document.getElementById(id);
    if (!(id in originalContents)) {
        originalContents[id] = element.innerText; // Store the original content only if it's not already stored
    }
    element.contentEditable = true;
    element.focus();
    element.style.border = "2px solid green"; // Add green border when editing
    element.addEventListener('blur', function () {
        element.contentEditable = false;
        element.style.border = "none"; // Remove border on blur
        syncWithHiddenFields(id); // Sync content with hidden fields
    });
}

//function checkForChanges() {
//    var hasChanges = false;
//    for (var id in originalContents) {
//        var element = document.getElementById(id);
//        if (element.innerText !== originalContents[id]) {
//            hasChanges = true;
//            break;
//        }
//    }
//    var saveButton = document.getElementById('saveButton');
//    if (hasChanges) {
//        saveButton.style.display = 'inline-block';
//        saveButton.disabled = false; // Enable the submit button
//    } else {
//        saveButton.style.display = 'none';
//        saveButton.disabled = true; // Disable the submit button
//    }
//}



function syncWithHiddenFields(id) {
    var element = document.getElementById(id);
    var hiddenInputId = "hidden-" + id.split('-').slice(1).join('-'); // Generate hidden input ID
    var hiddenInput = document.getElementById(hiddenInputId);
    if (hiddenInput) {
        hiddenInput.value = element.innerText; // Sync visible content with hidden input
    }
}