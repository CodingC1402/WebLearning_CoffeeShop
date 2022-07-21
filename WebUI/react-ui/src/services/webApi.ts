import axios from "axios"
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
    
});

webApi.interceptors.response.use((res) => {
    recursiveConvertToDayJS(res.data);
    return res;
})

function recursiveConvertToDayJS(obj: any) {
    const date = dayjs(obj, 'YYYY-MM-DDTHH:mm:ss', true);
    if (date.isValid()) {
        return new Date(date.toISOString());
    }

    if (Array.isArray(obj)) {
        const arr = obj as Array<any>;
        arr.forEach((element, index) => {
            arr[index] = recursiveConvertToDayJS(element);
        });
        return obj;
    }

    if (obj instanceof Object) {
        const keys = Object.keys(obj);
        keys.forEach((key) => {
            obj[key] = recursiveConvertToDayJS(obj[key]);
        })
    }

    return obj;
}