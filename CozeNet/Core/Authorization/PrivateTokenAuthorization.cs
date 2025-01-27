using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Core.Authorization
{
    public class PrivateTokenAuthorization : IAuthorization
    {
        private readonly string _token;

        public PrivateTokenAuthorization(string token)
        {
            _token = token;
        }

        public string GetAccessToken(int durationSecond)
        {
            return _token;
        }

        public Task<string> GetAccessTokenAsync(int durationSecond)
        {
            return Task.FromResult(_token);
        }
    }
}
