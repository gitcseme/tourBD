
$('.card-body .spot-list').on('click', 'div', function (event) {
    let searchText = event.currentTarget.innerText;
    let url = 'http://www.google.com/search?q=' + searchText;
    window.open(url, '_blank');
});