export class AuthService{
    static isAuthenticated = async () =>{
        const res = await fetch("https://localhost:7285/api/Auth/Authenticate",{
            credentials: "include",
        });
        const data = res.json();
        return data;
    }

    static onLogOut = async () => {
        const res = await fetch("https://localhost:7285/api/Auth/Logout", {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
        });
    }

    static onLogin = async (event: any, email: string, password: string) => {
        event.preventDefault();
        const res = await fetch("https://localhost:7285/api/Auth/Login", {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
            body: JSON.stringify({ Email: email, Password: password })
        })
        //navigate("/");
        //document.location.href = "/";
        console.log(res.json)
        return res.json();
    }
}