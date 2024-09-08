$(document).ready(function () {
    $('#reportForm').submit(function (e) {
        e.preventDefault();
        var year = $('#year').val();
        var url = $(this).data("annual-url");
        $.get(url, { year: year }, function (data) {
            $('#reportTable').html(data);
            $('#exportButton').show();
            $('#exportForm input[name="year"]').val(year);
        });
    });
});
