import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import App from './App';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import Home from './pages/Home';
import Employees from './pages/Employees';
import Orders from './pages/Orders';
import Coffees from './pages/Coffees';
import Shops from './pages/Shops';
import Empty from './pages/Empty';
import Customers from './pages/Customers';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<App />}>
          <Route path='/' element={<Home name='Home'/>} />
          <Route path='/shops' element={<Shops name='Shops'/>} />
          <Route path='/coffees' element={<Coffees name='Coffees'/>} />
          <Route path='/orders' element={<Orders name='Orders'/>} />
          <Route path='/employees' element={<Employees name='Employees'/>} />
          <Route path='/customers' element={<Customers name='Customers'/>} />
        </Route>
        <Route path="*" element={<Empty /> } />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);