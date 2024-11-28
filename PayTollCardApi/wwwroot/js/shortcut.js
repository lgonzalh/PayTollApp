// shortcut.js

const SendHorizontal = ({ className = "w-5 h-5" }) => (
    <svg xmlns="http://www.w3.org/2000/svg" className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
        <path d="M3 12h16m0 0l-6-6m6 6l-6 6"/>
    </svg>
);

const PayTollCardApi = () => {
    const [inputText, setInputText] = React.useState('');
    const [resultData, setResultData] = React.useState(null);
    const [statusMessage, setStatusMessage] = React.useState('');

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!inputText.trim()) {
            setStatusMessage('Por favor, ingrese un script SQL.');
            return;
        }

        //http://localhost:5293/api/Sql/execute
        setStatusMessage('Ejecutando el script...');
        fetch('https://paytollcard-2b6b0c89816c.herokuapp.com/api/Sql/execute', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                // Si no usas autenticación, puedes omitir la cabecera Authorization
                // 'Authorization': 'Bearer ' + obtenerToken(),
            },
            body: JSON.stringify({ query: inputText }),
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Error en la respuesta del servidor');
            }
            return response.json();
        })
        .then(data => {
            console.log('Respuesta de la API:', data); // Para depuración
            if (Array.isArray(data)) {
                setStatusMessage('El script fue ejecutado satisfactoriamente.');
                setResultData(data);
            } else {
                const errorMsg = data.message || 'Error, el script no se pudo ejecutar.';
                setStatusMessage(errorMsg);
                setResultData(null);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            setStatusMessage('Error, el script no se pudo ejecutar.');
            setResultData(null);
        });

        setInputText('');
    };

    // function obtenerToken() {
    //     // Si necesitas autenticación, obtén el token almacenado
    //     return localStorage.getItem('token') || '';
    // }

    return (
        <div className="min-h-screen bg-gray-50 flex items-start justify-center">
            <div className="max-w-3xl w-full mx-auto px-4">
                <form onSubmit={handleSubmit} className="mb-6">
                    <div className="relative">
                        <textarea
                            value={inputText}
                            onChange={(e) => setInputText(e.target.value)}
                            className="w-full min-h-[150px] p-4 rounded-md border border-gray-300 focus:ring-2 focus:ring-blue-500 focus:border-transparent resize-y"
                            placeholder="Escriba su script SQL aquí..."
                        />
                        <button
                            type="submit"
                            className="absolute bottom-4 right-4 p-2 bg-blue-500 text-white rounded-full hover:bg-blue-600 transition-colors"
                        >
                            <SendHorizontal />
                        </button>
                    </div>
                </form>

                {statusMessage && (
                    <div className="mb-4 text-center">
                        <p>{statusMessage}</p>
                    </div>
                )}

                {resultData && resultData.length > 0 ? (
                    <div className="border rounded-md overflow-hidden bg-white p-0 grid-container">
                        <div className="h-auto overflow-y-auto">
                            <table className="min-w-full divide-y divide-gray-200">
                                <thead className="bg-gray-50">
                                    <tr>
                                        {Object.keys(resultData[0]).map((key) => (
                                            <th
                                                key={key}
                                                className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                                            >
                                                {key}
                                            </th>
                                        ))}
                                    </tr>
                                </thead>
                                <tbody className="bg-white divide-y divide-gray-200">
                                    {resultData.map((row, index) => (
                                        <tr key={index}>
                                            {Object.keys(row).map((key) => (
                                                <td key={key} className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                                    {row[key] !== null ? row[key].toString() : ''}
                                                </td>
                                            ))}
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>
                ) : null}
            </div>
        </div>
    );
};

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<PayTollCardApi />);