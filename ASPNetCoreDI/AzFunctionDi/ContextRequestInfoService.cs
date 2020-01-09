using DemoApp.Services.RequestInfoService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Services
{
    public class ContextRequestInfoService:IRequestInfoService 
    {

        private IHttpContextAccessor _httpContextAccessor;

        public ContextRequestInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string IpAddress => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(); 
    }
}
