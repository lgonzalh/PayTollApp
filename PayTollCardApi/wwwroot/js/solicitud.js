export default function solicitudModule() {
    const solicitudFormContainer = document.getElementById('solicitudFormContainer');

    if (!solicitudFormContainer) {
        console.error('No se encontró el contenedor del formulario de solicitud.');
        return;
    }

    solicitudFormContainer.innerHTML = `
        <form id="solicitudForm">
            <div class="mb-3">
                <label for="cedula" class="form-label">Número de Cédula</label>
                <input type="text" class="form-control" id="cedula" required>
            </div>
            <div class="mb-3">
                <label for="tipoSolicitud" class="form-label">Tipo de Solicitud</label>
                <select class="form-select" id="tipoSolicitud" required>
                    <option value="">Seleccione</option>
                    <option value="Reclamo">Reclamo</option>
                    <option value="Petición">Petición</option>
                    <option value="Queja">Queja</option>
                </select>
            </div>
            <div class="mb-3">
                <label for="descripcionSolicitud" class="form-label">Descripción</label>
                <textarea class="form-control" id="descripcionSolicitud" rows="3" required></textarea>
            </div>
        </form>
    `;

    const submitSolicitud = document.getElementById('submitSolicitud');
    const solicitudForm = document.getElementById('solicitudForm');

    if (submitSolicitud && solicitudForm) {
        submitSolicitud.addEventListener('click', async () => {
            const cedula = document.getElementById('cedula').value;
            const tipoSolicitud = document.getElementById('tipoSolicitud').value;
            const descripcionSolicitud = document.getElementById('descripcionSolicitud').value;

            try {
                const response = await fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Solicitudes', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        cedula,
                        tipoSolicitud,
                        descripcionSolicitud
                    })
                });

                if (response.ok) {
                    alert('Solicitud enviada exitosamente.');
                    // Cerrar el modal si es necesario
                } else {
                    alert('Error al enviar la solicitud.');
                }
            } catch (error) {
                console.error('Error:', error);
                alert('Error al procesar la solicitud.');
            }
        });
    } else {
        console.error('No se encontró el botón submitSolicitud o el formulario solicitudForm.');
    }
}
