// auth-handler.js
class AuthHandler {
    constructor() {
        const storedApiBaseUrl = (localStorage.getItem('PAYTOLL_API_BASE_URL') || '').trim();
        this.API_BASE_URL = storedApiBaseUrl || window.location.origin;
        this.setupEventListeners();
    }

    setupEventListeners() {
        const init = () => {
            this.updateAuthUI();
            const loginForm = document.getElementById('loginForm');
            if (loginForm) {
                loginForm.addEventListener('submit', (e) => this.handleLogin(e));
            }
        };

        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', init);
        } else {
            init();
        }
    }

    updateAuthUI() {
        const loggedInUserStr = localStorage.getItem('loggedInUser');
        const authButtonContainer = document.querySelector('.navbar-nav .nav-item.ms-3');
        const loginModalElement = document.getElementById('loginModal');
        
        let user = null;
        if (loggedInUserStr) {
            try {
                user = JSON.parse(loggedInUserStr);
            } catch(e) {}
        }

        const userGreetingElement = document.getElementById("userGreeting");
        if (userGreetingElement) {
            if (user && (user.nombre || user.Nombre)) {
                userGreetingElement.textContent = `¡Hola de nuevo ${user.nombre || user.Nombre}!`;
            } else {
                userGreetingElement.textContent = "¡Bienvenido!";
            }
        }
        
        if (authButtonContainer) {
            if (user) {
                // Usuario logueado
                authButtonContainer.innerHTML = '<button class="btn btn-outline-danger rounded-pill px-4" id="btnLogout">Cerrar Sesión</button>';
                
                document.getElementById('btnLogout').addEventListener('click', () => {
                    localStorage.removeItem('loggedInUser');
                    localStorage.removeItem('authToken');
                    this.updateAuthUI(); // Actualizar interfaz inmediatamente
                    if (window.location.pathname.includes('services.html') || window.location.pathname.includes('admin.html')) {
                        window.location.href = 'index.html';
                    }
                });
                
                // Cerrar el modal si está abierto
                if (loginModalElement && typeof bootstrap !== 'undefined') {
                    try {
                        const loginModal = bootstrap.Modal.getInstance(loginModalElement);
                        if (loginModal) {
                            loginModal.hide();
                        }
                    } catch(e) { console.error('Error cerrando modal', e); }
                }
            } else {
                // Usuario no logueado
                authButtonContainer.innerHTML = '<button class="btn btn-outline-light rounded-pill px-4" data-bs-toggle="modal" data-bs-target="#loginModal">Acceder</button>';
            }
        }
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

            // Actualizar UI
            this.updateAuthUI();

            alert('Inicio de sesión exitoso.');
            window.location.href = 'services.html';

        } catch (error) {
            console.error('Error detallado:', error);
            alert('Error en el inicio de sesión: ' + error.message);
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

