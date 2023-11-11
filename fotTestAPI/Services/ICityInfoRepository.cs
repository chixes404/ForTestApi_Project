using fotTestAPI.Entites;
namespace fotTestAPI.Services

{
    public interface ICityInfoRepository
    {

        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name , string? searchQuery , int PageSize , int PageNumber);

        Task<City?> GetCityAsync(int cityId, bool IsIncludePointOfInterest);
        Task<City?> GetSingleCityAsync(int cityId);
        Task<IEnumerable<PointOfInterests>> GetPointsOfInterestsAsync(int cityId );
        Task<bool> CityExistAsync(int CityId);
        Task RemovePointOfInterest(PointOfInterests pointOfInterests);
        Task<PointOfInterests?> GetPointOfInterestsAsync(int CityId , int pointId);
        Task AddPointOfInterestAsync(int cityId, PointOfInterests pointOfInterests);
        Task AddCityAsync(City city);

        Task<bool> SaveChangeAsync();
    }
} 
