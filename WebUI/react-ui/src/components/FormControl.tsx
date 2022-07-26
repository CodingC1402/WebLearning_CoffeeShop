import React from "react";
import './FormControl.css';

type Props = {
	defaultValue: string;
	register: any;
	error?: any;
	type: 'date' | 'text' | 'number' | 'checkbox';
	placeholder?: string;
  required?: boolean;
  disabled?: boolean;
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
	label: string;
};

const FormControl = (props: Props) => {
  return (
    <div className="form-group py-2">
      <label htmlFor="input" className={props.required ? 'form-control-required' : ''}>{props.label}</label>
      <input
        className="form-control"
        id="input"
        disabled={props.disabled}
        type={props.type}
        placeholder={props.placeholder}
        onChange={props.onChange}
        defaultValue={props.defaultValue}
        required={props.required}
        {...props.register}
      />
      <small className="text-danger">
        {props.error ? props.error.message : undefined}
      </small>
    </div>
  );
};

export default FormControl;
