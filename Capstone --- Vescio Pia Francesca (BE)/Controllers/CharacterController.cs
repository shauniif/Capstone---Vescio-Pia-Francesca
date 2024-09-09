using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class CharacterController : Controller
    {   
        private readonly ICharacterService _characterSvc;

        public CharacterController(ICharacterService characterSvc)
        {
           _characterSvc = characterSvc;
        }
        public async Task<IActionResult> AllCharacters() 
        {
            var characters = await _characterSvc.GetAll();
            return View(characters);
        }
    }
}
