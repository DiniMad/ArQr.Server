export type UseState<T> = T | null;

export type CreateContextForUseState<T> = [
    T | null,
    (value: T | null) => void | ((value: T) => T)
] | null;