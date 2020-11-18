namespace ArQr.Model
{
    public class TokenOption
    {
        public string JwtSigningKey                    { get; set; }
        public int    JwtExpireIntervalInMinutes       { get; set; }
        public int    RefreshTokenExpireIntervalInDays { get; set; }
    }
}