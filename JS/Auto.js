function ajustarAnchoNotas() {
    var gridViewWidth = document.querySelector('#<%= gvIndicadores.ClientID %>').clientWidth;
    var DivNotas = document.querySelector('#notas');
    DivNotas.style.width = gridViewWidth + 'px';
}

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

function ShowModalDt() {
    $("#Modal_Detalle").modal("show");
}
function CloseModalDt() {
    $("#Modal_Detalle").modal("hide");
}

function ShowModalAu() {
    $("#Modal_Autorizar").modal("show");
}
function CloseModalAu() {
    $("#Modal_Autorizar").modal("hide");
}

function ShowModalEl() {
    $("#Modal_Eliminar").modal("show");
}
function CloseModalEl() {
    $("#Modal_Eliminar").modal("hide");
}

$('#btnActualizar_Click').click(function () {
    $('#Modal_Agregar').find('input[type=text]').each(function () {
        var id = $(this).data('id');
        var valor = $(this).val();
        // aquí puedes llamar a una función de C# para guardar el valor en la base de datos si ha sido modificado
        if (valor !== '') {
            $.ajax({
                type: "POST",
                url: "ActualizarValor.aspx/Actualizar",
                data: JSON.stringify({ id: id, valor: valor }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                },
                error: function (xhr, status, error) {
                    console.log(error);
                }
            });
        }
    });
});

 