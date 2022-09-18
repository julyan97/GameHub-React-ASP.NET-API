import * as React from 'react';
import { useContext, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../../App';
import { AuthService } from '../../Services/AuthService';

export interface ILoginProps {
}

export function Login(props: ILoginProps) {
    const username: any = useRef(null);
    const Password: any = useRef(null);
    const RememberMe: any = useRef(false);
    const navigate = useNavigate();
    const auth = useContext(AuthContext);
    
    const Login = (e: any) =>{
        AuthService.onLogin(e, username.current.value, Password.current.value)
        .then(res =>{
            auth.isAuthenticated = res.result;
            navigate("/");
            console.log(res.result)
        })
    }
    return (
        <>
            {/* Hello world */}
            <div className="text-center" style={{ marginTop: "5%" }}>
                <h1 className="text-light">Login</h1>
            </div>
            <div className=" d-block text-center">
                <form className="d-inline-block p-3" id="account" >
                    <div>
                        <div
                            className="col-md-12 col-md-6 mb-3 d-inline-block"
                            style={{ display: "inline-block" }}
                        >
                            <div className="text-danger" asp-validation-summary="All" />
                            <label style={{ color: "white" }} htmlFor="validationServer01">
                                UserName
                            </label>
                            <div>
                                <input ref={username} asp-for="UserName" className="form-control " />
                                <span className="text-danger" asp-validation-for="UserName" />
                                <div style={{ color: "white" }} className="valid-feedback"></div>
                            </div>
                        </div>
                        <div
                            className="col-md-12 col-md-6 mb-3 d-inline-block"
                            style={{ display: "inline-block !important" }}
                        >
                            <label style={{ color: "white" }}>Password</label>
                            <input ref={Password} asp-for="Password" className="form-control " />
                            <span className="text-danger" asp-validation-for="Password" />
                        </div>
                        <div
                            className="col-md-12 col-md-6 mb-3 d-inline-block text-left mt-2"
                            style={{ display: "block" }}
                        >
                            <input ref={RememberMe} style={{ marginRight: "0.5em" }} asp-for="RememberMe" type={'checkbox'} />
                            <label style={{ color: "forestgreen" }}>Remember me</label>
                        </div>
                    </div>
                    <button onClick={(e) => Login(e)} className="btn btn-primary border-0 bg-dark btn-lg" >
                        Login
                    </button>
                </form>
            </div>
        </>
    );
}
