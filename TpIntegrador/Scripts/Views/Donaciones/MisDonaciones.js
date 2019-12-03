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

        data.forEach(function (item,index) {
            html += `${buildTD(item)}`;
        });

        return html;
    }

    const buildTD = function (item,index) {
        switch (item.tipo) {
            case 'insumo':
                return `
                <tbody>
                <tr>
                <td>-</td>
                <td>${item.DonacionesInsumosVM.Nombre}</td>
                <td>Insumo</td>
                <td>${item.DonacionesInsumosVM.Estado}</td>
                <td>${item.total} ${item.DonacionesInsumosVM.NombreDonado}</td>
                <td>${item.DonacionesInsumosVM.Cantidad}</td>
                <td>
                    <a class="btn waves-effect waves-light" href="/Propuestas/VerDetalles/${item.DonacionesInsumosVM.IdPropuestaDonacionInsumo}">Detalle</a>
                </td>
                </tr>
                </tbody>`;
            case 'monetario':
                return `
                <tbody>
                <tr>
                <td>${item.DonacionesMonetariasVM.Fecha}</td>
                <td>${item.DonacionesMonetariasVM.Nombre}</td>
                <td>Monetario</td>
                <td>${item.DonacionesMonetariasVM.Estado}</td>
                <td>${item.total} $</td>
                <td>${item.DonacionesMonetariasVM.Dinero}</td>
                <td>
                    <a class="btn waves-effect waves-light" href="/Propuestas/VerDetalles/${item.DonacionesMonetariasVM.IdPropuestaDonacionMonetaria}">Detalle</a>
                </td>
                </tr>
                </tbody>`;
            case 'horastrabajo':
                return `
                <tbody>
                <tr>
                <td>-</td>
                <td>${item.DonacionesHorasTrabajoVM.Nombre}</td>
                <td>Horas Trabajo</td>
                <td>${item.DonacionesHorasTrabajoVM.Estado}</td>
                <td>${item.total} Horas</td>
                <td>${item.DonacionesHorasTrabajoVM.Cantidad}</td>
                <td>
                    <a class="btn waves-effect waves-light" href="/Propuestas/VerDetalles/${item.DonacionesHorasTrabajoVM.IdPropuestaDonacionHorasTrabajo}">Detalle</a>
                </td>
                </tr>
                </tbody>`;
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

