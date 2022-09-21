import * as React from 'react';
import Popup from 'reactjs-popup';

export interface IEventsPageProps {
}

export function EventsPage(props: IEventsPageProps) {
    return (
        <div >
            <Popup trigger={<div
                className=" d-inline-block text-left mt-5 container-style"
                id="event-div"
                style={{ width: "18rem" }}
            >
                <img
                    src="https://www.global-esports.news/wp-content/uploads/2022/01/League-of-Legends-2022.jpg"
                    className="card-img-top"
                    style={{ height: 200 }}
                    alt="${x[i].imageUrl}"
                    title="${x[i].imageUrl}"
                />
                <div className="card-body text-center">
                    <h5 >
                        Owner's nick : Name
                    </h5>
                    <h5 >
                        Devision : Silver
                    </h5>
                    <h5 >
                        Players needed : 5
                    </h5>
                    <h5 >
                        Starts on : 
                    </h5>
                </div>
            </div>} >

                <>
                    <div
                        style={
                            { 
                                width:"",
                                marginBottom: 100,
                                position:"fixed",
                                top: 30
                            }}
                        className="btn-container-style text-center d-inline-block modal-dialog-scrollable outside-div"
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
                                        Owner's nick : Name
                                    </h6>
                                    <p className="card-text">Devision : Silver</p>
                                    <p className="card-text">
                                    </p>
                                    <p className="card-text">Starts on : @Model.StartDate</p>
                                    <p className="card-text">Ends on : @Model.DueDate</p>
                                    <p className="card-text">
                                    </p>
                                    <p className="card-text" style={{ wordBreak: "break-word" }}>
                                        Description : @Model.Description
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
                                    <ul>
                                        <li style={{ fontSize: 20 }} className="card-text ml-4">
                                            <div className="ml-sm-5 d-inline-block float-right ">
                                                <a
                                                    asp-route-playername="@player.UsernameInGame"
                                                    asp-route-roomid="@Model.Id"
                                                    asp-action="Decline"
                                                    asp-controller="GameEvent"
                                                >
                                                    <i
                                                        className="far fa-times-circle fa-lg ml-2"
                                                        style={{ color: "red" }}
                                                    />
                                                </a>
                                            </div>
                                        </li>
                                        <li style={{ fontSize: 20 }} className="card-text ml-4">
                                            @player.UsernameInGame
                                            <div className="ml-sm-5 d-inline-block float-right ">
                                                <a
                                                    asp-route-playername="@player.UsernameInGame"
                                                    asp-route-roomid="@Model.Id"
                                                    asp-action="Accept"
                                                    asp-controller="GameEvent"
                                                >
                                                    <i
                                                        className="far fa-check-circle fa-lg ml-2"
                                                        style={{ color: "green" }}
                                                    />
                                                </a>
                                                <a
                                                    asp-route-playername="@player.UsernameInGame"
                                                    asp-route-roomid="@Model.Id"
                                                    asp-action="Decline"
                                                    asp-controller="GameEvent"
                                                >
                                                    <i
                                                        className="far fa-times-circle fa-lg ml-2"
                                                        style={{ color: "red" }}
                                                    />
                                                </a>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <div style={{ position: "relative" }} id="last">
                                    <form
                                        className="mt-5 text-left form-event"
                                        id="form1"
                                        asp-controller="GameEvent"
                                        asp-action="GameEventAddPlayer"
                                        method="POST"
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
                                            style={{ marginLeft: "5%", width: "70%", marginBottom: "4%" }}
                                            type="text"
                                            name="userNick"
                                            id="username"
                                            className="form-control is-invalid d-inline-block"
                                            aria-describedby="validatedInputGroupPrepend"
                                        />
                                        <button
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
                </>


            </Popup>
        </div>
    );
}
