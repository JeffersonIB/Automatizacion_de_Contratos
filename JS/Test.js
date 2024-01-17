
//function () {
//    // Obtener el modal
//    var modal = $('#Modal_Calificar');

//    // Agregar el atributo "data-backdrop"
//    modal.attr('data-backdrop', 'static');

//    // Obtener la tabla que contiene el calendario
//    var tablaCalendario = $('#tablaCalendario');

//    // Evitar que se cierre el modal al hacer clic en la tabla
//    tablaCalendario.click(function (event) {
//        event.stopPropagation();
//    });

//    // Evitar que se cierre el modal al hacer clic en el botón de cerrar
//    $('#btnCerrarModal').click(function (event) {
//        event.stopPropagation();
//        modal.modal('hide');
//    });

//    // Evitar que se cierre el modal al seleccionar una fecha del calendario
//    $('#Calendar1').on('select', function (event) {
//        event.stopPropagation();
//    });
//}




function ShowModalAg() {
    $("#Modal_Agregar").modal("show");
}
function CloseModalAg() {
    $("#Modal_Agregar").modal("hide");
}

function ShowModalAc() {
    $("#Modal_Actualizar").modal("show");
}
function CloseModalAc() {
    $("#Modal_Actualizar").modal("hide");
}

function ShowModalCl() {
    $("#Modal_Calificar").modal("show");
}
function CloseModalCl() {
    $("#Modal_Calificar").modal("hide");
}

function ShowModalEl() {
    $("#Modal_Eliminar").modal("show");
}
function CloseModalEl() {
    $("#Modal_Eliminar").modal("hide");
}
