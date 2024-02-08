using HomesApi.Helpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;

public class AuthHelperTests
{
		private IConfiguration _config;
		private AuthHelper _authHelper;

		public AuthHelperTests()
		{
				_config = Substitute.For<IConfiguration>();
				_config.GetSection("AppSettings:PasswordKey").Value.Returns("ksjnfjndfin89238-$%&$gEgsr-sgusgf#$-sd##n(()g-fggfd#$T&G");
				_config.GetSection("AppSettings:TokenKey").Value.Returns("skdjfERT&&&sfsg-we4DFBrt65Bdfe-sgreG&&sg5e4grgbhnm5##TYv-fgbq314hju6/()&*");

				_authHelper = new AuthHelper(_config);
		}

		[Fact]
		public void CreatePasswordSalt_ReturnsByteArray()
		{
				byte[] result = _authHelper.CreatePasswordSalt();
				Assert.Equal(16, result.Length);
		}

		[Fact]
		public void GetPasswordHash_ReturnExpectedHash_ForGivenPasswordAndSalt()
		{
				var password = "password";
				var salt = _authHelper.CreatePasswordSalt();
				var hash = _authHelper.GetPasswordHash(password, salt);

				Assert.NotNull(hash);
				Assert.Equal(32, hash.Length);

				// Further assertions can include checking for specific hash values if deterministic inputs are used
		}

		[Fact]
		public void CreateToken_ReturnsValidJwtToken()
		{
				var userId = 1L;
				var email = "test@test.com";

				var token = _authHelper.CreateToken(userId, email);

				Assert.False(string.IsNullOrEmpty(token));

				// You can extend this test by decoding the JWT and verifying the claims and signature
		}
}
