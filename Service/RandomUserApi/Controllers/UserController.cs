using System.Collections.Generic;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RandomUser.Business.Contract.LoggerService;
using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Entity.Dto;
using RandomUser.Business.Model;

namespace RandomUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var users = _repository.UserRepository.GetAll();
            _logger.LogInfo("Users.");

            var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersResult);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var user = _repository.UserRepository.GetById(id);
            _logger.LogInfo("User.");

            var userResult = _mapper.Map<UserDto>(user);
            return Ok(userResult);
        }

        // POST: api/User
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UserDto user)
        {
            if (user == null)
            {
                _logger.LogError("Owner object sent from client is null.");
                return BadRequest("Owner object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid owner object sent from client.");
                return BadRequest("Invalid model object");
            }
            var userEntity = _mapper.Map<User>(user);
            _repository.UserRepository.Add(userEntity);
            _mapper.Map<UserDto>(user);
            _repository.Save();
            var createdUser = _mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("Get", new { id = user.Id }, createdUser);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto user)
        {
            if (user == null)
            {
                _logger.LogError("Owner object sent from client is null.");
                return BadRequest("Owner object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid owner object sent from client.");
                return BadRequest("Invalid model object");
            }
            var userEntity = _repository.UserRepository.GetById(id);
            if (userEntity == null)
            {
                _logger.LogError($"User with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            _mapper.Map(user, userEntity);
            _repository.UserRepository.Update(userEntity);
            _repository.Save();
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _repository.UserRepository.GetById(id);
            if (user == null)
            {
                _logger.LogError($"User with id: {id}, hasn't been found in db.");
                return NotFound();
            }
            _repository.UserRepository.Remove(user);
            _repository.Save();
            return NoContent();
        }
    }
}
