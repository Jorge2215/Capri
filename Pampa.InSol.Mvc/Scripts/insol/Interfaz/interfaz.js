var interfaz = (function () {
    
    //Inicializador
    function init(opts) {
        $txtNombre = $(opts.txtNombre);
        $selCicloAplicativo = $(opts.selCicloAplicativo);
        $selProductoOrigen = $(opts.selProductoOrigen);
        $selModuloOrigen = $(opts.selModuloOrigen);
        $txtIDInterfaz = $(opts.txtIDInterfaz);
        $selProductoDestino = $(opts.selProductoDestino);
        $selModuloDestino = $(opts.selModuloDestino);
        $chkActivo = $(opts.chkActivo);

        $btnBuscar = $(opts.btnBuscar);
        $btnBuscar.on('click', search);

        $btnLimpiar = $(opts.btnLimpiar);
        $btnLimpiar.on('click', cleanFilters);

        $divInterfaz = $(opts.divInterfaz);
        $divInterfaz.hide();
        $gridInterfaz = $(opts.gridInterfaz);
        $gridInterfaz.data('kendoGrid').dataSource.data([]);

        $urlEditarInterfaz = opts.urlEditarInterfaz;
        $urlDeleteInterfaz = opts.urlDeleteInterfaz;
    }

    //Variables
    var $txtNombre;
    var $selCicloAplicativo;
    var $selProductoOrigen;
    var $selModuloOrigen;
    var $txtIDInterfaz;
    var $selProductoDestino;
    var $selModuloDestino;
    var $chkActivo;

    var $btnBuscar;
    var $btnLimpiar;

    var $divInterfaz;
    var $gridInterfaz;

    var $urlEditarInterfaz;
    var $urlDeleteInterfaz;

    var filterModel = {
        Nombre: null
    }

    function onSelectedRow(row) {
        var empresaData = row.sender.select();
        var dataItem = this.dataItem(empresaData.closest("tr"));

        window.location.replace($urlEditarInterfaz + '/' + dataItem.Id);
    }

    //Filtro-seleccion Producto Origen - Obtencion de modulos
    function filterModulosOrigen() {
        return {
            idProducto: $("#productoOrigenSelect").val()
        };
    }

    //Filtro-seleccion Producto Destino - Obtencion de modulos
    function filterModulosDestino() {
        return {
            idProducto: $("#productoDestinoSelect").val()
        };
    }

    //Limpia los filtros de busqueda
    function cleanFilters() {
        //debugger;
        $txtNombre.val('');
        $selCicloAplicativo.data("kendoDropDownList").select(0);

        $selModuloOrigen.data("kendoDropDownList").select(0);
        $selModuloDestino.data("kendoDropDownList").select(0);

        $selProductoOrigen.data("kendoDropDownList").dataSource.filter("");
        $selProductoDestino.data("kendoDropDownList").dataSource.filter("");

        

        //$selProductoOrigen.val(constants.string_empty);
        //$("#productoOrigenSelect").data("kendoDropDownList").select(0);
        $selProductoOrigen.data("kendoDropDownList").select(0);
        $selProductoDestino.data("kendoDropDownList").select(0);

        $txtIDInterfaz.val('');
        $chkActivo.prop('checked', true);

        $divInterfaz.hide();
        $gridInterfaz.data('kendoGrid').dataSource.data([]);
    }

    //Busqueda
    function search() {
        filterModel.Nombre = $txtNombre.val();
        filterModel.CicloAplicativoId = $selCicloAplicativo.getKendoDropDownList().value();
        filterModel.IdProductoOrigen = $selProductoOrigen.getKendoDropDownList().value();
        filterModel.IdModuloOrigen = $selModuloOrigen.getKendoDropDownList().value();
        filterModel.Id = $txtIDInterfaz.val();
        filterModel.IdProductoDestino = $selProductoDestino.getKendoDropDownList().value();
        filterModel.IdModuloDestino = $selModuloDestino.getKendoDropDownList().value();
        filterModel.Activo = $chkActivo.is(':checked');

        $divInterfaz.show();
        $gridInterfaz.data("kendoGrid").dataSource.read();
    }

    //Retorna el objeto con los argumentos para el filtrado
    function getFilters() {
        return { filterModel: filterModel };
    }

    //Elimina un Interfaz
    function deleteInterfaz(id) {
        $.ajax({
            url: $urlDeleteInterfaz,
            data: { id: id },
            method: 'POST',
            success: function () {
                var grid = $gridInterfaz.data("kendoGrid");
                grid.dataSource.read();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                showErrorNotification(thrownError);
            }
        });
    }

    var module = {
        init: init,
        getFilters: getFilters,
        deleteInterfaz: deleteInterfaz,
        onSelectedRow: onSelectedRow,
        filterModulosOrigen: filterModulosOrigen,
        filterModulosDestino: filterModulosDestino
    }

    return module;
}());