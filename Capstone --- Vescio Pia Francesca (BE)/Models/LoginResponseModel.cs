using Capstone_____Vescio_Pia_Francesca__BE_.DTO;
using Capstone_____Vescio_Pia_Francesca__BE_.Entity;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class LoginResponseModel
    {
        public UserResponseDTO User { get; set; }
        /// <summary>
        /// Token per l'autenticazione.
        /// </summary>
        public required string Token { get; set; }
    }
}
