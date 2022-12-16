jQuery(function ($) {
    $('form').submit(function (e) {
        SiteTools.ShowLoading();
    });

    $('#tablaClients').DataTable();

    $(document).on('click', ".create-btn", function (e) {
        $('#txt_userclient').val('');
        $('#txt_firstname').val('');
        $('#txt_seconname').val('');
        $('#txt_lastname').val('');
        $('#txt_charge').val('');
        $('#txt_phonenumber').val('');
        $('#txt_email').val('');
        $('#ddl_contract').val('');
        $('#btncreate').removeClass('hide-control');
        $('#btnupdate').addClass('hide-control');
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

        PostOrUpdate('POST', urlCreate, ClientDTO);
    });

    $(document).on('click', ".update-modal", function (e) {
        $('#hdIdClient').val($(this).attr('data-id-client'));
        $('#btncreate').addClass('hide-control');
        $('#btnupdate').removeClass('hide-control');
        SiteTools.ShowLoading();
        $.ajax({
            url: urlGetById,
            type: 'GET',
            cache: false,
            data: { pId: $('#hdIdClient').val() }
        }).done(function (result) {
            SiteTools.HideLoading();
            $('#txt_userclient').val(result.userClient);
            $('#txt_firstname').val(result.firstName);
            $('#txt_seconname').val(result.secondName);
            $('#txt_lastname').val(result.lastName);
            $('#txt_charge').val(result.charge);
            $('#txt_phonenumber').val(result.phoneNumber);
            $('#txt_email').val(result.email);
            $('#ddl_contract').val(result.idContract);
        }).fail(function (data) {
            catchError(data);
        });
    });

    $(document).on('click', ".update-client", function (e) {
        e.preventDefault();
        if (!SiteTools.CamposRequeridosClass('Campo requerido'))
            return false;

        var ClientDTO = {
            IdClient: $('#hdIdClient').val(),
            UserClient: $("#txt_userclient").val(),
            FirstName: $("#txt_firstname").val(),
            SecondName: $("#txt_seconname").val(),
            LastName: $("#txt_lastname").val(),
            Charge: $("#txt_charge").val(),
            PhoneNumber: $("#txt_phonenumber").val(),
            Email: $("#txt_email").val(),
            IdContract: $("#ddl_contract").val()
        };

        PostOrUpdate('PUT', urlUpdate, ClientDTO);
    });

    $(document).on('click', ".delete-client", function (e) {
        e.preventDefault();
        $('#hdIdClient').val($(this).attr('data-id-client'));
        Delete(this);
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

    function PostOrUpdate(method, url, model) {
        var token = $('input[name="__RequestVerificationToken"]').val();
        SiteTools.ShowLoading();
        $.ajax({
            url: url,
            type: method,
            data: {
                __RequestVerificationToken: token,
                pModel: JSON.stringify(model)
            },
            success: function (result) {
                $('#modal-form-create-client').modal('hide');
                if ($.trim(result.status) == 'true') {
                    SiteTools.Messages('Clients administration.', result.mesaje, 'gritter-success');
                    FindClients();
                }
                else {
                    SiteTools.HideLoading();
                    SiteTools.Messages('Clients administration.', result.mesaje, 'gritter-warning');
                }
            },
            error: function (error) {
                catchError(error);
            }
        });
    }

    function Delete(control) {
        if (confirm(`Are you sure you want to delete the client ${$(control).attr('data-nombre')}?`) == true) {
            var token = $('input[name="__RequestVerificationToken"]').val();
            SiteTools.ShowLoading();
            $.ajax({
                url: urlDelete,
                type: 'POST',
                cache: false,
                data: {
                    __RequestVerificationToken: token,
                    pId: $('#hdIdClient').val()
                }
            }).done(function (result) {
                SiteTools.HideLoading();
                SiteTools.Messages('Administración de clients.', result.mesaje, 'gritter-success');
                FindClients();
            }).fail(function (data) {
                catchError(data);
            });
        }
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
