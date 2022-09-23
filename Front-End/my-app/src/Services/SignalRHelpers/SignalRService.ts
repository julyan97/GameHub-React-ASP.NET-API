import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { useEffect } from 'react';
import { OutGoingNotificationMethods } from './OutGoingNotificationMethods';

export interface IIncomingConnection {
    name: string,
    method: any,
}

export interface SignalRConnection {
    connection: any,
    setConnection: any
}

export class SignalRService {

    static host = 'https://localhost:7285/hubs/notification';
    static IncomingConnection: Array<IIncomingConnection> = [];
    static Connection: SignalRConnection = { connection: null, setConnection: null };
    private static isConnected = false;


    //meant to be used in a useEffect hook
    static EstablishConnection = (setConnection: any) => {
        useEffect(() => {
            const newConnection = new HubConnectionBuilder()
                .withUrl(this.host)
                .withAutomaticReconnect()
                .build();

            setConnection(newConnection);
        }, [])
    }

    static tryConnect = async (connection: HubConnection) => {
        try {
            if (connection != null && this.isConnected === false) {
                const res = await connection.start();
                    this.isConnected = true;
                    console.log("Connected");
            }
        }
        catch (e) {
            console.log("Connection failed: " + e)
            this.isConnected = false
        }
        if(this.isConnected)
            for (let index = 0; index < this.IncomingConnection.length; index++) {
                const element = this.IncomingConnection[index];
                connection.on(element.name, element.method);
            }
    }
    //Connection element should be of type {name,method}
    static CreateEndPointMethods = async (connection: any) => {
        useEffect(() => {
            this.tryConnect(connection)

        }, [connection])


    }

    static RegisterIncomingMethods = (methods: Array<IIncomingConnection>, reconnect: boolean, setConnection?: any, connection?: any,) => {
        for (let i = 0; i < methods.length; i++) {
            const method = methods[i];
            this.IncomingConnection.push(method);
        }


        if (reconnect) {
            this.Connection.setConnection = setConnection;
            this.Connection.connection = connection;
            OutGoingNotificationMethods.connection = connection;
            this.EstablishConnection(this.Connection.setConnection);
        }

        this.CreateEndPointMethods(this.Connection.connection);
    }

}