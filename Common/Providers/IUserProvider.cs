using System.Collections.Generic;


namespace Common.Providers
{
    public interface IUserProvider
        : IProvider
    {
        long GetCurrentUserId();

        IEnumerable<string> GetClaimsByType(string claimType);
    }
}
