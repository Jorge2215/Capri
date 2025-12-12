
var rol_funcion = (function () {

    function onSelectedRow(row) {
        var rolData = row.sender.select();
        var dataItem = this.dataItem(rolData.closest("tr"));

        window.location.replace($urlEditarRol + '/' + dataItem.Id);
    }

    function cleanFilters() {
        $descripcion_consulta.val('');
        $activo_consulta.prop('checked', true);
        $divRoles.hide();
        $grid.data('kendoGrid').dataSource.data([]);
    }

    function search() {
        filterModel.Descripcion = $descripcion_consulta.val();
        filterModel.Activo = $activo_consulta.is(':checked');
        $divRoles.show();
        $grid.data("kendoGrid").dataSource.read();
    }

    function getFilters() {
        return { filterModel: filterModel };
    }

    function init(opts) {
        $grid = $(opts.div_grilla);
        $descripcion_consulta = $(opts.descripcion_consulta);
        $activo_consulta = $(opts.chk_activos);
        $urlEditarRol = opts.urlEditarRol;

        $divRoles = $(opts.divRoles);
        $divRoles.hide();

        $btnConsultar = $(opts.btn_consultar);
        $btnConsultar.on('click', search);

        $btnLimpiar = $(opts.btn_limpiar_pantalla);
        $btnLimpiar.on('click', cleanFilters);

        cleanFilters();      
    }

    var $grid;
    var $btnConsultar;
    var $btnLimpiar;
    var $urlEditarUsuario;
    var $urlEditarRol;

    var $divRoles;

    var filterModel = {
        Descripcion: null,
        SoloActivos: null,
    }

    var $descripcion_consulta;
    var $activo_consulta;

    var module = {
        init: init,
        onSelectedRow: onSelectedRow,
        getFilters: getFilters,
    }

    return module;

}());
