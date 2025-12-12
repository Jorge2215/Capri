var producto = (function () {

    function onSelectedRow(row) {
        var empresaData = row.sender.select();
        var dataItem = this.dataItem(empresaData.closest("tr"));

        window.location.replace($urlEditarProducto + '/' + dataItem.Id);
    }

    //Limpia los filtros de busqueda
    function cleanFilters() {
        $txtNombre.val('');
        $selCicloAdaptativo.data("kendoDropDownList").select(0);
        $txtIDProducto.val('');
        $chkActivo.prop('checked', true);

        $divProductos.hide();
        $gridProductos.data('kendoGrid').dataSource.data([]);
    }

    //Busqueda
    function search() {
        filterModel.Nombre = $txtNombre.val();
        //filterModel.CicloAplicativoId = $selCicloAdaptativo.getKendoDropDownList().value();
        filterModel.IdCicloAplicativo = $selCicloAdaptativo.data("kendoDropDownList").value();
        filterModel.Id = $txtIDProducto.val();
        filterModel.Activo = $chkActivo.is(':checked');

        $divProductos.show();
        $gridProductos.data("kendoGrid").dataSource.read();
    }

    //Retorna el objeto con los argumentos para el filtrado
    function getFilters() {
        return { filterModel: filterModel };
    }

    //Elimina un Producto
    function deleteProducto(id) {
        $.ajax({
            url: $urlDeleteProducto,
            data: { id: id },
            method: 'POST',
            success: function (data) {
                if (data.success)
                    showSuccessNotification("Producto", "<div style='word-wrap: break-word; white-space: normal;'>" + data.message + "</div>");
                else
                    showAlertNotification("Producto", "<div style='word-wrap: break-word; white-space: normal;'>" + data.message + "</div>");
                var grid = $gridProductos.data("kendoGrid");
                grid.dataSource.read();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                showErrorNotification(thrownError);
            }
        });
    }

    //Inicializador
    function init(opts) {
        $txtNombre = $(opts.txtNombre);
        $selCicloAdaptativo = $(opts.selCicloAdaptativo);
        $txtIDProducto = $(opts.txtIDProducto);
        $chkActivo = $(opts.chkActivo);

        $btnBuscar = $(opts.btnBuscar);
        $btnBuscar.on('click', search);

        $btnLimpiar = $(opts.btnLimpiar);
        $btnLimpiar.on('click', cleanFilters);

        $divProductos = $(opts.divProductos);
        $divProductos.hide();
        $gridProductos = $(opts.gridProductos);

        $urlEditarProducto = opts.urlEditarProducto;
        $urlDeleteProducto = opts.urlDeleteProducto;

        $gridProductos.data('kendoGrid').dataSource.data([]);
    }

    //Variables
    var $txtNombre;
    var $selCicloAdaptativo;
    var $txtIDProducto;
    var $chkActivo;

    var $btnBuscar;
    var $btnLimpiar;

    var $divProductos;
    var $gridProductos;

    var $urlEditarProducto;
    var $urlDeleteProducto;

    var filterModel = {
        Nombre: null
    }

    var module = {
        init: init,
        getFilters: getFilters,
        deleteProducto: deleteProducto,
        onSelectedRow: onSelectedRow,
    }

    return module;
}());