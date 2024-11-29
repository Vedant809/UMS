using Microsoft.AspNetCore.Mvc;
using UMS.Interface;
using UMS.Models;

namespace UMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username,string password)
        {
            bool isValidUser = await _userRepository.login(username, password);
            Console.WriteLine(isValidUser);
            if(isValidUser)
            {
                return RedirectToAction("UserScreen", "UserDetails");
            }
            else
            {
                ModelState.AddModelError("", "Not a Valid User");
                return View();
                
            }
            return Ok();
            //return View();
        }

    }
}
