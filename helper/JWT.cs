using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace carthage.Helper;
public class JwtAuthenticationManager
{
  //key declaration
  private readonly IConfiguration _configuration;

  public JwtAuthenticationManager(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string Authenticate(string username)
  {

    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);
    SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, username)
        }),
      Expires = DateTime.UtcNow.AddDays(2),

      SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }
}
