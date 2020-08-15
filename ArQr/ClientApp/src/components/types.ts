export type UseStateType<T> = T | null;

export type CreateContextForUseStateType<T> = [
    T | null,
    (value: T | null) => void | ((value: T) => T)
] | null;