using gulfscoop.com.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace gulfscoop.com.Controllers
{
    public class ArticleController : ApiController
    {

        public ArticleViewModel Get()
        {
            return new ArticleViewModel() { title = "This is a new Article" };
        }
    }
}
