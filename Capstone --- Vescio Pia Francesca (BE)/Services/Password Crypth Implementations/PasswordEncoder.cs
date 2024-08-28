using System.Text;
using System.Security.Cryptography;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Services.Password_Crypth_Implementations
{
    public class PasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {   // istanza dell'algoritmo SHA256
            SHA256 sha256 = SHA256.Create();

            // conversione delle password in un array di Byte attraverso la codifica UTF8 
            // E calcola l'hash della password come array di byte
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // conversione dell'array in una stringa Base64 
            return Convert.ToBase64String(hashedBytes);
        }

        public bool IsSame(string plainText, string codedText)
        {   //codifica la password in chiario con lo stesso meotodo

            // Se i due hash sono uguali, significa che la passowrd in chiario e la password "hashata" sono equivalenti.
            return Encode(plainText) == codedText;
        }
    }
}
