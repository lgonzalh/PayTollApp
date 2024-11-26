// contact.js - Popup de Contacto
$(document).ready(function () {
    // Cuando se envía el formulario
    $('form').submit(async function (event) {
        // Evitar el comportamiento de envío de formulario predeterminado
        event.preventDefault();
  
        // Obtener los valores del formulario
        var nombre = $('#nombre').val();
        var correo = $('#correo').val();
        var mensaje = $('#mensaje').val();
  
        // Construir el objeto de datos
        const data = {
            idContacto: 0,
            idUsuario: 61, // Valor estático de ejemplo
            nombre: nombre,
            correo: correo,
            mensaje: mensaje,
            fechaContacto: new Date().toISOString(),
            usuario: {
                cedula: '1022395073', // Valor estático de ejemplo
                nombre: nombre,
                correoElectronico: correo,
                contrasena: 'prueba123', // Valor estático de ejemplo
                fechaCreacion: new Date().toISOString()
            }
        };
  
        try {
            //http://localhost:7209/api/Contactos/enviar
            const response = await fetch('https://paytollcard-28537ba559dc.herokuapp.com/api/Contactos/enviar', {
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
  
            // Mostrar el modal de confirmación existente
            var confirmacionMensaje = '¡Hola ' + nombre + '! Tu mensaje "' + mensaje + '" ha sido enviado correctamente a la dirección de correo ' + correo + '.';
            $('#mensajeConfirmacion').text(confirmacionMensaje);
            $('#confirmacionModal').modal('show');
  
            // Agregar evento para redirigir después de cerrar el modal
            $('#confirmacionModal').on('hidden.bs.modal', function () {
                alert('El usuario ha enviado el mensaje correctamente.');
                window.location.href = 'index.html';
            });
  
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al enviar el mensaje. Por favor, inténtalo de nuevo.');
        }
    });
  });