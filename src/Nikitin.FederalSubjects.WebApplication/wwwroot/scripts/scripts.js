$(document).ready(function() {
    let link = $(location).attr('pathname').split('/')[1];
    switch (link) {
        case 'index':
            setActive('');
            break;

        default:
            setActive(link);
            break;
    }

    function setActive(link) {
        $(`ul.nav a[href="/${link}"]`).removeClass('link-dark').addClass('active');
    }
});
