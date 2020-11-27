namespace ArQr.Models
{
    public sealed record CachePaymentResource(string GatewayName,
                                              byte   Quantity,
                                              long   PriceInRial,
                                              byte   OffAmountInThousandToman,
                                              long   UserId,
                                              byte   ServiceId);

    public sealed record CacheUploadSession(long UserId, long MediaContentId, byte MaxSizeInMb);
}