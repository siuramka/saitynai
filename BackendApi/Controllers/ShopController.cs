using System.Security.Claims;
using AutoMapper;
using BackendApi.Auth.Handlers;
using BackendApi.Auth.Models;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using BackendApi.Helpers;
using BackendApi.Helpers.Sorting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/shops")]
public class ShopController : ControllerBaseWithUserId
{
    private IRepositoryManager _repositoryManager;
    private IMapper _mapper;
    private IAuthorizationService _authorizationService;

    public ShopController(IRepositoryManager repositoryManager, IMapper mapper, IAuthorizationService authorizationService)
    {
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _authorizationService = authorizationService;
    }

    // api/shops
    //Admin can see all shops
    //User can see all shops
    //Seller can see all of HIS shops
    [Authorize]
    [HttpGet(Name = "GetShops")]
    [ProducesResponseType(200, Type = typeof(List<ShopDtos.ShopDtoReturn>))]
    public async Task<IActionResult> GetAllPaging([FromQuery] ShopParameters shopParameters)
    {
        PagedList<Shop> shops;

        if (UserHasRole(ShopUserRoles.ShopSeller))
        {
            shops = await _repositoryManager.Shops.GetAllShopsPagedAsync(shopParameters, UserId);
        }
        else
        {
            shops = await _repositoryManager.Shops.GetAllShopsPagedAsync(shopParameters);
        }

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(shops.Metadata));

        var shopDtos = shops.Select(shop =>
            new ShopDtos.ShopDtoReturn(shop.Id, shop.Name, shop.Description, shop.ContactInformation));
        return Ok(shopDtos);
    }

    // api/shops/{id}
    [Authorize]
    [HttpGet("{id:int}", Name = "GetShop")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(ShopDtos.ShopDtoReturn))]
    public async Task<IActionResult> Get(int id)
    {
        var shop = await _repositoryManager.Shops.GetShopByIdAsync(id);

        if (!UserHasRole(ShopUserRoles.ShopUser) && !UserHasRole(ShopUserRoles.Admin)) //if not user and not admin check if seller and owns the shop
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, shop, PolicyNames.SellerShopOwner);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
        }
        //Seller can only see his own shops

        if (shop == null)
            return NotFound();

        var shopReturnDto = _mapper.Map<ShopDtos.ShopDtoReturn>(shop);
        return Ok(shopReturnDto);
    }

    [Authorize(Roles = ShopUserRoles.ShopSeller)]
    [HttpPost(Name = "CreateShop")]
    public async Task<IActionResult> Post(ShopDtos.ShopCreateDto shopCreateDto)
    {
        var shop = _mapper.Map<Shop>(shopCreateDto);

        try
        {
            shop.ShopUserId = UserId;
            _repositoryManager.Shops.Create(shop);
            await _repositoryManager.SaveAsync();
        }
        catch
        {
            return BadRequest();
        }

        var shopResponse = _mapper.Map<ShopDtos.ShopDtoReturn>(shop);

        return CreatedAtAction(nameof(Post), shopResponse);
    }

    // api/shops/{id}
    [Authorize]
    [HttpPut("{id:int}", Name = "UpdateShop")]
    public async Task<IActionResult> Put(ShopDtos.ShopUpdateDto shopUpdateDto, int id)
    {
        var shop = await _repositoryManager.Shops.GetShopByIdAsync(id);

        //Seller can edit only his own shops

        var authorizationResult = await _authorizationService.AuthorizeAsync(User, shop, PolicyNames.SellerShopOwner);

        if (!authorizationResult.Succeeded)
        {
            return Forbid();
        }

        if (shop == null)
        {
            return NotFound();
        }

        _mapper.Map(shopUpdateDto, shop);

        _repositoryManager.Shops.Update(shop);
        await _repositoryManager.SaveAsync();

        var shopReturnDto = _mapper.Map<ShopDtos.ShopDtoReturn>(shop);

        return Ok(shopReturnDto);
    }

    [Authorize(Roles = ShopUserRoles.Admin)]
    [HttpDelete("{id:int}", Name = "DeleteShop")]
    public async Task<IActionResult> Delete(int id)
    {
        var shop = await _repositoryManager.Shops.GetShopByIdAsync(id);

        if (shop == null)
        {
            return NotFound();
        }

        _repositoryManager.Shops.Delete(shop);
        await _repositoryManager.SaveAsync();

        return NoContent();
    }
}