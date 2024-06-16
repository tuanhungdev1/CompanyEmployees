using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers {

    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) {
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto) {
            if (userForRegistrationDto is null)
                return BadRequest("userForRegistrationDto object sent from client is null");
            var result = await _service.AuthenticationService.RegisterUser(userForRegistrationDto);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user) {
            if(user is null) return BadRequest("user object sent from client is null");

            if(!await _service.AuthenticationService.ValidateUser(user)) {
                return Unauthorized();
            }

            var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }
    }
}
