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

            //http://localhost:5085/api/Usuarios/login;
            const endpoint = 'https://paytollcard-28537ba559dc.herokuapp.com/apiUsuarios/login';

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
                return response.text();
            })
            .then(result => {
                // Puedes ajustar esta parte si el backend devuelve un token u otros datos
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

