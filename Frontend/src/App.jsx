import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [seats, setSeats] = useState([]);
  const [selectedSeats, setSelectedSeats] = useState([]);
  // Estado para manejar el sector actual (1 para Baja, 2 para Alta)
  const [sectorId, setSectorId] = useState(1);

  useEffect(() => {
    // Pedimos los asientos a la API usando el sectorId actual
    fetch(`http://localhost:5171/api/v1/events/${sectorId}/seats`)
      .then(res => res.json())
      .then(data => {
        setSeats(data);
        // Limpiamos la seleccion anterior al cambiar de sector para evitar errores
        setSelectedSeats([]);
      })
      .catch(err => console.error("Error al conectar con la API:", err));
  }, [sectorId]); // Este efecto se dispara cada vez que cambia el sectorId

  const toggleSeat = (seatId, status) => {
    if (status !== 'Available') return;
    
    if (selectedSeats.includes(seatId)) {
      setSelectedSeats(selectedSeats.filter(id => id !== seatId));
    } else {
      setSelectedSeats([...selectedSeats, seatId]);
    }
  };

  return (
    <div style={{ padding: '20px', textAlign: 'center', fontFamily: 'Arial, sans-serif' }}>
      <h1>Sistema de Ticketing - Entrega 1</h1>
      
      {/* --- SELECTOR DE SECTORES --- */}
      <div style={{ marginBottom: '20px', display: 'flex', gap: '15px', justifyContent: 'center' }}>
        <button 
          onClick={() => setSectorId(1)}
          style={{
            padding: '10px 20px',
            backgroundColor: sectorId === 1 ? '#3498db' : '#ecf0f1',
            color: sectorId === 1 ? 'white' : 'black',
            borderRadius: '5px',
            border: '1px solid #bdc3c7',
            cursor: 'pointer',
            fontWeight: 'bold',
            transition: '0.3s'
          }}>
          Platea Baja (Asientos 1-50)
        </button>
        <button 
          onClick={() => setSectorId(2)}
          style={{
            padding: '10px 20px',
            backgroundColor: sectorId === 2 ? '#3498db' : '#ecf0f1',
            color: sectorId === 2 ? 'white' : 'black',
            borderRadius: '5px',
            border: '1px solid #bdc3c7',
            cursor: 'pointer',
            fontWeight: 'bold',
            transition: '0.3s'
          }}>
          Platea Alta (Asientos 51-100)
        </button>
      </div>

      <h3>Mapa de Asientos: {sectorId === 1 ? 'Platea Baja' : 'Platea Alta'}</h3>
      
      <div style={{ marginBottom: '10px', fontSize: '18px' }}>
        Asientos seleccionados: <strong>{selectedSeats.length}</strong>
      </div>

      {/* --- GRILLA DE ASIENTOS --- */}
      <div style={{ 
        display: 'grid', 
        gridTemplateColumns: 'repeat(10, 45px)', 
        gap: '10px', 
        justifyContent: 'center',
        marginTop: '20px',
        backgroundColor: '#f4f4f4',
        padding: '20px',
        borderRadius: '10px',
        border: '1px solid #ddd'
      }}>
        {seats.map(seat => {
          const isSelected = selectedSeats.includes(seat.id);
          return (
            <div 
              key={seat.id} 
              onClick={() => toggleSeat(seat.id, seat.status)}
              style={{
                width: '45px',
                height: '45px',
                backgroundColor: isSelected ? '#3498db' : (seat.status === 'Available' ? '#2ecc71' : '#e74c3c'),
                color: 'white',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                borderRadius: '5px',
                fontSize: '12px',
                fontWeight: 'bold',
                cursor: seat.status === 'Available' ? 'pointer' : 'not-allowed',
                transform: isSelected ? 'scale(1.1)' : 'scale(1)',
                transition: 'all 0.2s ease',
                boxShadow: isSelected ? '0 0 10px rgba(52, 152, 219, 0.5)' : 'none'
              }}>
              {seat.seatNumber}
            </div>
          );
        })}
      </div>

      {/* --- LEYENDA --- */}
      <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'center', gap: '20px' }}>
        <span>🟢 Disponible</span>
        <span>🔵 Seleccionado</span>
        <span>🔴 Ocupado</span>
      </div>

      {/* --- ACCION SIMULADA --- */}
      <div style={{ marginTop: '30px' }}>
        <button 
          disabled={selectedSeats.length === 0}
          onClick={() => {
            alert(`Reserva simulada en ${sectorId === 1 ? 'Platea Alta' : 'Platea Baja'}. Seleccionaste ${selectedSeats.length} asientos.`);
          }}
          style={{
            padding: '12px 25px',
            fontSize: '16px',
            backgroundColor: selectedSeats.length > 0 ? '#27ae60' : '#bdc3c7',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor: selectedSeats.length > 0 ? 'pointer' : 'not-allowed',
            fontWeight: 'bold',
            transition: 'background-color 0.3s'
          }}>
          Confirmar Selección (Simulado)
        </button>
      </div>
    </div>
  )
}

export default App