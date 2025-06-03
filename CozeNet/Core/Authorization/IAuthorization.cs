using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozeNet.Core.Authorization
{
    public interface IAuthorization
    {
        string GetAccessToken(int durationSecond = 86399);
        Task<string> GetAccessTokenAsync(int durationSecond = 86399, CancellationToken cancellationToken = default);
    }
}
