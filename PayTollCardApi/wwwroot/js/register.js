document.addEventListener('DOMContentLoaded', function () {
    // Validación de Fecha de Nacimiento
    const fechaNacimientoInput = document.getElementById('fecha_nacimiento');
    const edadError = document.getElementById('edadError');
    const today = new Date();

    if (fechaNacimientoInput) {
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
                if (age < 18 || age > 80) {
                    edadError.textContent = `Debes tener entre 18 y 80 años. Tu edad es ${age} años.`;
                    edadError.style.display = 'block';
                } else {
                    edadError.textContent = '';
                    edadError.style.display = 'none';
                }
            } else {
                edadError.textContent = '';
                edadError.style.display = 'none';
            }
        });
    }

    // Validación de coincidencia de contraseñas
    const passwordInput1 = document.getElementById('contrasena');
    const passwordInput2 = document.getElementById('confirmar_contrasena');
    const passwordError = document.getElementById('passwordError');

    const validatePasswords = () => {
        if (passwordInput1.value !== passwordInput2.value) {
            passwordError.textContent = 'Las contraseñas no coinciden.';
            passwordError.style.display = 'block';
        } else {
            passwordError.textContent = '';
            passwordError.style.display = 'none';
        }
    };

    if (passwordInput1 && passwordInput2) {
        passwordInput1.addEventListener('input', validatePasswords);
        passwordInput2.addEventListener('input', validatePasswords);
    }

    // Manejo del envío del formulario de registro
    const registerForm = document.getElementById('registerForm');
    const registerMessage = document.getElementById('registerMessage');
    const numeroIdentificacion = document.getElementById('numero_identificacion');
    const correoElectronicoInput = document.getElementById('correo_electronico');
    const correoError = document.getElementById('correoError');
    const cedulaError = document.getElementById('cedulaError');

    if (registerForm) {
        registerForm.addEventListener('submit', function (e) {
            e.preventDefault();

            // Limpiar mensajes anteriores
            registerMessage.innerHTML = '';
            correoError.style.display = 'none';
            cedulaError.style.display = 'none';

            const cedula = numeroIdentificacion.value.trim();
            const nombres = document.getElementById('nombres').value.trim();
            const apellidos = document.getElementById('apellidos').value.trim();
            const nombreCompleto = `${nombres} ${apellidos}`;
            const correoElectronico = correoElectronicoInput.value.trim();
            const contrasena = document.getElementById('contrasena').value.trim();
            const fechaCreacion = new Date().toISOString();

            // Validaciones básicas
            if (!cedula) {
                cedulaError.textContent = 'La cédula es requerida.';
                cedulaError.style.display = 'block';
                return;
            }

            if (!correoElectronico) {
                correoError.textContent = 'El correo electrónico es requerido.';
                correoError.style.display = 'block';
                return;
            }

            if (passwordInput1.value !== passwordInput2.value) {
                passwordError.textContent = 'Las contraseñas no coinciden.';
                passwordError.style.display = 'block';
                return;
            }

            const data = {
                cedula,
                nombre: nombreCompleto,
                correoElectronico,
                contrasena,
                fechaCreacion
            };

            console.log('Datos enviados al backend:', data);

            fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Usuarios/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data),
            })
            .then(response => {
                console.log('Estado de respuesta:', response.status);
                if (response.status === 400) {
                    return response.json().then((error) => {
                        throw new Error(error.Message || 'Error al registrar el usuario.');
                    });
                }
                if (!response.ok) {
                    return response.text().then((error) => {
                        throw new Error(error || 'Error al registrar el usuario.');
                    });
                }
                return response.json();
            })
            .then(result => {
                registerMessage.innerHTML = '<div class="alert alert-success" role="alert">El usuario se ha registrado correctamente.</div>';
                registerForm.reset();
            })
            .catch(error => {
                console.error('Error:', error);
                registerMessage.innerHTML = `<div class="alert alert-danger" role="alert">${error.message}</div>`;
            });
        });
    } else {
        console.error('Error: Elementos del DOM relacionados con el formulario no encontrados.');
    }
});
