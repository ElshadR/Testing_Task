using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.RequestLibrary.Core
{
    public class RequestSender
    {
        private const string _BaseUrl = "https://localhost:44339/";

        private readonly RestClient _client = null;
        public RequestSender()
        {
            _client = new RestClient(_BaseUrl);
        }

        public async Task<RequestResult<T>> GetResponse<T>(string url, Action<IRestRequest> action = null) where T : new()
        {
            try
            {
                var request = new RestRequest(url, Method.GET);

                action?.Invoke(request);

                var response = _client.Execute<T>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new RequestResult<T>
                    {
                        IsSucced = true,
                        Data = response.Data
                    };
                else
                    return new RequestResult<T>
                    {
                        IsSucced = false,
                        ErrorMessage = response.ErrorMessage
                    };
            }
            catch (Exception ex)
            {
                return new RequestResult<T>
                {
                    IsSucced = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<RequestResult<T>> PostResponse<T>(string url, Action<IRestRequest> action = null) where T : new()
        {
            try
            {
                var request = new RestRequest(url, Method.POST);

                action?.Invoke(request);

                var response = _client.Execute<T>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new RequestResult<T>
                    {
                        IsSucced = true,
                        Data = response.Data
                    };
                else
                    return new RequestResult<T>
                    {
                        IsSucced = false,
                        ErrorMessage = response.ErrorMessage
                    };
            }
            catch (Exception ex)
            {
                return new RequestResult<T>
                {
                    IsSucced = false,
                    ErrorMessage = ex.Message
                };
            }
        } 
        public async Task<RequestResult<T>> PutResponse<T>(string url, Action<IRestRequest> action = null) where T : new()
        {
            try
            {
                var request = new RestRequest(url, Method.PUT);

                action?.Invoke(request);

                var response = _client.Execute<T>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return new RequestResult<T>
                    {
                        IsSucced = true,
                        Data = response.Data
                    };
                else
                    return new RequestResult<T>
                    {
                        IsSucced = false,
                        ErrorMessage = response.ErrorMessage
                    };
            }
            catch (Exception ex)
            {
                return new RequestResult<T>
                {
                    IsSucced = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
