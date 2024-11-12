export default function extractoModule() {
    document.getElementById('extractoFormContainer').innerHTML = `
        <form id="extractoForm">
            <div class="mb-3">
                <label for="cedulaExtracto" class="form-label">Cédula</label>
                <input type="text" class="form-control" id="cedulaExtracto" required>
            </div>
        </form>
    `;

    document.getElementById('extractoModalSubmit').addEventListener('click', async () => {
        const cedula = document.getElementById('cedulaExtracto').value;

        try {
            const response = await fetch(`http://localhost:5206/api/Extracto/${cedula}`, { method: 'GET' });

            if (!response.ok) throw new Error('Error al obtener el extracto.');

            const result = await response.json();
            alert('Extracto obtenido correctamente.');
            console.log(result);
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al obtener el extracto.');
        }
    });
}
