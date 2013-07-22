﻿using ServiceStack.ServiceHost;

namespace BetterCms.Module.Api.Operations.Blog.BlogPosts.BlogPost
{
    [Route("/blog-posts/{BlogPostId}", Verbs = "GET")]
    public class GetBlogPostRequest : RequestBase<GetBlogPostModel>, IReturn<GetBlogPostResponse>
    {
        public virtual System.Guid BlogPostId
        {
            get
            {
                return Data.BlogPostId;
            }
            set
            {
                Data.BlogPostId = value;
            }
        }
    }
}