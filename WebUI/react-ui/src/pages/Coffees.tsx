import React, { useEffect, useState } from 'react'
import CoffeeItem from '../components/CoffeeItem';
import Coffee from '../models/coffee';
import { fetchCoffeeData } from '../services/coffeeService';
import { usePageName } from '../utils/extraHook';
import './Coffees.css'

type CoffeesProps = {
  name: string;
}

const Coffees = (props: CoffeesProps) => {
  usePageName(props.name);
  const [coffee, setCoffee] = useState<Coffee[]>([]);

  useEffect(() => {
    fetchCoffeeData().then((data) => {
      if (data) setCoffee(data);
    });
  }, []);

  return (
    <div id="coffee-div" className='h-100'>
      <div className='d-flex flex-wrap justify-content-center'>
        {coffee.map((data) => {
          return <CoffeeItem key={data.id} name={data.name} origins={data.origin} varieties={data.origin} notes={data.notes} price={data.price}/>
        })}
      </div>
    </div>
  )
}

export default Coffees