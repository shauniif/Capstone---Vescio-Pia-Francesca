using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class LoginResponseModel
    {
        public User User { get; set; }
        /// <summary>
        /// Token per l'autenticazione.
        /// </summary>
        public required string Token { get; set; }
    }
}
