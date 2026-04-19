import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const [seats, setSeats] = useState([]);
  const [selectedSeats, setSelectedSeats] = useState([]);

  useEffect(() => {
    fetch('http://localhost:5171/api/v1/events/1/seats')
      .then(res => res.json())
      .then(data => setSeats(data))
      .catch(err => console.error("Error al conectar con la API:", err));
  }, []);

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
      <h3>Evento: Concierto de Rock - Sector 1</h3>
      
      <div style={{ marginBottom: '10px', fontSize: '18px' }}>
        Asientos seleccionados: <strong>{selectedSeats.length}</strong>
      </div>

      <div style={{ 
        display: 'grid', 
        gridTemplateColumns: 'repeat(10, 45px)', 
        gap: '10px', 
        justifyContent: 'center',
        marginTop: '20px',
        backgroundColor: '#f4f4f4',
        padding: '20px',
        borderRadius: '10px'
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

      <div style={{ marginTop: '20px', display: 'flex', justifyContent: 'center', gap: '20px' }}>
        <span>🟢 Disponible</span>
        <span>🔵 Seleccionado</span>
        <span>🔴 Ocupado</span>
      </div>

      {/* --- BOTÓN FIJO (Sin condiciones) --- */}
      <div style={{ marginTop: '30px' }}>
        <button 
          onClick={() => {
            if (selectedSeats.length === 0) {
              alert("Por favor, seleccioná al menos un asiento.");
            } else {
              alert(`Reservando asientos: ${selectedSeats.join(', ')}`);
            }
          }}
          style={{
            padding: '12px 25px',
            fontSize: '16px',
            // El color cambia solo para dar feedback, pero el botón siempre está
            backgroundColor: selectedSeats.length > 0 ? '#3498db' : '#bdc3c7',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor: selectedSeats.length > 0 ? 'pointer' : 'not-allowed',
            fontWeight: 'bold'
          }}>
          Confirmar Selección (Simulado)
        </button>
      </div>
    </div>
  )
}

export default App