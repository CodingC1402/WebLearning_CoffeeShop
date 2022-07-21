import dayjs from "dayjs";

export function formatDate(date: Date, format: string = 'DD/MM/YYYY') {
    var betterDay = dayjs(date).format(format);
    return betterDay;
}