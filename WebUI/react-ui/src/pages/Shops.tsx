import React from 'react'
import { usePageName } from '../utils/extraHook';

type ShopsProps = {
  name: string;
}

const Shops = (props: ShopsProps) => {
  usePageName(props.name);
  
  return (
    <div>Shops</div>
  )
}

export default Shops