function populatePublishers() {
    var selectedBookId = $("#SelectedBookId").val();
    if (selectedBookId) {
        $.ajax({
            url: '/BookCopy/GetPublishersUrl', 
            type: 'GET',
            success: function (getPublishersUrl) {
                $.ajax({
                    url: getPublishersUrl,
                    data: { bookId: selectedBookId },
                    type: 'GET',
                    success: function (data) {
                        var publisherSelect = $("#publishers");
                        publisherSelect.empty();
                        publisherSelect.prop("disabled", false);
                        publisherSelect.append($("<option>").val("").text("Select a publisher"));
                        $.each(data, function (index, publisher) {
                            publisherSelect.append($("<option>").val(publisher.publisherId).text(publisher.publisherName));
                        });
                    },
                    error: function () {
                        alert("Error occurred while fetching publishers.");
                    }
                });
            },
            error: function () {
                alert("Error occurred while fetching the publishers URL.");
            }
        });
    } else {
        
    }
}



function populateShelves() {
    var selectedRoomId = $("#SelectedRoomId").val();
    if (selectedRoomId) {
        $.ajax({
            url: '/BookCopy/GetShelvesUrl', 
            type: 'GET',
            success: function (getShelvesUrl) {
                $.ajax({
                    url: getShelvesUrl,
                    data: { roomId: selectedRoomId },
                    type: 'GET',
                    success: function (data) {
                        var shelfSelect = $("#shelves");
                        shelfSelect.empty();
                        shelfSelect.prop("disabled", false);
                        shelfSelect.append($("<option>").val("").text("Select a shelf"));
                        $.each(data, function (index, shelf) {
                            shelfSelect.append($("<option>").val(shelf.shelfId).text(shelf.shelfNumber));
                        });
                    },
                    error: function () {
                        alert("Error occurred while fetching shelves.");
                    }
                });
            },
            error: function () {
                alert("Error occurred while fetching the shelves URL.");
            }
        });
    } else {
      
    }
}



