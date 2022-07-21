import { useState } from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';
import './App.css';
import DrawerItem from './components/DrawerItem';
import {Cup} from './resources/images';

function App() {
  const [name, setName] = useState("");
  const location = useLocation();
  const path = location.pathname.substring(1);

  return (
    <div className='d-flex flex-column vh-100'>
      <div key={"drawer-div"} className='drawer-div'>
        <div className='drawer'>
          <div className='list-group rounded-0 flex-grow-1'>
            <Link className='brand-item text-decoration-none container drawer-link' to={'/'}>
              <img className='m-auto brand-icon' src={Cup} alt="123" />
              <span className='d-block text-center flex-grow-1 m-auto'>.｡･ﾟCOFFEE SHOPﾟ･｡.</span>
            </Link>
            <DrawerItem currentPath={path} iconName={'house'} to={''} text={'Home'}></DrawerItem>
            <DrawerItem currentPath={path} iconName={'building'} to={'employees'} text={'Employees'}></DrawerItem>
            <DrawerItem currentPath={path} iconName={'people'} to={'customers'} text={'Customers'}></DrawerItem>
            <DrawerItem currentPath={path} iconName={'receipt-cutoff'} to={'orders'} text={'Orders'}></DrawerItem>
            <DrawerItem currentPath={path} iconName={'shop'} to={'shops'} text={'Shops'}></DrawerItem>
            <DrawerItem currentPath={path} iconName={'cup-hot'} to={'coffees'} text={'Coffees'}></DrawerItem>
          </div>
          <Link to={'/'} className='drawer-footer d-flex flex-column'>
            <span className='d-block text-center m-auto'>www.coffee-shop.com</span>
          </Link>
        </div>
        <div key={"drawer-content"} className="drawer-content">
          <div key={"drawer-navbar"} className='drawer-navbar'>
            <span className='m-auto user-select-none'>	° ˖ ✧ {name} ✧ ˖ °</span>
          </div>
          <div key={"drawer-outlet"} className='drawer-outlet'>
            <Outlet context={setName}/>
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
