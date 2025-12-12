
var newUsuario = (function () {

    function validateForm() {
        fillUserNt = toggleInvalid(usuarioModel.UsuarioNT, $txtUsuario);
        fillNombre = toggleInvalid(usuarioModel.Nombre, $txtNombre);
        fillApellido = toggleInvalid(usuarioModel.Apellido, $txtApellido);

        return fillNombre && fillUserNt && fillApellido;
    }

    function save() {
        usuarioModel.Id = $idUsuario;
        usuarioModel.UsuarioNT = $txtUsuario.val();
        usuarioModel.Nombre = $txtNombre.val();
        usuarioModel.Apellido = $txtApellido.val();
        usuarioModel.Activo = $chkActivo.is(':checked');

        if (validateForm()) {
            var rolesRelacionados = $("#rolesRelacionados option");
            for (var i = 0; i < rolesRelacionados.length; i++) {
                rolesSelected.push(rolesRelacionados[i].value);
            }
            var url;
            if (usuarioModel.Id == 0) {
                url = $urlNuevoUsuario;
            } else {
                url = $urlEditarUsuario;
            }

            $.ajax({
                url: $urlNuevoUsuario,
                data: { newUser: usuarioModel, rolesId: rolesSelected },
                method: 'POST',
                success: function (data) {
                    if (usuarioModel.Id == 0) {
                        clean();
                        
                    }
                    showSuccessNotification(page.titles.titleUsuario, resources.messages.successSave.replace('{0}', 'Usuario'));
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                }
            });
            rolesSelected = [];
        } else {
            showAlertNotification(page.titles.titleUsuario, resources.messages.camposObligatorios)
        }
    }

    function clean() {
        $txtUsuario.val(constants.string_empty);
        $txtNombre.val(constants.string_empty);
        $txtApellido.val(constants.string_empty);

        //
        $btn_roles_remove_all.click();

        var my_options = $("#rolesDisponibles option");
        //var selected = $("#my_select").val();

        my_options.sort(function (a, b) {
            if (a.value > b.value) return 1;
            if (a.value < b.value) return -1;
            return 0
        })

        $("#rolesDisponibles").empty().append(my_options);
        //$("#my_select").val(selected);
    }

    function preload_listbox_rol() {
        $btn_roles_add.on('click', function () {
            $("#rolesDisponibles option:selected").appendTo("#rolesRelacionados");
            $("#rolesRelacionados option").attr("selected", false);
        })
        $btn_roles_add_all.on('click', function () {
            $("#rolesDisponibles option").appendTo("#rolesRelacionados");
            $("#rolesRelacionados option").attr("selected", false);
        })
        $btn_roles_remove.on('click', function () {
            $("#rolesRelacionados option:selected").appendTo("#rolesDisponibles");
            $("#rolesDisponibles option").attr("selected", false);
        })
        $btn_roles_remove_all.on('click', function () {
            $("#rolesRelacionados option").appendTo("#rolesDisponibles");
            $("#rolesDisponibles option").attr("selected", false);
        });
    }

    function searchAD() {
        $txtNombre.val(constants.string_empty);
        $txtApellido.val(constants.string_empty);
        if ($txtUsuario.val()) {
            $.get({
                url: $urlGetUsuarioAD,
                data: { usuarioNT: $txtUsuario.val() },
                success: function (data) {
                    if (data.success) {
                        $txtNombre.val(data.user.Nombre);
                        $txtApellido.val(data.user.Apellido);
                    } else {
                        showAlertNotification(page.titles.titleUsuario, resources.messages.userADValidation.replace('{0}', $txtUsuario.val()))
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                },
            });
        }
    }

    function init(opts) {
        $idUsuario = opts.idUsuario;
        $txtUsuario = $(opts.txtUsuario);
        $txtNombre = $(opts.txtNombre);
        $txtApellido = $(opts.txtApellido);
        $chkActivo = $(opts.chkActivo);

        $btn_guardar_relaciones = $(opts.btn_guardar_relaciones);
        $btn_roles_add = $(opts.btn_roles_add);
        $btn_roles_add_all = $(opts.btn_roles_add_all);
        $btn_roles_remove = $(opts.btn_roles_remove);
        $btn_roles_remove_all = $(opts.btn_roles_remove_all);

        $btnGuardar = $(opts.btnGuardar);
        $btnGuardar.on('click', save);
        preload_listbox_rol();

        $urlNuevoUsuario = opts.urlNuevoUsuario;
        $urlEditarUsuario = opts.urlEditarUsuario;
        $urlGetUsuarioAD = opts.urlGetUsuarioAD;

        if ($idUsuario == 0) {
            $txtUsuario.on('blur', searchAD);
            $txtUsuario.on('change', searchAD);
        } else {
            $txtUsuario.attr('readonly', 'readonly');
        }
    }

    var ids_left;
    var ids_right;
    var ids_roles_left;
    var ids_roles_right;

    var idUsuario;
    var $txtUsuario;
    var $txtNombre;
    var $txtApellido;
    var $chkActivo;


    var $btn_guardar_relaciones;
    var $btn_roles_add;
    var $btn_roles_add_all;
    var $btn_roles_remove;
    var $btn_roles_remove_all;

    var $btnGuardar;

    //#region Urls
    var $urlNuevoUsuario;
    var $urlEditarUsuario;
    var $urlGetUsuarioAD;
    //#endregion Urls

    var rolesSelected = [];

    var usuarioModel = {
        Id: null,
        UsuarioNT: null,
        Nombre: null,
        Apellido: null,
        Activo: null,
    }

    var module = {
        init: init,
    }

    return module;

}());
