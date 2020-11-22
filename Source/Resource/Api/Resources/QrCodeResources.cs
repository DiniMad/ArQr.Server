namespace Resource.Api.Resources
{
    public sealed record QrCodeResource(string  Title,
                                        string  Description,
                                        int     CreationDate,
                                        int     ExpireDate,
                                        string? AssociatedPhoneNumber,
                                        string? AssociatedWebsite,
                                        bool    Expired,
                                        long    OwnerId);
}