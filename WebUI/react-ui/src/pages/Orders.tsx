import React from 'react'
import { usePageName } from '../utils/extraHook';

type OrdersProps = {
  name: string;
}

const Orders = (props: OrdersProps) => {
  usePageName(props.name);
  
  return (
    <div>Orders</div>
  )
}

export default Orders