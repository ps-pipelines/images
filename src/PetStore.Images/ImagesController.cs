using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace PetStore.Images
{
    [Route("api/v1/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IStore store;

        public ImagesController(IStore store)
        {
            this.store = store;
        }

        [HttpGet]
        [Route("")]
        public virtual IEnumerable<string> GetFiles()
        {
            return store.Files.Keys;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual IActionResult GetFile(string id)
        {
            return File(store.Files[id], "image/jpeg");
        }

        [HttpPost]
        [Route("")]
        public virtual IEnumerable<string> Upload(IEnumerable<IFormFile> files)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files));

            var results = new List<string>();

            foreach (var file in files)
            {
                var content = FormFileToByteArray(file);
                var id = Guid.NewGuid().ToString();
                store.Files.Add(id, content);
                results.Add(id);
            }

            return results;
        }


        private static byte[] FormFileToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
