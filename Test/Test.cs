using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozeNet.Core.Authorization;

namespace Test;

internal class Test
{
    static public void Run(Options options)
    {
        IAuthorization authorization = new JWTAuthorization(options.AppID, options.EndPoint, options.PublicKey, options.PrivateKey);
        Console.WriteLine(authorization.GetAccessToken());
    }
}
