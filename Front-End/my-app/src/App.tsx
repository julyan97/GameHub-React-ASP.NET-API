import React, { useContext, useEffect, useState } from 'react';
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
import { IContextModel } from './Models/Models';
import { CreateEvent } from './Components/CreateEvent/CreateEvent';
import { AuthService } from './Services/AuthService';

export const AuthContext = React.createContext<IContextModel>({
  setUserName: null,
  username: "",

  setIsAuthentication: null,
  isAuthenticated :false,

  connection : null
});

function App() {
  const [Connection, setConnection] = useState(null);
  const [Message, setMessage] = useState("July");
  const [Name, setName] = useState("")
  const [Authentication, setAuthentication] = useState(false)
  
  const auth = useContext(AuthContext)

  useEffect(() => {
    AuthService.isAuthenticated()
        .then(data => {
            setAuthentication(data.authenticated)
            setName(data.userName);

            // console.log(data.userName);
            // console.log(data.authenticated);
            // console.log(auth.isAuthenticated);
        })
})

  //SignalR SetUp Begin
  const ReceiveMessage = (message: string) => {
    //setMessage(message)
  }

  SignalRService.RegisterIncomingMethods([
    { name: "ReceiveMessage", method: ReceiveMessage },
  ], true, setConnection, Connection);


  //SignalR SetUp End


  return (
    <AuthContext.Provider value={{
      setUserName: setName,
      username: Name,
      setIsAuthentication: setAuthentication,
      isAuthenticated : Authentication,
      connection: Connection
    }}>

      <div className="App">
        <BrowserRouter>
        <NavBar />
          <Routes>
            <Route path='/' element={<Home />} />
            <Route path='/login' element={<Login />} />
            <Route path='/register' element={<Register />} />
            <Route path='/createEvent' element={<CreateEvent />} />
          </Routes>
        </BrowserRouter>
      </div>
    </AuthContext.Provider>
  );
}

export default App;
