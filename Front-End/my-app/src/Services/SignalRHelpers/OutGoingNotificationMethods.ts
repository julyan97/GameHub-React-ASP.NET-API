export class OutGoingNotificationMethods {

    private static _connection: any;
    public static get connection() {
        return OutGoingNotificationMethods._connection;
    }
    public static set connection(value) {
        OutGoingNotificationMethods._connection = value;
    }


    static SendMessage = async (message: string) => {
        try {
            await this.connection.send('SendMessage', message);
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }

    static SendNotification = async () => {
        try {
            await this.connection.send('SendNotification');
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }
}