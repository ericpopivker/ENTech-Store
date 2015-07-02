using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENTech.Store.Api.DAL;

namespace ENTech.Store.Api.BL
{

    public class ProductFacade : IUploadedEventHandler
    {
        private readonly ProductRepository _repository = new ProductRepository();

        public void Create(Product product)
        {
            _repository.Create(product.Id, product);
        }

        public Product GetById(string id)
        {
            return _repository.GetById(id);
        }

        public void Update(Product product)
        {
            _repository.Update(product.Id, product);
        }

        public void OnUploaded(string uploadId, string entityId, string fieldName, string url)
        {
            if (fieldName.ToLower() == "logo")
            {
                var p = GetById(entityId);

                if (p.LogoUploadId == uploadId)
                {
                    p.LogoUrl = url;
                    p.LogoUploadId = "";
                    Update(p);
                }//else - the upload is not actual (was canceled before)
            }

        }
    }


}