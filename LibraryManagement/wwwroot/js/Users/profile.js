


var originalContents = {}; 

function makeEditable(id) {
    var element = document.getElementById(id);
    if (!(id in originalContents)) {
        originalContents[id] = element.innerText; 
    }
    element.contentEditable = true;
    element.focus();
    element.style.border = "2px solid green"; 
    element.addEventListener('blur', function () {
        element.contentEditable = false;
        element.style.border = "none"; 
        syncWithHiddenFields(id); 
    });
}

function syncWithHiddenFields(id) {
    var element = document.getElementById(id);
    var hiddenInputId = "hidden-" + id.split('-').slice(1).join('-');
    var hiddenInput = document.getElementById(hiddenInputId);
    if (hiddenInput) {
        hiddenInput.value = element.innerText; 
    }
}