$(document).ready(function () {
    $('#reportForm').submit(function (e) {
        e.preventDefault();
        var formData = $(this).serialize();
        $.get('/Book/GetPopularityReport', formData, function (data) {
            $('#reportTable').html(data);
            $('#exportButton').show();

            
            $('#exportForm input[name="startDate"]').val($('#startDate').val());
            $('#exportForm input[name="endDate"]').val($('#endDate').val());
            $('#exportForm input[name="reportType"]').val($('#reportType').val());
        });
    });
});
