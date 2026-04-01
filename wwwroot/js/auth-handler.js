// auth-handler.js
class AuthHandler {
    constructor() {
        const storedApiBaseUrl = (localStorage.getItem('PAYTOLL_API_BASE_URL') || '').trim();
        this.API_BASE_URL = storedApiBaseUrl || window.location.origin;
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

        const endpoint = `${this.API_BASE_URL}/api/Usuarios/login`;

        const data = {
            CorreoElectronico: email,
            Contrasena: password
        };

        try {
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

            const responseData = await this.readResponsePayload(response);

            if (!response.ok) {
                const errorMessage = this.getErrorMessage(response.status, responseData);
                throw new Error(errorMessage);
            }

            console.log('Login exitoso:', responseData);

            if (responseData.token) {
                localStorage.setItem('authToken', responseData.token);
            }

            const loggedInUser = responseData.usuario || responseData.Usuario;
            if (loggedInUser) {
                localStorage.setItem('loggedInUser', JSON.stringify(loggedInUser));
            }

            window.location.href = 'services.html';

        } catch (error) {
            console.error('Error detallado:', error);
            alert('Error en el inicio de sesiÃ³n: ' + error.message);
        }
    }

    async readResponsePayload(response) {
        const contentType = response.headers.get('content-type') || '';
        if (contentType.includes('application/json')) {
            return response.json();
        }

        const rawText = await response.text();
        return { rawText };
    }

    getErrorMessage(status, payload) {
        if (payload?.message) {
            return payload.message;
        }

        if (payload?.Message) {
            return payload.Message;
        }

        if (status === 404) {
            return 'No se encontró el endpoint de login. Verifica la URL del API o configura PAYTOLL_API_BASE_URL en localStorage.';
        }

        if (payload?.rawText && payload.rawText.includes('<!DOCTYPE')) {
            return 'El servidor respondió HTML en lugar de JSON. El frontend está apuntando a un host sin backend API.';
        }

        return 'No fue posible iniciar sesión.';
    }
}

new AuthHandler();

