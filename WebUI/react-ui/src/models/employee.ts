import { Dayjs } from "dayjs";
import Person, { GenderType } from "./person";

export default class Employee extends Person {
    constructor(
        public id: number,
        public fullName: string,
        public DOB: Dayjs,
        public phoneNumber: string,
        public gender: GenderType,
        public startDate: Dayjs,
        public shopId: number,
        public resignDate?: Dayjs,
        public email?: string
    ) {
        super(id, fullName, DOB, phoneNumber, gender, email);
    }
}