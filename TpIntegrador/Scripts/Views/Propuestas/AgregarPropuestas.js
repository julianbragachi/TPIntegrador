$(document).ready(function() {
    $('select').formSelect();

    const btnAddInsumo = $('#btnAddInsumo');

    const handleAddInsumoClick = function() {
        const containerInsumos = $("#container-insumos");
        const countInsumos = containerInsumos.children().length;

        const createInputFn = function(icon, field) {
            return `
                <div class="input-field col s5">
                    <i class="material-icons prefix">${icon}</i>
                    <input class="validate text-box single-line" data-val="true" id="Insumos_${countInsumos}__${field}" name="Insumos[${countInsumos}].${field}" type="text" value="">
                    <label>${field}</label>
                </div>`;
        }

        containerInsumos.append(`
            <div id="insumo-${countInsumos}">
                ${createInputFn("build", "Nombre")}
                ${createInputFn("poll", "Cantidad")}
                <div class="col s2" style="text-align: center; margin-top: 23px;">
                    <i class="material-icons prefix" insumoId="${countInsumos}" id="delete-icon-${countInsumos}">delete</i>
                </div>
            </div>`
        );

        $(`#delete-icon-${countInsumos}`).click(function() {
            const insumoId = $(this).attr("insumoId");

            $(`#insumo-${insumoId}`).remove();

        });
    };


    if (btnAddInsumo) {
        btnAddInsumo.click(handleAddInsumoClick);
    }

});