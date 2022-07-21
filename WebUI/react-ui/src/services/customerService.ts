import Customer from "../models/customer";
import { getUrl, HostController, webApi } from "./webApi";

export async function fetchCustomerData() {
    try {
        let res = await webApi.get<Customer[]>(getUrl(HostController.CUSTOMER));
        return res.data;
    } catch (err) {
        console.error(err);
    }
}

export async function deleteCustomerData(id: number) {
    try {
        await webApi.delete<undefined>(getUrl(HostController.CUSTOMER) + `${id}`);
    } catch (err) {
        console.error(err);
    }
}