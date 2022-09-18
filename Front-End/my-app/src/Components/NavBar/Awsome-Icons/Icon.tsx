import { faCoffee } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as React from 'react';

export interface IIconProps {
    type: any,
    className?: string
}

export function Icon(props: IIconProps) {
    return (
        <>
            <div style={{ userSelect: "none",position:"relative",top: "50%",left: '50%',transform: 'translate(-50%, -50%) scale(1.5)',padding:"0 1em", color:"white"}}>
                <FontAwesomeIcon icon={props.type} className= {`nav-link d-inline-block p-0 text-white ${props.className}`}  />
            </div>
        </>
    );
}
