namespace CozeNet.AspNetCore;

/// <summary>
/// 扣子配置项
/// </summary>
public class CozeOptions
{
    /// <summary>
    /// api终结点 国内版：api.coze.cn  国际版：api.coze.com
    /// </summary>
    public string Endpoint { get; set; } = "api.coze.cn";

    /// <summary>
    /// 应用id
    /// </summary>
    public string? AppId { get; set; }

    /// <summary>
    /// 公钥内容
    /// </summary>
    public string? PublicKey { get; set; }

    /// <summary>
    /// 私钥文件名
    /// </summary>
    public string? PrivateKey { get; set; }

    /// <summary>
    /// 个人访问令牌 如果配置了就会注入PrivateTokenAuthorization
    /// </summary>
    public string? PersonalAccessToken { get; set; }

    /// <summary>
    /// 智能体id
    /// </summary>
    public string? BotId { get; set; }

    /// <summary>
    /// 注入的HttpClient自定义名称
    /// </summary>
    public string? HttpClientName { get; set; }

    /// <summary>
    /// access token 有效期 单位:s 不传默认一天
    /// </summary>
    public int? TokenExpire { get; set; }
}