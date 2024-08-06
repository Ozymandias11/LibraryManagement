$(document).ready(function () {
    $('#reportForm').submit(function (e) {
        e.preventDefault();
        var year = $('#year').val();
        $.get('/Customer/GetMonthlyRegistrationReport', { year: year }, function (data) {
            $('#reportTable').html(data);
            $('#exportButton').show();
            $('#exportForm input[name="year"]').val(year);
        });
    });
});
