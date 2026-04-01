// contact.js - Popup de Contacto
$(document).ready(function () {
    // Cuando se envÃ­a el formulario
    $('form').submit(async function (event) {
        // Evitar el comportamiento de envÃ­o de formulario predeterminado
        event.preventDefault();
  
        // Obtener los valores del formulario
        var nombre = $('#nombre').val();
        var correo = $('#correo').val();
        var mensaje = $('#mensaje').val();
        const loggedInUser = JSON.parse(localStorage.getItem('loggedInUser') || 'null');

        if (!loggedInUser || !loggedInUser.id) {
            alert('Debes iniciar sesión para enviar un mensaje de contacto.');
            return;
        }
  
        // Construir el objeto de datos
        const data = {
            idContacto: 0,
            idUsuario: loggedInUser.id,
            nombre: nombre,
            correo: correo,
            mensaje: mensaje,
            fechaContacto: new Date().toISOString()
        };
  
        try {
            //http://localhost:7209/api/Contactos/enviar
            const response = await fetch('/api/Contactos/enviar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
  
            if (!response.ok) {
                throw new Error('Error al enviar el mensaje.');
            }
  
            const result = await response.json();
  
            // Mostrar el modal de confirmaciÃ³n existente
            var confirmacionMensaje = 'Â¡Hola ' + nombre + '! Tu mensaje "' + mensaje + '" ha sido enviado correctamente a la direcciÃ³n de correo ' + correo + '.';
            $('#mensajeConfirmacion').text(confirmacionMensaje);
            $('#confirmacionModal').modal('show');
  
            // Agregar evento para redirigir despuÃ©s de cerrar el modal
            $('#confirmacionModal').on('hidden.bs.modal', function () {
                alert('El usuario ha enviado el mensaje correctamente.');
                window.location.href = 'index.html';
            });
  
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al enviar el mensaje. Por favor, intÃ©ntalo de nuevo.');
        }
    });
  });
