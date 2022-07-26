import Employee from "../models/employee";
import { getUrl, HostController, webApi } from "./webApi";

export async function fetchEmployeeData() {
    let res = await webApi.get<Employee[]>(getUrl(HostController.EMPLOYEE));
    return res.data;
}

export async function updateEmployeeData(employee: Employee, id: number) {
    await webApi.put(getUrl(HostController.EMPLOYEE) + id, {...employee, id: id});
}

export async function addEmployeeData(employee: Employee) {
    let res = await webApi.post(getUrl(HostController.EMPLOYEE), employee);
    return res.data;
}

export async function deleteEmployeeData(id: number) {
    await webApi.delete<undefined>(getUrl(HostController.EMPLOYEE) + `${id}`);
}