var noOrdenar = [{ 'bSortable': false, "bSearchable": false, 'aTargets': [5] }];

jQuery(function ($) {

    $('form').submit(function (e) {
        $(".modalCargando").show();
    });

    $('#tablaClients').DataTable();

    $(document).on('click', ".create-btn", function (e) {
        $('#txt_nombrec').val('');
        $('#txt_nombreb').val('');
        $('#ddl_Empresa').val('');
        $('#btncreate').removeClass('ocultarControl');
        $('#btnupdate').addClass('ocultarControl');
    });

    $(document).on('click', ".create-client", function (e) {
        e.preventDefault();

        if (!SiteTools.CamposRequeridosClass('Required field'))
            return false;

        var ClientDTO = {
            UserClient: $("#txt_userclient").val(),
            FirstName: $("#txt_firstname").val(),
            SecondName: $("#txt_seconname").val(),
            LastName: $("#txt_lastname").val(),
            Charge: $("#txt_charge").val(),
            PhoneNumber: $("#txt_phonenumber").val(),
            Email: $("#txt_email").val(),
            IdContract: $("#ddl_contract").val()
        };

        var token = $('input[name="__RequestVerificationToken"]').val();
        SiteTools.ShowLoading();
        $.ajax({
            url: urlCreate,
            type: 'POST',
            data: {
                __RequestVerificationToken: token,
                pModel: JSON.stringify(ClientDTO)
            },
            success: function (result) {
                $('#modal-form-create-client').modal('hide');                
                if ($.trim(result.Estado) == 'true') {
                    SiteTools.Messages('Administración de clients.', result.Mensaje, 'gritter-success');
                    FindClients();
                }
                else {
                    cerrar_cargando();
                    SiteTools.Messages('Administración de clients.', result.Mensaje, 'gritter-warning');
                }
            },
            error: function (error) {
                catchError(error);
            }
        });
    });

    $(document).on('click', ".modifica-modal", function (e) {
        $('#hdIdBlob').val($(this).attr('data-id-client'));
        $('#btncreate').addClass('ocultarControl');
        $('#btnupdate').removeClass('ocultarControl');
        abrir_cargando();
        $.ajax({
            url: urlGetById,
            type: 'GET',
            cache: false,
            data: { pId: $('#hdIdBlob').val() }
        }).done(function (result) {
            cerrar_cargando();
            $('#txt_nombrec').val(result.CONTAINERNAME);
            $('#txt_nombreb').val(result.BLODNAME);
            $('#txt_extension').val(result.EXTENSION);
            $('#ddl_Empresa').val(result.CODEMP);
        }).fail(function (data) {
            catchError(data);
        });
    });

    $(document).on('click', ".modificar-client", function (e) {
        e.preventDefault();

        if (!SiteTools.CamposRequeridosClass('Campo requerido')) {
            return false;
        }

        var FE_BLOBPATHS = {
            IDBLOBPATH: $('#hdIdBlob').val(),
            CONTAINERNAME: $('#txt_nombrec').val(),
            BLODNAME: $('#txt_nombreb').val(),
            EXTENSION: $('#txt_extension').val(),
            CODEMP: $('#ddl_Empresa').val()
        };

        var token = $('input[name="__RequestVerificationToken"]').val();
        abrir_cargando();
        $.ajax({
            url: urlUpdate,
            type: 'POST',
            cache: false,
            data: {
                __RequestVerificationToken: token,
                model: JSON.stringify(FE_BLOBPATHS)
            }
        }).done(function (result) {
            $('#modal-form-create-client').modal('hide');
            if ($.trim(result.Estado) == 'true') {
                SiteTools.Messages('Administración de clients.', result.Mensaje, 'gritter-success');
                FindClients();
            }
            else {
                cerrar_cargando();
                SiteTools.Messages('Administración de clients.', result.Mensaje, 'gritter-warning');
            }

        }).fail(function (data) {
            catchError(data);
        });
    });

    $(document).on('click', ".eliminar-client", function (e) {
        e.preventDefault();
        $('#hdIdBlob').val($(this).attr('data-id-client'));
        bootbox.confirm(`Esta seguro que desea eliminar la client ${$(this).attr('data-nombre')}?`, function (result) {
            if (result) {
                var token = $('input[name="__RequestVerificationToken"]').val();
                abrir_cargando();
                $.ajax({
                    url: urlDelete,
                    type: 'POST',
                    cache: false,
                    data: {
                        __RequestVerificationToken: token,
                        pId: $('#hdIdBlob').val()
                    }
                }).done(function (result) {
                    cerrar_cargando();
                    SiteTools.Messages('Administración de clients.', result.Mensaje, 'gritter-success');
                    FindClients();
                }).fail(function (data) {
                    catchError(data);
                });
            }
        });
    });

    function FindClients() {
        $.ajax({
            url: urlGetAll,
            type: 'GET',
            cache: false,
            success: function (result) {
                SiteTools.HideLoading();
                $('#tablaClients').dataTable().fnClearTable();
                $('#tablaClients').html(result);
                $('#tablaClients').DataTable();
            },
            error: function (error) {
                catchError(error);
            }
        });
    }

    function catchError(data) {
        SiteTools.HideLoading();
        var mensaje = '';
        if (data.status == 401) {
            mensaje = 'Acceso denegado. No autorizado.';
        }
        else if (data.status == 403) {
            mensaje = 'Prohibido. No tiene permisos para realizar la acción.';
        }
        else if (data.status == 404) {
            mensaje = 'No encontrado. Recurso no encontrado, por favor contacte al administrador de la aplicación.';
        }

        $.gritter.add({
            title: 'Error al realizar la accion.',
            text: mensaje,
            class_name: 'gritter-error'
        });
    }
});
