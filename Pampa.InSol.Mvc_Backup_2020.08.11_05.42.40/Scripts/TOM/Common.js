kendo.culture("es-AR");

function isUndefined(func) {
    return (typeof func == 'undefined')
}

function isEmpty(v, allowBlank) {
    if (typeof v === 'object' || typeof v === 'function') {
        var i;
        for (i in v) {
            if (v.hasOwnProperty(i)) {
                return false;
            }
        }
        return true;
    } else {
        // not an object or function - same as before
        return (v === null || (v == undefined) || (!allowBlank ? v === '' : false));
    }
};

function SetLoading(targetId) {

    var options = {
        opacity: 0.7,
        classOveride: false
    };

    var target = $('#' + targetId);
    return new ajaxLoader(target, options);
};

function setMarkers(point) {
    if (point.dataItem.ProblemaMensaje == null || point.dataItem.ProblemaMensaje == "") {
        return null;
    }
    else {

        var draw = kendo.drawing;
        var signo = $("#signoDeExclamacion").val();
        var image = new draw.Image(signo, point.rect);

        return image;
    }
}


function toggleHandler(e) {
    e.preventDefault();
    var visual = e.visual;
    if (visual != null) {
        var transform = null;
        if (e.show) {
            transform = kendo.geometry.transform().scale(1.5, 1.5, visual.rect().center());
        }
        visual.transform(transform);
    }
}

function casingLabelVisible(point) {
    return !point.dataItem.EsEspejado;
}


function setLegendSymbol(e) {
    if (e.series.color == "white") {
        return null;
    }
    else {
        return e.createVisual();
    }
}

function ocultarSerieProblemas() {
    if ($("#txtVerProblemas").text() == " Ver problemas") {
        $("#txtVerProblemas").text(" Ocultar problemas")
    }
    else {
        $("#txtVerProblemas").text(" Ver problemas")
    }
    $("#ProfundidadVsTiempo").getKendoChart()._legendItemClick(0);
}

/**
Devuelve el index de una columna de la grilla de Kendo dado el nombre del campo que esta bindeado
 */
function GetKendoGridColumnIndexFromFieldName($gridToSearch, columnFieldName) {
    return $gridToSearch.find('.k-grid-header [data-field=' + columnFieldName + ']').index();
}

var recalcularGridSize = function ($grilla) {
    var gridElement = $grilla,
    newHeight = gridElement.innerHeight(),
    otherElements = gridElement.children().not(".k-grid-content"),
    otherElementsHeight = 0;

    otherElements.each(function () {
        otherElementsHeight += $(this).outerHeight();
    });

    gridElement.children(".k-grid-content").height(newHeight - otherElementsHeight);
}

function showSuccessNotification(title, message) {
    $('#myModal').modal('hide');
    if (message != null) {
        $("#popupNotification").data("kendoNotification").show({ title: title, message: message }, "success");
    }
    else {
        $("#popupNotification").data("kendoNotification").show({ title: title }, "success");
    }
}

function showErrorNotification(message) {
    $("#popupNotification").data("kendoNotification").show({ message: message }, "error");
}

function showAlertNotification(title, message) {
    $("#popupNotification").data("kendoNotification").show({ title: title, message: message }, "alert");
}

function showInfoNotification(title, message) {
    $("#popupNotification").data("kendoNotification").show({ title: title, message: message }, "info");
}

function setDataInfo(button) {

    var msg = $(button).data("confirmMsg");
    var title = $(button).data("confirmTitle");

    $('#msg').text(msg);
    $('#confirmation_wnd_title').text(title);
}

function setConfirm(button) {
    var kendoWindow = $('#confirmation').kendoWindow({
        resizable: false,
        modal: true
    });

    kendoWindow.data("kendoWindow")
        .content($("#modal-confirmation").html());

    setDataInfo(button);

    kendoWindow.data("kendoWindow")
        .center().open();

    kendoWindow
        .find(".modal-confirm,.modal-cancel")
            .click(function () {
                if ($(this).hasClass("modal-confirm")) {
                    setConfirmAction(button);
                }

                kendoWindow.data("kendoWindow").close();
            })
            .end();
}

