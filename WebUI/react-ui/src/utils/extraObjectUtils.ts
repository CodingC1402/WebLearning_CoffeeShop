export function getKeysFromClass<T>(type: new(...args: any) => T) {
    const instance = new type();
    console.log(Object.keys(instance));
    return Object.keys(instance);
}