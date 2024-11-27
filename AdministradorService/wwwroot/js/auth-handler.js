// auth-handler.js
class AuthHandler {
    constructor() {
        this.API_BASE_URL = 'https://paytollcard-28537ba559dc.herokuapp.com';
        this.setupEventListeners();
    }

    setupEventListeners() {
        document.addEventListener('DOMContentLoaded', () => {
            const loginForm = document.getElementById('loginForm');
            if (loginForm) {
                loginForm.addEventListener('submit', (e) => this.handleLogin(e));
            }
        });
    }

    async handleLogin(event) {
        event.preventDefault();

        const email = document.getElementById('correo_electronico').value;
        const password = document.getElementById('contrasena').value;

        // Definir el endpoint correctamente
        const endpoint = `${this.API_BASE_URL}/api/Usuarios/login`;

        // Preparar los datos a enviar
        const data = {
            CorreoElectronico: email,
            Contrasena: password
        };

        try {
            // Logs para debugging
            console.log('Enviando solicitud a:', endpoint);
            console.log('Datos enviados:', data);
            console.log('Intentando login con:', { email });

            const response = await fetch(endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify(data)
            });

            console.log('Respuesta del servidor:', response.status);

            if (!response.ok) {
                const errorText = await response.text();
                console.error('Error response:', errorText);
                throw new Error(errorText || 'Error en el inicio de sesión');
            }

            const responseData = await response.json();
            console.log('Login exitoso:', responseData);

            // Si el backend devuelve un token, lo almacenamos
            if (responseData.token) {
                localStorage.setItem('authToken', responseData.token);
            }

            // Redirigir al usuario a la página de servicios
            window.location.href = 'services.html';

        } catch (error) {
            console.error('Error detallado:', error);
            alert('Error en el inicio de sesión: ' + error.message);
        }
    }
}

// Inicializar el manejador
new AuthHandler();
