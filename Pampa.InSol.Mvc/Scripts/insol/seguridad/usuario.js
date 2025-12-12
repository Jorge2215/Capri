
var usuario = (function () {
    function onSelectedRow(row) {
        var usuarioData = row.sender.select();
        var dataItem = this.dataItem(usuarioData.closest("tr"));
        
        window.location.replace($urlEditarUsuario + '/' + dataItem.Id);
    }

    function cleanFilters(){
        $txtUsuario.val('');
        $txtNombre.val('');
        $txtApellido.val('');
        $activo.prop('checked', true);

        $divUsuarios.hide();
        $grid.data('kendoGrid').dataSource.data([]);
    }

    function search() {
        filterModel.UsuarioNT = $txtUsuario.val();
        filterModel.Nombre = $txtNombre.val();
        filterModel.Apellido = $txtApellido.val();
        filterModel.Activo = $activo.is(':checked');

        $divUsuarios.show();
        $grid.data("kendoGrid").dataSource.read();
    }

    function getFilters() {
        return { filterModel: filterModel };
    }

    function init(opts) {
        $grid = $(opts.grid);
        $txtUsuario = $(opts.txtUsuario);
        $txtNombre = $(opts.txtNombre);
        $txtApellido = $(opts.txtApellido);
        $activo = $(opts.Activo);

        $divUsuarios = $(opts.divUsuarios);
        $divUsuarios.hide();

        $btnLimpiar = $(opts.btnLimpiar);
        $btnLimpiar.on('click', cleanFilters);

        $btnBuscar = $(opts.btnBuscar);
        $btnBuscar.on('click', search);

        $urlEditarUsuario = opts.urlEditarUsuario;

        $grid.data('kendoGrid').dataSource.data([]);
    }

    var $grid;
    var $txtUsuario;
    var $txtNombre;
    var $txtApellido;
    var $activo;
    var $btnLimpiar;
    var $btnBuscar;
    
    var $divUsuarios;

    var $urlEditarUsuario;

    var filterModel = {
        UsuarioNT: null,
        Nombre: null,
        Apellido: null,
        Activo: null,
    }

    var module = {
        init: init,
        getFilters: getFilters,
        onSelectedRow: onSelectedRow,
    }

    return module;

}());
