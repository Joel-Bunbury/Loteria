var cartas = [];
var tablas = [];
var owlCarrousel;
var ocultarEliminar = false;
var ordenarCartas = true;
var modalGanadores = new bootstrap.Modal(document.getElementById('ganadores'));

function GetCartas() {
    //Block();
    return axios.get("/Loteria/GetCarta").then(response => {
        response.data.forEach(element => {
            let carta = new CartaVM(element);
            cartas.push(carta);
        });

        CargarCartas();
        //UnBlock();
    }).catch(error => {
        //UnBlock();
        console.log(error);
    });
}

function GetTarjetas() {
    //Block();
    return axios.get("/Loteria/GetTarjetas").then(response => {
        response.data.forEach(element => {
            let tabla = new TablaVM(element);
            tablas.push(tabla);
        });

        ActualizarTablas()
        //UnBlock();
    }).catch(error => {
        //UnBlock();
        console.log(error);
    });
}
Block();
//Evento al cargar pagina    
window.addEventListener('load', function () {
    
    let cartas = GetCartas();
    let tarjetas = GetTarjetas();
    Promise.all([cartas, tarjetas]).then(values => {
        console.log(values); // [3, 1337, "foo"]
        UnBlock();
    });
});


function CargarCartas() {
    let divCarta = document.getElementById("cartas");
    cartas.forEach(element => {
        var marcado
        let carta = `<div class="card" id="carta${element.cartaID}" onclick="SelectCarta(${ element.cartaID })">
                        <img src="${element.imagen}" class="card-img-top" alt="${element.nombre}">
                        <div class="card-body">
                        <h5 class="card-title">${element.nombre}</h5>
                        <p class="card-text">${element.lema}</p>
                    </div>`;
        divCarta.innerHTML = divCarta.innerHTML + carta;
    });

    console.log(cartas);
    console.log(divCarta);

    //inicializarCarrousel();
}

function ActualizarCartas(id) {
    if (ocultarEliminar) {
        document.getElementById(`carta${id}`).style.display = 'none';
    } else {
        document.getElementById(`carta${id}`).classList.add('seleccionada');
    }
}

function OcultarMostrarCartas() {
    if (ocultarEliminar) {
        ocultarEliminar = false;
        cartas.forEach(element => {
            if (element.marcado == 1) {
                document.getElementById(`carta${element.cartaID}`).style.display = 'flex';
                document.getElementById(`carta${element.cartaID}`).classList.add('seleccionada');
            }
        });
        document.getElementById('MostrarOcultarCartas').innerHTML = 'Ocultar Tarjetas Seleccionadas';
    } else {
        ocultarEliminar = true;
        cartas.forEach(element => {
            if (element.marcado == 1) {
                document.getElementById(`carta${element.cartaID}`).classList.remove('seleccionada');
                document.getElementById(`carta${element.cartaID}`).style.display = 'none';
            }
        });
        document.getElementById('MostrarOcultarCartas').innerHTML = 'Mostrar Tarjetas Seleccionadas';
    }
}

function CambiarOrdenCartas() {
    document.getElementById("cartas").innerHTML = '';
    if (ordenarCartas) {
        ordenarCartas = false;
        cartas = cartas.sort(() => Math.random() - 0.5);
        document.getElementById('RevolverCartas').innerHTML = 'Ordenar cartas';
        CargarCartas();
    } else {
        ordenarCartas = true;
        cartas.sort((x, y) => x.cartaID - y.cartaID);
        document.getElementById('RevolverCartas').innerHTML = 'Desordenar cartas'; 
        CargarCartas();
    }

    ActulizarListadoCartas();
}

function ActulizarListadoCartas() {
    if (ocultarEliminar) {
        cartas.forEach(element => {
            if (element.marcado == 1) {
                document.getElementById(`carta${element.cartaID}`).classList.remove('seleccionada');
                document.getElementById(`carta${element.cartaID}`).style.display = 'none';
            }
        });
    } else {
        cartas.forEach(element => {
            if (element.marcado == 1) {
                document.getElementById(`carta${element.cartaID}`).style.display = 'flex';
                document.getElementById(`carta${element.cartaID}`).classList.add('seleccionada');
            }
        });
    }
}


