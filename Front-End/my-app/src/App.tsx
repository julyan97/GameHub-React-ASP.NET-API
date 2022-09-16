import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import NavBar from './Components/NavBar/NavBar';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";
import { Home } from './Components/Home/Home';
import { Login } from './Components/Login/Login';
import { Register } from './Components/Register/Register';
import { IIncomingConnection, SignalRService } from './Services/SignalRHelpers/SignalRService';
import { OutGoingNotificationMethods } from './Services/SignalRHelpers/OutGoingNotificationMethods';

function App() {
  const [Connection, setConnection] = useState(null);
  const [Message, setMessage] = useState("July");

  //SignalR SetUp Begin
  const ReceiveMessage = (message: string) => {
    setMessage(message)
  }

  SignalRService.RegisterIncomingMethods([
    { name: "ReceiveMessage", method: ReceiveMessage },
  ], true, setConnection, Connection);


  //SignalR SetUp End


  return (
    <div className="App">
      <h1>{Message}</h1>
      <button onClick={() => OutGoingNotificationMethods.SendMessage("pesky")}>Button</button>
      <NavBar />
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/login' element={<Login />} />
          <Route path='/register' element={<Register />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
