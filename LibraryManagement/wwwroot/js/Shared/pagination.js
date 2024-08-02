jQuery(document).ready(function ($) {
    $('#pageSize').on('change', function () {
        updateUrl();
    });

    $('#startDate, #endDate').on('change', function () {
        updateUrl();
    });

    function updateUrl() {
        var currentUrl = new URL(window.location.href);
        var newPageSize = $('#pageSize').val();
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();

        currentUrl.searchParams.set('pageSize', newPageSize);
        currentUrl.searchParams.set('pageNumber', '1');

        if (startDate) {
            currentUrl.searchParams.set('StartDate', startDate);
        } else {
            currentUrl.searchParams.delete('StartDate');
        }

        if (endDate) {
            currentUrl.searchParams.set('EndDate', endDate);
        } else {
            currentUrl.searchParams.delete('EndDate');
        }

        if (originalBookId) currentUrl.searchParams.set('originalBookId', originalBookId);
        if (publisherId) currentUrl.searchParams.set('publisherId', publisherId);
        if (edition) currentUrl.searchParams.set('edition', edition);

        window.location.href = currentUrl.toString();
    }

    function setSelectedPageSize(pageSize) {
        $('#pageSize option').each(function () {
            if ($(this).val() == pageSize) {
                $(this).prop('selected', true);
                return false;
            }
        });
    }

    setSelectedPageSize(pageSize);

    function adjustPagination() {
        var $pagination = $('.pagination');
        var $pageItems = $pagination.find('.page-item:not(:first-child):not(:last-child)');

        if ($(window).width() < 768) {
            if ($pageItems.length > 5) {
                $pageItems.hide();
                var $currentPage = $pagination.find('.page-item.active');
                $currentPage.show();
                $currentPage.prev().show();
                $currentPage.next().show();
                $pagination.find('.page-item:nth-child(2)').show();
                $pagination.find('.page-item:nth-last-child(2)').show();
            }
        } else {
            $pageItems.show();
        }
    }

    $(window).on('load resize', adjustPagination);

    
    $('.pagination .page-link').each(function () {
        var href = $(this).attr('href');
        if (href) {
            var url = new URL(href, window.location.origin);
            if (originalBookId) url.searchParams.set('originalBookId', originalBookId);
            if (publisherId) url.searchParams.set('publisherId', publisherId);
            if (edition) url.searchParams.set('edition', edition);
            $(this).attr('href', url.toString());
        }
    });
});