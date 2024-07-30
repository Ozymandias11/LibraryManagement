jQuery(document).ready(function ($) {
    $('#pageSize').on('change', function () {
        var newPageSize = $(this).val();
        var currentUrl = new URL(window.location.href);
        currentUrl.searchParams.set('pageSize', newPageSize);
        currentUrl.searchParams.set('pageNumber', '1');
        window.location.href = currentUrl.toString();
    });

    function setSelectedPageSize(pageSize) {
        $('#pageSize option').each(function() {
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
});