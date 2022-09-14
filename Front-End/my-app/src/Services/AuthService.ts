export class AuthService{
    static isAuthenticated = async () =>{
        const res = await fetch("https://localhost:7285/api/Auth/isAuth",{
            credentials: "include",
        });
        const data = res.json();
        return data;
    }
}