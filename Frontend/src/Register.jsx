import { useState } from "react";
import fondo from "./assets/fondo.jpg";

function Register({ goToLogin }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const isValidEmail = (email) => {
  return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
};

  const handleRegister = async () => {

  //VALIDACIÓN DE EMAIL
  if (!isValidEmail(email)) {
    alert("Ingresá un correo válido (ej: usuario@gmail.com)");
    return;
  }

  //VALIDACIÓN DE PASSWORD
  if (password.length < 4) {
    alert("La contraseña debe tener al menos 4 caracteres");
    return;
  }

  const res = await fetch("http://localhost:5171/api/v1/auth/register", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ email, password })
  });

  if (!res.ok) {
    alert("Error al registrarse, El usuario ya existe");
    return;
  }

  alert("Usuario creado correctamente");
  goToLogin();
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
    {/* OSCURECE EL FONDO */}
    <div style={{
      position: "absolute",
      inset: 0,
      background: "rgba(0,0,0,0.6)"
    }} />

    <div className="login-card">
      <h1>Crear cuenta</h1>

      <div className="input-group">
        <input
          placeholder="Correo"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />
      </div>

      <div className="input-group">
        <input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
      </div>

      <button className="login-btn" onClick={handleRegister}>
        Registrarse
      </button>

      <div className="register-link">
        ¿Ya tenés cuenta?{" "}
        <span onClick={goToLogin}>Login</span>
      </div>
    </div>
  </div>
);
}

export default Register;