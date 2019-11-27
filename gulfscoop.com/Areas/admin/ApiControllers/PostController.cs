using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DAL.Database;
using gulfscoop.com.Models;

namespace gulfscoop.com.Areas.admin.ApiControllers
{
    //[Route("api/admin/Post")]
    public class PostController : ApiController
    {
        private readonly ApplicationDbContext _db;


        public PostController()
        {
            this._db = new ApplicationDbContext();
        }
        public PostController(ApplicationDbContext Db)
        {
            this._db = Db;
        }
        // GET: api/Post
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Post/5
        public string Get(int id)
        {
            return "You have requested " + id;
        }

        [AllowAnonymous]
        // POST: api/Post
        public HttpStatusCode Post([FromBody]Post post)
        {
                
            try{
                _db.Posts.Add(post);

            }
            catch (Exception e)
            {
                return HttpStatusCode.InternalServerError;
            }
            
            return HttpStatusCode.Created;
        }
        //public Task<HttpStatusCode>  PostAsync([FromBody]Post post)
        //{
        //    Task.Delay(4000);
        //    try
        //    {
        //        _db.Posts.Add(post);

        //    }
        //    catch (Exception e)
        //    {
        //        return Task.FromResult(HttpStatusCode.InternalServerError);
        //    }

        //    return Task.FromResult(HttpStatusCode.Created);
        //}

        // PUT: api/Post/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Post/5
        public void Delete(int id)
        {
        }

    }
}
