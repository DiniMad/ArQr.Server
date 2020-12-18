namespace Blazor.ApiResources
{
    public sealed record VerifyMediaContentResource(long MediaContentId, bool Verify = true);
}