using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Cryptography;
using CozeNet.Core.Models;
using CozeNet.Utils;
using Microsoft.IdentityModel.Tokens;

namespace CozeNet.Core.Authorization
{
    public class JWTAuthorization : IAuthorization
    {
        private readonly string _appID;
        private readonly string _endpoint;
        private readonly string _publicKeyFingerprint;
        private readonly string _privateKeyPem;
        private readonly HttpClient _httpClient;
        private readonly RSAParameters _rsaParameters;

        public JWTAuthorization(string appID, string endpoint, string publicKeyFingerprint, string privateKeyFile, HttpClient? httpClient = null)
        {
            _appID = appID;
            _endpoint = endpoint;
            _publicKeyFingerprint = publicKeyFingerprint;
            _privateKeyPem = System.IO.File.ReadAllText(privateKeyFile);
            _httpClient = httpClient ?? new HttpClient();
            using var rsa = RSA.Create();
            rsa.ImportFromPem(_privateKeyPem);
            _rsaParameters = rsa.ExportParameters(true);
        }

        private string GenerateToken(double expireInSecond = 600)
        {
            var securityKey = new RsaSecurityKey(_rsaParameters);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
            DateTime utcNow = DateTime.UtcNow;
            var jwtToken = new JwtSecurityToken(
                issuer: _appID,
                audience: _endpoint,
                claims: [],
                expires: utcNow.AddSeconds(expireInSecond),
                signingCredentials: signingCredentials
            );
            jwtToken.Header.Add("kid", _publicKeyFingerprint);
            jwtToken.Payload.Add("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            jwtToken.Payload.Add("jti", Guid.NewGuid().ToString());
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public string GetAccessToken(int durationSecond)
        {
            var task = GetAccessTokenAsync(durationSecond);
            task.Wait();
            return task.Result;
        }

        public async Task<string> GetAccessTokenAsync(int durationSecond)
        {
            var jwt = GenerateToken(durationSecond);
            var body = new TokenRequestBody
            {
                DurationSeconds = durationSecond
            };
            var content = JsonContent.Create(body);
            using var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_endpoint}/api/permission/oauth2/token");
            request.Content = content;
            request.Headers.Add("Authorization", $"Bearer {jwt}");
            using var response = await _httpClient.SendAsync(request);
            var token = await response.GetJsonObjectAsync<OAuthToken>();
            return token?.AccessToken ?? string.Empty;
        }
    }
}
