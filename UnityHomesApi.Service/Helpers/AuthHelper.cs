using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace HomesApi.Helpers;

public interface IAuthHelper
{
    byte[] CreatePasswordSalt();
    byte[] GetPasswordHash(string password, byte[] passwordSalt);
    string CreateToken(long userId, string email, string username);
}

public class AuthHelper : IAuthHelper
{
    private readonly IConfiguration _config;

    public AuthHelper(IConfiguration config)
    {
        _config = config;
    }

    public byte[] CreatePasswordSalt()
    {
        byte[] passwordSalt = new byte[128 / 8];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(passwordSalt);
        }

        return passwordSalt;
    }

    public byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
        string passwordSaltPlusString =
            _config.GetSection("AppSettings:PasswordKey").Value
            + Convert.ToBase64String(passwordSalt);

        return KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        );
    }

    public string CreateToken(long userId, string email, string username)
    {
        Claim[] claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim("email", email),
            new Claim("username", username)
        };

        string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

        SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(tokenKeyString != null ? tokenKeyString : "")
        );

        SigningCredentials credentials = new SigningCredentials(
            tokenKey,
            SecurityAlgorithms.HmacSha512Signature
        );

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = credentials,
            Expires = DateTime.Now.AddDays(2)
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
