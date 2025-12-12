this.errores = "";
this.camposRequeridos = "";
this.numeroInvalido = "";
this.fechaInvalida = "";
this.comparaFechas = "";
this.comparaNumeros = "";
var labelText = "";
this.validator;

function validacion() {

    this.validarYConfirmar = function ($form) {
        var isValid = this.validar($form);

        if (isValid) {
            loadConfirmDialog(event.target.id);
        }
    }

    this.validar = function ($form) {

        this.validator = $($form.selector).kendoValidator({

            rules: {
                greaterdate: function (input) {
                    if (input.is("[data-greaterdate-msg]") && input.val() != "") {
                        var date = kendo.parseDate(input.val());
                        var otherDate = kendo.parseDate($("[name='" + input.data("greaterdateField") + "']").val());
                        return otherDate == null || otherDate.getTime() <= date.getTime();
                    }

                    return true;
                },
                minDate: function (input) {
                    if ((input.data("role") == "datetimepicker" || input.data("role") == "datepicker") && input.val() != "") {

                        var date = kendo.parseDate($(input).val());

                        return date < new Date();
                    }
                    return true;

                },

                mayorQue: function (input) {
                    if (input.is("[data-mayorque-msg]") && input.val() != "") {
                        var value = kendo.parseFloat(input.val());
                        var otherValue = kendo.parseFloat($("[name='" + input.data("mayorqueField") + "']").val());
                        return otherValue == null || value > otherValue
                    }

                    return true;
                },

                menorQue: function (input) {
                    if (input.is("[data-menorque-msg]") && input.val() != "") {
                        var value = kendo.parseFloat(input.val());
                        var otherValue = kendo.parseFloat($("[name='" + input.data("menorqueField") + "']").val());
                        return otherValue == null || value < otherValue
                    }

                    return true;
                },

                menorQue2: function (input) {
                if (input.is("[data-menorque2-msg]") && input.val() != "") {
                    var value = kendo.parseFloat(input.val());
                    var otherValue = kendo.parseFloat($("[name='" + input.data("menorque2Field") + "']").val());
                return otherValue == null || value < otherValue
            }

            return true;
        }

            },

            validate: function (e) {
                errores = "";
                camposRequeridos = "";
                numeroInvalido = "";
                comparaFechas = "";
                fechaInvalida = "";
                comparaNumeros = "";

                marcarCamposInvalidosConErroresAgrupados($form);

                acumularMensajeEnNotificationBar(camposRequeridos, numeroInvalido, fechaInvalida, comparaFechas, comparaNumeros);

                var staticNotification = $("#staticNotification").data("kendoNotification");
                staticNotification.getNotifications().remove();
                staticNotification.show(errores, "errorStyle");
            }

        }).data("kendoValidator");

        return this.validator.validate();
    }

    this.isNotValidKendoNumber = function ($input) {
        isNotValidKendoNumber($input);
    }

    this.isNotValidKendoDate = function ($input) {
        isNotValidKendoDate($input);
    }
}

function acumularMensajeEnNotificationBar(camposRequeridos, numeroInvalido, fechaInvalida, comparaFechas, comparaNumeros) {
    if (camposRequeridos != "") {
        errores += "<li> Campos obligatorios: " + camposRequeridos + "</li>";
    }

    if (numeroInvalido != "") {
        errores += "<li>Formato inválido: " + numeroInvalido + "</li>";
    }

    if (fechaInvalida != "") {
        errores += "<li>Fecha inválida: " + fechaInvalida + "</li>";
    }

    if (comparaFechas != "") {
        errores += comparaFechas;
    }

    if (comparaNumeros != "") {
        errores += comparaNumeros;
    }
}

function marcarCamposInvalidosConErroresAgrupados($form) {


    var kendoElements = $($form.selector).find("span.k-widget");

    agruparRequeridosDeRazor($form);

    $.each(kendoElements, function (key, value) {
        agregarEstiloCampoInvalidoKendo(value);
        agruparMensajesDeCamposRequeridos(value);
        agregarMensajesFormato(value);
        agregarMensajesComparaNumeros(value);
        agregarMensajesComparaFechas(value);
    });

}

function agruparMensajesDeCamposRequeridos(value) {

    var $input = $(value).find("input.k-invalid");

    if (($input.is("[data-val-required]") || $input.attr("required")) && $.trim($input.val()) === "") {

        this.camposRequeridos += getLabelInvalid($input, this.camposRequeridos);
    }
}

