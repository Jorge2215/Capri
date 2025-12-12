var msgFuncionInexistente = 'No se encuentra definida la función "{0}"';

$(document).ready(function () {
    $('#btnBuscar').bind('click', Buscar);
});

function Buscar(e) {
    e.preventDefault();
    if (ValidarConfiguracionDeVista()) {
        $.ajax({
            type: "GET",
            cache: false,
            url: ObtenerUrlBusqueda(),
            dataType: "json",
            data: ObtenerDatosFiltro(),
            success: function (result) {
                if (!result.success) {
                    debugger;
                    result.error.forEach(function (item, index) {
                        showErrorNotification(item);
                    });
                }
                else {
                    $('#contenedorGrilla').html(result.view);
                }
            }
        });
    }
}

function ValidarConfiguracionDeVista(e) {

    if (isUndefined(ObtenerUrlBusqueda)) {
        showErrorNotification(msgFuncionInexistente.format('ObtenerUrlBusqueda'));
    }
    else if (isUndefined(ObtenerDatosFiltro)) {
        showErrorNotification(msgFuncionInexistente.format('ObtenerDatosFiltro'));
    }
    else {
        return true;
    }
    return false;
}

function GenerarAccionesGrilla(e, funcionVisibilidad) {

    $('#contenedorGrilla').find(".k-grid-Editar").each(function (idx, element) {

        var currentDataItem = $("#" + e.sender.element[0].id).data("kendoGrid").dataItem($(this).closest("tr"));
        if (!isUndefined(funcionVisibilidad) && funcionVisibilidad(currentDataItem)) {
            $(this).remove();
        }
        else {
            $(element).find("span").addClass("fa fa-pencil-square-o");
        }
    });

    $('#contenedorGrilla').find(".k-grid-Eliminar").each(function (idx, element) {

        var currentDataItem = $("#" + e.sender.element[0].id).data("kendoGrid").dataItem($(this).closest("tr"));
        if (!isUndefined(funcionVisibilidad) && funcionVisibilidad(currentDataItem)) {
            $(this).remove();
        }
        else {
            $(element).find("span").addClass("fa fa-trash-o");
        }
    });

    $('#contenedorGrilla').find(".k-grid-Ver").each(function (idx, element) {
        $(element).find("span").addClass("fa fa-file-text-o");
    });

}

function limpiarFiltros() {
    window.history.pushState(null, null, location.origin + location.pathname);
    location.reload();

}