document.addEventListener('DOMContentLoaded', function () {
    // Validación de Fecha de Nacimiento
    const fechaNacimientoInput = document.getElementById('fecha_nacimiento');
    if (fechaNacimientoInput) {
        const edadLabel = document.getElementById('edad_label');
        const today = new Date();

        const maxDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
        const minDate = new Date(today.getFullYear() - 80, today.getMonth(), today.getDate());

        fechaNacimientoInput.min = minDate.toISOString().split('T')[0];
        fechaNacimientoInput.max = maxDate.toISOString().split('T')[0];

        fechaNacimientoInput.addEventListener('input', function () {
            const birthDate = new Date(this.value);
            if (!isNaN(birthDate)) {
                let age = today.getFullYear() - birthDate.getFullYear();
                const monthDiff = today.getMonth() - birthDate.getMonth();
                const dayDiff = today.getDate() - birthDate.getDate();
                if (monthDiff < 0 || (monthDiff === 0 && dayDiff < 0)) {
                    age--;
                }
                edadLabel.textContent = age < 18 
                    ? `Debe ser mayor de 18 años. Su edad es ${age} años.` 
                    : age > 80 
                        ? `Debe ser menor o igual a 80 años. Su edad es ${age} años.` 
                        : `Su edad es ${age} años`;
            } else {
                edadLabel.textContent = '';
            }
        });
    }

    // Mostrar/Ocultar contraseña y validación de coincidencia
    const togglePassword1 = document.getElementById('togglePassword1');
    const togglePassword2 = document.getElementById('togglePassword2');
    const passwordInput1 = document.getElementById('contrasena');
    const passwordInput2 = document.getElementById('confirmar_contrasena');
    const passwordError = document.getElementById('password_error');

    if (togglePassword1 && togglePassword2) {
        togglePassword1.addEventListener('click', function () {
            const type = passwordInput1.type === 'password' ? 'text' : 'password';
            passwordInput1.type = type;
            const icon = this.querySelector('i');
            icon.classList.toggle('fa-eye-slash');
            icon.classList.toggle('fa-eye');
        });

        togglePassword2.addEventListener('click', function () {
            const type = passwordInput2.type === 'password' ? 'text' : 'password';
            passwordInput2.type = type;
            const icon = this.querySelector('i');
            icon.classList.toggle('fa-eye-slash');
            icon.classList.toggle('fa-eye');
        });

        const validatePasswords = () => {
            passwordError.textContent = passwordInput1.value !== passwordInput2.value 
                ? '¡Las contraseñas no coinciden!' 
                : '';
        };

        passwordInput1.addEventListener('input', validatePasswords);
        passwordInput2.addEventListener('input', validatePasswords);
    }

    // Manejo del envío del formulario de registro
    const registerForm = document.getElementById('registerForm');
    const registerMessage = document.createElement('div');
    registerMessage.id = 'registerMessage';
    document.body.appendChild(registerMessage);

    if (registerForm) {
        registerForm.addEventListener('submit', function (e) {
            e.preventDefault();

            const data = {
                cedula: document.getElementById('numero_identificacion')?.value.trim(),
                nombre: document.getElementById('nombres')?.value.trim() + " " + document.getElementById('apellidos')?.value.trim(),
                correoElectronico: document.getElementById('correo_electronico')?.value.trim(),
                contrasena: document.getElementById('contrasena')?.value.trim(),
                fechaCreacion: new Date().toISOString()
            };

            console.log('Datos enviados al backend:', data);

            //http://localhost:5085/api/Usuarios/register
            fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Usuarios/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data),
            })
            .then(response => {
                console.log('Estado de respuesta:', response.status);
                if (!response.ok) {
                    return response.text().then((error) => {
                        throw new Error(error || 'Error al registrar el usuario.');
                    });
                }
                return response.text();
            })
            .then(result => {
                alert('El usuario se ha registrado correctamente.');
                window.location.href = 'index.html';
            })
            .catch(error => {
                console.error('Error:', error);
                registerMessage.innerHTML = '<div class="alert alert-danger" role="alert">' + error.message + '</div>';
            });
        });
    } else {
        console.error('Error: Elementos del DOM relacionados con el formulario no encontrados.');
    }
});