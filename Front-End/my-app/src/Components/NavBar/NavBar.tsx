import * as React from 'react';
import { useEffect, useState } from 'react';
import logo from "../../Root/img/output-onlinepngtools.png"
import { AuthService } from '../../Services/AuthService';
import style from "./style.module.css"
export interface INavBarProps {
}

export default function NavBar(props: INavBarProps) {
    const [authenticated, setAuthenticated] = useState(false);
    const [userName, setUserName] = useState("");

    useEffect(() => {
        AuthService.isAuthenticated()
            .then(data => {
                setAuthenticated(data.authenticated);
                setUserName(data.userName);

                console.log(data.userName);
                console.log(data.authenticated);
            })

    }, [])

    const onLogOut = async () => {
        const res = await fetch("https://localhost:7285/api/Auth/Logout", {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
        });
        document.location.href = "/";
    }

    return (
        <>
            <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" />


            <link rel="stylesheet" href="../../Root/css/index.css" />
            <link rel="stylesheet" href="../../Root/css/style.css" />
            <nav className="p-2" style={{ backgroundColor: '#1a031d62' }}>
                <div style={{ float: 'left', display: 'inline-block' }}>
                    <a style={{ color: 'rgb(255, 255, 255)', padding: 0, marginLeft: '10%' }} className={style.nav_link} href="/home">
                        <img src={logo} style={{ height: '2em', width: '9em' }} />
                    </a>
                </div>
                <ul className="nav justify-content-end" style={{ userSelect: 'none' }}>
                    {!authenticated ? (<>
                        <li className="nav-item">
                            <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} href="/login">Login</a>
                        </li>
                        <li className="nav-item">
                            <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} href="/register">Register</a>
                        </li>
                    </>) :
                        (<>
                            <li className="nav-item">
                                <a className={style.nav_link} style={{ color: 'rgb(255, 255, 255)' }} >{userName}</a>
                            </li>
                            <li className="nav-item" onClick={() => onLogOut()}>
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
