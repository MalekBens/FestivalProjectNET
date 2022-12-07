using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
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
          new Claim("email", username)
        }),
      Expires = DateTime.UtcNow.AddDays(2),

      SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
  }

  public string? DecriptToken(string token)
  {
    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
    SecurityToken jsonToken = tokenHandler.ReadToken(token);
    var tokenS = jsonToken as JwtSecurityToken;
    try
    {
      return tokenS.Claims.First(claim => claim.Type == "email").Value;
    }
    catch (System.Exception)
    {
      return null;
    }
  }
}
