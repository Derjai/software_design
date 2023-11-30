window.readUploadedFileAsBytes = function (inputFile) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onloadend = () => resolve(new Uint8Array(reader.result));
        reader.onerror = reject;
        reader.readAsArrayBuffer(inputFile.files[0]);
    });
};
window.validateForm = function (formElement) {
    var isValid = true;
    var errorMessage = "";

    var tipoDocumento = formElement.querySelector("#TipoDocumento");
    if (!tipoDocumento.value) {
        isValid = false;
        errorMessage += "El tipo de documento es obligatorio.\n";
    }

    var numDoc = formElement.querySelector("#NumDoc");
    if (!numDoc.value) {
        isValid = false;
        errorMessage += "El número de documento es obligatorio.\n";
    } else if (numDoc.value.length > 10 || !/^[0-9]*$/.test(numDoc.value)) {
        isValid = false;
        errorMessage += "El número de documento no puede superar los 10 caracteres y solo puede contener números.\n";
    }

    var nombre = formElement.querySelector("#Nombre");
    if (!nombre.value) {
        isValid = false;
        errorMessage += "El nombre es obligatorio.\n";
    } else if (nombre.value.length > 30 || !/^[a-zA-Z ]*$/.test(nombre.value)) {
        isValid = false;
        errorMessage += "El nombre no puede superar los 30 caracteres y solo puede contener letras y espacios.\n";
    }

    var segundoNombre = formElement.querySelector("#SegundoNombre");
    if (segundoNombre.value.length > 30 || !/^[a-zA-Z ]*$/.test(segundoNombre.value)) {
        isValid = false;
        errorMessage += "El segundo nombre no puede superar los 30 caracteres y solo puede contener letras y espacios.\n";
    }

    var apellidos = formElement.querySelector("#Apellidos");
    if (!apellidos.value) {
        isValid = false;
        errorMessage += "Los apellidos son obligatorios.\n";
    } else if (apellidos.value.length > 60 || !/^[a-zA-Z ]*$/.test(apellidos.value)) {
        isValid = false;
        errorMessage += "Los apellidos no pueden superar los 60 caracteres y solo pueden contener letras y espacios.\n";
    }

    var fechaNacimiento = formElement.querySelector("#FechaNacimiento");
    if (!fechaNacimiento.value) {
        isValid = false;
        errorMessage += "La fecha de nacimiento es obligatoria.\n";
    }

    var genero = formElement.querySelector("#Genero");
    if (!genero.value) {
        isValid = false;
        errorMessage += "El género es obligatorio.\n";
    }

    var correoElectronico = formElement.querySelector("#CorreoElectronico");
    if (!correoElectronico.value) {
        isValid = false;
        errorMessage += "El correo electrónico es obligatorio.\n";
    } else if (!/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(correoElectronico.value)) {
        isValid = false;
        errorMessage += "El formato del correo electrónico no es válido.\n";
    }

    var celular = formElement.querySelector("#Celular");
    if (!celular.value) {
        isValid = false;
        errorMessage += "El número de celular es obligatorio.\n";
    } else if (celular.value.length != 10 || !/^[0-9]*$/.test(celular.value)) {
        isValid = false;
        errorMessage += "El número de celular debe tener 10 caracteres y solo puede contener números.\n";
    }

    var foto = formElement.querySelector("#Foto");
    if (foto.files[0] && foto.files[0].size > 2 * 1024 * 1024) {
        isValid = false;
        errorMessage += "La imagen no puede superar los 2 MB.\n";
    }

    return { isValid: isValid, errorMessage: errorMessage };
};