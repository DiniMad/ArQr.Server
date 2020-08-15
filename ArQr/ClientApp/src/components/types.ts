export type UseState<T> = T | null;

export type CreateContextForUseState<T> = [
    T | null,
    (value: T | null) => void | ((value: T) => T)
] | null;

export type UserType = {
    id: string
    username: string
    email: string
    emailConfirmed: boolean
    phoneNumber: string
    phoneNumberConfirmed: boolean
}