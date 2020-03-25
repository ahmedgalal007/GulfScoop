using DAL.Database;
using gulfsoccer.Areas.Admin.Models.PostViewModels;
using System.Collections.Generic;
using System.Web.Http;

namespace gulfsoccer.Areas.Admin.ApiControllers
{
    public class PostController : ApiController
    {
        // GET: api/Post
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Post/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Post
        public void Post([FromBody]CreatePostViewModel post)
        {
            if (post.id == 0)
            {
            }
        }

        // PUT: api/Post/5
        public void Put(int id, [FromBody]Post value)
        {
        }

        // DELETE: api/Post/5
        public void Delete(int id)
        {
        }
    }
}