using Microsoft.AspNetCore.Mvc;
using Order.Data.Interfaces;
using Order.Domain.DTO.Request;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserData _userData;

        public UserController(IUserData userData)
        {
            _userData = userData;
        }



        [HttpPost("Add")]
        public async Task<IActionResult> Create(UserRequest userRequest)
        {
            var user= await _userData.Add(userRequest);
            return Ok(user);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userData.GetAll();
            return Ok(user);
        }
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await _userData.GetById(id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(UserRequest userRequest)
        {
            var result = await _userData.Update(userRequest.Id, userRequest);

            return Ok(result);
        }
    }
}
