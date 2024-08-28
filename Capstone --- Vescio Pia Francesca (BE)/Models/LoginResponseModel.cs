namespace Capstone_____Vescio_Pia_Francesca__BE_.Models
{
    public class LoginResponseModel
    {
        public required string Username { get; set; }
        /// <summary>
        /// Token per l'autenticazione.
        /// </summary>
        public required string Token { get; set; }
    }
}
