//Popup de Contacto
$(document).ready(function () {
    // Cuando se envía el formulario
    $('form').submit(function (event) {
      // Evitar el comportamiento de envío de formulario predeterminado
      event.preventDefault();
  
      // Obtener los valores del formulario
      var nombre = $('#nombre').val();
      var correo = $('#correo').val();
      var mensaje = $('#mensaje').val();
  
      // Construir el mensaje de confirmación personalizado
      var confirmacionMensaje = '¡Hola ' + nombre + '! Tu mensaje "' + mensaje + '" ha sido enviado correctamente a la dirección de correo ' + correo + '.';
  
      // Mostrar el mensaje de confirmación en el modal
      $('#mensajeConfirmacion').text(confirmacionMensaje);
      $('#confirmacionModal').modal('show');
    });
  });
  

  //Registro de usuario
  document.addEventListener('DOMContentLoaded', function() {
    const fechaNacimientoInput = document.getElementById('fecha_nacimiento');
    const edadLabel = document.getElementById('edad_label');
    const today = new Date();
    const minYear = today.getFullYear() - 80;
    const maxYear = today.getFullYear() - 18;

    const minDate = new Date(minYear, today.getMonth(), today.getDate()).toISOString().split('T')[0];
    const maxDate = new Date(maxYear, today.getMonth(), today.getDate()).toISOString().split('T')[0];

    fechaNacimientoInput.min = minDate;
    fechaNacimientoInput.max = maxDate;

    fechaNacimientoInput.addEventListener('input', function() {
      const birthDate = new Date(fechaNacimientoInput.value);
      if (!isNaN(birthDate)) {
        const age = today.getFullYear() - birthDate.getFullYear();
        const monthDiff = today.getMonth() - birthDate.getMonth();
        if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
          edadLabel.textContent = `Su edad es ${age - 1} años`;
        } else {
          edadLabel.textContent = `Su edad es ${age} años`;
        }
      } else {
        edadLabel.textContent = '';
      }
    });

    // Mostrar/Ocultar contraseña
    const togglePassword1 = document.getElementById('togglePassword1');
    const togglePassword2 = document.getElementById('togglePassword2');
    const passwordInput1 = document.getElementById('contrasena');
    const passwordInput2 = document.getElementById('confirmar_contrasena');
    const passwordError = document.getElementById('password_error');

    togglePassword1.addEventListener('click', function() {
      const type = passwordInput1.type === 'password' ? 'text' : 'password';
      passwordInput1.type = type;
      this.querySelector('i').classList.toggle('fa-eye');
      this.querySelector('i').classList.toggle('fa-eye-slash');
    });

    togglePassword2.addEventListener('click', function() {
      const type = passwordInput2.type === 'password' ? 'text' : 'password';
      passwordInput2.type = type;
      this.querySelector('i').classList.toggle('fa-eye');
      this.querySelector('i').classList.toggle('fa-eye-slash');
    });

    // Validación de contraseñas
    passwordInput2.addEventListener('input', function() {
      if (passwordInput1.value !== passwordInput2.value) {
        passwordError.textContent = 'No son iguales las contraseñas, revísalas!';
      } else {
        passwordError.textContent = '';
      }
    });
  });

  //Registar Vehiculo

  //Recargar Tarjeta

  //Extractos

  //Solicitudes