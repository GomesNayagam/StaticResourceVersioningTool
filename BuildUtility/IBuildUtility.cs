namespace BuildUtility
{
    using System;

    public interface IBuildUtility : IDisposable
    {
        bool VersionStaticContentUrl(BuildUtilityOption option);
    }
}
