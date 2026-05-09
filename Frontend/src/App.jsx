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

  const [view, setView] = useState("login");

  // Temporizador
  const [timeLeft, setTimeLeft] = useState(0);
  const [timerActive, setTimerActive] = useState(false);

  // Ahora guardamos múltiples reservationIds
  const [reservationIds, setReservationIds] = useState([]);

  // Cargar asientos
  const loadSeats = () => {
    fetch(`http://localhost:5171/api/v1/events/${sectorId}/seats`)
      .then(res => res.json())
      .then(data => setSeats(data))
      .catch(err => console.error(err));
  };

  // Recargar cuando cambia sector o login
  useEffect(() => {

    if (!isLogged) return;

    loadSeats();

  }, [sectorId, isLogged]);

  // Temporizador de expiración
  useEffect(() => {

    if (!timerActive) return;

    if (timeLeft <= 0) {

      setTimerActive(false);

      setReservationIds([]);

      alert("La reserva expiró");

      loadSeats();

      return;
    }

    const interval = setInterval(() => {
      setTimeLeft(prev => prev - 1);
    }, 1000);

    return () => clearInterval(interval);

  }, [timeLeft, timerActive]);

  // Refrescar asientos cada 5 segundos
  useEffect(() => {

    if (!isLogged) return;

    const interval = setInterval(() => {
      loadSeats();
    }, 5000);

    return () => clearInterval(interval);

  }, [sectorId, isLogged]);

  // Seleccionar asiento
  const toggleSeat = (seatId, status) => {

    if (status !== 'Available') return;

    if (selectedSeats.includes(seatId)) {

      setSelectedSeats(
        selectedSeats.filter(id => id !== seatId)
      );

    } else {

      setSelectedSeats([
        ...selectedSeats,
        seatId
      ]);
    }
  };

  // Limpiar selección local
  const resetLocalSelection = () => {

    if (
      window.confirm(
        "¿Deseas limpiar los asientos seleccionados actualmente?"
      )
    ) {
      setSelectedSeats([]);
    }
  };

  // Reservar
  const handleConfirm = async () => {

    const userId = localStorage.getItem("userId");

    if (selectedSeats.length === 0) return;

    try {

      const response = await fetch(
        "http://localhost:5171/api/v1/reservations",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({
            userId: parseInt(userId),
            seatIds: selectedSeats
          })
        }
      );

      // Manejo de errores
      if (!response.ok) {

        if (response.status === 409) {

          alert("Otro usuario reservó el asiento antes.");

          loadSeats();

          return;
        }

        const text = await response.text();

        alert("Error al reservar: " + text);

        return;
      }

      // Obtener respuesta
      const data = await response.json();

      // Guardar TODOS los reservationIds
      setReservationIds(data.reservationIds);

      alert("Reserva realizada con éxito");

      // Iniciar temporizador
      setTimeLeft(300);

      setTimerActive(true);

      // Limpiar selección
      setSelectedSeats([]);

      // Refrescar mapa
      loadSeats();

    } catch (error) {

      console.error(error);

      alert("Error de conexión");
    }
  };

  // Confirmar pago
  const handlePayment = async () => {

    if (reservationIds.length === 0) return;

    try {

      for (const reservationId of reservationIds) {

        const response = await fetch(
          "http://localhost:5171/api/v1/reservations/confirm",
          {
            method: "POST",
            headers: {
              "Content-Type": "application/json"
            },
            body: JSON.stringify({
              reservationId: reservationId
            })
          }
        );

        if (!response.ok) {

          alert("No se pudo confirmar uno de los pagos");

          return;
        }
      }

      alert("Pago confirmado correctamente");

      // Detener timer
      setTimerActive(false);

      setTimeLeft(0);

      // Limpiar reservas
      setReservationIds([]);

      // Limpiar selección
      setSelectedSeats([]);

      // Refrescar asientos
      loadSeats();

    } catch (error) {

      console.error(error);

      alert("Error de conexión");
    }
  };

  // Logout
  const handleLogout = () => {

    localStorage.removeItem("userId");

    setIsLogged(false);

    setView("login");

    setSelectedSeats([]);
  };

  // Pantallas login/register
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

    <div
      style={{
        padding: '20px',
        textAlign: 'center',
        fontFamily: 'Arial, sans-serif',
        backgroundColor: '#1a1a1a',
        color: '#ffffff',
        minHeight: '100vh'
      }}
    >

      <h1
        style={{
          color: '#ecf0f1',
          marginBottom: '30px'
        }}
      >
        Sistema de Ticketing - Entrega 2
      </h1>

      {/* Logout */}
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
          borderRadius: "5px",
          cursor: "pointer",
          fontWeight: 'bold'
        }}
      >
        Logout
      </button>

      {/* Sectores */}
      <div
        style={{
          marginBottom: '20px',
          display: 'flex',
          gap: '15px',
          justifyContent: 'center'
        }}
      >

        <button
          onClick={() => setSectorId(1)}
          style={{
            padding: '10px 20px',
            backgroundColor:
              sectorId === 1
                ? '#3498db'
                : '#2c3e50',
            color: 'white',
            borderRadius: '5px',
            cursor: 'pointer',
            border: '1px solid #34495e',
            fontWeight: 'bold'
          }}
        >
          Platea Baja
        </button>

        <button
          onClick={() => setSectorId(2)}
          style={{
            padding: '10px 20px',
            backgroundColor:
              sectorId === 2
                ? '#3498db'
                : '#2c3e50',
            color: 'white',
            borderRadius: '5px',
            cursor: 'pointer',
            border: '1px solid #34495e',
            fontWeight: 'bold'
          }}
        >
          Platea Alta
        </button>
      </div>

      {/* Timer */}
      {timerActive && (
        <div
          style={{
            backgroundColor: "#f39c12",
            color: "white",
            padding: "10px",
            borderRadius: "8px",
            width: "250px",
            margin: "0 auto 20px auto",
            fontWeight: "bold",
            fontSize: "1.2rem"
          }}
        >
          Tiempo restante:{" "}
          {Math.floor(timeLeft / 60)}:
          {String(timeLeft % 60).padStart(2, "0")}
        </div>
      )}

      <h3 style={{ color: '#bdc3c7' }}>
        {sectorId === 1
          ? 'Mapa: Platea Baja'
          : 'Mapa: Platea Alta'}
      </h3>

      <div
        style={{
          marginBottom: '10px',
          fontSize: '1.1rem'
        }}
      >
        Asientos seleccionados:
        <strong style={{ color: '#3498db' }}>
          {" "} {selectedSeats.length}
        </strong>
      </div>

      {/* Grilla */}
      <div
        style={{
          display: 'grid',
          gridTemplateColumns: 'repeat(10, 45px)',
          gap: '10px',
          justifyContent: 'center',
          marginTop: '20px',
          backgroundColor: '#2c3e50',
          padding: '20px',
          borderRadius: '12px',
          boxShadow: '0 4px 15px rgba(0,0,0,0.3)'
        }}
      >

        {seats.map(seat => {

          const isSelected =
            selectedSeats.includes(seat.id);

          return (
            <div
              key={seat.id}
              onClick={() =>
                toggleSeat(seat.id, seat.status)
              }
              style={{
                width: '45px',
                height: '45px',

                backgroundColor:

                  isSelected
                    ? '#3498db'

                    : (

                      seat.status === 'Available'

                        ? '#27ae60'

                        : '#c0392b'
                    ),

                color: 'white',

                display: 'flex',

                alignItems: 'center',

                justifyContent: 'center',

                borderRadius: '6px',

                fontSize: '12px',

                fontWeight: 'bold',

                cursor:
                  seat.status === 'Available'
                    ? 'pointer'
                    : 'not-allowed',

                transition: '0.2s',

                transform:
                  isSelected
                    ? 'scale(1.1)'
                    : 'scale(1)',

                border:
                  '1px solid rgba(255,255,255,0.1)'
              }}
            >
              {seat.seatNumber}
            </div>
          );
        })}
      </div>

      {/* Botones */}
      <div
        style={{
          marginTop: '30px',
          display: 'flex',
          justifyContent: 'center',
          gap: '15px'
        }}
      >

        <button
          disabled={selectedSeats.length === 0}
          onClick={resetLocalSelection}
          style={{
            padding: '12px 25px',
            fontSize: '16px',
            backgroundColor:
              selectedSeats.length > 0
                ? '#e74c3c'
                : '#7f8c8d',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor:
              selectedSeats.length > 0
                ? 'pointer'
                : 'not-allowed',
            fontWeight: 'bold',
            opacity:
              selectedSeats.length > 0
                ? 1
                : 0.5
          }}
        >
          Limpiar Selección
        </button>

        <button
          disabled={selectedSeats.length === 0}
          onClick={handleConfirm}
          style={{
            padding: '12px 30px',
            fontSize: '16px',
            backgroundColor:
              selectedSeats.length > 0
                ? '#2ecc71'
                : '#7f8c8d',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor:
              selectedSeats.length > 0
                ? 'pointer'
                : 'not-allowed',
            fontWeight: 'bold',
            opacity:
              selectedSeats.length > 0
                ? 1
                : 0.5
          }}
        >
          Confirmar Reserva ({selectedSeats.length})
        </button>

      </div>

      {/* Botón pagar */}
      {timerActive && reservationIds.length > 0 && (
        <div style={{ marginTop: '20px' }}>

          <button
            onClick={handlePayment}
            style={{
              padding: '14px 35px',
              fontSize: '18px',
              backgroundColor: '#f1c40f',
              color: '#2c3e50',
              border: 'none',
              borderRadius: '8px',
              cursor: 'pointer',
              fontWeight: 'bold'
            }}
          >
            Confirmar Pago
          </button>

        </div>
      )}

    </div>
  );
}

export default App;