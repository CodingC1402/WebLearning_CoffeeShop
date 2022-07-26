import Coffee from "../models/coffee";
import { getUrl, HostController, webApi } from "./webApi";

export async function fetchCoffeeData() {
    let res = await webApi.get<Coffee[]>(getUrl(HostController.COFFEE));
    return res.data;
}

export async function updateCoffeeData(employee: Coffee, id: number) {
    await webApi.put(getUrl(HostController.COFFEE) + id, {...employee, id: id});
}

export async function addCoffeeData(employee: Coffee) {
    let res = await webApi.post(getUrl(HostController.COFFEE), employee);
    return res.data;
}

export async function deleteCoffeeData(id: number) {
    await webApi.delete<undefined>(getUrl(HostController.COFFEE) + `${id}`);
}