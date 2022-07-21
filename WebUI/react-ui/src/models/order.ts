import { Model } from "./model";

export default class Order extends Model {
    public details: OrderDetail[] | null = null;
    constructor(
        public id: number,
        public customerId: number,
        public employeeId: number,
        public shopId: number,
        public total: number,
    ) {
        super(id);
    }
}

export class OrderDetail {
    constructor(
        public orderId: number,
        public coffeeId: number,
        public count: number
    ) {}
}