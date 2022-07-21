import Person, { GenderType } from "./person";

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