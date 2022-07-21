import { useEffect, useState } from "react";
import Table from "../components/Table";
import CustomerForm from "../forms/CustomerForm";
import Customer from "../models/customer";
import {
  addCustomerData,
  deleteCustomerData,
  fetchCustomerData,
  updateCustomerData,
} from "../services/customerService";
import { usePageName } from "../utils/extraHook";
import { formatDate } from "../utils/formatter";

type Props = {
  name: string;
};

const Customers = (props: Props) => {
  usePageName(props.name);
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [showForm, setShowForm] = useState(false);
  const [updatingObject, setUpdatingObject] = useState<Customer>();

  useEffect(() => {
    fetchCustomerData().then((data) => {
      if (data) setCustomers(data);
    });
  }, []);

  const updateCustomer = (customer: any) => {
    updateCustomerData(customer, updatingObject!.id).then(() => {
      setCustomers((arr) => {
        const index = arr.indexOf(updatingObject!);
        arr[index] = Object.assign(updatingObject!, customer);

        return arr;
      });
    }).catch((err) => {
      console.error(err);
    });
  }
  const addCustomer = (customer: any) => {
    addCustomerData(customer).then(data => {
      setCustomers((arr) => arr.concat(data));
    });
  }

  return (
    <div id="employee-top-div" className="p-3">
      <Table
        key={"data-table"}
        maxHeight="calc(100vh - 230px)"
        displayProps={[
          "fullName",
          "phoneNumber",
          "email",
          "registerSince",
          "point",
        ]}
        displayNames={["Name", "Phone", "Email", "Register since", "Point"]}
        displayWidths={[1, 1, 2, 1, 1]}
        displayConverters={[null, null, null, formatDate]}
        indexWidth={"50px"}
        commandWidth={"150px"}
        data={customers}
        alignHead={"center"}
        alignData={"center"}
        justifyHead={"center"}
        justifyData={"center"}
        borderRadius={8}
        onDelete={(obj) => {
          deleteCustomerData((obj as Customer).id).then(() => {
            setCustomers((v) => {
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
        <button type="button" className="btn btn-success btn-add" onClick={() => {
          setUpdatingObject(undefined);
          setShowForm(true);
        }}>
          Add customer
        </button>
      </div>

      {showForm ? (
        <CustomerForm
          customer={updatingObject}
          title={`Customer ${updatingObject ? "update" : "add"}`}
          onSubmit={updatingObject ? updateCustomer : addCustomer}
          onClose={function (): void {
            setShowForm(false);
          }}
          show={showForm}
        />
      ) : undefined}
    </div>
  );
};

export default Customers;
