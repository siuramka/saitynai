using AutoMapper;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/shops/{shopId}/softwares/{softwareId}/subscriptions")]
public class SubscriptionController : ControllerBase
{

    private ISubscriptionRepository _subscriptionRepository;
    private ISubscriptionService _subscriptionService;
    private ISoftwareRepository _softwareRepository;
    private IMapper _mapper; 
    public SubscriptionController(ISubscriptionRepository subscriptionRepository, IMapper mapper, ISubscriptionService subscriptionService, ISoftwareRepository softwareRepository)
    {
        _mapper = mapper;
        _softwareRepository = softwareRepository;
        _subscriptionService = subscriptionService;
        _subscriptionRepository = subscriptionRepository;
    }
    
    [ApiExplorerSettings(IgnoreApi = true)] //swagger doesnt like this
    [HttpGet(Name = "GetAllSubscriptionsPaging")]
    [Route("/api/subscriptions")]
    public async Task<IActionResult> GetAllPaging([FromQuery] SubscriptionParameters subscriptionParameters)
    {
        var subscriptions = await _subscriptionRepository.GetAllSubscriptionsPagedAsync(subscriptionParameters);
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(subscriptions.Metadata));
        
        var subscriptionDtoReturns = subscriptions.Select(subscriptionQuery => _mapper.Map<SubscriptionDtos.SubscriptionDtoReturn>(subscriptionQuery));
    
        return Ok(subscriptionDtoReturns);
    }
    
    
    [HttpGet("{subscriptionId}", Name = "GetSubscription")]
    public async Task<IActionResult> Get(int subscriptionId, int softwareId, int shopId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId, shopId, softwareId);

        if (subscription == null)
        {
            return NotFound();
        }

        var subscriptionReturnDto = _mapper.Map<SubscriptionDtos.SubscriptionDtoReturn>(subscription);
        
        return Ok(subscriptionReturnDto);
    }
    
    [HttpPut("{subscriptionId}", Name = "UpdateSubscription")]
    public async Task<IActionResult> Put(SubscriptionDtos.SubscriptionUpdateDto subscriptionUpdateDto, int subscriptionId, int softwareId, int shopId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId, shopId, softwareId);
    
        if (subscription == null)
        {
            return NotFound();
        }
    
        _mapper.Map(subscriptionUpdateDto, subscription);
        await _subscriptionRepository.UpdateAsync(subscription);
    
        var softwareDtoReturn = _mapper.Map<SubscriptionDtos.SubscriptionUpdateDto>(subscriptionUpdateDto);
    
        return Ok(softwareDtoReturn);
    }
    
    [HttpPost(Name = "CreateSubscription")]
    public async Task<IActionResult> Post(SubscriptionDtos.SubscriptionCreateDto subscriptionCreateDto, int softwareId, int shopId)
    {
        var software = await _softwareRepository.GetSoftwareByIdAsync(softwareId, shopId);
        
        if (software == null)
        {
            return BadRequest("Software not found...");
        }
        
        var subscription = _mapper.Map<Subscription>(subscriptionCreateDto);
        var subscriptionWithTerms = await _subscriptionService.GetSubscriptionWithTerms(subscriptionCreateDto, subscription, software);

        try
        {
            await _subscriptionRepository.CreateAsync(subscriptionWithTerms);
        }
        catch
        {
            return BadRequest();
        }
        
        var subscriptionDtoReturn = _mapper.Map<SubscriptionDtos.SubscriptionDtoReturn>(subscription);
        
        return Ok(subscriptionDtoReturn);
    }
    
    [HttpDelete("{subscriptionId}", Name = "DeleteSubscription")]
    public async Task<IActionResult> Delete(int subscriptionId, int shopId, int softwareId)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId, shopId, softwareId);
        if (subscription == null)
        {
            return NotFound();
        }
    
        await _subscriptionRepository.DeleteAsync(subscription);
    
        return NoContent();
    }
}