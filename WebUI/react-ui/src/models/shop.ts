import Employee from "./employee";
import { Model } from "./model";

export default class Shop extends Model {
    public staffs: Employee[] | null = null;
    constructor(
        public id: number,
        public address: string,
        public phone: string,
        public establishedSince: Date,
    ) {
        super(id);
    }
}