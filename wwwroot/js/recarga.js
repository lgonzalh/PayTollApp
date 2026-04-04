export default function recargaModule() {
    const recargaFormContainer = document.getElementById('recargaFormContainer');

    if (!recargaFormContainer) {
        console.error('No se encontró el contenedor del formulario de recarga.');
        return;
    }

    recargaFormContainer.innerHTML = `
        <form id="recargaForm">
            <div class="mb-3">
                <label for="cedulaRecarga" class="form-label">Número de Cédula</label>
                <input type="text" class="form-control" id="cedulaRecarga" required>
            </div>
            <div class="mb-3">
                <label for="montoRecarga" class="form-label">Monto a Recargar</label>
                <input type="number" class="form-control" id="montoRecarga" required>
            </div>
            <div class="mb-3">
                <label for="metodoPagoRecarga" class="form-label">Método de Pago</label>
                <input type="text" class="form-control" id="metodoPagoRecarga" required placeholder="Ej: NEQUI, DAVIPLATA, EFECTIVO">
            </div>
        </form>
    `;

    const submitRecarga = document.getElementById('submitRecarga');
    const recargaForm = document.getElementById('recargaForm');

    if (submitRecarga && recargaForm) {
        submitRecarga.addEventListener('click', async () => {
            const API_BASE_URL = (localStorage.getItem('PAYTOLL_API_BASE_URL') || '').trim() || window.location.origin;
            const cedula = document.getElementById('cedulaRecarga').value;
            const monto = document.getElementById('montoRecarga').value;
            const metodoPagoInput = document.getElementById('metodoPagoRecarga').value;
            const metodoPago = metodoPagoInput ? metodoPagoInput.toUpperCase() : '';

            try {
                const response = await fetch(`${API_BASE_URL}/api/Recargas/recargar`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        cedula: cedula,
                        monto: parseFloat(monto),
                        metodoPago: metodoPago
                    })
                });

                if (response.ok) {
                    alert('Recarga exitosa.');
                    // Cerrar el modal si es necesario
                } else {
                    const errorMsg = await response.text();
                    alert(`Error en la recarga: ${errorMsg}`);
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

