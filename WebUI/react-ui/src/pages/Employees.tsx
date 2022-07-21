import dayjs, { Dayjs } from 'dayjs';
import { useEffect, useState } from 'react'
import Table from '../components/Table';
import Employee from '../models/employee';
import { deleteEmployeeData, fetchEmployeeData } from '../services/employeeService';
import { usePageName } from '../utils/extraHook';
import { keyOf } from '../utils/extraObjectUtils';
import { formatDate } from '../utils/formatter';
import './Employees.css'

type EmployeesProps = {
  name: string;
}

const Employees = (props: EmployeesProps) => {
  usePageName(props.name);
  const [employees, setEmployees] = useState<Employee[]>([]);

  useEffect(() => {
    fetchEmployeeData().then((data) => {
      if (data)
        setEmployees(data);
    })
  }, [])

  return (
    <div id='employee-top-div' className='p-3'>
      <Table 
        key={'data-table'}
        maxHeight='calc(100vh - 230px)'
        displayProps={[
          keyOf(Employee, 'fullName'), 
          keyOf(Employee, 'phoneNumber'), 
          keyOf(Employee, 'email'),
          keyOf(Employee, 'startDate'),
          keyOf(Employee, 'shopId'),
        ]}
        displayNames={['Name', 'Phone', 'Email', 'Start since', 'Shop id']}
        displayWidths={[1, 1, 2, 1, 1]}
        displayConverters={[null, null, null, formatDate]}
        indexWidth={'50px'}
        commandWidth={'150px'}
        data={employees}
        alignHead={'center'}
        alignData={'center'} 
        justifyHead={'center'} 
        justifyData={'center'}
        borderRadius={8}
        onDelete={obj => {
          deleteEmployeeData((obj as Employee).id).then(() => {
            setEmployees(v => {
              v.splice(v.indexOf(obj), 1);
              return v;
            })
          });
        }}
        onUpdate={() => {}}/>
      <div key={'buttons'} id='button-div'>
        <button type='button' className='btn btn-success btn-add'>Add employee</button>
      </div>
    </div>
  )
}

export default Employees