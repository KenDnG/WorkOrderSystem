jQuery(document).ready(function () {
    var numCollections = $(".inputnum").length;
    for (var i = 0; i < numCollections; i++) {
        var numResult = $(".inputnum")[i].value.slice(0, -3);
    }
    $(".inputnum").keyup(function () {
        this.value = Common.Formater.Rupiah(this.value);
    });
});

var Common = {
    /*** Sweet Alert ***/
    Alert: {
        Warning: function (sa_message) {
            swal({
                title: "Oops!",
                text: sa_message,
                type: "warning",
                allowOutsideClick: true,
                confirmButtonClass: "btn-warning"
            });
        },
        Error: function (sa_message) {
            swal({
                title: "Failed",
                text: sa_message,
                type: "error",
                allowOutsideClick: true,
                confirmButtonClass: "btn-error"
            });
        },
        ErrorFromModal: function (sa_message, sa_modal_id) {
            $('#' + sa_modal_id).modal('hide');
            swal({
                title: "Failed",
                text: sa_message,
                type: "error",
                allowOutsideClick: false,
                confirmButtonClass: "btn-error"
            }, function (isConfirm) {
                if (isConfirm) {
                    $('#' + sa_modal_id).modal('show');
                }
            });
        },
        Info: function (sa_message) {
            swal({
                title: "Information",
                text: sa_message,
                type: "info",
                allowOutsideClick: true,
                confirmButtonClass: "btn-info"
            });
        },
        Success: function (sa_message) {
            swal({
                title: "Success",
                text: sa_message,
                type: "success",
                allowOutsideClick: true,
                confirmButtonClass: "btn-success"
            });
        },
        SuccessThenRoute: function (sa_message, sa_url) {
            swal({
                title: "Success",
                text: sa_message,
                type: "success",
                allowOutsideClick: false,
                confirmButtonClass: "btn-success"
            }, function (isConfirm) {
                if (isConfirm) {
                    window.location.href = sa_url;
                }
            });
        },
        SuccessThenBack: function (sa_message) {
            swal({
                title: "Success",
                text: sa_message,
                type: "success",
                allowOutsideClick: false,
                confirmButtonClass: "btn-success"
            }, function (isConfirm) {
                if (isConfirm) {
                    window.history.back();
                }
            });
        },
        SuccessThenRefresh: function (sa_message) {
            swal({
                title: "Success",
                text: sa_message,
                type: "success",
                allowOutsideClick: false,
                confirmButtonClass: "btn-success"
            }, function (isConfirm) {
                if (isConfirm) {
                    window.location.reload(true);
                }
            });
        },
        WarningThenBack: function (sa_message) {
            swal({
                title: "",
                text: sa_message,
                type: "warning",
                allowOutsideClick: false,
                confirmButtonClass: "btn-warning"
            }, function (isConfirm) {
                if (isConfirm) {
                    window.history.back();
                }
            });
        },
    },
    Toast: {
        ToastSuccess: function (title, msg, subtitle) {
            $(document).Toasts('create', {
                class: 'bg-success',
                title: title,
                subtitle: subtitle,
                body: msg
            })
        }
    },
    SessManager: {
        Save: function (key, value) {
            sessionStorage.setItem(key, value);
        },
        Remove: function (key) {
            sessionStorage.removeItem(key);
        },
        Clear: function () {
            sessionStorage.clear();
        },
        GetSession: function (key) {
            sessionStorage.getItem(key);
        }
    },
    ValidatePage: {
        Error404: function () {
            var xname = sessionStorage.getItem("xname");
            if (xname == "" || xname == null) {
                window.location.href = "/PageNotFound";
            }
        }
    },
    Formater: {
        Rupiah: function (angka) {
            var number_string = angka.replace(/[^,\d]/g, '').toString(),
                split = number_string.split(','),
                sisa = split[0].length % 3,
                rupiah = split[0].substr(0, sisa),
                ribuan = split[0].substr(sisa).match(/\d{3}/gi);

            // tambahkan titik jika yang di input sudah menjadi angka ribuan
            if (ribuan) {
                separator = sisa ? '.' : '';
                rupiah += separator + ribuan.join('.');
            }

            rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
            return rupiah;
        }
    }
}

var XResponse = {
    Success: function (data) {
        if (data.result.status === "Success") {
            Common.Alert.Success(data.result.message);
        } else {
            Common.Alert.Error(data.result.message);
        }
    }
}

var Validator = {
    Email: {
        IsValid: function (email) {
            var params = {
                email: email
            }
            $.ajax({
                url: "",
                type: "POST",
                data: params,
                success: function (data) {
                    var isValid = data;;
                    if (!isValid) {
                        Common.Alert.Warning("");
                    }
                },
                error: function (error) {
                }
            });
        }
    }
}