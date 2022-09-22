import * as React from 'react';
import { useEffect, useRef, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { start } from 'repl';
import { EventService } from '../../../Services/EventService';
import { GameService } from '../../../Services/GameService';
import { OutGoingNotificationMethods } from '../../../Services/SignalRHelpers/OutGoingNotificationMethods';

export interface ICreateEventProps {
}

export function CreateEvent(props: ICreateEventProps) {

    const navigate = useNavigate();
    const [Games, setGames] = useState([]);

    const game: any = useRef(null)
    const inGameName: any = useRef(null)
    const rank: any = useRef(null);
    const playerCount: any = useRef(null);
    const startDate: any = useRef(null);
    const endDate: any = useRef(null);
    const discordUrl: any = useRef(null);
    const description: any = useRef(null);

    useEffect(() => {
        GameService.GetAll()
            .then(res => {
                setGames(res);
            })
    }, [])

    const GetRefValue = (ref: any) => {
        return ref.current.value;
    }

    const onSubmit = async (e: any) => {
        e.preventDefault();

        const requestObj = {
            gameName: GetRefValue(game),
            ownerInGameName: GetRefValue(inGameName),
            rank: GetRefValue(rank),
            numberOfPlayers: GetRefValue(playerCount),
            startDate: GetRefValue(startDate),
            dueDate: GetRefValue(endDate),
            discordUrl: GetRefValue(discordUrl),
            description: GetRefValue(description)
        }
        try {
            await EventService.CreateEvent(requestObj)
            await OutGoingNotificationMethods.UpdateAllGameEventsPages();
            navigate("/home");
        }
        catch (e) {
            console.log("CreateFailed: " + e)
        }

    }

    return (
        <main className="text-center">
            <form
                className="d-inline-block was-validated text-center add-form"
                asp-controller="GameEvent"
                asp-action="GameEventAdd"
                id="event-add"
                method="POST"
                style={{ marginBottom: 100 }}
            >
                <div className="text-center ">
                    <div className="w-75 mb-3 d-inline-block">
                        <label className="name">Game Event</label>
                        <br />
                        <select
                            ref={game}
                            asp-for="GameName"
                            className="custom-select d-in"
                        >
                            <option value="">Chose your game</option>
                            {Games.map(x => <option key={x} value={x}>{x}</option>)}

                        </select>
                        <div className="invalid-feedback">
                            <span
                                asp-validation-for="GameName"
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                    <div className="d-inline-block mb-3 w-75">
                        <label className="float-left name-label">
                            Enter your in-game username
                        </label>
                        <div className="input-group mt-2 is-invalid">
                            <input
                                ref={inGameName}
                                type="text"
                                className="form-control is-invalid"
                            />
                        </div>
                        <div className="invalid-feedback">
                            <span
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                    <div className="d-inline-block mb-3 w-75">
                        <label className="float-left name-label">Enter needed devision</label>
                        <div className="input-group mt-2 is-invalid">
                            <input
                                ref={rank}
                                type="text"
                                className="form-control is-invalid"
                            />
                        </div>
                        <div className="invalid-feedback">
                            <span
                                asp-validation-for="Devision"
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                    <div className="d-inline-block mb-3 w-75">
                        <label className="float-left name-label">Enter number of players</label>
                        <div className="input-group mt-2 is-invalid">
                            <input
                                ref={playerCount}
                                type="number"
                                min={1}
                                max={20}
                                className="form-control is-invalid"
                            />
                        </div>
                        <div className="invalid-feedback">
                            <span
                                asp-validation-for="NumberOfPlayers"
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                    <div className="w- mb-3 d-inline-block">
                        <div className="w-100">
                            <label htmlFor="startDate" className="name-label ">
                                Stars on
                            </label>
                        </div>
                        <input
                            ref={startDate}
                            id="startDate"
                            type="datetime-local"
                            className="form-control validate col-xl-9 col-lg-8 col-md-8 col-sm-7 d-inline-block"
                            data-large-mode="true"
                        />
                    </div>
                    <div className="d-inline-block input-group mb-3 justify-content-center">
                        <span
                            asp-validation-for="StartDate"
                            style={{ color: "#ff0000", fontSize: 15 }}
                        />
                    </div>
                    <div className="d-inline-block w-5 mb-3 ">
                        <div className="w-100">
                            <label htmlFor="dueDate" className="name-label">
                                Due date
                            </label>
                        </div>
                        <input
                            ref={endDate}
                            id="dueDate"
                            type="datetime-local"
                            className="form-control validate col-xl-9 col-lg-8 col-md-8 col-sm-7 d-inline-block"
                            data-large-mode="true"
                        />
                    </div>
                    <div className="input-group mb-3 justify-content-center">
                        <span
                            asp-validation-for="DueDate"
                            style={{ color: "#ff0000", fontSize: 15 }}
                        />
                    </div>
                    <div className="d-inline-block mb-3 w-75">
                        <label className="float-left name-label">Discord Url</label>
                        <div className="input-group mt-2 is-invalid">
                            <input
                                ref={discordUrl}
                                type="text"
                                className="form-control is-invalid"
                            />
                        </div>
                        <div className="invalid-feedback">
                            <span
                                asp-validation-for="DiscordUrl"
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                    <div className="d-inline-block mt-3 mb-3 w-75">
                        <label className="name-label float-left">Some more information</label>
                        <textarea
                            ref={description}
                            className="form-control is-invalid"
                            id="validationTextarea"
                            placeholder="Required example textarea"
                            defaultValue={""}
                        />
                        <div className="invalid-feedback">
                            <span
                                asp-validation-for="Description"
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                </div>
                <button onClick={(e) => onSubmit(e)} className="btn btn-primary border-0 bg-dark btn-lg" type="submit">
                    Add event
                </button>
            </form>
        </main>

    );
}
