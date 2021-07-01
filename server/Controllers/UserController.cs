using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;
using server.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        private readonly IEmailSender _emailSender;

        public UserController(ILogger<UserController> logger, IUserService userService, IEmailSender emailSender)
        {
            _logger = logger;
            _userService = userService;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            try
            {
                return Ok(_userService.Read());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            try
            {
                user.Created = user.Updated = DateTime.Now;
                if (ModelState.IsValid)
                {
                    var newUser = _userService.Create(user);
                    //send email on registartion
                    _emailSender.SendEmailAsync(user.UserName, "Welcome to User Registration APP", "We are happy to see you here");

                    return Ok(newUser);
                }
                return BadRequest("Invalid Model Accepted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public ActionResult Update(User user)
        {
            try
            {
                user.Updated = DateTime.Now;
                user.Created = user.Created.ToLocalTime();
                if (ModelState.IsValid)
                {
                    _userService.Update(user);
                    return Ok(user);
                }
                return BadRequest("Invalid Model Accepted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
