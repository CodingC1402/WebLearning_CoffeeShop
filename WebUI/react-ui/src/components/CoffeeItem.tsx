import React from 'react';
import { Random } from '../utils/random';
import './CoffeeItem.css';

type Props = {
    name: string,
    origins: string,
    varieties: string,
    notes: string,
    price: number,
}

const sources: string[] = [
    'https://img.freepik.com/premium-photo/ice-coffee-tall-glass-with-cream_79782-2043.jpg?w=996',
    'https://img.freepik.com/free-photo/coffee-shop-cafe-latte-cappuccino-newspaper-concept_53876-16322.jpg?t=st=1658809313~exp=1658809913~hmac=d34b1cefe9ae9ad095640363bea96e71b4d2ec9af592f2a104910cc83a8752e3&w=996',
    'https://img.freepik.com/premium-photo/ice-coffee-tall-glass-with-cream_79782-44.jpg?w=996',
    'https://img.freepik.com/premium-photo/cup-coffee-with-milk_664748-54.jpg?w=996',
    'https://media-cldnry.s-nbcnews.com/image/upload/t_nbcnews-fp-1200-630,f_auto,q_auto:best/newscms/2019_33/2203981/171026-better-coffee-boost-se-329p.jpg'
];

const CoffeeItem = (props: Props) => {
    return (
        <div className='card m-3 coffee-card'>
            <div id='coffee-img' className='card-img-top' style={{backgroundImage: `url(${sources[Random.randomInt(sources.length)]}`}}/>
            <div id='coffee-body' className='card-body'>
                <h5 id='coffee-title' className='p-0 m-0' >{props.name}</h5>
                <div className='card-text'>
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
                </div>
            </div>
            <div className='card-footer d-flex flex-row-reverse'>
                <button className='btn btn-primary'>Order</button>
                <h5 className='flex-grow-1 m-auto'>${props.price.toFixed(2)}</h5>
            </div>
        </div>
    )
}

export default CoffeeItem