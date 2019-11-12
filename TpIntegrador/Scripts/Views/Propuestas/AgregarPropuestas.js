$(document).ready(function () {
    $('select').formSelect();

    var horasSection = $("#horas-section");
    var insumosSection = $("#insumos-section");
    var monetariaSection = $("#monetaria-section");

    horasSection.hide();
    insumosSection.hide();
    monetariaSection.hide();

    var handleDonationTypeChange = function (elem) {
        horasSection.hide();
        insumosSection.hide();
        monetariaSection.hide();

        switch (elem.target.value) {
            case "0":
                monetariaSection.show();
                break;
            case "1":
                insumosSection.show();
                break;
            case "2":
                horasSection.show();
                break;
        }
    }

    $("[name='TipoDonacion']").each(function (i, elem) {
        $(elem).change(handleDonationTypeChange)
    });
});