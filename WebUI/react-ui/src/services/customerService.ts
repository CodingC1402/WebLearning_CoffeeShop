import Customer from "../models/customer";
import { getUrl, HostController, webApi } from "./webApi";

export async function fetchCustomerData() {
    let res = await webApi.get<Customer[]>(getUrl(HostController.CUSTOMER));
    return res.data;
}

export async function updateCustomerData(customer: Customer, id: number) {
    await webApi.put(getUrl(HostController.CUSTOMER) + id, {...customer, id: id});
}

export async function addCustomerData(customer: Customer) {
    let res = await webApi.post(getUrl(HostController.CUSTOMER), customer);
    return res.data;
}

export async function deleteCustomerData(id: number) {
    await webApi.delete<undefined>(getUrl(HostController.CUSTOMER) + id);
}