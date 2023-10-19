using AutoMapper;
using BackendApi.Auth.Models;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Helpers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/shops/{shopId:int}/softwares")]
public class SoftwareController : ControllerBaseWithUserId
{
    private ISubscriptionService _subscriptionService;
    private IAuthorizationService _authorizationService;
    private IRepositoryManager _repositoryManager;


    private IMapper _mapper;

    public SoftwareController(IRepositoryManager repositoryManager,IAuthorizationService authorizationService, IMapper mapper, ISubscriptionService subscriptionService)
    {
        _repositoryManager = repositoryManager;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _subscriptionService = subscriptionService;
    }
    
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)] //swagger doesnt like this
    [HttpGet("/api/softwares", Name = "GetAllSoftwares")]
    public async Task<IActionResult> GetAllSoftwares([FromQuery] SoftwareParameters softwareParameters)
    {
        PagedList<Software> softwares;
        
        if (UserHasRole(ShopUserRoles.Admin))
        {
            softwares = await _repositoryManager.Softwares.GetAllSoftwaresPagedAsync(softwareParameters);
        } 
        else if (UserHasRole(ShopUserRoles.ShopSeller))
        {
            softwares = await _repositoryManager.Softwares.GetAllSoftwaresPagedAsync(softwareParameters, UserId);
        }
        else
        {
            return Forbid();
        }

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(softwares.Metadata));
    
        var softwareDtoReturns =
            softwares.Select(softwareQuery => _mapper.Map<SoftwareDtos.SoftwareDtoReturnAll>(softwareQuery));
    
        return Ok(softwareDtoReturns);
    }
    
    [HttpGet(Name = "GetAllShopSoftwares")]
    public async Task<IActionResult> GetAllShopSoftwares([FromQuery] SoftwareParameters softwareParameters, int shopId)
    {
        if (!UserHasRole(ShopUserRoles.ShopUser))
        {
            var shop = await _repositoryManager.Shops.GetShopByIdAsync(shopId);
            
            //check if shop seller and shop owner
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, shop, PolicyNames.SellerShopOwner);
            
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
        }
        
        var softwares = await _repositoryManager.Softwares.GetAllSoftwaresPagedAsync(softwareParameters, shopId);

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(softwares.Metadata));

        var softwareDtoReturns =
            softwares.Select(softwareQuery => _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(softwareQuery));
        
        return Ok(softwareDtoReturns);
    }
    
    [HttpGet("{softwareId:int}",Name = "GetSoftware")]
    public async Task<IActionResult> Get(int softwareId, int shopId)
    {
        if (!UserHasRole(ShopUserRoles.ShopUser))
        {
            var shop = await _repositoryManager.Shops.GetShopByIdAsync(shopId);
            
            //check if shop seller and shop owner
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, shop, PolicyNames.SellerShopOwner);
            
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
        }
        
        var software = await _repositoryManager.Softwares.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
            return NotFound();

        var softwareReturnDto = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);
        return Ok(softwareReturnDto);
    }
    
    [HttpPost(Name = "CreateSoftware")]
    public async Task<IActionResult> Post(SoftwareDtos.SoftwareCreateDto softwareCreateDto, int shopId)
    {
        var shop = await _repositoryManager.Shops.GetShopByIdAsync(shopId);

        //check if shop seller and shop owner
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, shop, PolicyNames.SellerShopOwner);
        
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        var software = _mapper.Map<Software>(softwareCreateDto);
        software.ShopId = shopId;
        
        try
        {
            _repositoryManager.Softwares.Create(software);
            await _repositoryManager.SaveAsync();
        }
        catch
        {
            return BadRequest();
        }
        
        var softwareDtoReturn = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);
        return CreatedAtAction(nameof(Post), softwareDtoReturn);
    }

    [HttpPut("{softwareId:int}",Name = "UpdateSoftware")]
    public async Task<IActionResult> Put(SoftwareDtos.SoftwareUpdateDto softwareUpdateDto, int softwareId, int shopId)
    {
        var shop = await _repositoryManager.Shops.GetShopByIdAsync(shopId);
        //check if shop seller and shop owner
        var authorizationResult = await _authorizationService.AuthorizeAsync(User, shop, PolicyNames.SellerShopOwner);
        
        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }
    
        var software = await _repositoryManager.Softwares.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
        {
            return NotFound();
        }

        _mapper.Map(softwareUpdateDto, software);
        
        _repositoryManager.Softwares.Update(software);
        await _repositoryManager.SaveAsync();

        var softwareDtoReturn = _mapper.Map<SoftwareDtos.SoftwareDtoReturn>(software);
        return Ok(softwareDtoReturn);
    }
    
    [Authorize(Roles = ShopUserRoles.Admin)]
    [HttpDelete("{softwareId:int}",Name = "DeleteSoftware")]
    public async Task<IActionResult> Delete(int softwareId, int shopId)
    {
        var software = await _repositoryManager.Softwares.GetSoftwareByIdAsync(softwareId, shopId);

        if (software == null)
        {
            return NotFound();
        }

        _repositoryManager.Softwares.Delete(software);
        await _repositoryManager.SaveAsync();

        return NoContent();
    }
}