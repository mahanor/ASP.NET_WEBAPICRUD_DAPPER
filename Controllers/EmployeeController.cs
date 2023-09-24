using Dapper_CrudWebApi.Models;
using Dapper_CrudWebApi.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dapper_CrudWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepo _repo;

        private readonly IConfiguration _configuration;
     
        public EmployeeController(IConfiguration configuration, IEmployeeRepo repo)
        {
            _configuration = configuration;
            _repo = repo;

        }

      

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.EmpName == "Kiran" && login.EmpPassword == "Kiran@123")
            {
                var token = GenerateToken(login.EmpName);
                return Ok(token);
            }
            return BadRequest("Login Failed");


        }
        private string GenerateToken(string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
            };
            var token = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value, _configuration.GetSection("Jwt:Audience").Value, claims,
                expires: DateTime.Now.AddMinutes(50),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        [HttpGet("TestToken")]
        public IActionResult Test()
        {
            return Ok("Token Validated Succesfully ...!!!");
        }




        [HttpGet("GetAllEmp")]
        public async Task<IActionResult> GetAllEmp()
        {
            var _list = await _repo.GetAllEmp();
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

      
        [HttpPost("InsertEmp")]
        public async Task<IActionResult> Insert([FromBody]Employee employee)
        {
            var _result = await _repo.Insert(employee);
            
            return Ok(_result);
           
        }

        


        [HttpPut("UpdateEmp")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            var _result = await _repo.Update(employee);

            return Ok(_result);

        }


        [HttpDelete("DeleteEmp")]
        public async Task<IActionResult> Delete(int EmpId)
        {
            var _result = await _repo.Delete(EmpId);

            return Ok(_result);

        }

    }
}
