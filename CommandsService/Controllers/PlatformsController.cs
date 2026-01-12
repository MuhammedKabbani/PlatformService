namespace CommandsService.Controllers;

using System.Threading.Tasks;
using AutoMapper;
using CommandsService.Data.Repositories.Abstract;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/c/[controller]")]
public class PlatformsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPlatformRepository _platfromRepo;

    public PlatformsController(IMapper mapper, IPlatformRepository platfromRepo)
    {
        _mapper = mapper;
        _platfromRepo = platfromRepo;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetAll()
    {
        var platforms = await _platfromRepo.GetAllPlatformsAsync(trackChanges:false);
        var mappedPlatforms = _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

        return Ok(mappedPlatforms);
    }
    [HttpPost]
    public ActionResult Test()
    {
        System.Console.WriteLine("--> sync message recived");
        return Ok();
    }
}