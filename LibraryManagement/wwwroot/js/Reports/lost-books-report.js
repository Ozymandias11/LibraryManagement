$(document).ready(function () {
    $('#yearInput').hide();
    $('#reportPeriod').change(function () {
        var selectedPeriod = $(this).val();
        if (selectedPeriod === 'Annual') {
            $('#dateInputs').hide();
            $('#yearInput').show();
            $('#startDate').prop('required', false);
            $('#endDate').prop('required', false);
            $('#year').prop('required', true);
        } else if (selectedPeriod === 'Range') {
            $('#dateInputs').show();
            $('#yearInput').hide();
            $('#startDate').prop('required', true);
            $('#endDate').prop('required', true);
            $('#year').prop('required', false);
        } else {
            $('#dateInputs').hide();
            $('#yearInput').hide();
        }
    });

    $('#reportForm').submit(function (e) {
        e.preventDefault();
        var reportPeriod = $('#reportPeriod').val();
        var year = $('#year').val();
        var startDate, endDate;
        if (reportPeriod === 'Annual' && year) {
            startDate = year + '-01-01';
            endDate = year + '-12-31';
        } else {
            startDate = $('#startDate').val();
            endDate = $('#endDate').val();
        }
        var formData = {
            startDate: startDate,
            endDate: endDate,
            year: year,
            reportType: $('#reportType').val()
        };
        var url = reportPeriod === 'Annual' ? '/Book/GetAnnualLostBooksReport' : '/Book/GetLostBooksReport';
        $.get(url, formData, function (data) {
            $('#reportTable').html(data);
            $('#exportButton').show();
            $('#exportForm input[name="startDate"]').val(startDate);
            $('#exportForm input[name="endDate"]').val(endDate);
            $('#exportForm input[name="year"]').val(year);
            $('#exportForm input[name="reportType"]').val($('#reportType').val());

            if (reportPeriod === 'Annual') {
                $('#exportForm').attr('action', $('#exportForm').data('annual-url'));
            } else {
                $('#exportForm').attr('action', $('#exportForm').data('range-url'));
            }
        });
    });
});
