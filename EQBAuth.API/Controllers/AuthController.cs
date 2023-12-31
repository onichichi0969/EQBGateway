﻿using EQBAuth.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EQBAuth.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody] Models.Request.ReqAuth auth)
        {
            return Ok(await _authService.GenerateToken(auth));
        }
        [HttpGet("GenerateToken")]
        public async Task<IActionResult> GenerateToken2()
        {
            Models.Request.ReqAuth auth = new Models.Request.ReqAuth();
            auth.Username = "EQBAUTH";
            auth.Password = "EQUICOMSAVINGSBANK@2023";
            return Ok(await _authService.GenerateToken(auth));
        }
    }
}
