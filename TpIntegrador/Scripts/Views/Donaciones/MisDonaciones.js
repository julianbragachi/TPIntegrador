$(document).ready(function () {
    const SessionID = $('#hdnSessionID').val();
    const loader = $('#loader-mispropuestas');
    const view = $('#mispropuestas-container');

    const url = "http://localhost:50130/api/Donaciones?userid=" + SessionID;

    const buildTable = function (data) {
        return `
            <table>
                <thead>
                    <tr>
                        <th>Fecha Donación</th>
                        <th>Nombre</th>
                        <th>Tipo De Donacion</th>
                        <th>Estado</th>
                        <th>Total Recaudado</th>
                        <th>Mi Donación</th>
                        <th>Detalle Propuesta</th>
                    </tr>
                </thead>
                ${buildTBody(data)}
            </table>`
    };

    const buildTBody = function (data) {
        let html = '';

        data.forEach(function (item) {
            html = `
                <tbody>
                    <tr>
                        ${buildTD(item)}
                    </tr>
                </tbody>`;
        });

        return html;
    }

    const buildTD = function (item) {
        switch (item.tipo) {
            case 'insumo':
                return `
                <td>-</td>
                <td>${item.donacionesInsumos.PropuestasDonacionesInsumos.Propuestas.Nombre}</td>
                <td>${item.donacionesInsumos.PropuestasDonacionesInsumos.Nombre}</td>
                <td>Insumo</td>
                <td>${item.donacionesInsumos.PropuestasDonacionesInsumos.Propuestas.Estado}</td>
                <td>${item.total} ${item.donacionesInsumos.PropuestasDonacionesInsumos.Nombre}</td>
                <td>${item.donacionesInsumos.PropuestasDonacionesInsumos.Cantidad}</td>`;
            case 'monetario':
                return `
                <td>-</td>
                <td>${item.donacionesMonetarias.PropuestasDonacionesMonetarias.Propuestas.Nombre}</td>
                <td>Monetario</td>
                <td>${item.donacionesMonetarias.PropuestasDonacionesMonetarias.Propuestas.Estado}</td>
                <td>${item.total} $</td>
                <td>${item.donacionesMonetarias.Dinero}</td>
                <td>
                    <a class="btn waves-effect waves-light" href="/Propuestas/VerDetalles/${item.donacionesMonetarias.$id}">Detalle</a>
                </td>`;
            case 'horastrabajo':
                return `
                <td>-</td>
                <td>${item.donacion.donacionesHorasTrabajo.PropuestasDonacionesHorasTrabajo.Propuestas.Nombre}</td>
                <td>${item.donacion.donacionesHorasTrabajo.PropuestasDonacionesHorasTrabajo.Profesion}</td>
                <td>Horas Trabajo</td>
                <td>${item.donacion.donacionesHorasTrabajo.PropuestasDonacionesHorasTrabajo.Propuestas.Estado}</td>
                <td>${item.donacion.total} Horas</td>
                <td>${item.donacion.donacionesHorasTrabajo.PropuestasDonacionesHorasTrabajo.CantidadHoras}</td>`;
        }
    }

    $.get(url, function (res) {
        loader.hide();

        if (res.length > 0) {
            view.append(buildTable(res));
        } else {
            view.append(`<div class="card-panel red"><span class="white-text">No se han encontrados resultados para <strong>${query}</strong></span></div>`);
        }

        view.show();
    }).fail(function (e) {
        console.log({ e });
        view.empty();
        loader.hide();
        view.append(`<div class="card-panel red"><span class="white-text">Ups, hubo un erro al buscar la informacion.</span></div>`);
        view.show();
    })
});

