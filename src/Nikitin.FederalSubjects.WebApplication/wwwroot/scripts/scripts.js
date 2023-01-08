$(document).ready(function() {
    let link = $(location).attr('pathname').split('/')[1];
    $(`ul.nav a[href="/${link}"]`).removeClass('link-dark').addClass('active');
});
