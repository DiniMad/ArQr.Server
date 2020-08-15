export type useStateType<T> = T | null;

export type createContextForUseStateType<T> = [
    T | null,
    (value: T | null) => void | ((value: T) => T)
] | null;