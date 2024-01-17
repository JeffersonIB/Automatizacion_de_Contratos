﻿
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


var end = new Date('04/15/2023 11:59 PM');
var _second = 1000;
var _minute = _second * 60;
var _hour = _minute * 60;
var _day = _hour * 24;
var timer;

function showRemaining() {
    var now = new Date();
    var distance = end - now;
    if (distance < 0) {

        clearInterval(timer);
        document.getElementById('countdown').innerHTML = 'AEXPIRED!';

        return;
    }
    var days = Math.floor(distance / _day);
    var hours = Math.floor((distance % _day) / _hour);
    var minutes = Math.floor((distance % _hour) / _minute);
    var seconds = Math.floor((distance % _minute) / _second);

    document.getElementById('countdown').innerHTML = days + ' dias, ';
    document.getElementById('countdown').innerHTML += hours + ' horas, ';
    document.getElementById('countdown').innerHTML += minutes + ' minutos y ';
    document.getElementById('countdown').innerHTML += seconds + ' segundos';
}

timer = setInterval(showRemaining, 1000);
