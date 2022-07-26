import React, { useState } from "react";
import { useForm } from "react-hook-form";
import Modal, { ModalProps } from "../components/Modal";
import { yupResolver } from "@hookform/resolvers/yup";
import Employee, { EmployeeSchema } from "../models/employee";
import FormControl from "../components/FormControl";
import { formatDate } from "../utils/formatter";
import dayjs from "dayjs";

type Props = {
  employee?: Employee;
};

const EmployeeForm = (
  props: Props &
    Omit<
      ModalProps,
      "children" | "buttonType" | "confirmBtnText" | "cancelBtnText"
    >
) => {
  const [resigned, setResigned] = useState(false);

  const formHook = useForm<Employee>({
    resolver: yupResolver(EmployeeSchema),
  });
  const {
    register,
    formState: { errors },
  } = formHook;

  return (
    <Modal
      {...props}
      onSubmit={props.onSubmit ? (data) => {} : undefined}
      useFormHook={formHook}
      width="600px"
      confirmBtnText="Submit"
      buttonType="submit"
      cancelBtnText="Cancel"
    >
      <FormControl
        label="Full name"
        required={true}
        key={"full-name"}
        defaultValue={props.employee ? props.employee.fullName : ""}
        register={register("fullName")}
        error={errors.fullName}
        type={"text"}
      />
      <FormControl
        label="Date of birth"
        key={"dob"}
        required={true}
        defaultValue={formatDate(
          props.employee ? props.employee.dob : new Date(),
          "YYYY-MM-DD"
        )}
        register={register("dob")}
        error={errors.dob}
        type={"date"}
      />
      <FormControl
        label="Email"
        key={"email"}
        required={true}
        defaultValue={props.employee ? props.employee.email || "" : ""}
        register={register("email")}
        error={errors.email}
        type={"text"}
      />
      <FormControl
        label="Phone number"
        key={"phone-number"}
        required={true}
        defaultValue={props.employee ? props.employee.phoneNumber : ""}
        register={register("phoneNumber")}
        error={errors.phoneNumber}
        type={"text"}
      />
      <FormControl
        label="Start since"
        key={"start-since"}
        required={true}
        defaultValue={formatDate(
          props.employee ? props.employee.startDate : new Date(),
          "YYYY-MM-DD"
        )}
        register={register("startDate")}
        error={errors.startDate}
        type={"date"}
      />
      <FormControl
        label="Resign since"
        key={"point"}
        defaultValue={props.employee ? props.employee.resignDate ? formatDate(props.employee.resignDate, "YYYY-MM-DD") : "" : ""}
        register={register("resignDate")}
        error={errors.resignDate}
        type={"date"}
      />
    </Modal>
  );
};

export default EmployeeForm;