function agruparMensajesNumeroInvalido(value) {
    var $input = $(value).find("input.k-invalid");

    if (isNotValidKendoNumber($input)) {

        this.formatoInvalido += getLabelInvalid($input, this.formatoInvalido);
    }
}

function agruparMensajesFechaInvalida(value) {
    var $input = $(value).find("input.k-invalid");

    if (isNotValidKendoDate($input)) {

        this.fechaInvalida += getLabelInvalid($input, this.fechaInvalida);
    }
}

function agregarEstiloCampoInvalidoKendo(value) {

    var $input = $(value).find("input.k-invalid");
    var $span = $(value).find("span.k-dropdown-wrap, span.k-numeric-wrap, span.k-picker-wrap");
    if ($input.size() > 0) {
        $($span).addClass("widget-validation-error");
    } else {
        $($span).removeClass("widget-validation-error");
    }
}

function agruparRequeridosDeRazor($form) {

    var razorInputRequired = $($form.selector).find('input.k-invalid').each(function () {
        var $input = $(this)
        if ($input.attr('required') && $.trim($input.val() === "")) {

            this.camposRequeridos += getLabelInvalid($input, this.camposRequeridos);

            $input.addClass("widget-validation-error");

        } else {
            $input.removeClass("widget-validation-error");
        }
    });
}

function isNotValidKendoNumber($input) {
    var numberKendoElement = $("#" + $input.attr("id")).data("kendoNumericTextBox");

    return $input.is("[data-val-number]") && numberKendoElement && !numberKendoElement.value() && $.trim($input.val()) !== "";
}

function isValidKendoNumber($input) {
    var numberKendoElement = $("#" + $input.attr("id")).data("kendoNumericTextBox");

    return $input.is("[data-val-number]") && numberKendoElement && numberKendoElement.value() && $.trim($input.val()) !== "";
}

function isNotValidKendoDate($input) {
    var dateKendoElement = $("#" + $input.attr("id")).data("kendoDateTimePicker");
    return $input.is("[data-val-date]") && dateKendoElement && !dateKendoElement.value() && $.trim($input.val()) !== "";
}

function isValidKendoDate($input) {
    var dateKendoElement = $("#" + $input.attr("id")).data("kendoDateTimePicker");
    return $input.is("[data-val-date]") && dateKendoElement && dateKendoElement.value() && $.trim($input.val()) !== "";
}

function getLabelInvalid($input, contenedor) {

    if (contenedor && contenedor != "") {
        this.labelText = ", ";
    } else {
        this.labelText = "";
    }

    return this.labelText += $("label[for=" + $input.attr("id") + "] ").text();
}

function agregarMensajesComparaFechas(value) {
    var $input = $(value).find("input.k-invalid");

    if (isValidKendoDate($input)) {
        this.comparaFechas = "<li>" + $input.data("greaterdateMsg") + "</li>";
    }
}

function agregarMensajesComparaNumeros(value) {
    var $input = $(value).find("input.k-invalid");

    if (isValidKendoNumber($input)) {
        var mensajeMayor = $input.data("mayorqueMsg");
        var mensajeMenor = $input.data("menorqueMsg");
        var mensajeMenor2 = $input.data("menorque2Msg");
        var unidadDeMedida = $input.data("unidaddemedida");
        var mayorValue = kendo.parseFloat($("[name='" + $input.data("mayorqueField") + "']").val());
        var menorValue = kendo.parseFloat($("[name='" + $input.data("menorqueField") + "']").val());

        if (mensajeMayor) {
            this.comparaNumeros += "<li>" + mensajeMayor + " (" + mayorValue + unidadDeMedida +")";
            if (mensajeMenor) {
                this.comparaNumeros += " y "+  mensajeMenor + " (" + menorValue + unidadDeMedida + ") </li>";
            }
            else {
                this.comparaNumeros += "<li>";
            }
        }else if (mensajeMenor) {
            var otherValue = kendo.parseFloat($("[name='" + $input.data("menorqueField") + "']").val());
            this.comparaNumeros += "<li>" + mensajeMenor + " (" + otherValue + unidadDeMedida +")</li>";
        }
        if (mensajeMenor2) {
            var otherValue = kendo.parseFloat($("[name='" + $input.data("menorque2Field") + "']").val());
            this.comparaNumeros += "<li>" + mensajeMenor2 + " (" + otherValue + unidadDeMedida + ")</li>";
        }
    }
}

function agregarMensajesFormato(value) {

    agruparMensajesFechaInvalida(value);
    agruparMensajesNumeroInvalido(value);
}
