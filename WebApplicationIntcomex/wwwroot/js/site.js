// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Espacio de nombre  of function 
var SiteTools = {
    //--------------------------------------------------------------------------------------
    // Funcion que controla los mensajes de error, advertencia o satisfactorios.
    Messages: function (titulo, texto, className) {
        if (!!className) {
            $.gritter.add({
                title: titulo,
                text: texto,
                class_name: className
            });
        }
    },

    //--------------------------------------------------------------------------------------
    // Funcion valida una expreción regular y retor un true or false dependiendo la expreción.
    restrictBolean: function (restrict, valueKey, value, pMaxLength) {
        if (restrict != null && restrict != undefined) {
            var reg = ExpresionRegular(restrict);
            if (reg.test(valueKey)) {
                return false;
            }
            else {
                if (value != undefined && pMaxLength != undefined) {
                    if (value.length >= pMaxLength) {
                        return false;
                    }
                }
                return true;
            }
        }
        else {
            if (value != undefined && pMaxLength != undefined) {
                if (value.length >= pMaxLength) {
                    return false;
                }
            }
            return true;
        }
    },

    //--------------------------------------------------------------------------------------
    // Funcion que ajusta el valor del control dependiente la expreción regular.
    restrictValor: function (restrict, control, valorDefecto) {
        var reg = ExpresionRegular(restrict),
            contenido = $(control).val(),
            valorCorregido = contenido.replace(new RegExp(reg, 'g'), '').replace(/ +/g, ' ');

        valorCorregido = valorCorregido.trim();

        if (restrict == 'correo') {
            if (!reg.test(contenido)) {
                $(control).val('');
            }
            else {
                var vArray = [];
                vArray.push('@yopmail.', '@yahoo.com', '@memeil.', '@thraml.', '@arasj.', '@maildrop.', '@awdrt.', '@mailinator.', '@harakirimail.', '@spam4.'
                    , '@dispostable.', '@correotemporal.', '@sharklasers.', '@guerrillamail.', '@grr.', '@guerrillamailblock.', '@pokemail.'
                    , '@spamgourmet.', '@getnada.', '@trashmail.', '@spambog.', '@tempr.', '@discard.', '@discardmail.', '@0815.', '@btcmail.'
                    , '@hartbot.', '@freundin.', '@smashmail.', '@s0ny.', '@pecinan.', '@budaya-tionghoa.', '@lajoska.', '@1mail.', '@from.'
                    , '@ahk.', '@usako.', '@ichigo.', '@kkmail.', '@prin.', '@rapt.', '@bouncr.', '@moakt.', '@disbox.', '@tmpmal.', '@tmmails.'
                    , '@disbox.', '@tmail.', '@bareed.', '@mailforspam.', '@mailnesia.', '@mailnull', '@ch.MintEmail.', '@armyspy.', '@cuvox.'
                    , '@dayrep.', '@einrot.', '@fleckens.', '@gustr.', '@jourrapide.', '@rhyta.', '@superrito.', '@teleworm.', '@knol-power.', '@now.'
                    , '@spamcero.', '@tempemail.', '@slopsbox.', '@2prong.'
                );

                vArray.forEach(function (object) {
                    if (contenido.toLowerCase().indexOf(object.toLowerCase()) > -1)
                        $(control).val('');
                });
            }
        }
        else {
            if (valorCorregido != "")
                $(control).val(valorCorregido);
            else
                $(control).val(valorCorregido != '' ? valorCorregido : (valorDefecto != undefined ? valorDefecto : valorCorregido));
        }
    },

    //--------------------------------------------------------------------------------------
    // Funcion que valida campos requeridos por clase "CampoRequerido".
    CamposRequeridosClass: function (mensajeError, pCssName) {
        var returnError = true,
            cssName = pCssName == null ? "CampoRequerido" : pCssName;
        $(`.${cssName}`).each(function (index, item) {
            var controlid = item.id,
                typeControl = $(item).prop("type"),
                spError = controlid.replace("txt_", "sp_").replace("rb_", "sp_").replace("hd_", "sp_").replace("ddl_", "sp_");

            if (typeControl == "radio") {
                var control = controlid.replace("rb_", "hd_");
                item = $(`#${control}`)[0];
            }

            if (typeControl.indexOf('select') > -1) {
                if ($(item).val() == "") {
                    $(`#${spError}`).text(mensajeError);
                    $(`#${spError}`).removeClass('hide-control');
                    returnError = false;
                }
                else {
                    $("#" + spError).hide();
                }
            }
            else if ($(item).val() == "" || $(item).val() == null || $(item).val() == undefined) {
                $(`#${spError}`).text(mensajeError);
                $(`#${spError}`).removeClass('hide-control');
                returnError = false;
            }
            else {
                $(`#${spError}`).hide();
            }
        });

        return returnError;
    },

    // ---------------------------------------------------------------------------------------------
    // Show a loading gif, and block page
    ShowLoading: function () {
        $('.ModalProgress:first').css('display', 'block');
        $('#DivImgProgress').css('display', 'block');
    },

    // ---------------------------------------------------------------------------------------------
    // Hide a loading gif
    HideLoading: function () {
        $('.ModalProgress:first').css('display', 'none');
        $('#DivImgProgress').css('display', 'none');
    }
}
//--------------------------------------------------------------------------------------
// Funcion evalua las expreciones regulares.
function ExpresionRegular(name) {
    var preregex = {
        "letters": /[^A-zÀ-ú\s]+|[-!$%^&*()_+|~=`{}\[\]:";'<>?,.Ç\/]+/
        , "lettersAndNumbers": /[^A-zÀ-ú\d\\s]+|[\[\]_]+/
        , "numbers": /[^0-9]/
        , "correo": /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    };
    if (preregex[name] != undefined) {
        return preregex[name];
    }
    else {
        return name;
    }
}

//--------------------------------------------------------------------------------------
// Boquedo f12
//--------------------------------------------------------------------------------------
(function () {
    $(document).keydown(function (event) { if (event.keyCode == 123) { return false; } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { return false; } });
    $(document).on("contextmenu", function (e) { e.preventDefault(); });
})()

