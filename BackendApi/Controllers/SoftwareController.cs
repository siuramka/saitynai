using AutoMapper;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/softwares")]
public class SoftwareController : ControllerBase
{
    private ISoftwareRepository _softwareRepository;
    private IMapper _mapper;

    public SoftwareController(ISoftwareRepository softwareRepository, IMapper mapper)
    {
        _mapper = mapper;
        _softwareRepository = softwareRepository;
    }

    [HttpGet(Name = "GetSoftwares")]
    public async Task<IActionResult> GetAllPaging([FromQuery] SoftwareParameters softwareParameters)
    {
        var softwares = await _softwareRepository.GetAllSoftwaresPagedAsync(softwareParameters);

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(softwares.Metadata));

        var softwareDtoReturns =
            softwares.Select(softwareQuery => _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(softwareQuery));

        return Ok(softwareDtoReturns);
    }

    // api/softwares/{softwareId}"
    [HttpGet("{softwareId}", Name = "GetSoftware")]
    public async Task<IActionResult> Get(int softwareId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId);

        if (software == null)
            return NotFound();

        var softwareReturnDto = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);

        return Ok(softwareReturnDto);
    }
    
    // api/softwares"
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

        return Ok();
    }

    [HttpPut("{softwareId}", Name = "UpdateSoftware")]
    public async Task<IActionResult> Put(SoftwareDtos.SoftwareUpdateDto softwareUpdateDto, int softwareId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId);

        if (software == null)
        {
            return NotFound();
        }

        _mapper.Map(softwareUpdateDto, software);
        await _softwareRepository.UpdateAsync(software);

        var softwareDtoReturn = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);

        return Ok(softwareDtoReturn);
    }

    [HttpDelete("{softwareId}", Name = "DeleteSoftware")]
    public async Task<IActionResult> Delete(int softwareId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId);

        if (software == null)
        {
            return NotFound();
        }

        await _softwareRepository.DeleteAsync(software);

        return NoContent();
    }
}