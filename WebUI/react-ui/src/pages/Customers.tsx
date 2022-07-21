import { Dayjs } from 'dayjs';
import React, { useState } from 'react'
import Table from '../components/Table';
import Customer from '../models/customer';
import { deleteCustomerData, fetchCustomerData } from '../services/customerService';
import { usePageName } from '../utils/extraHook';

type Props = {
  name: string;
}

const Customers = (props: Props) => {
  usePageName(props.name);
  const [customers, setCustomers] = useState<Customer[]>([]);

  fetchCustomerData().then((data) => {
    if (data)
      setCustomers(data);
  })

  return (
    <div id='employee-top-div' className='p-3'>
      <Table 
        key={'data-table'}
        maxHeight='calc(100vh - 230px)'
        displayProps={['fullName', 'phoneNumber', 'email', 'registerSince', 'point']}
        displayNames={['Name', 'Phone', 'Email', 'Register since', 'Point']}
        displayWidths={[1, 1, 2, 1, 1]}
        displayConverters={[null, null, null, (v: Dayjs) => {
          return v.format('DD/MM/YYYY');
        }]}
        indexWidth={'50px'}
        commandWidth={'150px'}
        data={customers}
        alignHead={'center'}
        alignData={'center'} 
        justifyHead={'center'} 
        justifyData={'center'}
        borderRadius={8}
        onDelete={obj => {
          deleteCustomerData((obj as Customer).id).then(() => {
            setCustomers(v => {
              v.splice(v.indexOf(obj), 1);
              return v;
            })
          });
        }}
        onUpdate={() => {}}/>
      <div key={'buttons'} id='button-div'>
        <button type='button' className='btn btn-success btn-add'>Add customer</button>
      </div>
    </div>
  )
}

export default Customers