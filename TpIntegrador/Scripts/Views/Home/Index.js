$(document).ready(function () {
    $('.parallax').parallax();
    $('.materialboxed').materialbox();


    $('#chkOnlyActive').change(function (e) {
        const checked = e.target.checked;

        if (checked)
            window.location.href = "./?onlyActive=true";
        else
            window.location.href = "./";

    });

});