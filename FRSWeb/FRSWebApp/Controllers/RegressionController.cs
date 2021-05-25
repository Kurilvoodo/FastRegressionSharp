using FRS.BLL.Interfaces;
using FRS.Entities;
using FRSWebApp.Models.Regression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FRSWebApp.Controllers
{
    public class RegressionController : Controller
    {
        public IRegressionAnalyzeService _regressionService;
        public ICorrelationService _correlationService;
        public RegressionController(IRegressionAnalyzeService regressionAnalyzeService, ICorrelationService correlationService)
        {
            _regressionService = regressionAnalyzeService;
            _correlationService = correlationService;
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRegressionVM createRegressionVM)
        {
            List<double> xData = new List<double>();
            List<double> yData = new List<double>();
            foreach (var item in createRegressionVM.XData.Split(' '))
            {
                xData.Add(double.Parse(item));
            }
            foreach (var item in createRegressionVM.YData.Split(' '))
            {
                yData.Add(double.Parse(item));
            }
            
            try
            {
                var correlactionCoefficient = _correlationService.CountCorrelationCoefficient(xData, yData);

                var regressionData = new RegressionData() { XArgumentsFromRegressionAnalyze = xData, YArgumentsFromRegressionAnalyze = yData };
                regressionData.UserId = createRegressionVM.userId;
                _regressionService.AddNewRegressionData(regressionData);
                return RedirectToAction("GetRegressionModels", "Regression", createRegressionVM.userId);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetRegressionModels(int userId)
        {
            return View(_regressionService.GetRegressionDataByUserId(userId));
        }

        [Authorize]
        public ActionResult CountForecast(int regressionModelId)
        {
            return View(_regressionService.GetRegressionDataById(regressionModelId));
        }
        [Authorize]
        public ActionResult CountedForecast(RegressionData regressionData, string xToForeCast)
        {
            var model = _regressionService.CountForecast(double.Parse(xToForeCast), regressionData);
            var correlactionCoefficient = _correlationService.CountCorrelationCoefficient(regressionData.XArgumentsFromRegressionAnalyze, regressionData.YArgumentsFromRegressionAnalyze);
            ForeCastModel foreCastModel = new ForeCastModel()
            {
                ACoefficientForX = model.ACoefficientForX,
                BFreeCoefficient = model.BFreeCoefficient,
                RegressionDataId = regressionData.RegressionDataId,
                XRegressiontCounting = model.XRegressiontCounting,
                YRegressionResult = model.YRegressionResult,
                UserId = regressionData.UserId,
                correllation = correlactionCoefficient
            };
            return View(foreCastModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SaveToMyRegressionModel(ForeCastModel foreCastModel)
        {
            var regresseionData = _regressionService.GetRegressionDataById(foreCastModel.RegressionDataId);
            regresseionData.XArgumentsFromRegressionAnalyze.Add(foreCastModel.XRegressiontCounting);
            regresseionData.YArgumentsFromRegressionAnalyze.Add(foreCastModel.YRegressionResult);
            _regressionService.UpdateRegressionData(regresseionData);
            return RedirectToAction("GetRegressionModels", "Regression", foreCastModel.UserId);
        }
        [AllowAnonymous]
        public ActionResult QuickLineRegression()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult QuickLineRegressionAnswer(QuickRegression quickRegression)
        {
            quickRegression.FunctionType = FunctionType.SimpleLine;
            List<double> xData = new List<double>();
            List<double> yData = new List<double>();
            foreach (var item in quickRegression.XArgs.Split(' '))
            {
                xData.Add(double.Parse(item));
            }
            foreach (var item in quickRegression.YArgs.Split(' '))
            {
                yData.Add(double.Parse(item));
            }

            var coefficient = _correlationService.CountCorrelationCoefficient(xData, yData);
            RegressionData regressionData = new RegressionData()
            {
                XArgumentsFromRegressionAnalyze = xData,
                YArgumentsFromRegressionAnalyze = yData
            };

            var regressionModel = _regressionService.CalculateRegression(regressionData);
            var modelWithAnswer = _regressionService.CountForecast(double.Parse(quickRegression.X), regressionModel);
            quickRegression.aAnswer = modelWithAnswer.ACoefficientForX;
            quickRegression.bAnswer = modelWithAnswer.BFreeCoefficient;
            quickRegression.yAnswer = modelWithAnswer.YRegressionResult;
            return View(quickRegression);
        }
    }
}