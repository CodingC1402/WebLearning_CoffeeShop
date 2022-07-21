import { Dayjs } from "dayjs";
import Person, { GenderType } from "./person";

export default class Customer extends Person {
    constructor(
        public id: number,
        public fullName: string,
        public DOB: Dayjs,
        public phoneNumber: string,
        public gender: GenderType,
        public registerSince: Dayjs,
        public point: number,
        public email?: string
    ) {
        super(id, fullName, DOB, phoneNumber, gender, email);
    }
}