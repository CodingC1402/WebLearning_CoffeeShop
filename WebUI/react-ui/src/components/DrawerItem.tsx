import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import './DrawerItem.css'

type DrawerItemProps = {
    iconName: string,
    to: string,
    currentPath?: string,
    text: string
}

const DrawerItem = (props: DrawerItemProps) => {
  const selected = props.currentPath === props.to;
  const [hover, setHover] = useState(false);

  const colors = ['#484848', '#585858', '#686868', '#888888'];
  const currentColor = colors[(selected ? 2 : 0) + (hover ? 1 : 0)]

  return (
    <Link className='text-decoration-none' to={props.to} > 
        <div className='list-group-item d-flex flex-row' key={"home-link"} onMouseEnter={() => setHover(true)} onMouseLeave={() => setHover(false)} style={{backgroundColor: currentColor}}>
            <i className={`drawer-icon bi bi-${props.iconName}`}></i>
            {props.text}
        </div>
    </Link>
  )
}

export default DrawerItem