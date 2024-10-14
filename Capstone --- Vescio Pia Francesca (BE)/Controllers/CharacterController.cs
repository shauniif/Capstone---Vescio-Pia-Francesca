using Capstone_____Vescio_Pia_Francesca__BE_.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_____Vescio_Pia_Francesca__BE_.Controllers
{
    public class CharacterController : Controller
    {   
        private readonly ICharacterService _characterSvc;


        public async Task<IActionResult> GetCharacterImage(int id)
        {
            var character = await _characterSvc.Read(id);
            if (character?.Image == null)
            {
                return NotFound();
            }
            var characterPhotodata = character.Image.Substring(23);
            byte[] imageBytes = Convert.FromBase64String(characterPhotodata);
            return File(imageBytes, "image/jpeg");
        }
        public CharacterController(ICharacterService characterSvc)
        {
           _characterSvc = characterSvc;
        }
        public async Task<IActionResult> AllCharacters() 
        {
            var characters = await _characterSvc.GetAll();
            return View(characters);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var character = await _characterSvc.Read(id);
            return View(character);
        }

        
    }
}
