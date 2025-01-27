using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
        private readonly RSA _privateKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly HttpClient _httpClient;

        public JWTAuthorization(string appID, string endpoint, string publicKeyFingerprint, string privateKeyFile, HttpClient? httpClient = null)
        {
            _appID = appID;
            _endpoint = endpoint;
            _publicKeyFingerprint = publicKeyFingerprint;
            _privateKey = RSA.Create();
            var privateKeyPem = File.ReadAllText(privateKeyFile);
            _privateKey.ImportFromPem(privateKeyPem);
            var securityKey = new RsaSecurityKey(_privateKey);
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
            _httpClient = httpClient ?? new HttpClient(); 
        }

        private string GenerateToken(double expireInSecond = 600)
        {
            DateTime utcNow = DateTime.UtcNow;
            var jwtToken = new JwtSecurityToken(
                issuer: _appID,
                audience: _endpoint,
                claims: new Claim[] {
                },
                expires: utcNow.AddSeconds(expireInSecond),
                signingCredentials: _signingCredentials
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
            var bodyStr = JsonSerializer.Serialize(body);
            var content = JsonContent.Create(body);
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_endpoint}/api/permission/oauth2/token");
            request.Content = content;
            request.Headers.Add("Authorization", $"Bearer {jwt}");
            var response = await _httpClient.SendAsync(request);
            var token = await response.GetJsonObjectAsync<OAuthToken>();
            return token?.AccessToken ?? string.Empty;
        }
    }
}
