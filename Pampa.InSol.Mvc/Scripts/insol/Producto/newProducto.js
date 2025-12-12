var newProducto = (function () {

    //Abre el popup para agregar un Módulo
    function openPopupAgregarModulo() {
        $popupAgregarModulo.data("kendoWindow").center();
        $popupAgregarModulo.data("kendoWindow").open();
    }

    //Agrega un nuevo módulo a la lista de módulos
    function agregarModulo() {
        if ($txtModuloNombre.val() != null && $txtModuloNombre.val() != '') {
            //Verificar que el modulo no exista
            if (!checkExistencia(0, $txtModuloNombre.val())) {
                //Agregarlo a la lista global
                let item = [0, 0, $txtModuloNombre.val(), $txtModuloDescripcion.val()];
                $listaModulos.push(item);

                $gridModulos.data("kendoGrid").dataSource.read();
                closePopupAgregarModulo();
            }
            else {
                showAlertNotification("Producto", 'El Módulo ya está agregado');
            }
        }
        else {
            showAlertNotification("Producto", 'Debe ingresar un nombre para el Módulo');
        }
    }

    //Evalua si existe un modulo en la lista global
    function checkExistencia(id, nombre) {
        for (var i = 0; i < $listaModulos.length; i++) {

            //if (($listaModulos[i][0] === id) && ($listaModulos[i][1] === $txtModuloNombre.val())) {

            //    return true;
            //}
            if ($listaModulos[i][0] === $txtModuloNombre.val()) {
                return true;
            }
        }

        return false;
    };

    //Elimina un modulo de la lista global
    function eliminarModulo(nombre) {
        const eliminarModuloLocal = () => {
            var grid = $gridModulos.data("kendoGrid");
            for (var i = 0; i < $listaModulos.length; i++) {
                if (($listaModulos[i][2] == nombre)) {
                    $listaModulos.splice(i, 1);
                    i = $listaModulos.length;
                }
            }

            grid.dataSource.read();
            showSuccessNotification('Producto', 'Se quitó el módulo.');
        }

        if ($IDProducto == 0) {
            eliminarModuloLocal();
        }
        else {
            $.ajax({
                url: $urlDeleteModuloRelacionado,
                data: { idProducto: $IDProducto, nombre: nombre },
                method: 'POST',
                success: function () {
                    eliminarModuloLocal();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                }
            });
        }
    }

    //Retorna la lista de modulos al backend
    function getListaModulos() {
        var list = [];

        for (var i = 0; i < $listaModulos.length; i++) {
            list.push({
                IdProducto: $listaModulos[i][0],
                IdModulo: $listaModulos[i][1],
                Nombre: $listaModulos[i][2],
                Descripcion: $listaModulos[i][3]
            });
        }

        return { listaModulos: list };
    }

    //Cierra el popup para agregar un Módulo
    function closePopupAgregarModulo() {
        $popupAgregarModulo.popUpClose();
        cleanPopUpAgregarModulo();
    }

    //Limpia los campos del popup de Agregar Módulo
    function cleanPopUpAgregarModulo() {
        $txtModuloNombre.val(constants.string_empty);
        $txtModuloDescripcion.val(constants.string_empty);
    }

    //Abre la lista de Interfaces
    function openPopupListaInterfaces() {
        $gridInterfaces.data("kendoGrid").dataSource.read();
        $popupListaInterfaces.data("kendoWindow").center();
        $popupListaInterfaces.data("kendoWindow").open();
    }

    //Cierra la lista de Interfaces
    function closePopupListaInterfaces() {
        $popupListaInterfaces.popUpClose();
    }

    //Filtro IdProducto usado para obtener las Interfaces
    function getFilterIdProducto() {
        return { idProducto: $IDProducto };
    }
    
    //Validacion de campos
    function validateForm() {
        result = toggleInvalid(productoModel.Nombre, $txtNombre);
        return result;
    }

    //Limpiar campos
    function clean() {
        $txtNombre.val(constants.string_empty);
        $selProductoPlataforma.data('kendoDropDownList').value(null);
        $selTipoProducto.data('kendoDropDownList').value(null);
        $selServicio.data('kendoDropDownList').value(null);

        $selSitio.data('kendoDropDownList').value(null);
        $selObsolescencia.data('kendoDropDownList').value(null);
        $selCicloAplicativo.data('kendoDropDownList').value(null);

        //$selAmbiente.data('kendoDropDownList').value(null);
        //$selRolAmbiente.data('kendoDropDownList').value(null);
        //$chkActivo.val(constants.string_empty);

        $txtGerencia.val(constants.string_empty);
        $txtJefaturaTI.val(constants.string_empty);
        $txtRespTI.val(constants.string_empty);
        $txtRespNegocio.val(constants.string_empty);
        $txtRespMesa.val(constants.string_empty);

        $txtDescripcion.val(constants.string_empty);

        $cmbNegocio.data('kendoMultiSelect').value(null);
        $cmbProceso.data('kendoMultiSelect').value(null);

        ProductoAmbienteModel.Id = null;

        //$txtServicio.val(constants.string_empty);
        //$txtObsolescencia.val(constants.string_empty);
        //$txtAgrupador.val(constants.string_empty);
        //$txtTipo.val(constants.string_empty);

        //$txtAppDevPath.val(constants.string_empty);
        //$txtAppDevUrl.val(constants.string_empty);
        //$txtBaseDevServer.val(constants.string_empty);
        //$txtBaseDevBase.val(constants.string_empty);
        //$txtBaseDevUsuario.val(constants.string_empty);
        //$txtBaseDevPassword.val(constants.string_empty);

        //$txtAppQAPath.val(constants.string_empty);
        //$txtAppQAUrl.val(constants.string_empty);
        //$txtBaseQAServer.val(constants.string_empty);
        //$txtBaseQABase.val(constants.string_empty);
        //$txtBaseQAUsuario.val(constants.string_empty);
        //$txtBaseQAPassword.val(constants.string_empty);

        //$txtAppPRODPath.val(constants.string_empty);
        //$txtAppPRODUrl.val(constants.string_empty);
        //$txtBasePRODServer.val(constants.string_empty);
        //$txtBasePRODBase.val(constants.string_empty);
        //$txtBasePRODUsuario.val(constants.string_empty);
        //$txtBasePRODUsuEmer.val(constants.string_empty);
    }

    //Guardado
    function save() {
        productoModel.Id = $IDProducto;
        productoModel.Nombre = $txtNombre.val();
        productoModel.EsPlataforma = $('#EsPlataforma').is(":checked");
        productoModel.IdPlataforma = $selProductoPlataforma.data('kendoDropDownList').value();
        productoModel.IdCicloAplicativo = $selCicloAplicativo.val();
        productoModel.Activo = $chkActivo.is(':checked');
        productoModel.RespTI = $txtRespTI.val();
        productoModel.RespNegocio = $txtRespNegocio.val();
        productoModel.RespMesa = $txtRespMesa.val();
        productoModel.Descripcion = $txtDescripcion.val();

        productoModel.Sox = $chkSox.is(':checked');

        productoModel.Modo = $chkModo;
        productoModel.AccesoInternet = $chkAccesoInternet.is(':checked');
        productoModel.IdServicio = $selServicio.data('kendoDropDownList').value();
        productoModel.IdSitio = $selSitio.data('kendoDropDownList').value();
        productoModel.IdObsolescencia = $selObsolescencia.data('kendoDropDownList').value();
        productoModel.IdTipo = $selTipoProducto.data('kendoDropDownList').value();

        productoModel.Gerencia = $txtGerencia.val();
        productoModel.Agrupador = $txtAgrupador.val();
        productoModel.JefaturaTI = $txtJefaturaTI.val();

        var listadoNegocios = [];
        var negocios = $cmbNegocio.data('kendoMultiSelect').value();

        for (var i = 0; i < negocios.length; i++) {
            listadoNegocios.push({
                IdProducto: $IDProducto,
                IdNegocio: negocios[i]
            });
        }


        var listadoProcesos = [];
        var procesos = $cmbProceso.data('kendoMultiSelect').value();
        for (var i = 0; i < procesos.length; i++) {
            listadoProcesos.push({
                IdProducto: $IDProducto,
                IdProceso: procesos[i]
            });
        }
        //ambienteDev.IdProducto = $IDProducto;
        //ambienteDev.IdAmbiente = $IdAmbienteDEV;
        //ambienteDev.AppPath = $txtAppDevPath.val();
        //ambienteDev.AppURL = $txtAppDevUrl.val();
        //ambienteDev.BaseServer = $txtBaseDevServer.val();
        //ambienteDev.BaseNombre = $txtBaseDevBase.val();
        //ambienteDev.BaseUsuario = $txtBaseDevUsuario.val();
        //ambienteDev.BasePass = $txtBaseDevPassword.val();

        //ambienteQa.IdProducto = $IDProducto;
        //ambienteQa.IdAmbiente = $IdAmbienteQA;
        //ambienteQa.AppPath = $txtAppQAPath.val();
        //ambienteQa.AppURL = $txtAppQAUrl.val();
        //ambienteQa.BaseServer = $txtBaseQAServer.val();
        //ambienteQa.BaseNombre = $txtBaseQABase.val();
        //ambienteQa.BaseUsuario = $txtBaseQAUsuario.val();
        //ambienteQa.BasePass = $txtBaseQAPassword.val();

        //ambienteProd.IdProducto = $IDProducto;
        //ambienteProd.IdAmbiente = $IdAmbientePROD;
        //ambienteProd.AppPath = $txtAppPRODPath.val();
        //ambienteProd.AppURL = $txtAppPRODUrl.val();
        //ambienteProd.BaseServer = $txtBasePRODServer.val();
        //ambienteProd.BaseNombre = $txtBasePRODBase.val();
        //ambienteProd.BaseUsuario = $txtBasePRODUsuario.val();
        //ambienteProd.UsuEmer = $txtBasePRODUsuEmer.val();

        var listadoModulos = [];

        for (var i = 0; i < $listaModulos.length; i++) {
            listadoModulos.push({
                IdProducto: $listaModulos[i][0],
                IdModulo: $listaModulos[i][1],
                Nombre: $listaModulos[i][2],
                Descripcion: $listaModulos[i][3]
            });
        }

        if (validateForm()) {
            $.ajax({
                url: $urlSaveProducto,
                data: { productoModel: productoModel, listaModulos: listadoModulos, listaNegocios: listadoNegocios, listaProcesos: listadoProcesos },
                method: 'POST',
                success: function (id) {
                    clean();
                    showSuccessNotification("Producto", resources.messages.successSave.replace('{0}', 'Producto'));
                    if ($IDProducto == 0) {
                        $IDProducto = id;
                        if ($selAmbiente.val() != 0 && $selRolAmbiente.val() != 0)
                            AgregarAmbiente();
                        window.location.replace($urlEditProducto + id);
                    } else {
                        window.location.replace($urlIndexProducto);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                }
            });
        } else {
            showAlertNotification("Producto", resources.messages.camposObligatorios)
        }
    }

    function EnableCmbProcesoNegocio() {

        if ($selCicloAplicativo.val() == 0) {
            $cmbProceso.data('kendoMultiSelect').value(null);
            $cmbNegocio.data('kendoMultiSelect').value(null);

            $cmbNegocio.data('kendoMultiSelect').enable(false);
            $cmbProceso.data('kendoMultiSelect').enable(false);
            return;
        }

        if ($selCicloAplicativo.val() != 0) {
            $cmbNegocio.data('kendoMultiSelect').enable(true);
            $cmbProceso.data('kendoMultiSelect').enable(true);
        }

        let cmbProcesos = $("#cmbProceso").data("kendoMultiSelect");

        $.ajax({
            url: $urlGetProcesos,
            data: { id: $selCicloAplicativo.val() },
            method: 'GET',
            success: function (data) {
                $cmbProceso.data('kendoMultiSelect').value(null);
                var dataSource = new kendo.data.DataSource({ data: data });
                cmbProcesos.setDataSource(dataSource);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                showErrorNotification(thrownError);
            }
        });
    }
    //Elimina un ambiente
    function deleteAmbiente(id) {
        console.log(id);
        //$.ajax({
        //    url: $urlDeleteProducto,
        //    data: { id: id },
        //    method: 'POST',
        //    success: function (data) {
        //        if (data.success)
        //            showSuccessNotification("Producto", "<div style='word-wrap: break-word; white-space: normal;'>" + data.message + "</div>");
        //        else
        //            showAlertNotification("Producto", "<div style='word-wrap: break-word; white-space: normal;'>" + data.message + "</div>");
        //        var grid = $gridProductos.data("kendoGrid");
        //        grid.dataSource.read();
        //    },
        //    error: function (xhr, ajaxOptions, thrownError) {
        //        showErrorNotification(thrownError);
        //    }
        //});
    }
    //function setCmbProcesos(data) {

    //    $("#cmbProceso").kendoMultiSelect({
    //        dataTextField: "Descripcion",
    //        dataValueField: "Id",
    //        dataSource: data,
    //        filter: "contains",
    //        placeholder: "Seleccione el Proceso...",
    //        downArrow: true,
    //    });
    //}

    function SetProductoPlataforma() {

        let aux = $selProductoPlataforma.data('kendoDropDownList');
        //if ($('#EsProducto').is(":checked")) {
        //    aux.enable(true);
        //}
        if ($('#EsPlataforma').is(":checked")) {

            aux.enable(false);
            aux.value(null);
        }
        if ($('#EsProducto').is(":checked")) {

            aux.enable(true);
        }
    }

    function SetModo() {
        if ($('#OnPremise').is(":checked")) {
            $chkModo = "P"
        } else if ($('#Cloud').is(":checked")) {
            $chkModo = "C";
        } else if ($('#Hibrido').is(":checked"))
        {
            $chkModo = "H";
        }
    }

    function HasValue(data) {
        switch (data){
            case null:
            case undefined:
            case '':
            case 0:
                return false;
                break
        }
        if (Number.isInteger(Number.parseInt(data)))
            return true;
        return false;
    }

    ///
    var dialog = $("#dialog");
    var show = $("#show");

    dialog.kendoDialog({
        width: "400px",
        //title: "Ambiente",
        closable: true,
        modal: false,
        content: "<p>Es necesario guardar el nuevo <strong>Producto</strong> para crear su Ambiente.<br/>¿Desea continuar?<p>",
        actions: [
            { text: 'No', action: onCancel },
            { text: 'Sí', primary: true, action: onOK }
        ],
        initOpen: onInitOpen,
        open: onOpen,
        close: onClose,
        show: onShow,
        hide: onHide
    });

    function onInitOpen(e) {
        //kendoConsole.log("event :: initOpen");
    }

    function onOpen(e) {
        //kendoConsole.log("event :: open");
    }

    function onShow(e) {
        //kendoConsole.log("event :: show");
    }

    function onHide(e) {
        //kendoConsole.log("event :: hide");
    }

    function onClose(e) {
        show.fadeIn();
        //kendoConsole.log("event :: close");
    }
    function onCancel(e) {
      //k  kkendoConsole.log("action :: cancel");
    }

    function onOK(e) {
        save();
    }

    show.click(function () {
        if (!validarAmbienteForm())
            return;
        dialog.data("kendoDialog").open();
        show.fadeOut();
    });
    ///
    function AgregarAmbiente() {
        if (!validarAmbienteForm())
            return;

        ProductoAmbienteModel.IdProducto = $IDProducto;
        ProductoAmbienteModel.IdAmbiente = $selAmbiente.val();
        ProductoAmbienteModel.IdRolAmbiente = $selRolAmbiente.val();
        ProductoAmbienteModel.Server = $txtServer.val();
        ProductoAmbienteModel.Nombre = $txtNombreAmb.val();
        ProductoAmbienteModel.Usuario = $txtUsuario.val();
        ProductoAmbienteModel.UsuarioEmer = $txtUsuarioEmer.val();
        ProductoAmbienteModel.Comentario = document.getElementById("Comentario").value;

        let gridAmbientes = $('#gridAmbientes').data('kendoGrid');
        $.ajax({
            url: $urlAddAmbiente,
            data: { model: ProductoAmbienteModel },
            method: 'POST',
            success: function () {

                gridAmbientes.dataSource.read();

                showSuccessNotification("Ambiente", resources.messages.successSave.replace('{0}', 'Ambiente'));
                CleanAmbientesForm();
                document.getElementById("btnAmbiente").value = "Agregar";
                ProductoAmbienteModel.Id = null;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                showErrorNotification(thrownError);
            }
        });
    }

    function validarAmbienteForm(){

        if ($selAmbiente.val() == 0 || $selRolAmbiente.val() == 0) {

            showAlertNotification("Ambiente", "Faltan campos obligatorios");

            $selAmbiente.val() == 0 ? $selAmbiente.data('kendoDropDownList').focus() : $selRolAmbiente.data('kendoDropDownList').focus();

            return false;
        }
        return true;
    }
    function CleanAmbientesForm() {

        $selAmbiente.data('kendoDropDownList').value(null);
        $selRolAmbiente.data('kendoDropDownList').value(null);
        $txtServer.val(constants.string_empty);
        $txtNombreAmb.val(constants.string_empty);
        $txtUsuario.val(constants.string_empty);
        $txtUsuarioEmer.val(constants.string_empty);
        document.getElementById("Comentario").value = constants.string_empty;
    }

    function EditarAmbiente(data) {

        ProductoAmbienteModel.Id = data;
        $selAmbiente.data('kendoDropDownList').focus();

        var grid = $gridAmbientes.data('kendoGrid');

        var row = grid.dataSource.get(data);

        $selAmbiente.data('kendoDropDownList').value(row.IdAmbiente);
        $txtServer.val(row.Server);
        $txtUsuario.val(row.Usuario);
        $selRolAmbiente.data('kendoDropDownList').value(row.IdRolAmbiente);
        $txtNombreAmb.val(row.Nombre);
        $txtUsuarioEmer.val(row.UsuarioEmer);
        document.getElementById("Comentario").value  = row.Comentario;

        document.getElementById("formAmbiente").style.borderColor = "#00A7E3";

        document.getElementById("btnAmbiente").value = "Guardar Cambios";

    }
 
    //Inicializador
    function init(opts) {
        //Datos basicos
        $IDProducto = opts.IDProducto;
        $txtNombre = $(opts.txtNombre);
        $selCicloAplicativo = $(opts.selCicloAplicativo);
        $selObsolescencia = $(opts.selObsolescencia);
        $selServicio = $(opts.selServicio);
        $selTipoProducto = $(opts.selTipoProducto);
        $selSitio = $(opts.selSitio);
        $selAmbiente = $(opts.selAmbiente);
        $selRolAmbiente = $(opts.selRolAmbiente);
        $chkActivo = $(opts.chkActivo);
        $txtRespTI = $(opts.txtRespTI);
        $txtRespNegocio = $(opts.txtRespNegocio);
        $txtRespMesa = $(opts.txtRespMesa);
        $txtDescripcion = $(opts.txtDescripcion);

        $chkSox = $(opts.chkSox);
        $txtServicio = $(opts.txtServicio);
        $cmbNegocio = $(opts.cmbNegocio);
        $cmbProceso = $(opts.cmbProceso);
        $txtObsolescencia = $(opts.txtObsolescencia);
        $txtGerencia = $(opts.txtGerencia);
        $txtAgrupador = $(opts.txtAgrupador);
        $txtJefaturaTI = $(opts.txtJefaturaTI);
        $txtTipo = $(opts.txtTipo);
        $txtSitio = $(opts.txtSitio);

        $chkEsPlataforma = $(opts.chkEsPlataforma);
        $selProductoPlataforma = $(opts.selProductoPlataforma);
        $chkAccesoInternet = $(opts.chkAccesoInternet);
        $("[name='modo']").on('click', SetModo);

        $btnAmbiente = $(opts.btnAmbiente);
        $btnAmbiente.on('click', AgregarAmbiente);
        $txtServer = $(opts.txtServer);
        $txtUsuario = $(opts.txtUsuario);
        $txtUsuarioEmer = $(opts.txtUsuarioEmer);
        $txtNombreAmb= $(opts.txtNombreAmb);
        $txtComentario = $(opts.txtComentario);

        //$btnEditAmbiente = $(opts.btnEditAmbiente);
        //$btnEditAmbiente.on('click', EditarAmbiente);

        $['#EditarAmbiente'] = $(opts.btnEditAmbiente);
        $['#EditarAmbiente'].on('click', EditarAmbiente);

        ////Ambiente DEV
        //$IdAmbienteDEV = opts.IdAmbienteDEV;
        //$txtAppDevPath = $(opts.txtAppDevPath);
        //$txtAppDevUrl = $(opts.txtAppDevUrl);
        //$txtBaseDevServer = $(opts.txtBaseDevServer);
        //$txtBaseDevBase = $(opts.txtBaseDevBase);
        //$txtBaseDevUsuario = $(opts.txtBaseDevUsuario);
        //$txtBaseDevPassword = $(opts.txtBaseDevPassword);

        ////Ambiente QA
        //$IdAmbienteQA = opts.IdAmbienteQA;
        //$txtAppQAPath = $(opts.txtAppQAPath);
        //$txtAppQAUrl = $(opts.txtAppQAUrl);
        //$txtBaseQAServer = $(opts.txtBaseQAServer);
        //$txtBaseQABase = $(opts.txtBaseQABase);
        //$txtBaseQAUsuario = $(opts.txtBaseQAUsuario);
        //$txtBaseQAPassword = $(opts.txtBaseQAPassword);

        ////Ambiente PROD
        //$IdAmbientePROD = opts.IdAmbientePROD;
        //$txtAppPRODPath = $(opts.txtAppPRODPath);
        //$txtAppPRODUrl = $(opts.txtAppPRODUrl);
        //$txtBasePRODServer = $(opts.txtBasePRODServer);
        //$txtBasePRODBase = $(opts.txtBasePRODBase);
        //$txtBasePRODUsuario = $(opts.txtBasePRODUsuario);
        //$txtBasePRODUsuEmer = $(opts.txtBasePRODUsuEmer);

        
        //Obtiene los modulos ya existentes y los guarda en la variable global
        var list = opts.listaModulosExistentes;
        for (var i in opts.listaModulosExistentes) {
            $listaModulos.push([list[i]['IdProducto'], list[i]['IdModulo'], list[i]['Nombre'], list[i]['Descripcion']]);
        }

        //Modulos
        $btnAddModulo = $(opts.btnAddModulo);
        $btnAddModulo.on('click', openPopupAgregarModulo);
        $gridModulos = $(opts.gridModulos);

        $popupAgregarModulo = $(opts.popupAgregarModulo);
        $txtModuloNombre = $(opts.txtModuloNombre);
        $txtModuloDescripcion = $(opts.txtModuloDescripcion);

        $btnAgregarPopupAgregarModulo = $(opts.btnAgregarPopupAgregarModulo);
        $btnAgregarPopupAgregarModulo.on('click', agregarModulo);
        $btnCerrarPopupAgregarModulo = $(opts.btnCerrarPopupAgregarModulo);
        $btnCerrarPopupAgregarModulo.on('click', closePopupAgregarModulo);

        //Listado de Interfaces
        $popupListaInterfaces = $(opts.popupListaInterfaces);
        $gridInterfaces = $(opts.gridInterfaces);
        $btnOpenInterfaces = $(opts.btnOpenInterfaces);
        $btnOpenInterfaces.on('click', openPopupListaInterfaces);
        $btnCerrarPopupListaInterfaces = $(opts.btnCerrarPopupListaInterfaces);
        $btnCerrarPopupListaInterfaces.on('click', closePopupListaInterfaces);

        if ($IDProducto != 0) {
            $gridInterfaces.data("kendoGrid").dataSource.read();
        }

        $gridAmbientes = $(opts.gridAmbientes);

        if ($IDProducto != 0)
            $gridAmbientes.data('kendoGrid').dataSource.read();

        //Botones
        btnGuardar = $(opts.btnGuardar);
        btnGuardar.on('click', save);

        //URLs
        $urlSaveProducto = opts.urlSaveProducto;
        $urlIndexProducto = opts.urlIndexProducto;
        $urlDeleteModuloRelacionado = opts.urlDeleteModuloRelacionado;
        $urlGetProcesos = opts.urlGetProcesos;
        $urlAddAmbiente = opts.urlAddAmbiente;
        $urlEditProducto = opts.urlEditProducto;

        if (HasValue($selCicloAplicativo.val())) {
           $cmbProceso.data('kendoMultiSelect').enable(true);
           $cmbNegocio.data('kendoMultiSelect').enable(true);

           //if ($cmbProceso.val() == null) {
                //$.ajax({
                //    url: $urlGetProcesos,
                //    data: { id: $selCicloAplicativo.val() },
                //    method: 'GET',
                //    success: function (data) {
                //        var dataSource = new kendo.data.DataSource({ data: data });
                //        $("#cmbProceso").data("kendoMultiSelect").setDataSource(dataSource);
                //        setTimeout(function () {
                //            $("#cmbProceso").data("kendoMultiSelect").value($cmbProceso.val());

                //        }, 2000)
                //        //$("#cmbProceso").filter();
                //    },
                //    error: function (xhr, ajaxOptions, thrownError) {
                //        showErrorNotification(thrownError);
                //    }
                //});
            //}
        }

        $selCicloAplicativo.on("change", EnableCmbProcesoNegocio);

        $("[name='esPlataforma']").on('click', SetProductoPlataforma);

        if ($('#EsPlataforma').is(":checked")) {
            $selProductoPlataforma.data('kendoDropDownList').value(null);
            $selProductoPlataforma.data('kendoDropDownList').enable(false);
        }

        SetModo();
    }

    //Variables
    var $IDProducto;
    var $txtNombre;
    var $selCicloAplicativo;
    var $selObsolescencia;
    var $selTipoProducto;
    var $selServicio;
    var $selSitio;
    var $chkActivo;
    var $txtRespTI;
    var $txtRespNegocio;
    var $txtRespMesa;
    var $txtDescripcion;

    //Ambiente
    var $selAmbiente;
    var $selRolAmbiente;
    var $txtServer;
    var $txtUsuario;
    var $txtUsuarioEmer;
    var $txtNombreAmb;
    var $txtComentario;

    var $chkSox;
    var $txtServicio;
    var $cmbNegocio;
    var $cmbProceso;
    var $txtObsolescencia;
    var $txtGerencia;
    var $txtAgrupador;
    var $txtJefaturaTI;
    var $txtTipo;
    var $txtSitio;

    var $chkEsPlataforma;

    var $selProductoPlataforma;
    var $chkModo;
    let $chkAccesoInternet;

    //var $IdAmbienteDEV;
    //var $txtAppDevPath;
    //var $txtAppDevUrl;
    //var $txtBaseDevServer;
    //var $txtBaseDevBase;
    //var $txtBaseDevUsuario;
    //var $txtBaseDevPassword;

    //var $txtAppQAPath;
    //var $txtAppQAUrl;
    //var $txtBaseQAServer;
    //var $txtBaseQABase;
    //var $txtBaseQAUsuario;
    //var $txtBaseQAPassword;

    //var $txtAppPRODPath;
    //var $txtAppPRODUrl;
    //var $txtBasePRODServer;
    //var $txtBasePRODBase;
    //var $txtBasePRODUsuario;
    //var $txtBasePRODUsuEmer;

    var $btnAddModulo;
    var $gridModulos;

    var $popupAgregarModulo;
    var $txtModuloNombre;
    var txtModuloDescripcion;
    var btnAgregarPopupAgregarModulo;
    var btnCerrarPopupAgregarModulo;

    var $popupListaInterfaces;
    var $gridInterfaces;
    var $btnOpenInterfaces;
    var $btnCerrarPopupListaInterfaces;

    var $gridAmbientes;
    var $btnAmbiente
    var $btnEditAmbiente;
    var $btnGuardar;

    var $urlSaveProducto;
    var $urlIndexProducto;
    var $urlDeleteModuloRelacionado;
    var $urlGetProcesos;
    var $urlAddAmbiente;
    var $urlEditProducto;

    //Entidades
    var productoModel = {
        Id: null,
        Nombre: null,
        Descripcion: null,
        CicloAplicativoId: null,
        RespNegocio: null,
        RespMesa: null,
        RespTI: null,
        Activo: true
    }

    var ProductoAmbienteModel = {
        Id: null,
        IdProducto: null,
        IdAmbiente: null,
        IdRolAmbiente: null,
        Server: null,
        Nombre: null,
        Usuario: null,
        UsuarioEmer: null,
        Comentario: null,
    }

    //var ambienteDev = {
    //    IdProducto: null,
    //    IdAmbiente: null,
    //    AppPath: null,
    //    AppURL: null,
    //    BaseServer: null,
    //    BaseNombre: null,
    //    BaseUsuario: null,
    //    BasePass: null,
    //    UsuEmer: null
    //}

    //var ambienteQa = {
    //    IdProducto: null,
    //    IdAmbiente: null,
    //    AppPath: null,
    //    AppURL: null,
    //    BaseServer: null,
    //    BaseNombre: null,
    //    BaseUsuario: null,
    //    BasePass: null,
    //    UsuEmer: null
    //}

    //var ambienteProd = {
    //    IdProducto: null,
    //    IdAmbiente: null,
    //    AppPath: null,
    //    AppURL: null,
    //    BaseServer: null,
    //    BaseNombre: null,
    //    BaseUsuario: null,
    //    BasePass: null,
    //    UsuEmer: null
    //}

    var $listaModulos = []; 

    var module = {
        init: init,
        getListaModulos: getListaModulos,
        eliminarModulo: eliminarModulo,
        getFilterIdProducto: getFilterIdProducto,
        EditarAmbiente: EditarAmbiente
    }

    return module;
}());