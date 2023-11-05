using AutoMapper;
using BackendApi.Auth.Models;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Authorize(Roles = ShopUserRoles.ShopUser)]
[Route("api/shops/{shopId:int}/softwares/{softwareId:int}/subscriptions")]
public class SubscriptionController : ControllerBaseWithUserId
{
    private IRepositoryManager _repositoryManager;
    private ISubscriptionService _subscriptionService;
    private IAuthorizationService _authorizationService;
    private IMapper _mapper;
    public SubscriptionController(IRepositoryManager repositoryManager, IAuthorizationService authorizationService, IMapper mapper, ISubscriptionService subscriptionService)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _subscriptionService = subscriptionService;
        _authorizationService = authorizationService;
    }

    [ApiExplorerSettings(IgnoreApi = true)] //swagger doesnt like this
    [HttpGet(Name = "GetAllSubscriptionsPaging")]
    [Route("/api/subscriptions")]
    public async Task<IActionResult> GetAllPaging([FromQuery] SubscriptionParameters subscriptionParameters)
    {
        var subscriptions = await _repositoryManager.Subscriptions.GetAllSubscriptionsPagedAsync(subscriptionParameters, UserId);

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(subscriptions.Metadata));

        var subscriptionDtoReturns = subscriptions.Select(subscriptionQuery => _mapper.Map<SubscriptionDtos.SubscriptionDtoReturnAll>(subscriptionQuery));
        return Ok(subscriptionDtoReturns);
    }

    [HttpGet("{subscriptionId:int}", Name = "GetSubscription")]
    public async Task<IActionResult> Get(int subscriptionId, int softwareId, int shopId)
    {
        var subscription = await _repositoryManager.Subscriptions.GetSubscriptionByIdAsync(subscriptionId, shopId, softwareId);
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, subscription, PolicyNames.ResourceOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        if (subscription == null)
        {
            return NotFound();
        }

        var subscriptionReturnDto = _mapper.Map<SubscriptionDtos.SubscriptionDtoReturn>(subscription);

        return Ok(subscriptionReturnDto);
    }

    [HttpPut("{subscriptionId:int}", Name = "UpdateSubscription")]
    public async Task<IActionResult> Put(SubscriptionDtos.SubscriptionUpdateDto subscriptionUpdateDto, int subscriptionId, int softwareId, int shopId)
    {
        var subscription = await _repositoryManager.Subscriptions.GetSubscriptionByIdAsync(subscriptionId, shopId, softwareId);
        var software = await _repositoryManager.Softwares.GetSoftwareByIdAsync(softwareId, shopId);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, subscription, PolicyNames.ResourceOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        if (subscription == null || software == null)
        {
            return NotFound();
        }

        var subscriptionWithTerms = await _subscriptionService.UpdateSubscription(subscriptionUpdateDto, subscription, software);

        _mapper.Map(subscriptionWithTerms, subscription);
        _repositoryManager.Subscriptions.Update(subscription);
        await _repositoryManager.SaveAsync();

        var softwareDtoReturn = _mapper.Map<SubscriptionDtos.SubscriptionUpdateDto>(subscriptionUpdateDto);

        return Ok(softwareDtoReturn);
    }

    [HttpPost(Name = "CreateSubscription")]
    public async Task<IActionResult> Post(SubscriptionDtos.SubscriptionCreateDto subscriptionCreateDto, int softwareId, int shopId)
    {
        var software = await _repositoryManager.Softwares.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
        {
            return BadRequest("Software not found...");
        }

        var subscription = _mapper.Map<Subscription>(subscriptionCreateDto);
        var subscriptionWithTerms = await _subscriptionService.GetSubscriptionWithTerms(subscriptionCreateDto, subscription, software);


        subscriptionWithTerms.ShopUserId = UserId;
        subscriptionWithTerms.Start = subscriptionWithTerms.Start.ToUniversalTime();
        subscriptionWithTerms.End = subscriptionWithTerms.End.ToUniversalTime();
        _repositoryManager.Subscriptions.Create(subscriptionWithTerms);
        await _repositoryManager.SaveAsync();

        var subscriptionDtoReturn = _mapper.Map<SubscriptionDtos.SubscriptionDtoReturn>(subscription);

        return CreatedAtAction(nameof(Post), subscriptionDtoReturn);
    }

    [HttpDelete("{subscriptionId:int}", Name = "DeleteSubscription")]
    public async Task<IActionResult> Delete(int subscriptionId, int shopId, int softwareId)
    {
        var subscription = await _repositoryManager.Subscriptions.GetSubscriptionByIdAsync(subscriptionId, shopId, softwareId);

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, subscription, PolicyNames.ResourceOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        if (subscription == null)
        {
            return NotFound();
        }

        _repositoryManager.Subscriptions.Delete(subscription);
        await _repositoryManager.SaveAsync();
        return NoContent();
    }
}