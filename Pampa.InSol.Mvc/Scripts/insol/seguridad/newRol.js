
var saveRol = (function () {

    function onSelectedRow(row) {
        var rolData = row.sender.select().closest("tr");
        var dataItem = this.dataItem(rolData);
        $('input#' + dataItem.Id).click();
    }

    function toggleSelected(e) {
        var row = $(e.currentTarget).parent().parent();
        var dataItem = $tree.data('kendoTreeList').dataItem(row);
        if (dataItem.IdPadre) {
            //Si tiene Padre y no esta seleccionado, lo seleccionamos
            var checkPadre = $('input#' + dataItem.IdPadre);
            if ($(e.currentTarget).is(':checked') && !checkPadre.is(':checked')) {
                checkPadre.click();
            }
        } else {
            //Si es Padre y esta deschequeado quitamos todos los hijos
            if (!$(e.currentTarget).is(':checked')) {
                var data = $tree.data('kendoTreeList').dataSource._data;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].IdPadre == dataItem.Id) {
                        var checkHijo = $('input#' + data[i].Id);
                        if (checkHijo.is(':checked')) {
                            checkHijo.click();
                        }
                    }
                    //cuando encontramos el siguiente padre cortamos la ejecución
                    if (data[i].Id > dataItem.Id && data[i].IdPadre == null) {
                        break;
                    }
                }
            }

        }
    }

    function getFuncionesSave() {
        var checkboxes = $($classChk);

        for (var i = 0; i < checkboxes.length; i++) {
            var chk = $(checkboxes[i]);
            if (chk.is(':checked')) {
                funciones.push(chk[0].id);
            }
        }
    }

    function onDataBound() {
        $($classChk).on('click', toggleSelected);
    }

    function clean() {
        $descripcion.val('');
        $activo.prop('checked', true);

        var checkboxes = $($classChk);

        for (var i = 0; i < checkboxes.length; i++) {
            var chk = $(checkboxes[i]);
            if (chk.is(':checked')) {
                chk.click();
            }
        }
    }
    
    function search() {
        $tree.data('kendoTreeList').dataSource.read();
    }

    function save() {
        getFuncionesSave();

        rolModel.Id = $idRol;
        if ($descripcion.val()) {
            rolModel.Descripcion = $descripcion.val();
        } else {
            rolModel.Descripcion = null;
        }
        rolModel.Activo = $activo.is(':checked');

        if (valid()) {
            $.ajax({
                url: $urlSave,
                data: { rolModel: rolModel, funciones: funciones },
                dataType: 'json',
                method: 'POST',
                success: function (data) {
                    if ($idRol == 0) {
                        clean();
                    }
                    showSuccessNotification(page.titles.titleRol, resources.messages.successSave.replace('{0}', 'Rol'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                },
            });
        }
        funciones = [];
    }

    function valid() {
        var desc = toggleInvalid(rolModel.Descripcion, $descripcion);

        return desc;
    }

    function getFilters() {
        if ($idRol == 0) {
            return { idRol: null };
        } else {
            return { idRol: $idRol };
        }
    }

    function init(opts) {
        $idRol = opts.idRol;
        $descripcion = $(opts.descripcion_consulta);
        $activo = $(opts.chk_activos);
        $classChk = opts.classChk;
        $btnSave = $(opts.btnSave);
        $tree = $(opts.tree);

        $urlSave = opts.urlSave;

        $btnSave.click(save)

        search();
    }

    var $tree;
    var $idRol;
    var $btnConsultar;
    var $btnLimpiar;
    var $classChk;

    var $urlSave;

    var rolModel = {
        Descripcion: null,
        Activo: null,
    }

    var funciones = [];

    var $div_arbol;
    var $div_filtros;
    var $btn_nuevo_ingreso;
    var $descripcion;
    var $activo;
    var url_alta_rol;
    var url_edit_partial;
    var url_tree_data;
    var url_save;
    var $btn_volver_alta;
    var $descripcion_alta;
    var $chk_activo_alta;
    var $id_alta;
    var url_validate_duplicated;
    var $ids_to_check;

    var module = {
        init: init,
        getFilters: getFilters,
        onSelectedRow: onSelectedRow,
        onDataBound: onDataBound,
    }

    return module;

}());
