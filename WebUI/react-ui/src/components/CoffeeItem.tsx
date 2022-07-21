import React from 'react';
import './CoffeeItem.css';

type Props = {
    name: string,
    origins: string,
    varieties: string,
    notes: string,
    price: number,
}

const CoffeeItem = (props: Props) => {
    return (
        <div id='coffee-card' className='card m-3'>
            <img id='coffee-img' className='card-img-top' src='https://media-cldnry.s-nbcnews.com/image/upload/t_nbcnews-fp-1200-630,f_auto,q_auto:best/newscms/2019_33/2203981/171026-better-coffee-boost-se-329p.jpg' alt='coffee cup'/>
            <div id='coffee-body' className='card-body'>
                <h5 id='coffee-title' className='p-0 m-0' >{props.name}</h5>
                <p className='card-text'>
                    Varieties:
                    <ul>
                        {props.varieties.split(' ').map(v => {
                            return <li>{v}</li>
                        })}
                    </ul>
                    Origins:
                    <ul>
                        {props.origins.split(' ').map(v => {
                            return <li>{v}</li>
                        })}
                    </ul>
                    Notes:
                    <ul>
                        {props.notes.split(' ').map(v => {
                            return <li>{v}</li>
                        })}
                    </ul>
                </p>
            </div>
            <div className='card-footer d-flex flex-row-reverse'>
                <button className='btn btn-primary'>Order</button>
                <h5 className='flex-grow-1 m-auto'>${props.price.toFixed(2)}</h5>
            </div>
        </div>
    )
}

export default CoffeeItem