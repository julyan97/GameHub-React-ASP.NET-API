export class EventService{
    static host = "https://localhost:7285/api/Event";

    static GetAll = async () => {
        const res = await fetch(`${this.host}/Events`, {
            credentials:"include"
        });

        return res.json();

    }

    static GetById = async (id: string) => {
        const res = await fetch(`${this.host}/GetById?id=${id}`, {
            credentials:"include"
        });

        return res.json();

    }

    static CreateEvent = async (params: any) => {
        const res = await fetch(`${this.host}/CreateEvent`, {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
            body: JSON.stringify(params)
        });
    }

    static RemovePlayerFromEvent = async (params: any) => {
        const res = await fetch(`${this.host}/RemovePlayerToEvent`, {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
            body: JSON.stringify(params)
        });
    }

    static RemoveById = async (id: string) => {
        const res = await fetch(`${this.host}/Delete/${id}`, {
            method: "Get",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
        });
    }

    
    static JoinEvent = async (params: any) => {
        const res = await fetch(`${this.host}/AddPlayerToEvent`, {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
            body: JSON.stringify(params)
        });
    }

    static ChangePlayerStatus = async (params: any) => {
        const res = await fetch(`${this.host}/ChangePlayerStatus`, {
            method: "Post",
            headers: {
                'Content-Type': 'application/json'
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            credentials: "include",
            body: JSON.stringify(params)
        });
    }
}