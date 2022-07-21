export function getKeysFromClass<T>(type: new(...args: any) => T) {
    const instance = new type();
    console.log(Object.keys(instance));
    return Object.keys(instance);
}

export function keyOf<T>(o: new(...args: any) => T, k: keyof T) {
    return k;
}