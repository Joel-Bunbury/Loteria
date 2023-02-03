class CartaVM {
    cartaID
    nombre;
    imagen;
    lema;
    marcado;
    constructor(model) {
        this.cartaID = model.cartaID;
        this.nombre = model.nombre;
        this.imagen = model.imagen;
        this.lema = model.lema;
        this.marcado = model.marcado;
    }
}

class TablaVM {
    tablaID;
    nombre;
    cartas;
    constructor(model) {
        this.tablaID = model.tablaID;
        this.nombre = model.nombre;
        this.cartas = model.cartas;
    }
}