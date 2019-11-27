using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace gulfsoccer.utilities.JSON
{
    public static class JConvert
    {
        public static object serializePosts(object posts)
        {
            return JsonConvert.SerializeObject(posts, Formatting.Indented,
                         new JsonSerializerSettings
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
        }
    }
}
