import { faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';
import Popup from 'reactjs-popup';
import useAuth from '../../../Hooks/useAuth';
import { EventService } from '../../../Services/EventService';
import { OutGoingNotificationMethods } from '../../../Services/SignalRHelpers/OutGoingNotificationMethods';

export interface IGameEventProps {
    id: string,
    ownerUserId: string
    rank: string,
    players?: [],
    playerCount: number,
    startingDate: string,
    endDate?: string,
    description: string,
    discordUrl: string,
    game: any,
    owner: any
}

export function GameEvent(props: IGameEventProps) {
    const navigate = useNavigate();
    const auth = useAuth();

    const DeleteById = async () => {
        await EventService.RemoveById(props.id);
        await OutGoingNotificationMethods.UpdateAllGameEventsPages();
    }
    return (
        <>
            <div
                className=" d-inline-block text-left mt-5 container-style"
                id="event-div"
                style={{ width: "18rem" }}
            >
                <div onClick={() => navigate(`/eventDetails`, { state: { event: props } })}>
                    <img
                        src="https://www.global-esports.news/wp-content/uploads/2022/01/League-of-Legends-2022.jpg"
                        className="card-img-top"
                        style={{ height: 200 }}
                        alt="${x[i].imageUrl}"
                        title="${x[i].imageUrl}"
                    />
                    <div className="card-body text-center">
                        <h5 >
                            Owner's nick : {props.owner.usernameInGame}
                        </h5>
                        <h5 >
                            Devision : {props.rank}
                        </h5>
                        <h5 >
                            Players needed : {props.playerCount}
                        </h5>
                        <h5>
                            Starts on : {Date.apply(props.startingDate)}
                        </h5>
                    </div>
                </div>

                <div className='text-center'>
                    {
                        auth.id === props.ownerUserId ?
                            <div onClick={() => DeleteById()} className="list-inline-item">
                                <button className="btn btn-danger btn-sm rounded-0 fa fa-trash" type="button" data-toggle="tooltip" data-placement="top" title="Delete">
                                    <FontAwesomeIcon icon={faTrash} /></button>
                            </div> :
                            <div className="list-inline-item">
                                <button className="btn btn-success btn-sm rounded-0 " type="button" data-toggle="tooltip" data-placement="top" title="Join">
                                    <FontAwesomeIcon icon={faPlus} /></button>
                            </div>
                    }
                </div>
            </div>


        </>
    );
}
