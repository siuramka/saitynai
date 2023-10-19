using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;
using BackendApi.Helpers;

namespace BackendApi.Data.Repository.Contracts;

public interface ISoftwareRepository : IRepository<Software>
{
    Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters);
    Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters, string userId);
    Task<PagedList<Software>> GetAllSoftwaresPagedAsync(SoftwareParameters parameters, int shopId);
    Task<Software?> GetSoftwareByIdAsync(int softwareId, int shopId);
}