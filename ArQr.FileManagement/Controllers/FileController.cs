using System.Linq;
using System.Threading.Tasks;
using ArQr.FileManagement.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArQr.FileManagement.Controllers
{
    [Authorize]
    [Route("/")]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("create")]
        public IActionResult StartSession(string fileMime, long fileSize)
        {
            var fileMimeArray = fileMime.Split("/");
            var extension     = fileMimeArray[1];
            var mediaType     = fileMimeArray[0];
            
            var sizeLimit = _fileService.Option.FileSizeLimitInByte.SingleOrDefault(pair => pair.Key == mediaType);
            if (sizeLimit.Key is null) return StatusCode(415, $"The {mediaType} not supported.");
            
            if (fileSize > sizeLimit.Value) return BadRequest($"Maximum file size is {sizeLimit}");

            var session = _fileService.CreateSession(extension, mediaType, fileSize);
            return Ok(session.Id);
        }

        [HttpPut("upload/{sessionId}/")]
        public async Task<IActionResult> UploadFileChunk(string sessionId, int chunkNumber)
        {
            if (chunkNumber < 1) return BadRequest("Invalid chunk number");

            var file = Request.Form.Files.First().OpenReadStream().ToByte();
            if (file.Length > _fileService.Option.ChunkSizeInByte)
                return StatusCode(415, $"Chunk size should be {_fileService.Option.ChunkSizeInByte} bytes");
            var session = _fileService.GetSession(sessionId);
            var sizeLimit =
                _fileService.Option.FileSizeLimitInByte.SingleOrDefault(pair => pair.Key == session.FileType).Value;
            var alreadyPersistedSize = session.PersistedChunksCount * _fileService.Option.ChunkSizeInByte;

            if (alreadyPersistedSize + file.Length > sizeLimit)
            {
                _fileService.RemoveSession(sessionId);
                return StatusCode(415, $"Maximum size of the {session.FileType} is {sizeLimit} bytes");
            }

            await _fileService.PersistBlockAsync(sessionId, chunkNumber, file);
            return Ok();
        }

        [HttpPost("end/{sessionId}/")]
        public IActionResult EndSession(string sessionId)
        {
            _fileService.RemoveSession(sessionId);

            return Ok();
        }

        [HttpGet("download/{sessionId}")]
        public async Task DownloadFile(string sessionId)
        {
            var session = _fileService.GetSession(sessionId);

            Response.ContentType                    = "application/octet-stream";
            Response.ContentLength                  = session.FileSize;
            Response.Headers["Content-Disposition"] = $"attachment; fileName={session.Id}.{session.FileExtension}";

            await _fileService.WriteToStreamAsync(Response.Body, session);
        }

        [HttpGet("configuration")]
        public IActionResult Configuration()
        {
            var configuration = new
            {
                _fileService.Option.ChunkSizeInByte,
                _fileService.Option.HasFileSizeLimit,
                _fileService.Option.FileSizeLimitInByte,
            };
            return Ok(configuration);
        }
    }
}