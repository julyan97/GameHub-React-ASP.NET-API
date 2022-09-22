export class GameService{
    static host = "https://localhost:7285/api/Game"

    static GetAll = async () => {
        const res = await fetch(`${this.host}/GetAll`, {
            credentials:"include"
        });

        return res.json();

    }
}