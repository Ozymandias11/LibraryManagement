function checkForChanges() {
    const form = document.getElementById('edit-template-form');
    const saveButton = document.getElementById('save-changes-button');
    let hasChanges = false;
    Array.from(form.elements).forEach(element => {
        if (element.name === 'Body') {
            // Handle TinyMCE editor content separately
            const editor = tinymce.get('body');
            const initialContent = element.getAttribute('data-placeholder');
            const currentContent = editor.getContent();
            if (currentContent !== initialContent) {
                hasChanges = true;
            }
        } else {
            // Handle other form elements
            if (element.hasAttribute('data-placeholder')) {
                const placeholder = element.getAttribute('data-placeholder');
                if (element.value !== placeholder) {
                    hasChanges = true;
                }
            }
        }
    });
    saveButton.style.display = hasChanges ? 'inline-block' : 'none';
}

document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('edit-template-form');

    form.addEventListener('input', checkForChanges);
    form.addEventListener('change', checkForChanges);

    tinymce.init({
        selector: '#body',
        plugins: 'code',
        toolbar: 'undo redo | bold italic | alignleft aligncenter alignright | code',
        height: 300,
        setup: function (editor) {
            editor.on('input', function () {
                checkForChanges();
            });
            editor.on('change', function () {
                checkForChanges();
            });
        }
    });
});