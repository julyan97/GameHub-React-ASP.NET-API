import * as React from 'react';
import { useEffect, useState } from 'react';
import Popup from 'reactjs-popup';
import { EventService } from '../../../Services/EventService';
import { SignalRService } from '../../../Services/SignalRHelpers/SignalRService';
import { GameEvent } from '../GameEvent/GameEvent';

export interface IEventsPageProps {
}

export function EventsPage(props: IEventsPageProps) {
    const [GameEvents, setGameEvents] = useState<Array<any>>([])
    const [ReRender, setReRender] = useState(0)
    useEffect(() => {
        EventService.GetAll()
            .then(res => {
                console.log(res);
                setGameEvents(res);
            });
    }, [ReRender])

    const RenderPage =  ()=>{
        setReRender(Math.random())
    }

    //SignalR Begin

    SignalRService.RegisterIncomingMethods([
        { name: "ReRenderGameEventsPage", method: () => RenderPage() }
    ], false);

    //SignalR End
    return (
        <>
            <div >
                {GameEvents.map((x, i) => <GameEvent
                    key={x.id}
                    ownerUserId={x.owner.userId}
                    id={x.id}
                    description={x.description}
                    discordUrl={x.discordUrl}
                    endDate={x.dueDate}
                    game={x.game}
                    playerCount={x.numberOfPlayers}
                    owner={x.owner}
                    players={x.players}
                    rank={x.rank}
                    startingDate={x.startDate}
                />)}
            </div>
        </>
    );
}
