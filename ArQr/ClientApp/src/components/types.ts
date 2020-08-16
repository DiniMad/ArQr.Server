export type UseState<T> = T | null;

export type CreateContextForUseState<T> = [
    T | null,
    (value: T | null) => void | ((value: T) => T)
] | null;

export type User = {
    id: string
    username: string
    email: string
    emailConfirmed: boolean
    phoneNumber: string
    phoneNumberConfirmed: boolean
}

export type ProductContentType = "Text" | "Picture" | "Video";

export type AdsProduct = {
    title: string,
    description: string,
    content: {
        type: ProductContentType,
        value: string
    }
}

export type UserIdentity = {
    email: string,
    password: string
}