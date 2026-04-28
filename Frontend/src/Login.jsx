import { useState } from "react";
import "./Login.css";
import fondo from "./assets/fondo.jpg";

function Login({ onLogin, goToRegister }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const response = await fetch("http://localhost:5171/api/v1/auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          email,
          password
        })
      });

      if (!response.ok) {
        alert("Credenciales incorrectas");
        return;
      }

      const data = await response.json();

      console.log("LOGIN RESPONSE:", data);

      if (!data.userId) {
        alert("Error: el backend no devolvió el userId");
        return;
      }

      localStorage.setItem("userId", data.userId);

      onLogin();

    } catch (error) {
      console.error(error);
      alert("Error de conexión");
    }
  };

  return (
    <div 
      className="login-container"
      style={{
        backgroundImage: `url(${fondo})`,
        backgroundSize: "cover",
        backgroundPosition: "left center",
        backgroundRepeat: "no-repeat",
        width: "100vw",
        height: "100vh",
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        position: "fixed",
        top: 0,
        left: 0
      }}
    >
      {/* OVERLAY OSCURO */}
      <div style={{
        position: "absolute",
        inset: 0,
        background: "rgba(0,0,0,0.6)"
      }} />

      <div className="login-card">
        
        <h1>Bienvenido de nuevo!</h1>
        <p>Ingresa tus credenciales para acceder a tu cuenta.</p>

        <div className="input-group">
          <input 
            type="email" 
            placeholder="Correo"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>

        <div className="input-group">
          <input 
            type="password" 
            placeholder="Contraseña"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        <div className="options">
          <span>Recordarme</span>
          <a href="#">Recuperar contraseña</a>
        </div>

        <button className="login-btn" onClick={handleLogin}>
          Login
        </button>

        <div className="register-link">
          ¿No tenés cuenta?{" "}
          <span onClick={goToRegister}>Registrate</span>
        </div>

      </div>
    </div>
  );
}

export default Login;