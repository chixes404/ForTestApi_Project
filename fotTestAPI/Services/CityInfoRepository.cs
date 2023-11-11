using fotTestAPI.DbContexts;
using fotTestAPI.Entites;
using fotTestAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace fotTestAPI.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)

        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderByDescending(x => x.Name).ToListAsync();
        }


        // Search and Filter 
            public async Task <IEnumerable<City>> GetCitiesAsync(string? name , string? searchQuery , int PageSize ,int PageNumber)
            {
                //if(string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(searchQuery)) // i comment them in case of i use pagination and not use
                //search query  
                //{
                //    return await GetCitiesAsync();
                //}

                var collection = _context.Cities as IQueryable<City>;
                if(!string.IsNullOrWhiteSpace(name))
                {
                    name = name.Trim();
                    collection = collection.Where(c => c.Name == name);
                }
                if(!string.IsNullOrWhiteSpace(searchQuery))
                {
                    searchQuery= searchQuery.Trim();
                    collection=collection.Where(a=>a.Name.Contains(searchQuery) || (a.Description!=null && a.Description.Contains(searchQuery)));


                }


                return await collection.OrderBy(x => x.Id).Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();
  

            }

        public async Task<bool> CityExistAsync(int CityId)
        {
            return await _context.Cities.AnyAsync(x => x.Id == CityId);
        }



        public async Task<City?> GetSingleCityAsync(int cityId)
        {
         
            return await _context.Cities.Where(v => v.Id == cityId).FirstOrDefaultAsync();

        }


        public async Task<City?> GetCityAsync(int cityId, bool IsIncludePointOfInterest)
        {
            if (IsIncludePointOfInterest)
            {

                return await _context.Cities.Include(c => c.pointsOfInterests).Where(x => x.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(v => v.Id == cityId).FirstOrDefaultAsync();

        }
         
        public async Task<PointOfInterests?> GetPointOfInterestsAsync(int CityId, int pointId)
        {

            return await _context.PointOfInterests.Where(x => x.CityId_v2 == CityId && x.Id == pointId).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<PointOfInterests>> GetPointsOfInterestsAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId_v2 == cityId).ToListAsync();


        }

        public async Task AddPointOfInterestAsync (int cityId, PointOfInterests pointOfInterests)
        {
            var city = await GetSingleCityAsync(cityId);

            if (city != null)
           
            {
                city.pointsOfInterests.Add(pointOfInterests);            
            }
                     
        }


        public async Task AddCityAsync( City Newcity)
        {
            _context.Cities.Add(Newcity);
        }


        public async Task RemovePointOfInterest(PointOfInterests pointOfInterests)
        {
            _context.PointOfInterests.Remove(pointOfInterests);

        }

        public async Task<bool> SaveChangeAsync()
        {

            return (await _context.SaveChangesAsync() > 0);
        }

    }

        }

 