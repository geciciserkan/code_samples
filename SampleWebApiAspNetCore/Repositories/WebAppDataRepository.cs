using Microsoft.Extensions.Configuration;
using SampleWebApiAspNetCore.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SampleWebApiAspNetCore.Repositories
{
    public class WebAppDataRepository : IWebAppDataRepository
    {
        private readonly WebAppDbContext _webAppDbContext;

        public WebAppDataRepository(IConfiguration conf)
        {
            _webAppDbContext = new WebAppDbContext(conf);
        }
        public WebAppDataEntity Get(string guid)
        {
            var result = _webAppDbContext.Get(guid);
            return result;
        }

        public void Set(WebAppDataEntity item)
        {
            _webAppDbContext.Set(item.guid, DateTime.UtcNow, item.userData);
        }

    

    }
}
