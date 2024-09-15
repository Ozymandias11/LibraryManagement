$(document).ready(function () {
    $('#reportForm').submit(function (e) {
        e.preventDefault();
        var year = $('#yearInput').val();

        if (year < 1900 || year > 2100) {
            $('#errorMessage').text('Please enter a valid year between 1900 and 2100.').show();
            return;
        }

        var startDate = new Date(year, 0, 1);
        var endDate = new Date(year, 11, 31);

        $.ajax({
            url: createLocalizedUrl('/Report/PrintReport'),
            method: 'POST',
            data: {
                startDate: startDate.toISOString(),
                endDate: endDate.toISOString()
            },
            xhrFields: {
                responseType: 'blob'
            },
            success: function (data) {
                var blob = new Blob([data], { type: 'application/pdf' });
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = 'report.pdf';
                link.click();
            },
            error: function () {
                $('#errorMessage').text('An error occurred while generating the report.').show();
            }
        });
    });
});