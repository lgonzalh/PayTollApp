export default function recargaModule() {
    document.getElementById('recargaFormContainer').innerHTML = `
        <form id="recargaForm">
            <div class="mb-3">
                <label for="cedula" class="form-label">Cédula</label>
                <input type="text" class="form-control" id="cedula" required>
            </div>
            <div class="mb-3">
                <label for="monto" class="form-label">Monto</label>
                <input type="number" class="form-control" id="monto" required>
            </div>
            <div class="mb-3">
                <label for="metodoPago" class="form-label">Método de Pago</label>
                <input type="text" class="form-control" id="metodoPago" required>
            </div>
        </form>
    `;

    document.getElementById('recargaModalSubmit').addEventListener('click', async () => {
        const data = {
            cedula: document.getElementById('cedula').value,
            monto: document.getElementById('monto').value,
            metodoPago: document.getElementById('metodoPago').value
        };

        try {
            const response = await fetch('http://localhost:5141/api/Recargas/recargar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) throw new Error('Error al recargar la tarjeta.');

            alert('La recarga se ha realizado correctamente.');
            window.location.href = 'index.html';
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al recargar la tarjeta.');
        }
    });
}
