
$(document).ready(function () {

    $('#addSpot').click(function () {
        var num = $('#SpotContainer').find('.newSpot').length - 1;
        var newNum = num + 1;
        var newElem = $('#singleSpot' + num).clone().attr('id', 'singleSpot' + newNum);

        newElem.find('.spotName').attr('name', 'Spots[' + newNum + ']').attr('id', 'id' + newNum).val('');

        $('#singleSpot' + num).after(newElem);
    });

});

function RemoveSpot(item) {
    var spotNode = $(item).parent().parent();
    var spots = $('#SpotContainer').find('.newSpot');

    if (spots.length > 1) {
        spotNode.remove();
        spots = $('#SpotContainer').find('.newSpot');

        for (i = 0; i < spots.length; ++i) {
            let spot = $(spots[i]);

            spot.find('.spotName').attr('name', 'Spots[' + i + ']').attr('id', 'id' + i);
            spot.attr('id', 'singleSpot' + i);
        }
    }
}