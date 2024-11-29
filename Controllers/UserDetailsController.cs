using Microsoft.AspNetCore.Mvc;
using UMS.Models;
using UMS.Repository;
using UMS.Interface;
using System.Net;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;

namespace UMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : Controller
    {
        private readonly IUserDetailsRepository _userDetails;
        
        public UserDetailsController(IUserDetailsRepository userDetails)
        {
            _userDetails = userDetails;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserScreen()
        {
            var userList = _userDetails.GetAll();
            return View(userList);
        }

        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] List<int> users)
        {
            try
            {
                bool result = await _userDetails.deleteUser(users);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]

        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]UserDetails user)
        {
            try
            {
                var newUser =await _userDetails.createUser(user);
                return Ok(newUser);
            }

            catch(Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> updateUser(UserDetails userDetails)
        {
            try
            {
                var updatedUser = await _userDetails.updateUser(userDetails);
                return Ok(updatedUser);
            }

            catch(Exception ex)
            {
                return StatusCode(500, new {success = false, message = ex.Message});
            }
        }

        [HttpGet]
        [Route("~/UserDetails/AddUser")]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpGet]
        [Route("/UserDetails/EditUser")]
        public IActionResult EditUser(int userId,string firstname,string lastname, string dob,string email,string roles)
        {
            DateTime parsedDob;
            if (!DateTime.TryParse(dob, out parsedDob))
            {
                return BadRequest("Invalid date format.");
            }

            ViewBag.UserId = userId;
            ViewBag.FirstName = firstname;
            ViewBag.LastName = lastname;
            ViewBag.DOB = parsedDob.ToString("yyyy-MM-dd");
            ViewBag.Email = email;
            ViewBag.Roles = roles;

            return View("EditUser");
            
        }

        [HttpGet]
        [Route("SearchUser")]
        public IActionResult searchUser(string? firstname)
        {
            try
            {
                if(firstname == null)
                {
                    return RedirectToAction("UserScreen", "UserDetails");
                }
                else
                {
                    var user = _userDetails.getUserByFirstName(firstname);
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
