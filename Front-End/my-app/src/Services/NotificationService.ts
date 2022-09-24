export class NotificationService{
    static host = "https://localhost:7285/api/Notification"

    static GetAll = async () => {
        const res = await fetch(`${this.host}/GetAll`, {
            credentials:"include"
        });

        return res.json();

    }

    static SetToRead = async (notificationId: string) => {
        const res = await fetch(`${this.host}/SetToRead/${notificationId}`, {
            credentials:"include"
        });

    }
}