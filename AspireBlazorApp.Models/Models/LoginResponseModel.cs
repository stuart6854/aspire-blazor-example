namespace AspireBlazorApp.Models.Models;

public class LoginResponseModel
{
    public string Token { get; set; }
    public long TokenExpired { get; set; }
}