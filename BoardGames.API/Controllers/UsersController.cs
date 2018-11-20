using AutoMapper;
using BoardGames.DataAccess.Entities;
using BoardGames.DomainLogic.Models;
using BoardGames.DomainLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BoardGames.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<UserResponseModel>> Get(string title)
        {
            var results = await _userService.GetAsync();
            return results.Select(t => _mapper.Map<User, UserResponseModel>(t));
        }

        [HttpPost, Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _userService.Authenticate(model.Username, model.Password);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Tokens:Issuer"],
                    _configuration["Tokens:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(20),
                    signingCredentials: creds
                );

                var results = new
                {
                    username = user.Username,
                    firstame = user.FirstName,
                    lastname = user.LastName,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };

                return Created("", results);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost, Route("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<RegisterRequestModel, User>(model);
                _userService.Register(user, model.Password);
                return Created("", model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { messsage = ex.Message });
            }
        }
    }
}