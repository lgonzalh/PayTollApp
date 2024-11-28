const { useState } = React;

const AdminDashboard = () => {
    const [activeSection, setActiveSection] = useState('usuarios');
    const [searchTerm, setSearchTerm] = useState('');

    // Datos de ejemplo
    const mockData = {
        usuarios: [
            { id: 1, usuario: 'usuario1', email: 'usuario1@email.com', fecha: '2024-01-01' },
            { id: 2, usuario: 'usuario2', email: 'usuario2@email.com', fecha: '2024-01-02' }
        ],
        vehiculos: [
            { id: 1, placa: 'ABC123', tipo: 'Automóvil', propietario: 'Juan Pérez' },
            { id: 2, placa: 'XYZ789', tipo: 'Camioneta', propietario: 'María García' }
        ]
    };

    const menuItems = [
        { id: 'usuarios', label: 'Usuarios', icon: 'users' },
        { id: 'vehiculos', label: 'Vehículos', icon: 'truck' },
        { id: 'recargas', label: 'Recargas', icon: 'credit-card' },
        { id: 'extractos', label: 'Extractos', icon: 'file-text' },
        { id: 'solicitudes', label: 'Solicitudes', icon: 'bell' },
        { id: 'mensajes', label: 'Mensajes', icon: 'message-square' }
    ];

    const renderContent = () => {
        const data = mockData[activeSection] || [];
        
        return (
            <div className="main-content">
                <input
                    type="text"
                    placeholder="Buscar..."
                    className="search-input"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
                
                <table className="data-table">
                    <thead>
                        <tr>
                            {data.length > 0 && Object.keys(data[0]).map(header => (
                                <th key={header}>{header}</th>
                            ))}
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map(item => (
                            <tr key={item.id}>
                                {Object.values(item).map((value, index) => (
                                    <td key={index}>{value}</td>
                                ))}
                                <td>
                                    <button className="btn btn-sm btn-primary me-2">Editar</button>
                                    <button className="btn btn-sm btn-danger">Eliminar</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    };

    return (
        <div className="row">
            <div className="col-md-3">
                <div className="sidebar">
                    <h2 className="h4 mb-4">Panel de Control</h2>
                    <nav>
                        {menuItems.map(item => (
                            <div
                                key={item.id}
                                className={`menu-item ${activeSection === item.id ? 'active' : ''}`}
                                onClick={() => setActiveSection(item.id)}
                            >
                                <i data-feather={item.icon} className="me-2"></i>
                                {item.label}
                            </div>
                        ))}
                    </nav>
                </div>
            </div>
            <div className="col-md-9">
                <h2 className="h4 mb-4">
                    {menuItems.find(item => item.id === activeSection)?.label}
                </h2>
                {renderContent()}
            </div>
        </div>
    );
};

// Inicializar los íconos después de que el componente se monte
ReactDOM.render(<AdminDashboard />, document.getElementById('root'));
feather.replace();