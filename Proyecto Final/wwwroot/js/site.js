// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#divVotos").ready(function () {
    traerVotos()
    traerVotosPrev()
});


function traerVotos() {
    $.ajax({
        url: "https://7daystodie-servers.com/api/?object=servers&element=voters&key=7sHIa1nwSfD6vyFLmUV49reCIqaXFkpxtDf&month=current&format=json&limit=10",
        type: 'GET',
        dataType: 'json',
        crossDomain: true
    }).done(function (result) {
        var primero = true;
        $(result.voters).each(function (index) {             
            $("#divVotos").append(generarVotos(this, primero));
            if (primero) { primero = false }
        })        
        
    }).fail(function (xhr, status, error) {
        alert(error)
    });
}

function traerVotosPrev() {
    $.ajax({
        url: "https://7daystodie-servers.com/api/?object=servers&element=voters&key=7sHIa1nwSfD6vyFLmUV49reCIqaXFkpxtDf&month=previous&format=json&limit=10",
        type: 'GET',
        dataType: 'json',
        crossDomain: true
    }).done(function (result) {
        var primero = true;
        $(result.voters).each(function (index) {
            $("#divVotosPrev").append(generarVotos(this, primero));
            if (primero) { primero=false }
        })

    }).fail(function (xhr, status, error) {
        alert(error)
    });
}

function generarVotos(publicacion, prim) {
    

    if (!prim) {
        var tarjetaVotos = `
        <tr class="voteTr">
            <td class="voteTd text-center">${publicacion.votes}</td>
            <td class="voteTd text-center">${publicacion.nickname}</td>
        </tr>`
    } else {
        var tarjetaVotos = `
        <tr class="voteTr primC">
            <td class="voteTd text-center"><i class="fa-solid fa-star"></i>   <b class="">${publicacion.votes}</b></td>
            <td class="voteTd text-center"><i class="fa-sharp fa-solid fa-crown"></i>  <b class="">${publicacion.nickname}</b></td>
        </tr>`
    }
    


    
    return tarjetaVotos;
}

