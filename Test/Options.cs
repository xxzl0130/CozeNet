using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Test;

internal class Options
{
    [Option('a', "app")]
    public string? AppID { get; set; }
    [Option('e', "end")]
    public string? EndPoint { get; set; } = "api.coze.cn";
    [Option('p', "public")]
    public string? PublicKey { get; set; }
    [Option('k', "private")]
    public string? PrivateKey { get; set; }
}
