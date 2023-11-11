using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fotTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpGet("GetFile")]
        public ActionResult GetFile(string FIleId) {

            var PathTofile = "Home Page.txt";

            if(!System.IO.File.Exists(PathTofile))
            {

                return NotFound();
            }

            var Byte = System.IO.File.ReadAllBytes(PathTofile);

            return File(Byte, "text/plain", Path.GetFileName(PathTofile));   


        }

		[HttpPost("upload")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			try
			{
				if (file != null && file.Length > 0)
				{
					var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

					if (!Directory.Exists(uploadDir))
					{
						Directory.CreateDirectory(uploadDir);
					}

					var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

					var filePath = Path.Combine(uploadDir, fileName);

					// Save the file to the server
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}

					return Ok(new { filePath });
				}

				return BadRequest("No file is uploaded.");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}


	}
}
