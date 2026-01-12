namespace PlatformService.Controllers;

using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.ASyncDataServices;
using PlatformService.Data.Repositories.Abstract;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;
    private readonly IMessageBusClient _messageBusClient;
    public PlatformsController(IPlatformRepository repository, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
        _messageBusClient = messageBusClient;
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
    {
        var platforms = await _repository.GetAllAsync(trackChanges: false);
        var platformsDto = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);
        return Ok(platformsDto);
    }

    [HttpGet("get-by-id/{id}", Name = nameof(GetPlatformById))]
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById([FromRoute]int id)
    {
        var platform = await _repository.FirstOrDefaultAsync(x => x.Id == id, trackChanges: false);
        if (platform == null)
        {
            return NotFound();
        }
        var platformDto = _mapper.Map<PlatformReadDto>(platform);
        return Ok(platformDto);
    }

    [HttpPost("create")]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform([FromBody]PlatformCreateDto platform)
    {
        var platformModel = _mapper.Map<Platform>(platform);
        await _repository.AddAsync(platformModel);
        await _repository.SaveAsync();
        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
        await _commandDataClient.SendPlatformToCommand(platformReadDto);
        try
        {
            var publishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
            publishedDto.Event = "Platform_Published";
            await _messageBusClient.PublishToPlatformAsync(publishedDto);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"--> Error during send async message {ex.Message}");
        }
        return CreatedAtRoute(nameof(GetPlatformById), new { id = platformReadDto.Id }, platformReadDto);
    }
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var platform = _repository.FirstOrDefault(x => x.Id == id, trackChanges: false);
        if (platform == null)
            return NotFound($"Platfrom with id {id} was not found");

        _repository.Remove(platform);
        var result = await _repository.SaveAsync();

        if (result <= 0)
            return BadRequest();

        return Ok($"Platfrom was deleted succesfully with Id = {platform.Id}");

    }
}