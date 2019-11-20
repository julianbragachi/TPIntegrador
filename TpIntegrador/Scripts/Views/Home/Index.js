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
        resultContainer.empty();
        loaderContainer.hide();
        resultContainer.append(`<div class="card-panel red"><span class="white-text">Ups, hubo un erro al buscar la informacion.</span></div>`);
        resultContainer.show();
    })

}

function buildProposalCard(propuesta) {
    return `
        <div class="masonry-item">
            <div class="card hoverable">
                <div class="card-image">
                    <img class="materialboxed" src="${propuesta.Foto}">
                    <span class="card-title">${propuesta.Nombre}</span>
                </div>

                <div class="card-content">
                    <p class="title-recaudado margin-bottom-20">Por: <strong>${propuesta.Usuarios.Nombre} ${propuesta.Usuarios.Apellido} (${propuesta.Usuarios.Email}, Fecha: ${propuesta.FechaCreacion}</strong></p>
                    <p class="title-recaudado margin-bottom-20">Valoracion: <strong>${propuesta.Valoracion}</strong></p>
                    <div class="margin-bottom-20">
                        <p style="margin-bottom: 5px;" class="title-recaudado">Referentes:</p>
                        ${buildReferentes(propuesta.PropuestasReferencias)}
                    </div>
                    <p><strong>Mas Informacion:</strong></p>
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

function buildReferentes(referencias) {
    let html = '';

    referencias.forEach(function (item) {
        html += `<p class="d-flex"><i style="margin-right: 10px;" class="material-icons prefix">account_circle</i>${item.Nombre} (${item.Telefono})</p>`;
    })

    return html;
}