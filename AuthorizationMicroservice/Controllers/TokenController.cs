using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly ILogger<TokenController> _logger;

        public TokenController(ITokenProvider tokenProvider, ILogger<TokenController> logger)
        {
            _logger = logger;
            _tokenProvider = tokenProvider;
        }

        [HttpPost]
        public IActionResult Get(UserData user)
        {
            _logger.LogInformation("HTTP POST request " + nameof(TokenController));
            if (user.Username == "" && user.Password == "")
            {
                _logger.LogError("Username or password is not provided");
                return BadRequest("Username or password is not provided");
            }

            var userCheck = _tokenProvider
                .Users()
                .FirstOrDefault(m => m.Username == user.Username && m.Password == user.Password);

            if (userCheck != null)
            {
                _logger.LogInformation("Token generation for user");
                var token = _tokenProvider.GenerateJWTToken(userCheck);
                if (token == null)
                {
                    _logger.LogError("Token generation for user failed");
                    return BadRequest(token);
                }

                _logger.LogInformation("Token sent to user");
                return Ok(new {token = token});
            }

            _logger.LogError("User not found in database");
            return NotFound(new {status = StatusCodes.Status404NotFound, message = "User Not found"});
        }
    }
}