function createConfirm(msg, title, action) {
    var kendoWindow = $('#confirmation').kendoWindow({
        resizable: false,
        modal: true
    });

    kendoWindow.data("kendoWindow")
        .content($("#modal-confirmation").html());

    $('#msg').text(msg);
    $('#confirmation_wnd_title').text(title);

    kendoWindow.data("kendoWindow")
        .center().open();

    kendoWindow
        .find(".modal-confirm")
            .click(function () {
                eval(action);
                kendoWindow.data("kendoWindow").close();
            })
    .end();
    kendoWindow
        .find(".modal-cancel")
            .click(function () {
                kendoWindow.data("kendoWindow").close();
            })
    .end();
}


function setConfirmAction(button) {
    var result = $(button).data("action");
    if (result == "submit") {
        var formId = $(button)[0].form.id;
        result = "$('#" + formId + "').submit()";
    }
    return eval(result);
}

function setConfirm(button) {
    var kendoWindow = $('#confirmation').kendoWindow({
        resizable: false,
        modal: true
    });

    kendoWindow.data("kendoWindow")
        .content($("#modal-confirmation").html());

    setDataInfo(button);
    $('#confirmation_wnd_title').prepend("<i class='glyphicon glyphicon-info-sign'></i>");
    kendoWindow.data("kendoWindow")
        .center().open();

    kendoWindow
        .find(".modal-confirm,.modal-cancel")
            .click(function () {
                if ($(this).hasClass("modal-confirm")) {
                    setConfirmAction(button);
                }

                kendoWindow.data("kendoWindow").close();
            })
            .end();
}
function loadConfirmDialog(buttonId) {
    var element = "#" + buttonId;
    setConfirm(element);
}


function createWarning(msg, title) {
    var kendoWindow = $('#warning').kendoWindow({
        resizable: false,
        modal: true
    });

    kendoWindow.data("kendoWindow")
        .content($("#modal-warning").html())

    $('#msgwarning').text(msg);
    $('#warning_wnd_title').text(title);
    $('#warning_wnd_title').prepend("<i class='fa fa-exclamation-triangle'></i>");
    kendoWindow.data("kendoWindow")
        .center().open();

    kendoWindow
        .find(".modal-cancel")
            .click(function () {
                kendoWindow.data("kendoWindow").close();
            })
            .end();
}

function setWarning(button) {

    createWarning($(button).data("warningMsg"), $(button).data("warningTitle"));
}

function loadWarningDialog(buttonId) {
    var element = "#" + buttonId;
    setWarning(element);
}

var autoSelectInputContent = function () {
    //wire focus of all numerictextbox widgets on the page
    $("input[type=text]").bind("focus", function () {
        var input = $(this);
        clearTimeout(input.data("selectTimeId")); //stop started time out if any

        var selectTimeId = setTimeout(function () {
            input.select();
        });

        input.data("selectTimeId", selectTimeId);
    }).blur(function (e) {
        clearTimeout($(this).data("selectTimeId")); //stop started timeout
    });
};

$(function () {
    /// Esto hace que todas las cajas de Kend al recibir el foto tengan su contenido seleccionado
    autoSelectInputContent();

    //// Comportamiento del spinner
    $(document).on({
        ajaxStart: function () { $(".loading_spinner").fadeIn(); },
        ajaxStop: function () { $(".loading_spinner").fadeOut(); }
    });
    $(window).load(function () {
        $(".loading_spinner").fadeOut(1000);
    })
    /// Manejo global de errores no manejados de ajax
    $.ajaxSetup({
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 403)
            {
                showErrorNotification('Su usuario no tiene permisos para ejecutar la acción solicitada');
                return;
            }

            showErrorNotification('Ocurrio un error inesperado');
        }
    });
})