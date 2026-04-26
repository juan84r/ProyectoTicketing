import { useEffect, useState } from 'react'
import './App.css'
import Login from "./Login";
import Register from "./Register";

function App() {
  const [seats, setSeats] = useState([]);
  const [selectedSeats, setSelectedSeats] = useState([]);
  const [sectorId, setSectorId] = useState(1);

  const [isLogged, setIsLogged] = useState(
    !!localStorage.getItem("userId")
  );

  const [view, setView] = useState("login"); // login | register

  // FUNCION PARA CARGAR ASIENTOS
  const loadSeats = () => {
    fetch(`http://localhost:5171/api/v1/events/${sectorId}/seats`)
      .then(res => res.json())
      .then(data => setSeats(data))
      .catch(err => console.error(err));
  };

  useEffect(() => {
    if (!isLogged) return;
    loadSeats();
    
  }, [sectorId, isLogged]);

  const toggleSeat = (seatId, status) => {
    if (status !== 'Available') return;

    if (selectedSeats.includes(seatId)) {
      setSelectedSeats(selectedSeats.filter(id => id !== seatId));
    } else {
      setSelectedSeats([...selectedSeats, seatId]);
    }
  };

  const handleConfirm = async () => {
  const userId = localStorage.getItem("userId");

  try {
    const response = await fetch("http://localhost:5171/api/v1/reservations", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        userId: parseInt(userId),
        seatIds: selectedSeats
      })
    });

    const text = await response.text(); 
    console.log("RESPUESTA BACKEND:", text);

    if (!response.ok) {
      alert("Error al reservar: " + text);
      return;
    }

    alert("Reserva realizada con éxito");
    setSelectedSeats([]);
    loadSeats();

  } catch (error) {
    console.error(error);
    alert("Error de conexión");
  }
};

  const handleLogout = () => {
    localStorage.removeItem("userId");
    setIsLogged(false);
    setView("login");
  };

  //SI NO ESTÁ LOGUEADO → MOSTRAR LOGIN / REGISTER
  if (!isLogged) {
    if (view === "login") {
      return (
        <Login 
          onLogin={() => setIsLogged(true)}
          goToRegister={() => setView("register")}
        />
      );
    }

    return (
      <Register 
        goToLogin={() => setView("login")}
      />
    );
  }

  
  return (
    <div style={{ padding: '20px', textAlign: 'center', fontFamily: 'Arial, sans-serif' }}>
      
      <h1>Sistema de Ticketing - Entrega 1</h1>

      <button 
        onClick={handleLogout}
        style={{
          position: "absolute",
          top: "20px",
          right: "20px",
          padding: "8px 15px",
          backgroundColor: "#e74c3c",
          color: "white",
          border: "none",
          borderRadius: "5px"
        }}>
        Logout
      </button>

      {/* SECTORES */}
      <div style={{ marginBottom: '20px', display: 'flex', gap: '15px', justifyContent: 'center' }}>
        <button onClick={() => setSectorId(1)}>
          Platea Baja
        </button>

        <button onClick={() => setSectorId(2)}>
          Platea Alta
        </button>
      </div>

      <h3>{sectorId === 1 ? 'Platea Baja' : 'Platea Alta'}</h3>

      <div>
        Asientos seleccionados: <strong>{selectedSeats.length}</strong>
      </div>

      {/* GRILLA */}
      <div style={{ 
        display: 'grid', 
        gridTemplateColumns: 'repeat(10, 45px)', 
        gap: '10px', 
        justifyContent: 'center',
        marginTop: '20px'
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
                backgroundColor: isSelected 
                  ? '#3498db' 
                  : (seat.status === 'Available' ? '#2ecc71' : '#e74c3c'),
                color: 'white',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                borderRadius: '5px',
                cursor: seat.status === 'Available' ? 'pointer' : 'not-allowed'
              }}>
              {seat.seatNumber}
            </div>
          );
        })}
      </div>

      <div style={{ marginTop: '30px' }}>
        <button 
          disabled={selectedSeats.length === 0}
          onClick={handleConfirm}
        >
          Confirmar Reserva
        </button>
      </div>

    </div>
  );
}

export default App;