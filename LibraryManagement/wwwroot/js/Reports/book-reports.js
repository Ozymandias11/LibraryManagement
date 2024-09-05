$(document).ready(function () {
    $('#yearInput').hide();

    $('#reportPeriod').change(function () {
        toggleDateInputs($(this).val());
    });

    function toggleDateInputs(selectedPeriod) {
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
    }


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

        
        var reportUrl = $(this).data('report-url');
        var annualUrl = $(this).data('annual-url');
        var rangeUrl = $(this).data('range-url');

        var url = reportPeriod === 'Annual' ? annualUrl : rangeUrl;

        $.get(url, formData, function (data) {
            $('#reportTable').html(data);
            $('#exportButton').show();
            updateExportForm(startDate, endDate, year);
        });
    });

    function updateExportForm(startDate, endDate, year) {
        $('#exportForm input[name="startDate"]').val(startDate);
        $('#exportForm input[name="endDate"]').val(endDate);
        $('#exportForm input[name="year"]').val(year);
        $('#exportForm input[name="reportType"]').val($('#reportType').val());

        var reportPeriod = $('#reportPeriod').val();
        var actionUrl = reportPeriod === 'Annual' ? $('#exportForm').data('annual-url') : $('#exportForm').data('range-url');
        $('#exportForm').attr('action', actionUrl);
    }
});
