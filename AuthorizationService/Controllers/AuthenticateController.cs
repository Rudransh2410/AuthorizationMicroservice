using AuthorizationService.Models;
using AuthorizationService.Repository;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more LogInformationrmation on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private static IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly ProducerConfig _producerConfig;
        public AuthenticateController(IAuthenticationService authenticationService, ILogger<AuthenticateController> logger, ProducerConfig producerConfig)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _producerConfig = producerConfig;
        }
        //Action Method to Generate JWT 
        [Route("Login")]
        [HttpGet]
        public IActionResult Authentication(string email, string password)
        {
            _logger.LogInformation("Trying to Login");
            User user = _authenticationService.Authentication(email, password);
            if (user == null)
            {
                _logger.LogWarning("Unauthorised Access !!!  Check user credentials");
                return new UnauthorizedResult();
            }
            string token = _authenticationService.TokenGeneration(email, password);
            _logger.LogInformation("Logged In Successfully");
            return new OkObjectResult(token);

        }
        [HttpGet("{userId}")]
        public IActionResult GetUserById(string userId)
        {
            try
            {
                User user = _authenticationService.GetUserById(userId);

                if (user != null)
                {
                    _logger.LogInformation("User with id" + userId + "returned successfully");
                    return new OkObjectResult(user);
                }
                else
                {
                    _logger.LogWarning(" User with id " + userId + " does not exist.");
                    return NotFound("Unavailable user");
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(AuthenticateController.GetUserById) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(AuthenticateController.GetUserById) + " Error Message " + e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                User user = _authenticationService.GetUserByEmail(email);
                if (user != null)
                {
                    _logger.LogInformation("User with email" + email + "returned successfully");
                    return new  OkObjectResult(user);
                }
                else
                {
                    _logger.LogWarning(" User with email " + email + " does not exist.");
                    return NotFound("Unavailable user");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(AuthenticateController.GetUserByEmail) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(AuthenticateController.GetUserByEmail) + " Error Message " + e.Message);
            }
        }

        //[Authorize]
        [Route("users")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                List<User> users = _authenticationService.GetUsers();

                if (users != null)
                {
                    _logger.LogInformation("Users returned successfully");
                    return new ObjectResult(users);
                }
                else
                {
                    _logger.LogWarning(" Users unavailable");
                    return NotFound("Users unavailable");
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(AuthenticateController.GetUsers) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(AuthenticateController.GetUsers) + " Error Message " + e.Message);
            }
        }
        [Route("signup")]
        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                if (user != null)
                {
                    _authenticationService.AddUser(user);
                    _logger.LogInformation("Users added successfully");
                    return new ObjectResult(user);
                }
                else
                {
                    _logger.LogWarning(" Undefined user");
                    return BadRequest("Undefined user");
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(AuthenticateController.AddUser) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(AuthenticateController.AddUser) + " Error Message " + e.Message);
            }
        }
        [Route("changepassword")]
        [HttpPut]
        public IActionResult ChangePassword(string email, string password)
        {
            try
            {

                bool isChanged = _authenticationService.ChangePassword(email, password);

                if (!isChanged)
                {
                    _logger.LogWarning("Password change failed");
                    return BadRequest("Password change failed");
                }

                _logger.LogInformation("Password updated successfully");
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError("Error occured from " + nameof(AuthenticateController.ChangePassword) + " Error Message " + e.Message);
                return BadRequest("Error occured from " + nameof(AuthenticateController.ChangePassword) + " Error Message " + e.Message);
            }
        }

    }

}
