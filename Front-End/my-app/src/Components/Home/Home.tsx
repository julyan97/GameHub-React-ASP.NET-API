import * as React from 'react';
import { useState } from 'react';
import { AuthService } from '../../Services/AuthService';

export interface IHomeProps {
}

export function Home(props: IHomeProps) {
    const [authenticated, setAuthenticated] = useState(false)

    React.useEffect(() => {
        AuthService.isAuthenticated()
        .then(data => setAuthenticated(data.authenticated));
    }, [])
    
    return (
        <>
            {/* Hello world */}
            <div
                className="jumbotron w-75 main-div text-left p-3"
                style={{ backgroundColor: "#08101dce" }}
            >
                <h1 className="display-4">Hello, gamer!</h1>
                <p className="lead">
                    This is the place where you can find your dream team.
                </p>
                {!authenticated ? (<>
                <hr className="my-4" />
                <p>But first, you need to navigate to one of the following pages.</p>
                <a
                    className="btn btn-primary border-0 bg-dark btn-lg"
                    href="/login"
                    role="button"
                >
                    Login
                </a>
                <a
                    className="btn btn-primary border-0 bg-dark btn-lg"
                    href="/register"
                    role="button"
                >
                    Register
                </a>
                </>) :(<>
                <p>Dont waste time, check the events!</p>
                <a
                    className="btn btn-primary border-0 bg-dark btn-lg"
                    style={{ color: "lawngreen" }}
                    href="/home"
                    role="button"
                >
                    See Events
                </a></>)}
            </div>
        </>
    );
}
