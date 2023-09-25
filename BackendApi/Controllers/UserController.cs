using AutoMapper;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/users/{userId}")]
public class UserController : ControllerBase
{
    private ISubscriptionRepository _subscriptionRepository;
    private IMapper _mapper;

    public UserController(IMapper mapper, ISubscriptionRepository subscriptionRepository)
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
    }

    [HttpGet("subscriptions", Name = "GetUserSubscriptions")]
    public async Task<IActionResult> Get([FromQuery] SubscriptionParameters subscriptionParameters, int userId)
    {
        var userSubscriptionsPaged = await _subscriptionRepository.GetAllSubscriptionsPagedAsync(subscriptionParameters, userId);
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(userSubscriptionsPaged.Metadata));

        var dtos = userSubscriptionsPaged.Select(subscriptionQuery =>
            _mapper.Map<SubscriptionDtos.SubscriptionDtoReturn>(subscriptionQuery));

        return Ok(dtos);
    }
}