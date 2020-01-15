using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Test
{
    public class Tests
    {
        private readonly IWebAppDataRepository webAppDataRepository;

        public  Tests(IConfiguration configuration)
        {
            webAppDataRepository = new WebAppDataRepository(configuration);
        }




        [SetUp]
        public void Setup()
        {
            var mockConfiguration = new Mock<IConfiguration>();
                       
        }

        [Test]
        public void Test1()
        {
            var result = webAppDataRepository.Get("06dcb55b-adc3-4131-9662-f450f3f9318e");
            Assert.IsTrue(result!= null,"OK");
        }


        [Test]
        public void Test2()
        {
            WebAppDataEntity input =new WebAppDataEntity();
            input.guid = "06dcb55b-adc3-4131-9662-f450f3f9318e";
            input.lastModified = DateTime.Now;
            var temp = new Dictionary<string, object>();
            temp.Add("key1", 100);
            temp.Add("key2", 120);

            try
            {
                webAppDataRepository.Set(input);
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


        }




    }
}