function ActualizarTablas() {
    let divTablas = document.getElementById("tablas");
    let contenidoTablas = '';

    tablas.forEach((element, index) => {
        contenidoTablas = contenidoTablas + `<div class="col-md-4">`;
        contenidoTablas = contenidoTablas + `<h3 class="subtitulo">Tabla: ${element.nombre}</h3>`;
        contenidoTablas = contenidoTablas + `<div class="grid">`;
        element.cartas.forEach(element => {
            let marcado = element.marcado == 1 ? `<img src="img/check.png" class="marcado" alt="check">` : "";
            let carta = `<div style="position:relative">
                                <img src="${element.imagen}" class="card-img-top" alt="${element.nombre}">
                                ${marcado}
                            </div>`;
            contenidoTablas = contenidoTablas + carta;
        });
        contenidoTablas = contenidoTablas + `</div></div>`;
    });


    divTablas.innerHTML = contenidoTablas;
}

function SelectCarta(id) {
    if (cartas.find(element => element.marcado == 0 && element.cartaID == id)) {
        cartas.forEach((c) => {
            if (c.cartaID == id) {
                c.marcado = 1;
            }
        });

        tablas.forEach((t) => {
            t.cartas.forEach(c => {
                if (c.cartaID == id) {
                    c.marcado = 1;
                }
            });
        });

        ValidarTablaGanadora();
        ActualizarTablas();
        ActualizarCartas(id);
    } 
}

function ValidarTablaGanadora() {
    let ganador = false;
    tablas.forEach((t, index) => {
        if (t.cartas.find(element => element.marcado == 0)) {
            console.log('Aun no gana');
        } else {
            ganador = true;
            console.log('Ganadora');
        }
    });

    if (ganador) {
        MostrarGanadoras();
    }
}

function MostrarGanadoras() {
    tablas.forEach((t, index) => {
        if (!t.cartas.find(element => element.marcado == 0)) {
            console.log(t);
            RenderizarTablasGanadoras(t);
            RegistrarGanador(t.tablaID);
        }
    });
    modalGanadores.show();
}

function RenderizarTablasGanadoras(tabla) {
    console.log(tabla);
    let divTablas = document.getElementById("tablasGanadoras");
    let contenidoTablas = divTablas.innerHTML;

    contenidoTablas = contenidoTablas + `<div class="col-md-6">`;
    contenidoTablas = contenidoTablas + `<h3>Tabla: ${tabla.nombre}</h3>`;
    contenidoTablas = contenidoTablas + `<div class="grid">`;
    tabla.cartas.forEach(element => {
        let marcado = element.marcado == 1 ? `<img src="img/check.png" class="marcado" alt="check">` : "";
        let carta = `<div style="position:relative">
                            <img src="${element.imagen}" class="card-img-top" alt="${element.nombre}">
                            ${marcado}
                        </div>`;
        contenidoTablas = contenidoTablas + carta;
    });
    contenidoTablas = contenidoTablas + `</div></div>`;


    divTablas.innerHTML = contenidoTablas;
}

function RegistrarGanador(tablaId) {
    //Block();
    axios.post("/Ganadores/RegistrarGanador/" + tablaId).then(response => {
        console.log(response);
        //UnBlock();
    }).catch(error => {
        //UnBlock();
        console.log(error);
    });
}


$("#ganadores").on('hide.bs.modal', function () {
    window.location.reload();
});

//function inicializarCarrousel() {
//    owlCarrousel = $('.owl-carousel').owlCarousel({
//        loop: true,
//        margin: 10,
//        responsiveClass: true,
//        responsive: {
//            0: {
//                items: 1,
//                nav: true
//            },
//            600: {
//                items: 4,
//                nav: false
//            },
//            1000: {
//                items: 6,
//                nav: true,
//                loop: false,
//                margin: 20
//            }
//        }
//    });
//}