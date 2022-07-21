import React from 'react'
import CoffeeItem from '../components/CoffeeItem';
import { usePageName } from '../utils/extraHook';

type CoffeesProps = {
  name: string;
}

const Coffees = (props: CoffeesProps) => {
  usePageName(props.name);

  return (
    <div className='h-100 d-flex flex-wrap'>
      <CoffeeItem name={'Brazil'} origins={'Hokaido Sergay'} varieties={'Compuchino Americano shito'} notes={'Blend roast something'} price={10.999929} ></CoffeeItem>
    </div>
  )
}

export default Coffees