using FRSWebApp.Models.Regression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using FRSWebApp.App_Start;
using FRS.Entities;
using FRS.BLL.Interfaces;

namespace FRSWebApp.Controllers.RestAPI
{
    public class RestRegressionController : ApiController
    {
        private IUserService _userService;
        private IRegressionAnalyzeService _regressionAnalyzeService;
        public RestRegressionController(IUserService userService, IRegressionAnalyzeService regressionAnalyzeService)
        {
            _userService = userService;
            _regressionAnalyzeService = regressionAnalyzeService;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/CreateNewRegression")]
        public IHttpActionResult Create(FRSClient frsClient)
        {
            if (string.IsNullOrWhiteSpace(frsClient.AccessKey) ||
                string.IsNullOrWhiteSpace(frsClient.SecretAccessKey) ||
                string.IsNullOrEmpty(frsClient.AccessKey) ||
                string.IsNullOrEmpty(frsClient.SecretAccessKey))
                return BadRequest();

            var userId = _userService.ApiAuth(frsClient.AccessKey, frsClient.SecretAccessKey);
            if (userId > 0)
            {
                _regressionAnalyzeService.AddNewRegressionData(frsClient.regressionData);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/GetRegressionModels")]
        public IHttpActionResult GetRegressionModels(FRSClient frsClient)
        {
            if (string.IsNullOrWhiteSpace(frsClient.AccessKey) ||
                string.IsNullOrWhiteSpace(frsClient.SecretAccessKey) ||
                string.IsNullOrEmpty(frsClient.AccessKey) ||
                string.IsNullOrEmpty(frsClient.SecretAccessKey))
                return BadRequest();

            var userId = _userService.ApiAuth(frsClient.AccessKey, frsClient.SecretAccessKey);
            if (userId > 0)
            {
                return Ok(_regressionAnalyzeService.GetRegressionDataByUserId(userId));
            }
            else
            {
                return BadRequest();
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/CountForecast")]
        public IHttpActionResult CountForecast(FRSClient fRSClient)
        {
            if (string.IsNullOrWhiteSpace(fRSClient.AccessKey) ||
                string.IsNullOrWhiteSpace(fRSClient.SecretAccessKey) ||
                string.IsNullOrEmpty(fRSClient.AccessKey) ||
                string.IsNullOrEmpty(fRSClient.SecretAccessKey))
                return BadRequest();

            var userId = _userService.ApiAuth(fRSClient.AccessKey, fRSClient.SecretAccessKey);
            if (userId > 0)
            {
                if (fRSClient.regressionData != null)
                {
                    var data = _regressionAnalyzeService.GetRegressionDataById(fRSClient.regressionData.RegressionDataId);

                    return Ok(_regressionAnalyzeService.CountForecast(fRSClient.X, data));
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("~/api/SaveToMyRegressionModel")]
        public IHttpActionResult SaveToMyRegressionModel(FRSClient frsClient)
        {
            if (string.IsNullOrWhiteSpace(frsClient.AccessKey) ||
                string.IsNullOrWhiteSpace(frsClient.SecretAccessKey) ||
                string.IsNullOrEmpty(frsClient.AccessKey) ||
                string.IsNullOrEmpty(frsClient.SecretAccessKey))
                return BadRequest();

            var userId = _userService.ApiAuth(frsClient.AccessKey, frsClient.SecretAccessKey);
            if (userId > 0)
            {
                if (frsClient.regressionData != null)
                {
                    try
                    {
                        _regressionAnalyzeService.UpdateRegressionData(frsClient.regressionData);
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}