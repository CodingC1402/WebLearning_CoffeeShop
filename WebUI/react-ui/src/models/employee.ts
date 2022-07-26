import Person, { GenderType } from "./person";
import * as yup from "yup";
import dayjs from "dayjs";
export default class Employee extends Person {
  constructor(
    public id: number,
    public fullName: string,
    public dob: Date,
    public phoneNumber: string,
    public gender: GenderType,
    public startDate: Date,
    public shopId: number,
    public resignDate?: Date,
    public email?: string
  ) {
    super(id, fullName, dob, phoneNumber, gender, email);
  }
}

export const EmployeeSchema = yup.object({
  fullName: yup
    .string()
    .required("Name is required")
    .min(1, "Name is too short")
    .max(60, "Name is too long"),
  dob: yup
    .date()
    .required("Date of birth is required")
    .min(dayjs().year(1900).toDate(), "Invalid date")
    .max(new Date(), "Invalid date"),
  phoneNumber: yup
    .string()
    .matches(
      /^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$/,
      "Not a valid phone number"
    ),
  gender: yup.mixed().oneOf(Object.values(GenderType)),
  startDate: yup
    .date()
    .required("Register date is required")
    .when("dob", (dob, schema) => {
      schema.min(dob, "Start date have to be after birth date");
      return schema;
    }),
	resignDate: yup.date().optional().when("startDate", (startDate, schema) => {
			schema.min(startDate, "resign date have to be after start date");
			return schema;
		}),
  shopId: yup.number().required().min(0, "Points can't be negative"),
  email: yup
    .string()
    .notRequired()
    .matches(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/, "Not a valid email address"),
});
