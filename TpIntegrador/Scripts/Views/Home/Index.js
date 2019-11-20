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

function handleSearchSubmit(e, userId) {
    e.preventDefault();

    const loaderContainer = $('#loader-search');
    const resultContainer = $('#result-search');
    const query = $("#autocomplete-input").val();

    loaderContainer.show();
    resultContainer.hide();

    const url = "http://localhost:50130/api/Propuestas?userid=" + userId + "&query=" + query;

    $.get(url, function (res) {
        loaderContainer.hide();
        resultContainer.empty();

        if (res.length > 0) {
            res.forEach(function (item) {
                resultContainer.append(buildProposalCard(item));
            });
        } else {
            resultContainer.append(`<div class="card-panel red"><span class="white-text">No se han encontrados resultados para <strong>${query}</strong></span></div>`);
        }


        resultContainer.show();
    }).fail(function (e) {
        console.log({ e });
        loaderContainer.hide();
        resultContainer.hide();
    })

}

function buildProposalCard(propuesta) {
    return `
        <div class="col m6 s12">
            <div class="card hoverable">
                <div class="card-image">
                    <img class="materialboxed" src="${propuesta.Foto}">
                    <span class="card-title">${propuesta.Nombre}</span>
                </div>
                <div class="card-content">
                    <p>${propuesta.Descripcion}</p>
                </div>
                <div class="card-action d-flex" style="justify-content: space-between">
                    <div>
                        <a href="/Propuestas/VerDetalles/${propuesta.$id}">Ver detalles</a>
                    </div>
                    <div>
                        <a href="/Propuestas/Valoracion/${propuesta.$id}?valor=Like">
                            <i class="tiny material-icons">thumb_up_alt</i>
                            Me gusta
                        </a>
                        <a href="/Propuestas/Valoracion/${propuesta.$id}?valor=Dislike">
                            <i class="tiny material-icons">thumb_down_alt</i>
                            No me gusta
                        </a>
                    </div>
                </div>
            </div>
        </div>`;
}