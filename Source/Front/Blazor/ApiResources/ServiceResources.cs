namespace Blazor.ApiResources
{
    public sealed record ServiceResource(byte Id, string Title, string Description, int UnitPriceInThousandToman);

    public sealed record CreateServiceResource(string Title,
                                               string Description,
                                               int    UnitPriceInThousandToman,
                                               string ProductType,
                                               int    Constraint,
                                               int    ExpireDurationInDays);
}