using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENTech.Store.Api.v1;
using System.IO;
using ENTech.Store.Api.BL;
using ENTech.Store.Api.DAL;

namespace ENTech.Store.Api.Tests
{
    [TestClass]
    public class UploadTests
    {
        [TestMethod]
        public void Upload_file_before_save()
        {
            MemoryRepository.Clear();

            var productService = new ProductFacade();
            var dispatcher = new UploadEventDispatcher();
            var storage = new LocalFileStorage();
            UploadSubscriber.SubscribeAllHandlers(dispatcher);
            
            var userId = "1";
            var uploadService = new UploadFacade(userId, storage, dispatcher);

            var uploadId = Guid.NewGuid().ToString();
            var upload = uploadService.CreateUpload(uploadId);

            var product = new DAL.Product()
            {
                Id="1",
                Title = "Test Title",
                LogoUploadId = upload.Id
            };

            uploadService.Save(upload.Id, ".txt", FileLimits.M1 , new MemoryStream()).Wait();
            
            productService.Create(product);
            uploadService.Attach(upload.Id, "Product", "Logo", "1");

            var p1 = productService.GetById("1");
            Assert.IsNotNull(p1.LogoUrl);
        }


        [TestMethod]
        public void Upload_file_after_save()
        {
            MemoryRepository.Clear();

            var productService = new ProductFacade();
            var dispatcher = new UploadEventDispatcher();
            var storage = new LocalFileStorage();
            UploadSubscriber.SubscribeAllHandlers(dispatcher);

            var userId = "1";
            var uploadService = new UploadFacade(userId, storage, dispatcher);

            var uploadId = Guid.NewGuid().ToString();
            var upload = uploadService.CreateUpload(uploadId);

            var product = new DAL.Product()
            {
                Id = "1",
                Title = "Test Title",
                LogoUploadId = upload.Id
            };

            productService.Create(product);
            
            uploadService.Attach(upload.Id, "Product", "Logo", "1");
            uploadService.Save(upload.Id, ".txt", FileLimits.M1, new MemoryStream()).Wait();


            var p1 = productService.GetById("1");
            Assert.IsNotNull(p1.LogoUrl);
        }


    }
    
}