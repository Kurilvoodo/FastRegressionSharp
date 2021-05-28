using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace FRSApiClient
{
    [Serializable]
    public class FRSClient
    {
        private string _baseUrl = "http://localhost:44385/api";
        public string AccessKey { get; set; }
        public string SecretAccessKey { get; set; }

        public RegressionData regressionData { get; set; }
        public RegressionModel regressionModel { get; set; }

        public double X { get; set; }

        private WebRequest GetWebRequestTmplate(string requestUrl)
        {
            WebRequest request = WebRequest.Create(requestUrl);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
            ser.WriteObject(request.GetRequestStream(), this);
            return request;
        }

        private FRSClient ParseResponse(WebRequest request)
        {
            var httpResponse = (HttpWebResponse)request.GetResponse();
            Stream newStream = httpResponse.GetResponseStream();
            StreamReader sr = new StreamReader(newStream);
            var result = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<FRSClient>(result);
        }

        public FRSClient GetMyModels()
        {
            string requestUrl = _baseUrl + "GetRegressionModels";

            var request = GetWebRequestTmplate(requestUrl);

            return ParseResponse(request);
        }

        public FRSClient CreateNewModel()
        {
            string requestUrl = _baseUrl + "CreateNewRegression";

            var request = GetWebRequestTmplate(requestUrl);

            return ParseResponse(request);
        }

        public FRSClient CountForecast()
        {
            string requestUrl = _baseUrl + "CountForecast";

            var request = GetWebRequestTmplate(requestUrl);

            return ParseResponse(request);
        }

        public FRSClient SaveToMyRegressionModel()
        {
            string requestUrl = _baseUrl + "SaveToMyRegressionModel";

            var request = GetWebRequestTmplate(requestUrl);

            return ParseResponse(request);
        }
    }
}