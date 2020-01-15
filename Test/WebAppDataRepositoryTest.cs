using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;
using SampleWebApiAspNetCore.v2.Controllers;
using System;
using System.Collections.Generic;

namespace Test
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

    public class WebAppDataRepositoryTest
    {

        WebappdataController _controller;
        IWebAppDataRepository _myInterface;
        IConfiguration configuration;


        [SetUp]
        public void Setup()
        {
            var _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "ConnectionStrings:DefaultConnection")]).Returns("data source=mssql11.turhost.com;initial catalog=AllSocialP;user id=AllSocial_temp;password=AllSocial27!!;MultipleActiveResultSets=True;");
            configuration = _configuration.Object;
            _myInterface = new WebAppDataRepository(configuration);
            _controller = new WebappdataController(_myInterface);
        }

        [Test]
        public void TestGetData()
        {
            var result = _controller.GetData("06dcb55b-adc3-4131-9662-f450f3f9318e");
            Assert.IsTrue(result != null);
        }


        [Test]
        public void TestSetData()
        {
            WebAppDataEntity mockObj = new WebAppDataEntity();
            mockObj.guid = "06dcb55b-adc3-4131-9662-f450f3f9318e";
            mockObj.lastModified = DateTime.Now;

            Dictionary<string, object> userData = new Dictionary<string, object>();
            userData.Add("key1", 100);
            userData.Add("key2", 200);
            userData.Add("key3", 300);

            mockObj.userData = userData;

            var result = _controller.SetData(mockObj, mockObj.guid);
            Assert.IsTrue(result!=null);
        }



    }
}