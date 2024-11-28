export default function extractoModule() {
    document.getElementById('extractoFormContainer').innerHTML = `
        <form id="extractoForm">
            <div class="mb-3">
                <label for="cedulaExtracto" class="form-label">Cédula</label>
                <input type="text" class="form-control" id="cedulaExtracto" required>
            </div>
            
        </form>
        <div id="extractoGridContainer" class="mt-4"></div>
        <div id="saldoGridContainer" class="mt-4"></div>
        <button type="button" class="btn btn-primary" id="extractoModalSubmit">Consultar</button>
        <button type="button" class="btn btn-success" id="imprimirExtracto">Imprimir</button>
    `;

    document.getElementById('extractoModalSubmit').addEventListener('click', async () => {
        const cedula = document.getElementById('cedulaExtracto').value;

        try {
            const response = await fetch(`https://paytollcard-2b6b0c89816c.herokuapp.com/api/Extracto/${cedula}`, { method: 'GET' });
            if (!response.ok) throw new Error('Error al obtener el extracto.');

            const result = await response.json();
            alert('Extracto obtenido correctamente.');
            renderExtractoGrid(result.movimientos);
            renderSaldoGrid(result);
        } catch (error) {
            console.error('Error:', error);
            alert('Hubo un problema al obtener el extracto.');
        }
    });

    function renderExtractoGrid(movimientos) {
        let gridHtml = `
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Fecha</th>
                        <th>Descripción</th>
                        <th>Peaje</th>
                        <th>Valor</th>
                        <th>Saldo</th>
                    </tr>
                </thead>
                <tbody>
        `;

        movimientos.forEach(mov => {
            gridHtml += `
                <tr>
                    <td>${new Date(mov.fecha).toLocaleDateString('es-CO')}</td>
                    <td>${mov.descripcion}</td>
                    <td>${mov.peaje}</td>
                    <td>${formatCurrency(mov.valor)}</td>
                    <td>${formatCurrency(mov.saldo)}</td>
                </tr>
            `;
        });

        gridHtml += `</tbody></table>`;
        document.getElementById('extractoGridContainer').innerHTML = gridHtml;
    }

    function renderSaldoGrid(data) {
        const saldoGridHtml = `
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>Saldo Anterior</th>
                        <th>Total Recargas</th>
                        <th>Total Pagos</th>
                        <th>Saldo Actual</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>${formatCurrency(data.saldoAnterior)}</td>
                        <td>${formatCurrency(data.totalRecargas)}</td>
                        <td>${formatCurrency(data.totalPagos)}</td>
                        <td>${formatCurrency(data.saldoActual)}</td>
                    </tr>
                </tbody>
            </table>
        `;

        document.getElementById('saldoGridContainer').innerHTML = saldoGridHtml;
    }

    function formatCurrency(value) {
        return new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' }).format(value);
    }

    document.getElementById('imprimirExtracto').addEventListener('click', () => {
        const printWindow = window.open('', '_blank');
        const content = `
            <html>
                <head>
                    <title>Extracto</title>
                    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
                </head>
                <body>
                    <h3>Extracto de Movimientos</h3>
                    <div>${document.getElementById('saldoGridContainer').innerHTML}</div>
                    <br>
                    <div>${document.getElementById('extractoGridContainer').innerHTML}</div>
                    <script>
                        window.onload = function() {
                            window.print();
                            window.close();
                        };
                    </script>
                </body>
            </html>
        `;
        printWindow.document.write(content);
        printWindow.document.close();
    });
}
