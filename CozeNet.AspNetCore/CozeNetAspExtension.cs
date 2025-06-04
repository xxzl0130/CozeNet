using CozeNet.Chat;
using CozeNet.Conversation;
using CozeNet.Core;
using CozeNet.Core.Authorization;
using CozeNet.File;
using CozeNet.Knowledge;
using CozeNet.Message;
using CozeNet.Workflow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CozeNet.AspNetCore;

/// <summary>
/// Asp扩展方法类
/// </summary>
public static class CozeNetAspExtension
{
    /// <summary>
    /// Asp扩展方法
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <returns></returns>
    public static IServiceCollection AddCozeNet(this IServiceCollection services,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        services.Configure<CozeOptions>(configuration.GetSection("Coze"));
        var cozeOptions = services.BuildServiceProvider().GetRequiredService<IOptions<CozeOptions>>().Value;
        //如果配置了PersonalAccessToken则注入PrivateTokenAuthorization
        if (string.IsNullOrWhiteSpace(cozeOptions.PersonalAccessToken) is false)
        {
            services.AddScoped<IAuthorization, PrivateTokenAuthorization>(sp =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<CozeOptions>>().CurrentValue;
                return new PrivateTokenAuthorization(options.PersonalAccessToken!);
            });
        }
        //否则注入JWTAuthorization
        else
        {
            services.AddScoped<IAuthorization, JWTAuthorization>(sp =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<CozeOptions>>().CurrentValue;
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = string.IsNullOrWhiteSpace(options.HttpClientName)
                    ? factory.CreateClient()
                    : factory.CreateClient(options.HttpClientName);
                return new JWTAuthorization(options.AppId!, options.Endpoint, options.PublicKey!, options.PrivateKey!,
                    httpClient);
            });
        }
        services.AddScoped<IAuthStore, AuthStore>();
        services.AddScoped<Context>(sp =>
        {
            var options = sp.GetRequiredService<IOptionsMonitor<CozeOptions>>().CurrentValue;
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var client = string.IsNullOrWhiteSpace(options.HttpClientName) ? factory.CreateClient() : factory.CreateClient(options.HttpClientName);
            var auth = sp.GetRequiredService<IAuthStore>();
            return new Context
            {
                AccessToken = auth.GetAccessTokenAsync(options.TokenExpire).GetAwaiter().GetResult(),
                EndPoint = options.Endpoint,
                HttpClient = client,
            };
        });
        services.AddScoped<ChatService>();
        services.AddScoped<FileService>();
        services.AddScoped<KnowledgeService>();
        services.AddScoped<MessageService>();
        services.AddScoped<WorkflowService>();
        services.AddScoped<ConversationService>();

        return services;
    }
}