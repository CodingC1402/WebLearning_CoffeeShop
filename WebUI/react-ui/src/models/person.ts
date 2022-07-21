import { Model } from "./model";

export enum GenderType {
    male,
    female,
    non
}

export default class Person extends Model {
    constructor(
        public id: number,
        public fullName: string,
        public dob: Date,
        public phoneNumber: string,
        public gender: GenderType,
        public email?: string
    ) {
        super(id);
    }
}