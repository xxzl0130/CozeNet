using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Core.Authorization
{
    public class PrivateTokenAuthorization(string token) : IAuthorization
    {
        public string GetAccessToken(int durationSecond)
        {
            return token;
        }

        public Task<string> GetAccessTokenAsync(int durationSecond, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(token);
        }
    }
}
