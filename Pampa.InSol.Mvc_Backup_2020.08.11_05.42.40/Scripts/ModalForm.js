$(function () {
    $.ajaxSetup({ cache: false });

    $("a[data-modal]").on("click", function (e) {

        // hide dropdown if any
        $(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        if (e.target.id.includes("Fin")) {
            $('#tipoMedicion').val("Final");
        } else {
            $('#tipoMedicion').val("Inicial");
        }

        $('#myModalContent').load(this.href, function () {

            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false
            }, 'show');

            bindForm(this, "formularioIngresoMedicion");
        });

        return false;
    });
});

function bindForm(dialog, form) {

    $('#' + form, dialog).submit(function () {
        event.preventDefault();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    var mensaje = "Volumen ambiente: " + result.Volumen + "m3 \n" +
                    "Volumen a 15: " + result.VolumenA15 + "m3 \n" +
                    "Masa: " + result.Masa + "TN";
                    var tipo = $('#tipoMedicion').val();

                    if (tipo && tablaMedicion) {
                        tablaMedicion.cargar(tipo, result);
                        var $row = $("#datosMedicion_" + tipo);
                        $row.find("#esNuevaMedicion").html(1);
                    }
                    
                    showSuccessNotification("Medición", mensaje);
                } else {
                    $("#myModalContent").html(result);
                    bindForm(dialog, form);
                }
            }
        });

        return false;
    });


}
