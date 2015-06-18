using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ENTech.Store.Api.v1;
using System.IO;

namespace ENTech.Store.Api.Tests
{
    [TestClass]
    public class UploadTests
    {
        [TestMethod]
        public void Upload_file_before_save()
        {
            var productService = new ProductService();
            var publisher = new UploadEventDispatcher();
            publisher.ProductService = productService;
            var uploadService= new UploadService(publisher);

            var upload = uploadService.CreateUpload();

            var product = new DAL.Product()
            {
                Id="1",
                Title = "Test Title",
                LogoUploadId = upload.Id
            };

            uploadService.Save(upload.Id, "test.txt", 0, new MemoryStream());
            
            productService.Create(product);
            uploadService.Attach(upload.Id, "Product", "1");

            var p1 = productService.GetById("1");
            Assert.IsNotNull(p1.LogoUrl);
        }


        [TestMethod]
        public void Upload_file_after_save()
        {
            var productService = new ProductService();
            var publisher = new UploadEventDispatcher();
            publisher.ProductService = productService;
            var uploadService = new UploadService(publisher);

            var upload = uploadService.CreateUpload();

            var product = new DAL.Product()
            {
                Id = "1",
                Title = "Test Title",
                LogoUploadId = upload.Id
            };

            productService.Create(product);
            
            uploadService.Attach(upload.Id, "Product", "1");
            uploadService.Save(upload.Id, "test.txt", 0, new MemoryStream());


            var p1 = productService.GetById("1");
            Assert.IsNotNull(p1.LogoUrl);
        }


    }
    
}