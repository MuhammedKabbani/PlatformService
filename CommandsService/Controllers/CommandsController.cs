namespace CommandsService.Controllers;

using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data.Repositories.Abstract;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/c/platforms/{platformId}/[controller]")]
public class CommandsController : ControllerBase
{
    private readonly IPlatformRepository _platformRepo;
    private readonly ICommandRepository _commandRepo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepository commandRepo, IMapper mapper, IPlatformRepository platformRepo)
    {
        _commandRepo = commandRepo;
        _mapper = mapper;
        _platformRepo = platformRepo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
    {
        if (!await _platformRepo.PlatformExsists(platformId))
            return NotFound();

        var commands = await _commandRepo.GetAllCommandsByPlatformAsync(platformId, trackChanges:false);
        
        var mappedCommands = _mapper.Map<IEnumerable<CommandReadDto>>(commands);
        
        return Ok(mappedCommands);
    } 
    [HttpGet("{commandId}", Name = nameof(GetCommandForPlatform))]
    public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandForPlatform(int platformId, int commandId)
    {
        if (!await _platformRepo.PlatformExsists(platformId))
            return NotFound();

        var commands = await _commandRepo.GetCommandAsync(platformId,commandId, trackChanges:false);
        
        var mappedCommands = _mapper.Map<CommandReadDto>(commands);
        
        return Ok(mappedCommands);
    } 
    [HttpPost]
    public async Task<ActionResult> CreateCommand(int platformId, CommandCreateDto commandCreateDto)
    {
        if (!await _platformRepo.PlatformExsists(platformId))
            return NotFound();

        var command = _mapper.Map<Command>(commandCreateDto);
        
        await _commandRepo.CreateCommandAsync(platformId, command);
        
        var commandReadDto = _mapper.Map<CommandReadDto>(command);

        return CreatedAtRoute(nameof(GetCommandForPlatform),
            new {platformId = platformId, commandId = commandReadDto. Id}, commandReadDto);
    } 
}