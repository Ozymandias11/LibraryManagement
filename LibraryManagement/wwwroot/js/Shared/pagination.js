jQuery(document).ready(function ($) {
    var $pageSize = $('#pageSize');
    var $startDate = $('#startDate');
    var $endDate = $('#endDate');

    $pageSize.on('change', updateUrl);
    $startDate.add($endDate).on('change', updateUrl);

    function updateUrl() {
        var currentUrl = new URL(window.location.href);
        var newPageSize = $pageSize.val();
        var startDate = $startDate.val();
        var endDate = $endDate.val();

        currentUrl.searchParams.set('pageSize', newPageSize);
        currentUrl.searchParams.set('pageNumber', '1');

        updateSearchParam(currentUrl, 'StartDate', startDate);
        updateSearchParam(currentUrl, 'EndDate', endDate);
        updateSearchParam(currentUrl, 'originalBookId', typeof originalBookId !== 'undefined' ? originalBookId : null);
        updateSearchParam(currentUrl, 'publisherId', typeof publisherId !== 'undefined' ? publisherId : null);
        updateSearchParam(currentUrl, 'edition', typeof edition !== 'undefined' ? edition : null);

        window.location.href = currentUrl.toString();
    }

    function updateSearchParam(url, param, value) {
        if (value) {
            url.searchParams.set(param, value);
        } else {
            url.searchParams.delete(param);
        }
    }

    function setSelectedPageSize(pageSize) {
        $pageSize.find('option').prop('selected', false)
            .filter('[value="' + pageSize + '"]').prop('selected', true);
    }

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

    setSelectedPageSize(pageSize);
    $(window).on('load resize', adjustPagination);

    $('.pagination .page-link').each(function () {
        var $this = $(this);
        var href = $this.attr('href');
        if (href) {
            var url = new URL(href, window.location.origin);
            updateSearchParam(url, 'originalBookId', typeof originalBookId !== 'undefined' ? originalBookId : null);
            updateSearchParam(url, 'publisherId', typeof publisherId !== 'undefined' ? publisherId : null);
            updateSearchParam(url, 'edition', typeof edition !== 'undefined' ? edition : null);
            $this.attr('href', url.toString());
        }
    });
});