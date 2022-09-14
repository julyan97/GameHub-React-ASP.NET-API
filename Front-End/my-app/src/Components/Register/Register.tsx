import * as React from 'react';
import { useRef } from 'react';
import { useNavigate } from 'react-router-dom';

export interface IRegisterProps {
}

export function Register(props: IRegisterProps) {
    const username: any = useRef(null);
    const email: any = useRef(null);
    const Password: any = useRef(null);
    const cPassword: any = useRef(null);

    const navigate = useNavigate();
    const onRegister = async (e: any) => {
        e.preventDefault();
        if (Password.current.value === cPassword.current.value) {
            const res = await fetch("https://localhost:7285/api/Auth/Register", {
                method: "Post",
                headers: {
                    'Content-Type': 'application/json'
                    // 'Content-Type': 'application/x-www-form-urlencoded',
                },
                credentials: "include",
                body: JSON.stringify({ Name: username.current.value, Email: email.current.value, Password: Password.current.value })
            })
            navigate("/");
            console.log(res.json)
        }
    }
    return (
        <>
            <h1 className="text-light" style={{ marginTop: "2em", textAlign: "center" }}>
                Register
            </h1>
            <div className=" d-block text-center" style={{ height: "35rem" }}>
                <form
                    className="d-inline-block p-3"
                >
                    <div className="form-col">
                        <div className="col-md-12 mb-2">
                            <label style={{ color: "white" }}>Username</label>
                            <input ref={username} className="form-control" asp-for="UserName" />
                            <div className="valid-feedback">
                                <span
                                    asp-validation-for="UserName"
                                    style={{ color: "red", fontSize: 15 }}
                                >
                                    {" "}
                                    Looks good!
                                </span>
                            </div>
                        </div>
                        <div className="col-md-12 mb-2">
                            <label style={{ color: "white" }}>Email</label>
                            <input ref={email} className="form-control" asp-for="Email" />
                            <div>
                                <span
                                    asp-validation-for="Email"
                                    style={{ color: "red", fontSize: 15 }}
                                />
                            </div>
                        </div>
                        <div className="col-md-12 mb-3">
                            <label style={{ color: "white" }}>Password</label>
                            <input ref={Password} asp-for="Password" className="form-control" />
                            <div>
                                <span
                                    asp-validation-for="Password"
                                    style={{ color: "red", fontSize: 15 }}
                                />
                            </div>
                        </div>
                        <div className="col-md-12 mb-3">
                            <label style={{ color: "white" }}>Confirm Password</label>
                            <input ref={cPassword} asp-for="ConfirmPassword" className="form-control" />
                            <div>
                                <span
                                    asp-validation-for="ConfirmPassword"
                                    style={{ color: "red", fontSize: 15 }}
                                />
                            </div>
                        </div>
                    </div>
                    <button className="btn btn-primary border-0 bg-dark btn-lg" onClick={(e)=>onRegister(e)}>
                        Let's Go
                    </button>
                    <div
                        className="g-recaptcha mt-3"
                        style={{ display: "none" }}
                        data-sitekey="your_site_key"
                    />
                </form>
            </div>
        </>

    );
}
