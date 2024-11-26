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

        try {
            // Agregar logs para debugging
            console.log('Intentando login con:', { email });

            const response = await fetch(`${this.API_BASE_URL}/apiUsuarios/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },
                body: JSON.stringify({
                    correoElectronico: email,
                    contrasena: password
                })
            });

            console.log('Respuesta del servidor:', response.status);

            if (!response.ok) {
                const errorText = await response.text();
                console.error('Error response:', errorText);
                throw new Error(errorText || 'Error en el inicio de sesión');
            }

            const data = await response.json();
            console.log('Login exitoso:', data);

            if (data.token) {
                localStorage.setItem('authToken', data.token);
            }

            window.location.href = 'services.html';

        } catch (error) {
            console.error('Error detallado:', error);
            alert('Error en el inicio de sesión: ' + error.message);
        }
    }
}

// Inicializar el manejador
new AuthHandler();