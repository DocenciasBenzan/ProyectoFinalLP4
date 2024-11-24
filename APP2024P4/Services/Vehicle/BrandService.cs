using APP2024P4.Data;
using APP2024P4.Data.Response.Vehicle;
using APP2024P4.Shared;

namespace APP2024P4.Services.Vehicle;

public interface IBrandService
{
    Task<ResultList<BrandResponse>> Get(string filter = "");
}


public class BrandService(IApplicationDbContext context) : IBrandService
{
    public async Task<ResultList<BrandResponse>> Get(string filter = "")
    {
        try
        {
            var r = context.Brands.Where(x => x.Name.ToLower().Contains(filter)).Select(x => new BrandResponse() { Id = x.Id, Name = x.Name, Image = x.Image }).OrderBy(x => x.Name).ToList();
            if (r is not null && r.Count > 0)
            {
                return ResultList<BrandResponse>.Success(r);
            }
            return ResultList<BrandResponse>.Failure("No brands founded");

        }
        catch (Exception ex)
        {
            return ResultList<BrandResponse>.Failure($"Error: Getting the brands : {ex.Message}");
        }
    }
}
