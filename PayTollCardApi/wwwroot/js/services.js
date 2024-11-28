document.addEventListener('DOMContentLoaded', () => {
    function showModal(modalId, modalContent) {
        if (document.getElementById(modalId)) {
            document.getElementById(modalId).remove();
        }
        const modalHTML = `
            <div class="modal fade" id="${modalId}" tabindex="-1" aria-labelledby="${modalId}Label" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content" style="background: rgba(44, 62, 80, 0.95); color: #fff;">
                        <div class="modal-header">
                            <h5 class="modal-title">${modalContent.title}</h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>
                        <div class="modal-body">${modalContent.body}</div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                            <button type="button" class="btn btn-primary" id="${modalContent.submitId}">Enviar</button>
                        </div>
                    </div>
                </div>
            </div>`;
        document.body.insertAdjacentHTML('beforeend', modalHTML);
        new bootstrap.Modal(document.getElementById(modalId)).show();
    }

    const userGreetingElement = document.getElementById("userGreeting");
    const user = JSON.parse(localStorage.getItem("loggedInUser"));
    if (user && user.nombre) {
        userGreetingElement.textContent = `¡Hola de nuevo ${user.nombre}!`;
    } else {
        userGreetingElement.textContent = "¡Bienvenido!";
    }

    const btnRegistrarVehiculo = document.getElementById('btnRegistrarVehiculo');
    const btnRecargarTarjeta = document.getElementById('btnRecargarTarjeta');
    const btnExtractos = document.getElementById('btnExtractos');
    const btnSolicitudes = document.getElementById('btnSolicitudes');

    if (btnRegistrarVehiculo) {
        btnRegistrarVehiculo.addEventListener('click', () => {
            showModal('vehicleModal', {
                title: 'Registrar Vehículo',
                body: '<div id="vehiculoFormContainer"></div>',
                submitId: 'submitVehiculo'
            });
            import('./vehiculo.js').then(module => module.default());
        });
    }

    if (btnRecargarTarjeta) {
        btnRecargarTarjeta.addEventListener('click', () => {
            showModal('recargaModal', {
                title: 'Recargar Tarjeta',
                body: '<div id="recargaFormContainer"></div>',
                submitId: 'submitRecarga'
            });
            import('./recarga.js').then(module => module.default());
        });
    }

    if (btnExtractos) {
        btnExtractos.addEventListener('click', () => {
            showModal('extractoModal', {
                title: 'Extractos',
                body: '<div id="extractoFormContainer"></div>',
                submitId: 'submitExtracto'
            });
            import('./extracto.js').then(module => module.default());
        });
    }

    if (btnSolicitudes) {
        btnSolicitudes.addEventListener('click', () => {
            showModal('solicitudModal', {
                title: 'Centro de Solicitudes',
                body: '<div id="solicitudFormContainer"></div>',
                submitId: 'submitSolicitud'
            });
            import('./solicitud.js').then(module => module.default());
        });
    }
});
