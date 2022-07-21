import React, { useRef, useState } from 'react'
import { Snek, SnekUnBorn } from '../resources/images'
import { Pop } from '../resources/sounds'
import './Empty.css'

type EmptyProps = {}

const Empty = (props: EmptyProps) => {
    const [born, setBorn] = useState(false);

    const SnekClicked = () => {
        setBorn(true);
        Pop.play();
    }

    return (
        <div id='empty-div'>
            <div>
                <h1 className='text-white fw-bold text-uppercase'>There is nothing here!!</h1>
                <h4 className='text-white'>No, really, there is nothing here.</h4>
                <h4 className='text-white'>But please have this cute snek instead.</h4>
                <button id='snek-btn' onClick={SnekClicked}>
                    <img className='round-image' src={born ? Snek : SnekUnBorn} alt="Snek is going on an adventure"></img>
                </button>
                <h4 className='text-white'>{born ? "I'm bon" : "..."}</h4>
            </div>
        </div>
    )
}

export default Empty