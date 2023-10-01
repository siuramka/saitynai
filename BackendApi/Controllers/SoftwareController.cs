using AutoMapper;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/shops/{shopId}/softwares")]
public class SoftwareController : ControllerBase
{
    private ISoftwareRepository _softwareRepository;
    private ISubscriptionRepository _subscriptionRepository;
    private ISubscriptionService _subscriptionService;

    private IMapper _mapper;

    public SoftwareController(ISoftwareRepository softwareRepository, IMapper mapper, ISubscriptionService subscriptionService, ISubscriptionRepository subscriptionRepository)
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
        _subscriptionService = subscriptionService;
        _softwareRepository = softwareRepository;
    }
    
    [ApiExplorerSettings(IgnoreApi = true)] //swagger doesnt like this
    [HttpGet("/api/softwares", Name = "GetAllSoftwares")]
    public async Task<IActionResult> GetAllSoftwares([FromQuery] SoftwareParameters softwareParameters)
    {
        var softwares = await _softwareRepository.GetAllSoftwaresPagedAsync(softwareParameters);
    
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(softwares.Metadata));
    
        var softwareDtoReturns =
            softwares.Select(softwareQuery => _mapper.Map<SoftwareDtos.SoftwareDtoReturnAll>(softwareQuery));
    
        return Ok(softwareDtoReturns);
    }
    
    [HttpGet(Name = "GetAllShopSoftwares")]
    public async Task<IActionResult> GetAllShopSoftwares([FromQuery] SoftwareParameters softwareParameters, int shopId)
    {
        var softwares = await _softwareRepository.GetAllSoftwaresPagedAsync(softwareParameters, shopId);

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(softwares.Metadata));

        var softwareDtoReturns =
            softwares.Select(softwareQuery => _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(softwareQuery));

        return Ok(softwareDtoReturns);
    }
    
    [HttpGet("{softwareId}",Name = "GetSoftware")]
    public async Task<IActionResult> Get(int softwareId, int shopId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
            return NotFound();

        var softwareReturnDto = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);

        return Ok(softwareReturnDto);
    }

    [HttpPost(Name = "CreateSoftware")]
    public async Task<IActionResult> Post(SoftwareDtos.SoftwareCreateDto softwareCreateDto, int shopId)
    {
        var software = _mapper.Map<Software>(softwareCreateDto);
        software.ShopId = shopId;
        
        try
        {
            await _softwareRepository.CreateAsync(software);
        }
        catch
        {
            return BadRequest();
        }
        
        var softwareDtoReturn = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);
        return CreatedAtAction(nameof(Post), softwareDtoReturn);
    }

    [HttpPut("{softwareId}",Name = "UpdateSoftware")]
    public async Task<IActionResult> Put(SoftwareDtos.SoftwareUpdateDto softwareUpdateDto, int softwareId, int shopId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
        {
            return NotFound();
        }

        _mapper.Map(softwareUpdateDto, software);
        await _softwareRepository.UpdateAsync(software);

        var softwareDtoReturn = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);

        return Ok(softwareDtoReturn);
    }
    

    [HttpDelete("{softwareId}",Name = "DeleteSoftware")]
    public async Task<IActionResult> Delete(int softwareId, int shopId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
        {
            return NotFound();
        }

        await _softwareRepository.DeleteAsync(software);

        return NoContent();
    }
}