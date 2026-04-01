// auth-handler.js
class AuthHandler {
    constructor() {
        // Usar el mismo dominio del frontend (local o desplegado)
        this.API_BASE_URL = window.location.origin;
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
            console.log('Intentando login con:', { CorreoElectronico: email });

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
                const errorData = await response.json();
                console.error('Error response:', errorData);
                throw new Error(errorData.message || errorData.Message || 'Error en el inicio de sesiÃ³n');
            }

            const responseData = await response.json();
            console.log('Login exitoso:', responseData);

            // Si el backend devuelve un token, lo almacenamos
            if (responseData.token) {
                localStorage.setItem('authToken', responseData.token);
            }

            const loggedInUser = responseData.usuario || responseData.Usuario;
            if (loggedInUser) {
                localStorage.setItem('loggedInUser', JSON.stringify(loggedInUser));
            }

            // Redirigir al usuario a la pÃ¡gina de servicios
            window.location.href = 'services.html';

        } catch (error) {
            console.error('Error detallado:', error);
            alert('Error en el inicio de sesiÃ³n: ' + error.message);
        }
    }
}

// Inicializar el manejador
new AuthHandler();

