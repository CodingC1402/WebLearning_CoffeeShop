import { Model } from "./model";

export default class Coffee extends Model{
    constructor(
        public id: number,
        public name: string,
        public notes: string,
        public origin: string,
        public variety: string,
        public price: number
    ) {
        super(id);
    }
}