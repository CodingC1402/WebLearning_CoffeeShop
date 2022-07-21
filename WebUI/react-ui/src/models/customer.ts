import Person, { GenderType } from "./person";
import * as yup from "yup";
import dayjs from "dayjs";

export default class Customer extends Person {
  constructor(
    public id: number,
    public fullName: string,
    public dob: Date,
    public phoneNumber: string,
    public gender: GenderType,
    public registerSince: Date,
    public point: number,
    public email?: string
  ) {
    super(id, fullName, dob, phoneNumber, gender, email);
  }
}

export const CustomerSchema = yup.object({
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
  registerSince: yup
    .date()
    .required("Register date is required")
    .when("DOB", (dob, schema) => {
      schema.min(dob);
      return schema;
    }),
  point: yup.number().required().min(0, "Points can't be negative"),
  email: yup
    .string()
    .notRequired()
    .matches(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/, "Not a valid email address"),
});
