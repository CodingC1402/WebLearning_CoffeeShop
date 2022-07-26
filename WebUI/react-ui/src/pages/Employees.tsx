import dayjs, { Dayjs } from "dayjs";
import { useEffect, useState } from "react";
import Table from "../components/Table";
import EmployeeForm from "../forms/EmployeeForm";
import Employee from "../models/employee";
import {
  addEmployeeData,
  deleteEmployeeData,
  fetchEmployeeData,
  updateEmployeeData,
} from "../services/employeeService";
import { usePageName } from "../utils/extraHook";
import { keyOf } from "../utils/extraObjectUtils";
import { formatDate } from "../utils/formatter";
import "./Employees.css";

type EmployeesProps = {
  name: string;
};

const Employees = (props: EmployeesProps) => {
  usePageName(props.name);
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [updatingObject, setUpdatingObject] = useState<Employee>();

  useEffect(() => {
    fetchEmployeeData().then((data) => {
      if (data) setEmployees(data);
    });
  }, []);

  const updateEmployee = (employee: any) => {
    updateEmployeeData(employee, updatingObject!.id)
      .then(() => {
        setEmployees((arr) => {
          const index = arr.indexOf(updatingObject!);
          arr[index] = Object.assign(updatingObject!, employee);

          return arr;
        });
      })
      .catch((err) => {
        console.error(err);
      });
  };
  const addEmployee = (employee: any) => {
    addEmployeeData(employee).then((data) => {
      setEmployees((arr) => arr.concat(data));
    });
  };

  return (
    <div id="employee-top-div" className="p-3">
      <Table
        key={"data-table"}
        maxHeight="calc(100vh - 230px)"
        displayProps={[
          keyOf(Employee, "fullName"),
          keyOf(Employee, "phoneNumber"),
          keyOf(Employee, "email"),
          keyOf(Employee, "startDate"),
          keyOf(Employee, "shopId"),
        ]}
        displayNames={["Name", "Phone", "Email", "Start since", "Shop id"]}
        displayWidths={[1, 1, 2, 1, 1]}
        displayConverters={[null, null, null, formatDate]}
        indexWidth={"50px"}
        commandWidth={"150px"}
        data={employees}
        alignHead={"center"}
        alignData={"center"}
        justifyHead={"center"}
        justifyData={"center"}
        borderRadius={8}
        onDelete={(obj) => {
          deleteEmployeeData((obj as Employee).id).then(() => {
            setEmployees((v) => {
              v.splice(v.indexOf(obj), 1);
              return v;
            });
          });
        }}
        onUpdate={(obj) => {
          setShowForm(true);
          setUpdatingObject(obj);
        }}
      />
      <div key={"buttons"} id="button-div">
        <button
          type="button"
          className="btn btn-success btn-add"
          onClick={() => {
            setUpdatingObject(undefined);
            setShowForm(true);
          }}
        >
          Add employee
        </button>
      </div>

      {showForm ? (
        <EmployeeForm
          employee={updatingObject}
          title={`Customer ${updatingObject ? "update" : "add"}`}
          onSubmit={updatingObject ? updateEmployee : addEmployee}
          onClose={function (): void {
            setShowForm(false);
          }}
          show={showForm}
        />
      ) : undefined}
    </div>
  );
};

export default Employees;
