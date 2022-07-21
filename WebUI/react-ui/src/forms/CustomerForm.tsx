import React from "react";
import { useForm } from "react-hook-form";
import Modal, { ModalProps } from "../components/Modal";
import Customer, { CustomerSchema } from "../models/customer";
import { yupResolver } from "@hookform/resolvers/yup";
import FormControl from "../components/FormControl";
import { formatDate } from "../utils/formatter";

type Props = {
  customer?: Customer;
};

const CustomerForm = (
  props: Props &
    Omit<
      ModalProps,
      "children" | "buttonType" | "confirmBtnText" | "cancelBtnText"
    >
) => {
  const formHook = useForm<Customer>({
    resolver: yupResolver(CustomerSchema),
  });
  const {
    register,
    formState: { errors },
  } = formHook;

  return (
    <Modal
      {...props}
      useFormHook={formHook}
      width="600px"
      confirmBtnText="Submit"
      buttonType="submit"
      cancelBtnText="Cancel"
    >
      <FormControl
        label="Full name"
        key={"full-name"}
        defaultValue={props.customer ? props.customer.fullName : ""}
        register={register('fullName')}
        error={errors.fullName}
        type={'text'}
      />
      <FormControl
        label="Date of birth"
        key={"dob"}
        defaultValue={formatDate(props.customer ? props.customer.dob : new Date(), 'YYYY-MM-DD')}
        register={register('dob')}
        error={errors.dob}
        type={'date'}
      />
      <FormControl
        label="Email"
        key={"email"}
        defaultValue={props.customer ? props.customer.email || "" : ""}
        register={register('email')}
        error={errors.email}
        type={'text'}
      />
      <FormControl
        label="Phone number"
        key={"phone-number"}
        defaultValue={props.customer ? props.customer.phoneNumber : ""}
        register={register('phoneNumber')}
        error={errors.phoneNumber}
        type={'text'}
      />
      <FormControl
        label="Register since"
        key={"register-since"}
        defaultValue={formatDate(props.customer ? props.customer.registerSince : new Date(), 'YYYY-MM-DD')}
        register={register('registerSince')}
        error={errors.registerSince}
        type={'date'}
      />
      <FormControl
        label="Point"
        key={"point"}
        defaultValue={props.customer ? props.customer.point.toString() : "0"}
        register={register('point')}
        error={errors.point}
        type={'number'}
      />
    </Modal>
  );
};

export default CustomerForm;
