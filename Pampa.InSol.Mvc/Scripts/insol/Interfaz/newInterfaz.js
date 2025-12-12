var newInterfaz = (function () {

    //Abre el popup para agregar un Comentario
    function openPopupAgregarComentario() {
        $popupAgregarComentario.data("kendoWindow").center();
        $popupAgregarComentario.data("kendoWindow").open();
    }

    //Agrega un nuevo comentario a la lista bitacora
    function agregarComentario() {
        if ($txtComentario.val() != null && $txtComentario.val() != '') {
            var fechaActual = new Date();
            //Agregarlo a la lista global
            let item = [0, 0, fechaActual, $usuarioActualId, $usuarioActual, $txtComentario.val()];
            $listaBitacora.push(item);

            $gridBitacora.data("kendoGrid").dataSource.read();
            closePopupAgregarComentario();
        }
        else {
            showAlertNotification("Interfaz", 'Debe ingresar un texto');
        }
    }

    //Elimina un comentario de la lista global
    function eliminarComentario(comentario, id) {
        const eliminarComentarioLocal = () => {
            var grid = $gridBitacora.data("kendoGrid");
            for (var i = 0; i < $listaBitacora.length; i++) {
                if (($listaBitacora[i][5] == comentario) || ($listaBitacora[i][0] == id)) {
                    $listaBitacora.splice(i, 1);
                    i = $listaBitacora.length;
                }
            }

            grid.dataSource.read();
            showSuccessNotification('Interfaz', 'Se quitó el comentario.');
        }

        if ($IDInterfaz == 0) {
            eliminarComentarioLocal();
        }
        else {
            $.ajax({
                url: $urlDeleteComentarioRelacionado,
                data: { idInterfaz: $IDInterfaz, idBitacoraInterfaz: id },
                method: 'POST',
                success: function () {
                    eliminarComentarioLocal();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                }
            });
        }
    }

    //Retorna la lista bitacora al backend
    function getListaBitacora() {      
        var list = [];

        for (var i = 0; i < $listaBitacora.length; i++) {
            list.push({
                Id: $listaBitacora[i][0],
                InterfazId: $listaBitacora[i][1],
                Fecha: $listaBitacora[i][2],
                IdUsuario: $listaBitacora[i][3],
                Usuario: $listaBitacora[i][4],
                Comentario: $listaBitacora[i][5]
            });
        }

        return { listaBitacora: list };
    }

    //Cierra el popup para agregar un Comentario
    function closePopupAgregarComentario() {
        $popupAgregarComentario.popUpClose();
        cleanPopUpAgregarComentario();
    }

    //Limpia los campos del popup de Agregar Comentario
    function cleanPopUpAgregarComentario() {
        $txtComentario.val(constants.string_empty);
    }

    //Filtro-seleccion Producto Origen - Obtencion de modulos
    function filterModulosOrigen() {
        return {
            idProducto: $("#selProductoOrigen").val()
        };
    }

    //Filtro-seleccion Producto Destino - Obtencion de modulos
    function filterModulosDestino() {
        return {
            idProducto: $("#selProductoDestino").val()
        };
    }

    //Validacion de campos
    function validateForm() {
        result = toggleInvalid(interfazModel.Nombre, $txtNombre);
        result &= toggleInvalid(interfazModel.FrecuenciaId, $selFrecuencia);
        result &= toggleInvalid(interfazModel.TipoInterfazId, $selTipoInterfaz);
        result &= toggleInvalid(interfazModel.IdProductoOrigen, $selProductoOrigen);
        result &= toggleInvalid(interfazModel.IdProductoDestino, $selProductoDestino);
        result &= toggleInvalid(interfazModel.TecnologiaId, $selTecnologia);
        result &= toggleInvalid(interfazModel.TransporteId, $selTransporte);

        return result;
    }

    //Limpiar campos
    function clean() {
        $txtDato.val(constants.string_empty);
        $txtNombre.val(constants.string_empty);
        $selTipoDato.val(constants.string_empty);
        $selCicloAplicativo.val(constants.string_empty);
        $selFrecuencia.val(constants.string_empty);
        $selTipoInterfaz.val(constants.string_empty);
        $selProductoOrigen.val(constants.string_empty);
        $selTecnologia.val(constants.string_empty);
        $selProductoDestino.val(constants.string_empty);
        $selModuloOrigen.val(constants.string_empty);
        $selTransporte.val(constants.string_empty);
        $selModuloDestino.val(constants.string_empty);
    }

    //Guardado
    function save() {
        interfazModel.Id = $IDInterfaz;
        interfazModel.Nombre = $txtNombre.val();
        interfazModel.CicloAplicativoId = $selCicloAplicativo.val();
        interfazModel.Dato = $txtDato.val();
        interfazModel.TipoDatoId = $selTipoDato.val();
        interfazModel.FrecuenciaId = $selFrecuencia.val();
        interfazModel.TipoInterfazId = $selTipoInterfaz.val();
        interfazModel.Activo = $chkActivo.is(':checked');
        interfazModel.IdProductoOrigen = $selProductoOrigen.val();
        interfazModel.TecnologiaId = $selTecnologia.val();
        interfazModel.IdProductoDestino = $selProductoDestino.val();
        interfazModel.IdModuloOrigen = $selModuloOrigen.val();
        interfazModel.TransporteId = $selTransporte.val();
        interfazModel.IdModuloDestino = $selModuloDestino.val();

        var listadoBitacora = [];

        for (var i = 0; i < $listaBitacora.length; i++) {
            listadoBitacora.push({
                Id: $listaBitacora[i][0],
                InterfazId: $listaBitacora[i][1],
                Fecha: $listaBitacora[i][2],
                IdUsuario: $listaBitacora[i][3],
                Usuario: $listaBitacora[i][4],
                Comentario: $listaBitacora[i][5]
            });
        }

        if (validateForm()) {
            $.ajax({
                url: $urlSaveInterfaz,
                data: { interfazModel: interfazModel, listaBitacora: listadoBitacora },
                method: 'POST',
                success: function (data) {
                    clean();
                    showSuccessNotification("Interfaz", resources.messages.successSave.replace('{0}', 'Interfaz'));
                    window.location.replace($urlIndexInterfaz);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    showErrorNotification(thrownError);
                }
            });
        } else {
            showAlertNotification("Interfaz", resources.messages.camposObligatorios)
        }
    }

    //Inicializador
    function init(opts) {
        $IDInterfaz = opts.IDInterfaz;
        $txtDato = $(opts.txtDato);
        $txtNombre = $(opts.txtNombre);
        $selTipoDato = $(opts.selTipoDato);
        $selCicloAplicativo = $(opts.selCicloAplicativo);
        $selFrecuencia = $(opts.selFrecuencia);
        $selTipoInterfaz = $(opts.selTipoInterfaz);
        $chkActivo = $(opts.chkActivo);
        $selProductoOrigen = $(opts.selProductoOrigen);
        $selTecnologia = $(opts.selTecnologia);
        $selProductoDestino = $(opts.selProductoDestino);
        $selModuloOrigen = $(opts.selModuloOrigen);
        $selTransporte = $(opts.selTransporte);
        $selModuloDestino = $(opts.selModuloDestino);

        //Datos de usuario actual
        $usuarioActualId = opts.usuarioActualId;
        $usuarioActual = opts.usuarioActual;

        //Obtiene los items de la bitacora ya existentes y los guarda en la variable global
        var list = opts.listaBitacoraExistente;
        for (var i in opts.listaBitacoraExistente) {
            $listaBitacora.push([list[i]['Id'], list[i]['InterfazId'], list[i]['Fecha'], list[i]['IdUsuario'], list[i]['Usuario'], list[i]['Comentario']]);
        }

        //Bitacora
        $btnAddComentario = $(opts.btnAddComentario);
        $btnAddComentario.on('click', openPopupAgregarComentario);
        $gridBitacora = $(opts.gridBitacora);

        $popupAgregarComentario = $(opts.popupAgregarComentario);
        $txtComentario = $(opts.txtComentario);
        $btnAgregarPopupAgregarComentario = $(opts.btnAgregarPopupAgregarComentario);
        $btnAgregarPopupAgregarComentario.on('click', agregarComentario);
        $btnCerrarPopupAgregarComentario = $(opts.btnCerrarPopupAgregarComentario);
        $btnCerrarPopupAgregarComentario.on('click', closePopupAgregarComentario);

        $btnGuardar = $(opts.btnGuardar);
        $btnGuardar.on('click', save);

        $urlSaveInterfaz = opts.urlSaveInterfaz;
        $urlIndexInterfaz = opts.urlIndexInterfaz;
        $urlDeleteComentarioRelacionado = opts.urlDeleteComentarioRelacionado;
    }

    var $IDInterfaz;
    var $txtDato;
    var $txtNombre;
    var $selTipoDato;
    var $selCicloAplicativo;
    var $selFrecuencia;
    var $selTipoInterfaz;
    var $chkActivo;
    var $selProductoOrigen;
    var $selTecnologia;
    var $selProductoDestino;
    var $selModuloOrigen;
    var $selTransporte;
    var $selModuloDestino;

    var $usuarioActualId;
    var $usuarioActual;

    var $btnAddComentario;
    var $gridBitacora;

    var $popupAgregarComentario;
    var $txtComentario;
    var $btnAgregarPopupAgregarComentario;
    var $btnCerrarPopupAgregarComentario;

    var $btnGuardar;

    var $urlSaveInterfaz;
    var $urlIndexInterfaz;
    var $urlDeleteComentarioRelacionado;

    //Entidades
    var interfazModel = {
        Id: null,
        Nombre: null,
        CicloAplicativoId: null,
        Dato: null,
        TipoDatoId: null,
        FrecuenciaId: null,
        TipoInterfazId: null,
        IdProductoOrigen: null,
        IdModuloOrigen: null,
        IdProductoDestino: null,
        IdModuloDestino: null,
        TecnologiaId: null,
        TransporteId: null,
        Activo: null
    }

    var $listaBitacora = [];

    var module = {
        init: init,
        filterModulosOrigen: filterModulosOrigen,
        filterModulosDestino: filterModulosDestino,
        getListaBitacora: getListaBitacora,
        eliminarComentario: eliminarComentario
    }

    return module;
}());