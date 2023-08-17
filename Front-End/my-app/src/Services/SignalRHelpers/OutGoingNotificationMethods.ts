import { HubConnection } from "@microsoft/signalr";

export class OutGoingNotificationMethods {

    private static _connection: HubConnection;
    public static get connection() {
        return OutGoingNotificationMethods._connection as HubConnection;
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

    static SendNotificationToAll = async () => {
        try {
            await this.connection.send('SendNotificationToAll');
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }

    static UpdateAllNotificationDetails = async () => {
        try {
            await this.connection.send('UpdateAllNotificationDetails');
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }

    static UpdateAllGameEventsPages = async () => {
        try {
            await this.connection.send('UpdateAllGameEventsPages');
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }


    static SendNotificationToGroup = async (groupId: string) => {
        try {
            await this.connection.send('SendNotificationToGroup',groupId);
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }

    static SendNotificationToUser = async (userName: string) => {
        try {
            await this.connection.send('SendNotificationToUser',userName);
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }

    static AddToGroup = async (groupId: string, username: string) => {
        try {
            await this.connection.send('AddToGroup',groupId, username);
        }
        catch (e) {
            console.log(e);
            alert('No connection to server yet.');
        }
    }


}