import { faBell, faCoffee, faIgloo, faNewspaper, faPlus } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as React from 'react';
import { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../../App';
import logo from "../../Root/img/output-onlinepngtools.png"
import { AuthService } from '../../Services/AuthService';
import { NotificationService } from '../../Services/NotificationService';
import { OutGoingNotificationMethods } from '../../Services/SignalRHelpers/OutGoingNotificationMethods';
import { SignalRService } from '../../Services/SignalRHelpers/SignalRService';
import { Icon } from './Awsome-Icons/Icon';
import style from "./style.module.css"
export interface INavBarProps {
}

export default function NavBar(props: INavBarProps) {
    const [userName, setUserName] = useState("");
    const [Counter, setCounter] = useState(0)
    const [Notifications, setNotifications] = useState<Array<any>>([])

    const nav = useNavigate();
    const auth = useContext(AuthContext)

    const LOgOut = () => {
        AuthService.onLogOut()
            .then(() => {
                nav("/");
                auth.setUserName("");
                auth.setIsAuthentication(false);
                auth.setId("");
                auth.setRoles([]);
            })
    }

    useEffect(() => {
        NotificationsUpdate();
    }, [])

    const NotificationsUpdate = async () => {

        await NotificationService.GetAll()
            .then((res: Array<any>) => {
                setCounter(res.filter(x => x.isRead === false).length);
                setNotifications(res);
                console.log(res)
                console.log(res.filter(x => x.isRead == true).length)
            })

    }

    const SetNotificationToRead = async (notificationId : string) =>{
        await NotificationService.SetToRead(notificationId);
        await NotificationsUpdate(); 
    }

    //SignalR Begin

    SignalRService.RegisterIncomingMethods([
        { name: "NotificationsUpdate", method: NotificationsUpdate }
    ], false);

    //SignalR End
    return (
        <>
            <link rel="stylesheet" href="../../Root/css/index.css" />
            <link rel="stylesheet" href="../../Root/css/style.css" />
            <nav className="p-2" style={{ backgroundColor: '#1a031d62' }}>

                <div style={{ float: 'left', display: 'inline-block', margin: "-10px" }}>
                    <a style={{ color: 'rgb(255, 255, 255)', padding: 0, marginLeft: '10%' }} className={style.nav_link} onClick={() => nav("/")}>
                        <img src={logo} style={{ height: '2em', width: '9em' }} />
                    </a>
                </div>
                <ul className="nav justify-content-end" style={{ userSelect: 'none' }}>

                    {!auth.isAuthenticated ? (<>
                        <li className="nav-item">
                            <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} onClick={() => nav("/login")}>Login</a>
                        </li>
                        <li className="nav-item">
                            <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} onClick={() => nav("/register")}>Register</a>
                        </li>
                    </>) :
                        (<>
                            <li className="nav-item">
                                <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} >{auth.username}</a>
                            </li>
                            <li className=' nav-item'>
                                <div className="btn-group" style={{ userSelect: "none", top: "50%", left: '50%', transform: 'translate(-50%, -50%) scale(1.2)' }}>
                                    <div className="dropdown">
                                        <a
                                            style={{boxSizing:"border-box",padding:"0.2px",borderRadius:"30%",marginRight:"1em"}}
                                            className="nav-link waves-effect waves-light"
                                            role="button"
                                            id="dropdownMenuLink"
                                            data-bs-toggle="dropdown"
                                            aria-expanded="false"
                                        >
                                            <span className="nav-link d-inline-block p-1 text-white">
                                                {Counter}
                                            </span>
                                            <FontAwesomeIcon icon={faBell} className="nav-link active d-inline-block p-0" style={{ color: "white" }} />
                                        </a>
                                        <ul

                                            id='nott' className="my-custom-scrollbar my-custom-scrollbar-primary dropdown-menu text-white"
                                            aria-labelledby="dropdownMenuLink">
                                            {Notifications.map(x =>
                                            {
                                                let NotificationRead =""
                                                if(x.isRead === false)
                                                NotificationRead = "red"

                                                return(
                                                <li key={x.id} className='d-block' onClick={() => SetNotificationToRead(x.id)}>
                                                    <a style={{ textDecoration: "none", color: "white" ,background:`${NotificationRead}`} }>
                                                        {x.message}
                                                    </a>
                                                </li>
                                                )
                                            }
                                            )}

                                        </ul>
                                    </div>
                                </div>
                            </li>
                            <li className='nav-item' onClick={() => nav("/home")} >
                                <Icon type={faIgloo} />
                            </li>
                            {/* <li className='nav-item' >
                                <Icon type={faNewspaper} />
                            </li> */}
                            <li className='nav-item' onClick={() => nav("/createEvent")} >
                                <Icon type={faPlus} />
                            </li>
                            <li className="nav-item" onClick={() => LOgOut()}>
                                <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} >LogOut</a>
                            </li>
                        </>)
                    }

                </ul>
                <style dangerouslySetInnerHTML={{ __html: "\n    #nott {\n        background-color: #151226f7;\n        width: 275px;\n        word-break: break-word;\n        overflow: hidden;\n        user-select: none;\n        overflow-y: scroll;\n        max-height: 280px;\n        left:0px;\n    }\n    #menuOpt {\n        display: none;\n    }\n    #userMenu{\n        display:inline;\n    }\n    .sidenav {\n        height: 100%;\n        width: 0;\n        position: fixed;\n        z-index: 1;\n        top: 0;\n        right: 0;\n        background-color: #0d091b;\n        overflow-x: hidden;\n        transition: 0.5s;\n        padding-top: 60px;\n    }\n\n        .sidenav a {\n            padding: 8px 8px 8px 32px;\n            text-decoration: none;\n            font-size: 25px;\n            color: #818181;\n            display: block;\n            transition: 0.3s;\n        }\n\n            .sidenav a:hover {\n                color: #f1f1f1;\n            }\n\n        .sidenav .closebtn {\n            position: absolute;\n            top: 0;\n            right: 25px;\n            font-size: 36px;\n            margin-left: 50px;\n        }\n\n    ::-webkit-scrollbar {\n        width: 12px;\n    }\n\n    ::-webkit-scrollbar-track {\n        background-color: #0d1626be;\n        -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);\n    }\n\n    ::-webkit-scrollbar-thumb {\n        -webkit-border-radius: 10px;\n        border-radius: 10px;\n        background: #19182c;\n        -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.5);\n    }\n    @media screen and (max-width: 800px) {\n        #nott{\n            left:-250px;\n\n        }\n        #userMenu{\n            display:none;\n        }\n        #menuOpt {\n            display: inline;\n        }\n        #homeLi{\n            display:none;\n        }\n        #newsLi{\n            display:none;\n        }\n        #addLi{\n            display:none;\n        }\n        #logoutLi{\n            display:none;\n        }\n    }\n\n" }} />
            </nav>

        </>
    );
}
