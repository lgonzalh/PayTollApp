const SendHorizontal = ({ className = "w-5 h-5" }) => (
    <svg xmlns="http://www.w3.org/2000/svg" className={className} viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
        <path d="M3 12h16m0 0l-6-6m6 6l-6 6"/>
    </svg>
);

const PayTollApp = () => {
    const [inputText, setInputText] = React.useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log('Enviado:', inputText);
        setInputText('');
    };

    return (
        <div className="min-h-screen bg-gray-50 flex items-start justify-center">
            <div className="max-w-3xl w-full mx-auto px-4">
                <form onSubmit={handleSubmit} className="mb-6">
                    <div className="relative">
                        <textarea
                            value={inputText}
                            onChange={(e) => setInputText(e.target.value)}
                            className="w-full min-h-[150px] p-4 rounded-md border border-gray-300 focus:ring-2 focus:ring-blue-500 focus:border-transparent resize-y"
                        />
                        <button
                            type="submit"
                            className="absolute bottom-4 right-4 p-2 bg-blue-500 text-white rounded-full hover:bg-blue-600 transition-colors"
                        >
                            <SendHorizontal />
                        </button>
                    </div>
                </form>

                <div className="border rounded-md overflow-hidden bg-white p-0 grid-container">
                    <div className="h-auto overflow-y-auto">
                        <div className="w-full grid grid-cols-6 gap-0">
                            <div className="col-span-6 grid grid-cols-6 gap-0">
                                {[...Array(6)].map((_, index) => (
                                    <div
                                        key={`header-${index}`}
                                        className="bg-gray-200 p-3 border-r border-b border-gray-300 last:border-r-0 min-h-[40px]"
                                    />
                                ))}
                            </div>
                            
                            <div className="col-span-6 grid grid-cols-6 gap-0">
                                {[...Array(6)].map((_, index) => (
                                    <div
                                        key={`content-${index}`}
                                        className="bg-white p-3 border-r border-b border-gray-300 last:border-r-0 min-h-[40px]"
                                    />
                                ))}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(<PayTollApp />);
