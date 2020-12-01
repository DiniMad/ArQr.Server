namespace Resource.Api.Resources
{
    public sealed record VerifyMediaContentResource(long MediaContentId, bool Verify = true);
}