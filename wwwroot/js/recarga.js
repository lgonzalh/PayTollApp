export default function recargaModule() {
    const recargaFormContainer = document.getElementById('recargaFormContainer');

    if (!recargaFormContainer) {
        console.error('No se encontrÃ³ el contenedor del formulario de recarga.');
        return;
    }

    recargaFormContainer.innerHTML = `
        <form id="recargaForm">
            <div class="mb-3">
                <label for="cedula" class="form-label">NÃºmero de CÃ©dula</label>
                <input type="text" class="form-control" id="cedula" required>
            </div>
            <div class="mb-3">
                <label for="montoRecarga" class="form-label">Monto a Recargar</label>
                <input type="number" class="form-control" id="montoRecarga" required>
            </div>
            <div class="mb-3">
                <label for="metodoPago" class="form-label">MÃ©todo de Pago</label>
                <select class="form-control" id="metodoPago" required>
                    <option value="PSE">PSE</option>
                    <option value="Tarjeta">Tarjeta</option>
                    <option value="Transferencia">Transferencia</option>
                </select>
            </div>
        </form>
    `;

    const submitRecarga = document.getElementById('submitRecarga');
    const recargaForm = document.getElementById('recargaForm');

    if (submitRecarga && recargaForm) {
        submitRecarga.addEventListener('click', async () => {
            const cedula = document.getElementById('cedula').value;
            const montoRecarga = document.getElementById('montoRecarga').value;
            const metodoPago = document.getElementById('metodoPago').value;

            try {
                const response = await fetch('/api/Recargas/recargar', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        cedula,
                        monto: Number(montoRecarga),
                        metodoPago
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
        console.error('No se encontrÃ³ el botÃ³n submitRecarga o el formulario recargaForm.');
    }
}

