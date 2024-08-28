namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Password_Crypth_Implementations
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        bool IsSame(string plainText, string codedText);
    }
}
