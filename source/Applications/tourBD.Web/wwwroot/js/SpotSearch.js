
$('.card-body .spot-list').on('click', 'div', function (event) {
    let main_area = $(event.currentTarget).parent().parent().siblings('.card-header')
        .children('.header-left')
        .children('label.main-area-label')[0].innerText.substring(6);
    let searchText = event.currentTarget.innerText + ', ' + main_area;
    let url = 'http://www.google.com/search?q=' + searchText;
    window.open(url, '_blank');
});