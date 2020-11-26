namespace ArQr.Models
{
    public sealed record CachePaymentResource(string GatewayName,
                                              byte   Quantity,
                                              long   PriceInRial,
                                              byte   OffAmountInThousandToman,
                                              long   UserId,
                                              byte   ServiceId);
}