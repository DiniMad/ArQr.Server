namespace Resource.Api.Resources
{
    public sealed record ServiceResource(byte Id, string Title, string Description, int UnitPriceInThousandToman);
    public sealed record CreateServiceResource(string Title, string Description, int UnitPriceInThousandToman);
}