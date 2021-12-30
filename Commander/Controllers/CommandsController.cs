using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : Controller
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commands= _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}",Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            Command command= _repository.GetCommandById(id);
            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(command));
            }
            return NotFound();
            
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel=_mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto=_mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById),new {Id=commandReadDto.Id},commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandCreateDto commandUpdateDto)
        {
            var commandFromRepo= _repository.GetCommandById(id);
            if(commandFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandFromRepo);
            _repository.UpdateCommand(commandFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }


        [HttpPatch("{id}")]
        public ActionResult PatchCommand(int id, JsonPatchDocument<CommandCreateDto> patchDoc)
        {
            var commandFromRepo = _repository.GetCommandById(id);
            if (commandFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch=_mapper.Map<CommandCreateDto>(commandFromRepo);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
