using APP2024P4.Data;
using APP2024P4.Data.Response.Vehicle;
using APP2024P4.Shared;
using Microsoft.EntityFrameworkCore;

namespace APP2024P4.Services.Vehicle;

public interface IModelService
{
    Task<ResultList<ModelResponse>> Get(string filter = "");
    Task<ResultList<ModelResponse>> GetByBrandId(int BrandId);
}

public class ModelService(IApplicationDbContext context) : IModelService
{
    public async Task<ResultList<ModelResponse>> Get(string filter = "")
    {
        try
        {
            var r = context.Models.Where(x => x.Name.ToLower().Contains(filter)).AsNoTracking().Select(
            x => new ModelResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                BrandId = x.BrandId,
                Brand = new BrandResponse()
                {
                    Id = x.Brand.Id,
                    Image = x.Brand.Image
                }
            }).OrderBy(x => x.Brand.Name).ToList();
            if (r is not null)
            {
                return ResultList<ModelResponse>.Success(r);
            }
            return ResultList<ModelResponse>.Failure("No Models founded");

        }
        catch (Exception ex)
        {
            return ResultList<ModelResponse>.Failure($"Error loading the models: ${ex.Message}");

        }
    }

    public async Task<ResultList<ModelResponse>> GetByBrandId(int BrandId)
    {
        try
        {
            var r = await context.Models.Where(x => x.BrandId == BrandId).Select(
            x => new ModelResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                BrandId = x.BrandId,
                Brand = new BrandResponse()
                {
                    Id = x.Brand.Id,
                    Image = x.Brand.Image
                }
            }).ToListAsync();
            if (r is not null && r.Count > 0)
            {
                return ResultList<ModelResponse>.Success(r);
            }
            return ResultList<ModelResponse>.Failure("No Models founded");

        }
        catch (Exception ex)
        {
            return ResultList<ModelResponse>.Failure($"Error loading the models: ${ex.Message}");

        }
    }
}
