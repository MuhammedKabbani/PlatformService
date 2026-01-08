namespace CommandsService.Controllers;

using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/c/[controller]")]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {
        
    }
    [HttpPost]
    public ActionResult TestInController()
    {
        System.Console.WriteLine("--> In PlatformsController");
        return Ok("Controller is working");
    }
}