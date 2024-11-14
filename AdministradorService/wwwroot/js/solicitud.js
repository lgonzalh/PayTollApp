export default function solicitudModule() {
    document.getElementById('solicitudFormContainer').innerHTML = `
        <form id="solicitudForm">
            <div class="mb-3">
                <label for="cedulaSolicitud" class="form-label">Cédula</label>
                <input type="text" class="form-control" id="cedulaSolicitud" required>
            </div>
            <div class="mb-3">
                <label for="tipoSolicitud" class="form-label">Tipo de Solicitud</label>
                <input type="text" class="form-control" id="tipoSolicitud" required>
            </div>
            <div class="mb-3">
                <label for="descripcionSolicitud" class="form-label">Descripción</label>
                <textarea class="form-control" id="descripcionSolicitud" rows="3" required></textarea>
            </div>
        </form>
    `;

    document.getElementById('solicitudModalSubmit').addEventListener('click', async () => {
        const data = {
            cedula: document.getElementById('cedulaSolicitud').value,
            tipoSolicitud: document.getElementById('tipoSolicitud').value,
            descripcion: document.getElementById('descripcionSolicitud').value
        };

        try {
            const response = await fetch('http://localhost:5005/api/Solicitudes/crear', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) throw new Error('Error al enviar la solicitud.');

            alert('La solicitud se ha enviado correctamente.');
            window.location.href = 'index.html';
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al enviar la solicitud.');
        }
    });
}
