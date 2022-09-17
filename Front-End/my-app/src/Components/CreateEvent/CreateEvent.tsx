import * as React from 'react';

export interface ICreateEventProps {
}

export function CreateEvent(props: ICreateEventProps) {

    
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
                            asp-for="GameName"
                            className="custom-select d-in"
                            name="gameName"
                        >
                            <option value="">Chose your game</option>
                            {/* @foreach (var gameName in (List)ViewData["GameNames"])
                            {"{"}
                            <option value="@gameName">@gameName</option>
                            {"}"} */}
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
                                type="text"
                                className="form-control is-invalid"
                                asp-for="OwnerName"
                            />
                        </div>
                        <div className="invalid-feedback">
                            <span
                                asp-validation-for="OwnerName"
                                style={{ color: "#ff0000", fontSize: 15 }}
                            />
                        </div>
                    </div>
                    <div className="d-inline-block mb-3 w-75">
                        <label className="float-left name-label">Enter needed devision</label>
                        <div className="input-group mt-2 is-invalid">
                            <input
                                type="text"
                                className="form-control is-invalid"
                                asp-for="Devision"
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
                                type="number"
                                min={1}
                                max={20}
                                className="form-control is-invalid"
                                asp-for="NumberOfPlayers"
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
                            id="startDate"
                            type="datetime-local"
                            asp-for="StartDate"
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
                            id="dueDate"
                            type="datetime-local"
                            asp-for="DueDate"
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
                                type="text"
                                className="form-control is-invalid"
                                asp-for="DiscordUrl"
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
                            className="form-control is-invalid"
                            id="validationTextarea"
                            asp-for="Description"
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
                <button className="btn btn-primary border-0 bg-dark btn-lg" type="submit">
                    Add event
                </button>
            </form>
        </main>

    );
}
