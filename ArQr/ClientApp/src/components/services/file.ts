export const blobUrlToFile = async (url: string, fileName = "file"): Promise<File> => {
    const response = await fetch(url);
    const blob = await response.blob();
    return new File([blob], fileName, {type: blob.type, lastModified: Date.now()});
};

export const objectToFormData = (data: object): FormData => {
    const formData = new FormData();
    
    for (const [key, value] of Object.entries(data))
        formData.append(key, value);
    
    return formData;
};