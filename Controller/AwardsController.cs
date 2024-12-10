using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AwardsController : ControllerBase
{
    private readonly AwardService _awardService;

    public AwardsController(AwardService awardService)
    {
        _awardService = awardService;
    }

    [HttpGet("intervals")]
    public IActionResult GetAwardIntervals()
    {
        var result = _awardService.GetAwardIntervals();
        return Ok(result);
    }
}
