using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStore.Images.Infrastructure;

namespace PetStore.Images
{
    [Route("/api/images")]
    public class ImagesController : Controller
    {
        private readonly IStore _store;

        public ImagesController(IStore store)
        {
            _store = store;
        }

        //[Consumes("image/jpeg")]
        //AddFileUploadParams
        [HttpPost]
        [Route("")]
        public IActionResult UploadImage(List<IFormFile> files)
        {
            if (files == null || !files.Any()) throw new ArgumentNullException(nameof(files));
            var file = files.First();
            if (file.Length <= 0) throw new Exception("no content in the file");

            var image = ReadFully(file.OpenReadStream());
            var name = _store.Save(file.Name, image);
            return Ok(name);
        }


        [HttpPut]
        [Route("")]
        public IActionResult UploadImage([FromBody]Image image)
        {
            _store.UpdateName(image.Id, image.Name);
            return Ok();
        }



        [HttpGet]
        public IActionResult GetImageByQs(
            [FromQuery(Name = "id")] string id, 
            [FromQuery(Name = "name")] string name)

        {
            return string.IsNullOrEmpty(id) 
                ? Ok(_store.Find(name)) 
                : Ok(_store.Load(id));
        }


        [HttpGet]
        [Route("content/{id}")]
        public IActionResult GetImage(string id)
        {
            var image = _store.Load(id);
            if (image == null)
            {
                return NotFound();
            }

            return File(image.Content, "image/jpeg");
        }



        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
