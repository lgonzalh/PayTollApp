export default function vehiculoModule() {
    document.getElementById('vehiculoFormContainer').innerHTML = `
        <ul class="nav nav-tabs" id="vehiculoTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="register-tab" data-bs-toggle="tab" data-bs-target="#register" type="button" role="tab" aria-controls="register" aria-selected="true">Registrar Vehículo</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="consult-tab" data-bs-toggle="tab" data-bs-target="#consult" type="button" role="tab" aria-controls="consult" aria-selected="false">Consultar Vehículo</button>
            </li>
        </ul>
        <div class="tab-content" id="vehiculoTabContent">
            <!-- Tab Registro -->
            <div class="tab-pane fade show active" id="register" role="tabpanel" aria-labelledby="register-tab">
                <form id="vehiculoForm">
                    <div class="mb-3">
                        <label for="cedula" class="form-label">Cédula</label>
                        <input type="text" class="form-control" id="cedula" required>
                    </div>
                    <div class="mb-3">
                        <label for="placa" class="form-label">Placa</label>
                        <input type="text" class="form-control" id="placa" required>
                    </div>
                    <div class="mb-3">
                        <label for="categoriaVehiculo" class="form-label">Categoría Vehículo</label>
                        <select class="form-control" id="categoriaVehiculo" required>
                            <option value="1">I - AUTOMÓVILES, CAMPEROS Y CAMIONETAS</option>
                            <option value="2">II - BUSES</option>
                            <option value="3">III - CAMIONES DE DOS EJES PEQUEÑOS</option>
                            <option value="4">IV - CAMIONES DE DOS EJES GRANDES</option>
                            <option value="5">V - CAMIONES DE TRES EJES Y CUATRO EJES</option>
                            <option value="6">VI - CAMIONES DE CINCO EJES</option>
                            <option value="7">VII - CAMIONES DE SEIS EJES</option>
                        </select>
                    </div>
                    <button type="button" class="btn btn-primary" id="registerVehicle">Enviar</button>
                </form>
            </div>
            <!-- Tab Consulta -->
            <div class="tab-pane fade" id="consult" role="tabpanel" aria-labelledby="consult-tab">
                <form id="consultForm">
                    <div class="mb-3">
                        <label for="consultCedula" class="form-label">Cédula</label>
                        <input type="text" class="form-control" id="consultCedula" required>
                    </div>
                    <button type="button" class="btn btn-primary" id="consultVehicle">Consultar</button>
                </form>
                <div id="consultResult" class="mt-3"></div>
            </div>
        </div>
    `;

    // Registro de vehículo
    document.getElementById('registerVehicle').addEventListener('click', async () => {
        const data = {
            cedula: document.getElementById('cedula').value,
            placa: document.getElementById('placa').value,
            categoriaVehiculo: parseInt(document.getElementById('categoriaVehiculo').value),
        };

        try {
            const response = await fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Vehiculos/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            });

            if (!response.ok) throw new Error('Error al registrar el vehículo.');
            alert('El vehículo se ha registrado correctamente.');
            window.location.href = 'index.html';
        } catch (error) {
            console.error('Error:', error);
            alert(error.message);
        }
    });

    // Consulta de vehículo
    document.getElementById('consultVehicle').addEventListener('click', async () => {
        const cedula = document.getElementById('consultCedula').value;
        try {
            const response = await fetch(`https://paytollcard-2b6b0c89816c.herokuapp.com/api/Vehiculos/getByCedula/${cedula}`);
            if (!response.ok) throw new Error('No se encontraron vehículos para esta cédula.');
            
            const vehicles = await response.json();
            let resultHtml = '<h5>Vehículos Registrados:</h5><ul>';
            vehicles.forEach(vehicle => {
                resultHtml += `<li>Placa: ${vehicle.placa}, Categoría: ${vehicle.categoriaVehiculo}</li>`;
            });
            resultHtml += '</ul>';
            document.getElementById('consultResult').innerHTML = resultHtml;
        } catch (error) {
            console.error('Error:', error);
            document.getElementById('consultResult').innerHTML = `<p class="text-danger">${error.message}</p>`;
        }
    });
}
