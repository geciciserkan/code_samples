using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.Repositories
{
    public interface IWebAppDataRepository
    {
        WebAppDataEntity Get(string guid);
        void Set(WebAppDataEntity item);
    }
}
