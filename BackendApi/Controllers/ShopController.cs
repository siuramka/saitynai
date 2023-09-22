using AutoMapper;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Data.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendApi.Controllers;

[ApiController]
[Route("api/shops")]
public class ShopController : ControllerBase
{
    private IShopRepository _shopRepository;
    private ISoftwareRepository _softwareRepository;
    private IMapper _mapper;

    public ShopController(IShopRepository shopRepository, IMapper mapper, ISoftwareRepository softwareRepository)
    {
        _softwareRepository = softwareRepository;
        _mapper = mapper;
        _shopRepository = shopRepository;
    }
    
    
    // api/shops
    [HttpGet(Name = "GetShops")]
    [ProducesResponseType(200, Type = typeof(List<ShopDtos.ShopDtoReturn>))]
    public async Task<IActionResult> GetAllPaging([FromQuery] ShopParameters shopParameters)
    {
        var shops = await _shopRepository.GetAllShopsPagedAsync(shopParameters);
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(shops.Metadata));

        var shopDtos = shops.Select(shop =>
            new ShopDtos.ShopDtoReturn(shop.Id, shop.Name, shop.Description, shop.ContactInformation));
        return Ok(shopDtos);
    }

    // api/shops/{id}
    [HttpGet("{id}", Name = "GetShop")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200, Type = typeof(ShopDtos.ShopDtoReturn))]
    public async Task<IActionResult> Get(int id)
    {
        var shop = await _shopRepository.GetShopByIdAsync(id);

        if (shop == null)
            return NotFound();

        var shopDto = new ShopDtos.ShopDtoReturn(shop.Id, shop.Name, shop.Description, shop.ContactInformation);
        return Ok(shopDto);
    }

    [HttpPost(Name = "CreateShop")]
    public async Task<IActionResult> Post(ShopDtos.ShopCreateDto shopCreateDto)
    {
        var shop = _mapper.Map<Shop>(shopCreateDto);

        try
        {
            await _shopRepository.CreateAsync(shop);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }

    // api/shops/{id}
    [HttpPut("{id}", Name = "UpdateShop")]
    public async Task<IActionResult> Put(ShopDtos.ShopUpdateDto shopUpdateDto, int id)
    {
        var shop = await _shopRepository.GetShopByIdAsync(id);

        if (shop == null)
        {
            return NotFound();
        }

        _mapper.Map(shopUpdateDto, shop);
        await _shopRepository.UpdateAsync(shop);

        var shopReturnDto = _mapper.Map<ShopDtos.ShopDtoReturn>(shop);
        
        return Ok(shopReturnDto);
    }
    
    [HttpDelete("{id}", Name = "DeleteShop")]
    public async Task<IActionResult> Delete(int id)
    {
        var shop = await _shopRepository.GetShopByIdAsync(id);

        if (shop == null)
        {
            return NotFound();
        }

        await _shopRepository.DeleteAsync(shop);

        return NoContent();
    }
}