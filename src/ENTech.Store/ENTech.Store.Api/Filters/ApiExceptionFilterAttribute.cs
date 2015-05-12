//using System;
//using System.Net;
//using System.Net.Http;
//using System.Web;
//using System.Web.Http.Filters;

//namespace ENTech.Store.Api.Filters
//{
//       public class ApiExceptionFilterAttribute  : ExceptionFilterAttribute
//    {
//        public override void OnException(HttpActionExecutedContext context)
//        {
//            var e = context.Exception;
//            var b = e as BusinessException;

//            if (e is BusinessException)
//            {
//                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error");
//                var r = HttpContext.Current.Response;
//                r.StatusCode = b.HttpCode;
//                r.ClearContent();
//                var obj = new
//                {
//                    Message = b.Message
//                };

//                var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
//                r.Write(json);
//                r.Headers.Add("Access-Control-Allow-Origin", "*");
//                r.End();
//            }
//            else
//                if (e is NotImplementedException)
//                {
//                    context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
//                }
//                else
//                {
//                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Server Error");
//                }
//        }
//}