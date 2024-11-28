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

    document.getElementById('cedula').addEventListener('blur', async () => {
        const cedula = document.getElementById('cedula').value;

        try {
            const response = await fetch(`https://paytollcard-2b6b0c89816c.herokuapp.com/api/Usuarios/categoria/${cedula}`);
            if (!response.ok) {
                throw new Error(`Error al obtener la categoría del usuario. Código: ${response.status}`);
            }

            const categoria = await response.json();
            console.log('Categoría obtenida:', categoria);
            alert(`Categoría del usuario: ${categoria.nombreCategoria}`);
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al obtener la categoría del usuario.');
        }
    });

    document.getElementById('recargaModalSubmit').addEventListener('click', async () => {
        const data = {
            cedula: document.getElementById('cedula').value,
            monto: document.getElementById('monto').value,
            metodoPago: document.getElementById('metodoPago').value
        };

        try {
            const response = await fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Recargas/recargar', {
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
