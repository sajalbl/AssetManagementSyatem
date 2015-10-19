using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Konnect.API.Interceptors
{
    public class APIInterceptor : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var req = request.Content.ReadAsStringAsync();
            //var dt = EncryptionHelper.DecryptString(req.Result);
            //request.Content = new StringContent(dt); 
            //request.Content.Headers.ContentType = new MediaTypeHeaderValue(@"application/json");
            //// var d1 = request.Content.ReadAsStringAsync();
            //return base.SendAsync(request, cancellationToken);

            // for API key
            //string apikey = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("apikey");
            //var apikey = request.Content.Headers.GetValues("appToken");
            //if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            //{
            //    var response = request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid call. Please use https.");
            //    throw new HttpResponseException(response);
            //}
            //var apikey = GetFirstHeaderValueOrDefault(request, "appToken");
            //if (string.IsNullOrEmpty(apikey))
            //{
            //    HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.Forbidden, "You can't use the API without the key.");
            //    throw new HttpResponseException(response);
            //}
            //else
            //{
            //    //var k = EncryptionHelper.EncryptString("ABCD");
            //    var dt = EncryptionHelper.DecryptString(apikey);
            //}
            return base.SendAsync(request, cancellationToken);
        }



        private string GetFirstHeaderValueOrDefault(HttpRequestMessage request, string headerKey)
        {
            IEnumerable<string> headerValues;
            HttpRequestMessage message = request ?? new HttpRequestMessage();
           // var a = ((string[])(message.Headers.GetValues("appToken")));
            if (message.Headers.TryGetValues(headerKey, out headerValues))
                return headerValues.FirstOrDefault();
            return string.Empty;
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
    public class ActionOfStreamContent : HttpContent
    {
        private readonly Action<Stream> _actionOfStream;

        public ActionOfStreamContent(Action<Stream> actionOfStream)
        {
            if (actionOfStream == null)
            {
                throw new ArgumentNullException("actionOfStream");
            }

            _actionOfStream = actionOfStream;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Task.Factory.StartNew(
                (obj) =>
                {
                    Stream target = obj as Stream;
                    _actionOfStream(target);
                },
                stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            // We can't know how much the Action<Stream> is going to write
            length = -1;
            return false;
        }
    }
}