using FRS.BLL.Interfaces;
using FRS.Entities;
using FRSWebApp.Models.Regression;
using System;
using System.Collections.Generic;
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
                regressionData.FunctionType = FunctionType.SimpleLine;
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
            var list = _regressionService.GetRegressionDataByUserId(userId);
            return View(list);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CountForecast(int regressionModelId)
        {
            var model = _regressionService.GetRegressionDataById(regressionModelId);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CountForecast(RegressionData regressionData)
        {
            var answerModel = _regressionService.CountForecast(double.Parse(regressionData.X), regressionData);

            regressionData.YResult = answerModel.YRegressionResult;
            TempData["answerModel"] = regressionData;
            return RedirectToAction("CountedForecast", "Regression", TempData);
        }

        [Authorize]
        public ActionResult CountedForecast()
        {
            var regressionModel = (RegressionData)TempData["answerModel"];
            return View(regressionModel);
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

        #region QuickAnonyuos

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

        [AllowAnonymous]
        public ActionResult QuickExponentialRegression()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult QuickExponentialRegressionAnswer(QuickRegression quickRegression)
        {
            quickRegression.FunctionType = FunctionType.Exponential;
            List<double> xData = new List<double>();
            List<double> yData = new List<double>();
            foreach (var item in quickRegression.XArgs.Split(' '))
            {
                xData.Add(double.Parse(item));
            }
            foreach (var item in quickRegression.YArgs.Split(' '))
            {
                yData.Add(Math.Log(double.Parse(item)));
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

        [AllowAnonymous]
        public ActionResult QuickLogarithmicRegression()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult QuickLogarithmicRegressionAnswer(QuickRegression quickRegression)
        {
            quickRegression.FunctionType = FunctionType.Logarithmic;
            List<double> xData = new List<double>();
            List<double> yData = new List<double>();
            foreach (var item in quickRegression.XArgs.Split(' '))
            {
                xData.Add(Math.Log(double.Parse(item)));
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

        [AllowAnonymous]
        public ActionResult QuickSedateRegression()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult QuickSedateRegressionAnswer(QuickRegression quickRegression)
        {
            quickRegression.FunctionType = FunctionType.Sedate;
            List<double> xData = new List<double>();
            List<double> yData = new List<double>();
            foreach (var item in quickRegression.XArgs.Split(' '))
            {
                xData.Add(Math.Log(double.Parse(item)));
            }
            foreach (var item in quickRegression.YArgs.Split(' '))
            {
                yData.Add(Math.Log(double.Parse(item)));
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

        #endregion QuickAnonyuos
    }
}