using AutoMapper;
using Azure;
using fotTestAPI.DbContexts;
using fotTestAPI.Entites;
using fotTestAPI.Model;
using fotTestAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace fotTestAPI.Controllers
{
	[Route("api/Cities")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		//private readonly IMailservice  _MailService;
        private readonly CityInfoContext _context;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        //private readonly  CitiesList ;

        public CitiesController(/* IMailservice MailService*/ CityInfoContext context ,ICityInfoRepository cityInfoRepository , IMapper mapper)
		{
			//_MailService = MailService; // localNailservice 
            _context = context; // DBContext --> database 
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


		[HttpGet("GetCity")]
		public async Task<ActionResult<IEnumerable<CityDtoWithoutPointOfInterest>>> GetCity()
		{
			var CitiesEntity = await _cityInfoRepository.GetCitiesAsync();
		
			return Ok(_mapper.Map<IEnumerable<CityDtoWithoutPointOfInterest>>(CitiesEntity));
		}



        [HttpGet("GetCity/{cityId}")]
		public async Task<ActionResult<IEnumerable<CitiesDto>>> GetCity(int cityId , bool IsIncludePointOfInterest = false)
		{
			var city = await _cityInfoRepository.GetCityAsync(cityId, IsIncludePointOfInterest); 

            if (city == null)
			{
				return NotFound();
			}
			if(IsIncludePointOfInterest)
			{
				return Ok(_mapper.Map<CitiesDto>(city));		
					
			}



			return Ok(_mapper.Map<CityDtoWithoutPointOfInterest>(city));
		}

////////////////////////////////////////////////////////////////////////////////////////////



		[HttpGet("GetCity/{cityid}/PointOfInterest")]
		public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityid)
		{

			if(!await _cityInfoRepository.CityExistAsync(cityid))
			{
				return NotFound();
			}
			var PointOI = await _cityInfoRepository.GetPointsOfInterestsAsync(cityid);
			if(PointOI == null)
			{
				return NotFound();
			}
			

				return Ok(_mapper.Map< IEnumerable<PointOfInterestDto>> (PointOI));
	

			}


		////////////////////////////////////////////////////////////////////////////
		
			
		[HttpGet("GetCity/{Cityid}/PointOfInterest/{PointId}")]
		public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterestById(int Cityid, int PointId)
		{

		if(!await _cityInfoRepository.CityExistAsync(Cityid))
			{
				return NotFound();
			}

			var PoI = await _cityInfoRepository.GetPointOfInterestsAsync(Cityid, PointId); 

				if (PoI == null)
			{
				return NotFound();
			}
				return Ok(_mapper.Map<PointOfInterestDto>(PoI));
		}



		// create point of interest : first =>  we want to create method of adding in Repository , then make the contract 
		// Second => 
		[HttpPost("{CityId}/CreatePointOfInterest")]         // ADD POINTOFINTEREST 


		public async Task<ActionResult> CreatePointOfInterest(int CityId, CreatePOintOfInterestDto pointOfInterest)
		{
			
			if (!await _cityInfoRepository.CityExistAsync(CityId))
			{

				return NotFound();
			}

			var finalPointOfInterest = _mapper.Map<Entites.PointOfInterests>(pointOfInterest);
			
			await _cityInfoRepository.AddPointOfInterestAsync(CityId, finalPointOfInterest);
			
			await _cityInfoRepository.SaveChangeAsync();
			
			return Ok(finalPointOfInterest);


		}

        [HttpPost("City")]

        public async Task<ActionResult> CreateCity (UpdateAndCreateCityDto Newcity )     // ADD CITY	
		{

			var AddedCity = _mapper.Map<Entites.City>(Newcity);
            await _cityInfoRepository.AddCityAsync(AddedCity);
            await _cityInfoRepository.SaveChangeAsync();
               return Ok(AddedCity);



        }






        [HttpPut("{CityId}/PointOfInterest/{PointOfInterestId}")]   // UPDATE POINTOFINTEREST INFO
		public async Task<ActionResult> UpdatePointOfInterest(int CityId, UpdatePointOfInterestDto updatepointOfInterest, int PointOfInterestId)

		{

            if (!await _cityInfoRepository.CityExistAsync(CityId))
            {

                return NotFound(); 
            }


			var UpdatedThePointOfInterest = await _cityInfoRepository.GetPointOfInterestsAsync(CityId, PointOfInterestId);

			if (UpdatedThePointOfInterest == null)
			{
				return NotFound();
			}
			
			_mapper.Map(updatepointOfInterest, UpdatedThePointOfInterest);

			await _cityInfoRepository.SaveChangeAsync();


			return NoContent();
		}

        [HttpPut("City/{CityId}")]     // UPDATE CITY INFO 

		public async Task<ActionResult> UpdateCity(int CityId , UpdateAndCreateCityDto updateCityDto)
		{
            if (!await _cityInfoRepository.CityExistAsync(CityId))
            {

                return NotFound();
            }

			var WillUpdatedCity = await _cityInfoRepository.GetSingleCityAsync(CityId);

            if (WillUpdatedCity == null)
            {
                return NotFound();
            }
            _mapper.Map(updateCityDto, WillUpdatedCity);

            await _cityInfoRepository.SaveChangeAsync();


            return NoContent(); // HTTP 204: Update was successful


        }


		//[HttpPatch("{CityId}/PointOfInterest/{PointOfInterestId}")]
		//public async Task<ActionResult> PartialUpdatePointOfInterest(int CityId, int PointOfInterestId, JsonPatchDocument<UpdatePointOfInterestDto> patchDocument)
		//{
		//	var city = _cityInfoRepository.Cities.FirstOrDefault(x => x.Id == CityId);

		//	if (city == null)
		//	{
		//		return NotFound();
		//	}

		//	var restorePointOfInterest = city.pointsOfInterests.FirstOrDefault(c => c.Id == PointOfInterestId);

		//	if (restorePointOfInterest == null)
		//	{
		//		return NotFound();
		//	}

		//	var PatchPointOfInterest = new UpdatePointOfInterestDto()
		//	{
		//		Name = restorePointOfInterest.Name,
		//		Description = restorePointOfInterest.Description,
		//	};

		//	patchDocument.ApplyTo(PatchPointOfInterest);

		//	restorePointOfInterest.Name = PatchPointOfInterest.Name;
		//	restorePointOfInterest.Description = PatchPointOfInterest.Description;

		//	return Ok(PatchPointOfInterest);
		//}


		[HttpDelete("{CityId}/PointOfInterest/{PointOfInterestId}")]

		public async Task<ActionResult> DeletePointOfInterest(int CityId, int PointOfInterestId)
		{


            if (!await _cityInfoRepository.CityExistAsync(CityId))
            {

                return NotFound();
            }

            var DeletedPoint = await  _cityInfoRepository.GetPointOfInterestsAsync(CityId, PointOfInterestId);

			if (DeletedPoint == null)
			{

				return NotFound();
			}

            await _cityInfoRepository.RemovePointOfInterest(DeletedPoint);
            await _cityInfoRepository.SaveChangeAsync();


            return NoContent();



		}

		//FILTER AND SEARCHING AND PAGINATION 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDtoWithoutPointOfInterest>>> GetCityByName(string? name, string? searchQuery, int PageSize, int PageNumber)
        {

            var CityName = await _cityInfoRepository.GetCitiesAsync(name, searchQuery, PageSize , PageNumber);


      
            return Ok(_mapper.Map<IEnumerable<CityDtoWithoutPointOfInterest>>(CityName));
        }




    }
}
