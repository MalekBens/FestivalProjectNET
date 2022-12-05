namespace carthage.Models.Auth;

public class Signin
{
  public string email { set; get; } = "";
  public string password { set; get; } = "";
}

public class SignUp
{
  public string firstName { set; get; } = "";
  public string lastName { set; get; } = "";
  public string email { set; get; } = "";
  public string password { set; get; } = "";
}