import * as React from 'react';
import { useContext, useEffect, useRef, useState } from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { AuthContext } from '../../../App';
import { EventService } from '../../../Services/EventService';
import { OutGoingNotificationMethods } from '../../../Services/SignalRHelpers/OutGoingNotificationMethods';
import { SignalRService } from '../../../Services/SignalRHelpers/SignalRService';

export interface IEventDetailsPageProps {
}

export function EventDetailsPage(props: IEventDetailsPageProps) {
    const { state } = useLocation()
    const playerName: any = useRef(null)

    const auth = useContext(AuthContext)
    const [Players, setPlayers] = useState(state.event.players);
    const [Rerender, setRerender] = useState(0)

    useEffect(() => {
        EventService.GetById(state.event.id)
            .then(res => {
                console.log(res);
                setPlayers(res.players);
            })
    }, [Rerender])


    const RemovePlayerFromEvent = async (playerName: string) => {
        const params: any = {
            eventId: state.event.id,
            playerName: playerName
        }
        await EventService.RemovePlayerFromEvent(params)
        await OutGoingNotificationMethods.UpdateAllNotificationDetails();
        setRerender(Math.random());
    }

    const JoinEvent = async (e: any) => {
        e.preventDefault();
        const params = {
            eventId: state.event.id,
            playerName: playerName.current.value
        }

        await EventService.JoinEvent(params)
        await OutGoingNotificationMethods.AddToGroup(state.event.id, auth.username)
        await OutGoingNotificationMethods.SendNotificationToUser(state.event.ownerUserId);
        await OutGoingNotificationMethods.UpdateAllNotificationDetails();
        //await OutGoingNotificationMethods.SendNotificationToGroup(state.event.id);

        console.log(Rerender);
        console.log(auth.username);
        setRerender(Math.random())

    }

    const AcceptPlayer= async (playerName: string) =>{
        const params ={
            eventId: state.event.id,
            playerName: playerName,
            status: true
        }

        await EventService.ChangePlayerStatus(params);
        await OutGoingNotificationMethods.UpdateAllNotificationDetails();
    }
    const VizualizePlayers =()=>{
        
        const playersIfOwnedEvent = (<>
                                        {Players.map((x: any, i: number) =>
                                    <div key={i}>
                                        <li style={{ fontSize: 20, wordSpacing: "100px", marginBottom: "4px" }} className="card-text ml-4 text-white">
                                            <div style={{ paddingRight: "4px", display: "inline-block" }}>{x.usernameInGame}</div>
                                            
                                                <>

                                                    <button
                                                        type="button"
                                                        className="btn btn-danger"
                                                        style={{ float: "right" }}
                                                        onClick={() => RemovePlayerFromEvent(x.usernameInGame)}>Delete
                                                    </button>
                                                    {x.status === false ?
                                                        <button
                                                            type="button"
                                                            className="btn btn-success"
                                                            style={{ float: "right" }}
                                                            onClick={() => AcceptPlayer(x.usernameInGame)}>Accept
                                                        </button> : <></>
                                                    }
                                                </>
                                                : <></>
                                            
                                        </li>
                                        <br />
                                    </div>
                                )}
        </>);

        const playerIfNotOwnedEvent = (<>
                                {Players.filter((x: any) => x.status === true).map((x: any, i: number) =>
                                    <div key={i}>
                                        <li style={{ fontSize: 20, wordSpacing: "100px", marginBottom: "4px" }} className="card-text ml-4 text-white">
                                            <div style={{ paddingRight: "4px", display: "inline-block" }}>{x.usernameInGame}</div>
                                        </li>
                                        <br />
                                    </div>
                                )}
        </>)
        
        return auth.id === state.event.ownerUserId ? playersIfOwnedEvent : playerIfNotOwnedEvent;
    }
    //SignalR Begin

    SignalRService.RegisterIncomingMethods([
        { name: "ReRenderDetails", method: () => setRerender(Math.random()) }
    ], false);

    //SignalR End
    return (
        <div className='text-center'>
            <div
                className="text-center d-inline-block w-75"
            >
                <div
                    className="d-inline-block  text-left mt-5 container-style detail"
                    style={{ backgroundImage: "url(https://www.global-esports.news/wp-content/uploads/2022/01/League-of-Legends-2022.jpg)" }}
                >
                    <div className="h-100 inside-el " style={{ opacity: 1 }}>
                        <div
                            style={{
                                display: "inline-block",
                                width: "45%",
                                height: "70%",
                                minHeight: "19em"
                            }}
                        >
                            <h6 style={{ fontSize: 25 }} className="card-title">
                                Owner's nick : {state.event.owner.usernameInGame}
                            </h6>
                            <p className="card-text">Devision : {state.event.rank}</p>
                            <p className="card-text">
                            </p>
                            <p className="card-text">Starts on : {state.event.startingDate}</p>
                            <p className="card-text">Ends on : {state.event.endDate}</p>
                            <p className="card-text">
                            </p>
                            <p className="card-text" style={{ wordBreak: "break-word" }}>
                                Description : {state.event.description}
                            </p>
                        </div>
                        <div
                            style={{
                                display: "inline-block",
                                width: "45%",
                                height: "70%",
                                verticalAlign: "top"
                            }}
                            className="text-left"
                        >
                            <h6 style={{ fontSize: 25 }} className="card-text">
                                Players for this event:
                            </h6>
                            <ul style={{}}>

{/* here */}        {VizualizePlayers()}

                            </ul>
                        </div>
                        <div style={{ position: "relative" }} id="last">
                            <form
                                className="mt-5 text-left form-event"
                                id="form1"
                            >
                                <div className="text-left">
                                    <p
                                        style={{ color: "lawngreen", marginLeft: "5%" }}
                                        className="card-text mb-3"
                                    >
                                        Your nick name :
                                    </p>
                                </div>
                                <input
                                    ref={playerName}
                                    style={{ marginLeft: "5%", width: "70%", marginBottom: "4%" }}
                                    type="text"
                                    name="userNick"
                                    className="form-control is-invalid d-inline-block"
                                    aria-describedby="validatedInputGroupPrepend"
                                />
                                <button
                                    onClick={(e) => JoinEvent(e)}
                                    id="joinEvent"
                                    className="btn btn-primary border-0 mb-1 bg-dark mr-2 btn-lg ml-3"
                                >
                                    I'm in
                                </button>
                                <div className="invalid-feedback">
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}
