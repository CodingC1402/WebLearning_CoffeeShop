import axios, { AxiosResponseTransformer } from "axios"
import dayjs from "dayjs";
var customParseFormat = require('dayjs/plugin/customParseFormat')
dayjs.extend(customParseFormat)

export const HOST = 'https://localhost:7157'
export enum HostController {
    EMPLOYEE = 'employee',
    COFFEE = 'coffee',
    ORDER = 'order',
    SHOP = 'shop',
    CUSTOMER = 'customer',
}

export function getUrl(controller: HostController, action?: string): string {
    return `${HOST}/${controller}/${action ? action : ''}`
}

export const webApi = axios.create({
    transformResponse: [transformResponse, ...(axios.defaults.transformResponse as AxiosResponseTransformer[])],
});

function transformResponse(data: any) {
    const date = dayjs(data, 'YYYY-MM-DDTHH:mm:ss', true);
    if (date.isValid()) {
        return new Date(date.toISOString());
    }

    if (Array.isArray(data)) {
        const arr = data as Array<any>;
        arr.forEach((element, index) => {
            arr[index] = transformResponse(element);
        });
        return data;
    }

    if (data instanceof Object) {
        const keys = Object.keys(data);
        keys.forEach((key) => {
            data[key] = transformResponse(data[key]);
        })
    }

    return data;
}