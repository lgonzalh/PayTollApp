// Configuración de las URLs de la API
const API_CONFIG = {
    BASE_URL: 'https://paytollcard-28537ba559dc.herokuapp.com',
    ENDPOINTS: {
        LOGIN: '/apiUsuarios/login',
        // Agrega aquí otros endpoints que necesites
    }
};

// Clase para manejar la autenticación
class AuthHandler {
    constructor() {
        this.setupEventListeners();
    }

    setupEventListeners() {
        // Esperar a que el DOM esté completamente cargado
        document.addEventListener('DOMContentLoaded', () => {
            // Obtener referencias a elementos del DOM
            const loginModal = document.getElementById('loginModal');
            const loginForm = document.getElementById('loginForm');
            const emailInput = document.getElementById('correo_electronico');
            const passwordInput = document.getElementById('contrasena');
            const loginButton = document.querySelector('.btn-login');

            // Configurar el modal de Bootstrap si existe
            if (loginModal) {
                const modal = new bootstrap.Modal(loginModal);
                
                // Agregar listener para el botón de login en la navbar
                const loginNavButton = document.querySelector('[data-bs-toggle="modal"][data-bs-target="#loginModal"]');
                if (loginNavButton) {
                    loginNavButton.addEventListener('click', () => modal.show());
                }
            }

            // Configurar el formulario de login si existe
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
            const response = await fetch(`${API_CONFIG.BASE_URL}${API_CONFIG.ENDPOINTS.LOGIN}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    CorreoElectronico: email,
                    Contrasena: password
                })
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || 'Error en el inicio de sesión');
            }

            const data = await response.json();
            
            // Guardar el token si la API lo devuelve
            if (data.token) {
                localStorage.setItem('authToken', data.token);
            }

            // Mostrar mensaje de éxito
            this.showNotification('¡Inicio de sesión exitoso!', 'success');
            
            // Redirigir al usuario
            window.location.href = 'services.html';

        } catch (error) {
            console.error('Error de autenticación:', error);
            this.showNotification('Correo o contraseña incorrectos', 'error');
        }
    }

    showNotification(message, type = 'info') {
        // Crear elemento de notificación
        const notification = document.createElement('div');
        notification.className = `alert alert-${type === 'error' ? 'danger' : 'success'} notification`;
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1000;
            padding: 15px;
            border-radius: 4px;
            animation: fadeIn 0.5s, fadeOut 0.5s 2.5s;
        `;
        notification.textContent = message;

        // Agregar al DOM
        document.body.appendChild(notification);

        // Remover después de 3 segundos
        setTimeout(() => {
            notification.remove();
        }, 3000);
    }
}

// Inicializar el manejador de autenticación
const authHandler = new AuthHandler();

// Agregar estilos para las notificaciones
const style = document.createElement('style');
style.textContent = `
    @keyframes fadeIn {
        from { opacity: 0; transform: translateY(-20px); }
        to { opacity: 1; transform: translateY(0); }
    }
    @keyframes fadeOut {
        from { opacity: 1; transform: translateY(0); }
        to { opacity: 0; transform: translateY(-20px); }
    }
`;
document.head.appendChild(style);