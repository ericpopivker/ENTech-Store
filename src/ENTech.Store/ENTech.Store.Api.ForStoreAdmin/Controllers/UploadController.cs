using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ENTech.Store.Services.CommandService.Definition;
using ENTech.Store.Services.Misc;

using ENTech.Store.Services.UploadModule.Requests;
using ENTech.Store.Services.UploadModule.Responses;
using ENTech.Store.Services.UploadModule.Commands;



namespace ENTech.Store.Api.ForStoreAdmin.Controllers
{
    [RoutePrefix("1.0/store-admin-api/uploads")]
    public class UploadController : ApiController
    {

        //POST uploads - create
        //PUT uploads/{id} 

        private readonly IExternalCommandService<BusinessAdminSecurityInformation> _businessAdminExternalCommandService;

        [HttpPost]
        [Route("1.0/uploads")]
        [ResponseType(typeof(UploadCreateResponse))]
        public async Task<UploadCreateResponse> Create(string role)
        {
            var createUploadRequest = new UploadCreateRequest(role);
            var result = _businessAdminExternalCommandService.Execute<UploadCreateRequest, UploadCreateResponse, UploadCreateCommand>(createUploadRequest);
            return result;
        }

        [HttpPut]
        [Route("1.0/uploads/{id}")]
        [ResponseType(typeof(UploadUpdateResponse))]
        public async Task PutFile(int id)
        {
            using (var s = await this.Request.Content.ReadAsStreamAsync())
            {
                var saveUploadResponse = new UploadUpdateRequest
                {
                    Id = id,
                    Stream = s,
                    Extension = ".txt"
                };

                _businessAdminExternalCommandService.Execute<UploadUpdateRequest, UploadUpdateResponse, UploadUpdateCommand>(saveUploadResponse);
            }
        }

        [HttpGet]
        [Route("1.0//uploads/{id}")]
        [ResponseType(typeof(UploadGetByIdResponse))]
        public async Task<object> GetById(int id)
        {
            var getUploadbyIdRequest = new UploadGetByIdRequest
            {
                Id = id
            };

            var upload = _businessAdminExternalCommandService.Execute<UploadGetByIdRequest, UploadGetByIdResponse, UploadGetByIdCommand>(getUploadbyIdRequest);
            return upload;
        }
    }
}
