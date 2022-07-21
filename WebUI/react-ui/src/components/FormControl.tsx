import React from "react";

type Props = {
	defaultValue: string;
	register: any;
	error?: any;
	type: 'date' | 'text' | 'number';
	placeholder?: string;
	label: string;
};

const FormControl = (props: Props) => {
  return (
    <div className="form-group py-2">
      <label htmlFor="input">{props.label}</label>
      <input
        className="form-control"
        id="input"
        type={props.type}
        placeholder={props.placeholder}
        defaultValue={props.defaultValue}
        {...props.register}
      />
      <small className="text-danger">
        {props.error ? props.error.message : undefined}
      </small>
    </div>
  );
};

export default FormControl;
