namespace ArQr.Models
{
    public sealed record CachePurchaseResource(string GatewayName,
                                              byte   Quantity,
                                              byte   OffAmountInThousandToman,
                                              long   UserId,
                                              byte   ServiceId);
}