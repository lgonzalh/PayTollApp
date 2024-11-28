// index.js

var myCarousel = document.querySelector('#imageCarousel');
var carousel = new bootstrap.Carousel(myCarousel, {interval: 3000, ride: 'carousel'});

document.addEventListener('DOMContentLoaded', function() {
    const loginForm = document.getElementById('loginForm');
    const passwordInput = document.getElementById('contrasena');
    const correoInput = document.getElementById('correo_electronico');

    // Verificar que los elementos existen
    if (loginForm && passwordInput && correoInput) {
        // Manejo del formulario de inicio de sesión
        loginForm.addEventListener('submit', function(e) {
            e.preventDefault();

            const correoElectronico = correoInput.value;
            const contrasena = passwordInput.value;

            // Ruta correcta del endpoint
            const endpoint = 'https://paytollcard-2b6b0c89816c.herokuapp.com/api/Usuarios/login';

            const data = {
                CorreoElectronico: correoElectronico,
                Contrasena: contrasena
            };

            // Agregar logs para depuración
            console.log('Datos enviados al backend:', data);

            fetch(endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data),
            })
            .then(response => {
                if (!response.ok) {
                    return response.text().then((errorMessage) => {
                        throw new Error(errorMessage || 'Credenciales incorrectas');
                    });
                }
                return response.json();
            })
            .then(result => {
                console.log('Inicio de sesión exitoso:', result);
                alert('Inicio de sesión exitoso.');
                window.location.href = 'services.html';
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Correo o contraseña incorrectos.');
            });
        });
    } else {
        console.error('Error: Elementos del DOM no encontrados.');
    }
});
