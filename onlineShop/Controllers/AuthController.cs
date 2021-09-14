using Microsoft.AspNetCore.Mvc;
using onlineShop.Data;
using onlineShop.Entity;
using onlineShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserModel request)
        {
            var response = await _authRepo.Register(
                new UserEntity { Name = request.Name , Email=request.Email , Address =request.Address }, request.Password
                );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginModel request)
        {
            var response = await _authRepo.Login(
            request.Email, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    
    }
}
