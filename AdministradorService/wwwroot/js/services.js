// services.js - Modales para Servicios
document.addEventListener('DOMContentLoaded', () => {
  function showModal(modalId, modalContent) {
      if (document.getElementById(modalId)) {
          document.getElementById(modalId).remove();
      }
      const modalHTML = `
          <div class="modal fade" id="${modalId}" tabindex="-1" aria-labelledby="${modalId}Label" aria-hidden="true">
              <div class="modal-dialog">
                  <div class="modal-content">
                      <div class="modal-header">
                          <h5 class="modal-title">${modalContent.title}</h5>
                          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                      </div>
                      <div class="modal-body">${modalContent.body}</div>
                      <div class="modal-footer">
                          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                      </div>
                  </div>
              </div>
          </div>`;
      document.body.insertAdjacentHTML('beforeend', modalHTML);
      new bootstrap.Modal(document.getElementById(modalId)).show();
  }

  const btnRegistrarVehiculo = document.getElementById('btnRegistrarVehiculo');
  const btnRecargarTarjeta = document.getElementById('btnRecargarTarjeta');
  const btnExtractos = document.getElementById('btnExtractos');
  const btnSolicitudes = document.getElementById('btnSolicitudes');

  if (btnRegistrarVehiculo) {
      btnRegistrarVehiculo.addEventListener('click', () => {
          showModal('vehicleModal', {
              title: 'Registrar Vehículo',
              body: '<div id="vehiculoFormContainer"></div>'
          });
          import('./vehiculo.js').then(module => module.default());
      });
  }

  if (btnRecargarTarjeta) {
      btnRecargarTarjeta.addEventListener('click', () => {
          showModal('recargaModal', {
              title: 'Recargar Tarjeta',
              body: '<div id="recargaFormContainer"></div>'
          });
          import('./recarga.js').then(module => module.default());
      });
  }

  if (btnExtractos) {
      btnExtractos.addEventListener('click', () => {
          showModal('extractoModal', {
              title: 'Extractos',
              body: '<div id="extractoFormContainer"></div>'
          });
          import('./extracto.js').then(module => module.default());
      });
  }

  if (btnSolicitudes) {
      btnSolicitudes.addEventListener('click', () => {
          showModal('solicitudModal', {
              title: 'Centro de Solicitudes',
              body: '<div id="solicitudFormContainer"></div>'
          });
          import('./solicitud.js').then(module => module.default());
      });
  }
});
