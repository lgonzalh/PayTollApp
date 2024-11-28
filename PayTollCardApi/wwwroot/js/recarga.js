export default function recargaModule() {
    const recargaFormContainer = document.getElementById('recargaFormContainer');

    if (!recargaFormContainer) {
        console.error('No se encontró el contenedor del formulario de recarga.');
        return;
    }

    recargaFormContainer.innerHTML = `
        <form id="recargaForm">
            <div class="mb-3">
                <label for="cedula" class="form-label">Número de Cédula</label>
                <input type="text" class="form-control" id="cedula" required>
            </div>
            <div class="mb-3">
                <label for="montoRecarga" class="form-label">Monto a Recargar</label>
                <input type="number" class="form-control" id="montoRecarga" required>
            </div>
        </form>
    `;

    const submitRecarga = document.getElementById('submitRecarga');
    const recargaForm = document.getElementById('recargaForm');

    if (submitRecarga && recargaForm) {
        submitRecarga.addEventListener('click', async () => {
            const cedula = document.getElementById('cedula').value;
            const montoRecarga = document.getElementById('montoRecarga').value;

            try {
                const response = await fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Recargas', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        cedula,
                        montoRecarga
                    })
                });

                if (response.ok) {
                    alert('Recarga exitosa.');
                    // Cerrar el modal si es necesario
                } else {
                    alert('Error en la recarga.');
                }
            } catch (error) {
                console.error('Error:', error);
                alert('Error al procesar la recarga.');
            }
        });
    } else {
        console.error('No se encontró el botón submitRecarga o el formulario recargaForm.');
    }
}
