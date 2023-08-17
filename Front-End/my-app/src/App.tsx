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
import { CreateEvent } from './Components/GameEvents/CreateEvent/CreateEvent';
import { AuthService } from './Services/AuthService';
import { EventsPage } from './Components/GameEvents/EventsPage/EventsPage';
import { EventDetailsPage } from './Components/GameEvents/EventDetailsPage/EventDetailsPage';
import RequireAuth from './Components/RequireAuth/RequireAuth';

export const AuthContext = React.createContext<IContextModel>({
  setId: null,
  id: "",

  setUserName: null,
  username: "",

  setIsAuthentication: null,
  isAuthenticated: false,

  setRoles: null,
  roles: [],

  connection: null

});

function App() {
  const [Connection, setConnection] = useState(null);
  const [Id, setId] = useState("")
  const [Name, setName] = useState("")
  const [Authentication, setAuthentication] = useState(false)
  const [Roles, setRoles] = useState<Array<string>>([])

  const auth = useContext(AuthContext)

  useEffect(() => {
    AuthService.isAuthenticated()
      .then(data => {
        setAuthentication(data.authenticated)
        setName(data.userName);
        setId(data.id);
        setRoles(data.roles)

        // console.log(data.userName);
        // console.log(data.authenticated);
        // console.log(auth.isAuthenticated);
      })
  }, [])

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
      setId: setId,
      id: Id,
      setUserName: setName,
      username: Name,
      setIsAuthentication: setAuthentication,
      isAuthenticated: Authentication,
      setRoles: setRoles,
      roles: Roles,

      connection: Connection
    }}>

      <div className="App">
        <BrowserRouter>
          <NavBar />
          <Routes>

            <Route path='/' element={<Home />} />
            {!Authentication ? 
            <>
            <Route path='/login' element={<Login />} />
            <Route path='/register' element={<Register />} />
            </>: <></>
            }

            <Route element={<RequireAuth allowedRoles={["User", "Admin"]} />}>
              <Route path='/createEvent' element={<CreateEvent />} />
              <Route path='/home' element={<EventsPage />} />
              <Route path='/eventDetails' element={<EventDetailsPage />} />
            </Route>
            
            <Route path='*' element={<Home />} />

          </Routes>
        </BrowserRouter>
      </div>
    </AuthContext.Provider>
  );
}

export default App;
