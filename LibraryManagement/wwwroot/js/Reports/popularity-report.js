$(document).ready(function () {
    // Initially hide year input and only show date inputs
    $('#yearInput').hide();

    // Handle the change of report period type
    $('#reportPeriod').change(function () {
        var selectedPeriod = $(this).val();
        if (selectedPeriod === 'Annual') {
            $('#dateInputs').hide();      // Hide date inputs
            $('#yearInput').show();       // Show year input
            $('#startDate').prop('required', false);
            $('#endDate').prop('required', false);
            $('#year').prop('required', true);
        } else if (selectedPeriod === 'Range') {
            $('#dateInputs').show();      // Show date inputs
            $('#yearInput').hide();       // Hide year input
            $('#startDate').prop('required', true);
            $('#endDate').prop('required', true);
            $('#year').prop('required', false);
        } else {
            // Hide both if nothing is selected
            $('#dateInputs').hide();
            $('#yearInput').hide();
        }
    });

    $('#reportForm').submit(function (e) {
        e.preventDefault();

        var reportPeriod = $('#reportPeriod').val();
        var year = $('#year').val();
        var startDate, endDate;

        // Handle date range for annual report
        if (reportPeriod === 'Annual' && year) {
            startDate = year + '-01-01';
            endDate = year + '-12-31';
        } else {
            startDate = $('#startDate').val();
            endDate = $('#endDate').val();
        }

        // Construct form data manually
        var formData = {
            startDate: startDate,
            endDate: endDate,
            year: year,
            reportType: $('#reportType').val()
        };

        var url = reportPeriod === 'Annual' ? '/Book/GetAnnualReport' : '/Book/GetPopularityReport';

        $.get(url, formData, function (data) {
            $('#reportTable').html(data);
            $('#exportButton').show();

            // Update export form data
            $('#exportForm input[name="startDate"]').val(startDate);
            $('#exportForm input[name="endDate"]').val(endDate);
            $('#exportForm input[name="year"]').val(year);
            $('#exportForm input[name="reportType"]').val($('#reportType').val());
        });
    });
});
