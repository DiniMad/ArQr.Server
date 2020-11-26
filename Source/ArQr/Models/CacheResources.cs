namespace ArQr.Models
{
    public sealed record CachePaymentResource(string GatewayName,
                                              byte   Quantity,
                                              byte   OffAmountInThousandToman,
                                              long   UserId,
                                              byte   ServiceId);
}