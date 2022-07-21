import Employee from "../models/employee";
import { getUrl, HostController, webApi } from "./webApi";

export async function fetchEmployeeData() {
    try {
        let res = await webApi.get<Employee[]>(getUrl(HostController.EMPLOYEE));
        return res.data;
    } catch (err) {
        console.error(err);
    }
}

export async function deleteEmployeeData(id: number) {
    try {
        await webApi.delete<undefined>(getUrl(HostController.EMPLOYEE) + `/${id}`);
    } catch (err) {
        console.error(err);
    }
}