using FRS.BLL.Interfaces;
using FRS.DAL.Interfaces;
using FRS.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FRS.BLL.Services
{
    public class RegressionAnalyzeService : IRegressionAnalyzeService
    {
        private IRegressionAnalyzeDao _regressionAnalyzeDao;

        public RegressionAnalyzeService(IRegressionAnalyzeDao regressionAnalyzeDao)
        {
            _regressionAnalyzeDao = regressionAnalyzeDao;
        }

        /// <summary>
        /// Add new Regression data with counting coefficient
        /// </summary>
        /// <param name="data"> Regression data that contains both known args massives and can contains paramteres </param>
        /// <returns>Integeer id reference which was given to new Regression model in database</returns>
        public int AddNewRegressionData(RegressionData data)
        {
            var calculatedRegression = CalculateRegression(data);

            int dataId = _regressionAnalyzeDao.AddNewRegressionData(calculatedRegression);
            calculatedRegression.RegressionDataId = dataId;
            UploadRegressionInFile(calculatedRegression);
            return dataId;
        }

        private static double SampleMeanCount(List<double> args)
        {
            double countResult = 0;
            foreach (var digit in args)
            {
                countResult += digit;
            }
            return countResult / args.Count;
        }

        public RegressionModel CountForecast(double xArg, RegressionData data)
        {
            var result = xArg * data.ACoefficientForX + data.BFreeCoefficient;
            return new RegressionModel()
            {
                ACoefficientForX = data.ACoefficientForX,
                BFreeCoefficient = data.BFreeCoefficient,
                XRegressiontCounting = xArg,
                YRegressionResult = result,
                FunctionType = data.FunctionType
            };
        }

        public RegressionModel CountMultipleForecast(List<double> xArgs, RegressionData data)
        {
            RegressionModel regressionModel = new RegressionModel();
            regressionModel.MultipleRegressionAnswers = new List<double>();
            foreach (var arg in xArgs)
            {
                var result = arg * data.ACoefficientForX + data.BFreeCoefficient;
                regressionModel.MultipleRegressionAnswers.Add(result);
            }
            regressionModel.ACoefficientForX = data.ACoefficientForX;
            regressionModel.BFreeCoefficient = data.BFreeCoefficient;

            return regressionModel;
        }

        public RegressionData GetRegressionDataById(int dataId)
        {
            return _regressionAnalyzeDao.GetRegressionDataById(dataId);
        }

        public IEnumerable<RegressionData> GetRegressionDataByUserId(int userId)
        {
            return _regressionAnalyzeDao.GetRegressionDataByUserId(userId);
        }

        public void UpdateRegressionData(RegressionData data)
        {
            var updatedData = CalculateRegression(data);
            UploadRegressionInFile(updatedData);
            _regressionAnalyzeDao.UpdateRegressionData(updatedData);
        }

        public RegressionData GetRegressionFromFile(int dataId)
        {
            return _regressionAnalyzeDao.GetRegressionFromFile(dataId);
        }

        public void UploadRegressionInFile(RegressionData data)
        {
            FileInfo fileInfo = new FileInfo(string.Format($"DataId_{data.RegressionDataId}_UserOwner_{data.UserId}_{DateTime.UtcNow}.txt"));
            var xRef = data.XArgumentsFromRegressionAnalyze;
            var yRef = data.YArgumentsFromRegressionAnalyze;
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                for (int i = 0; i < xRef.Count; i++)
                {
                    byte[] info =
                        new UTF8Encoding(true).GetBytes(xRef[i].ToString() + " " + yRef[i].ToString() + Environment.NewLine);
                    fs.Write(info, 0, info.Length);
                }
            }
            _regressionAnalyzeDao.UploadRegressionInFile(fileInfo, data.RegressionDataId);
        }

        public RegressionData CalculateRegression(RegressionData data)
        {
            //Arrange SampleMean for y massive and y massive
            var ySampleMean = SampleMeanCount(data.YArgumentsFromRegressionAnalyze);
            var xSampleMean = SampleMeanCount(data.XArgumentsFromRegressionAnalyze);

            /// <param name="n"> Count of digits in massive</param>
            var n = data.XArgumentsFromRegressionAnalyze.Count;

            //Count and establish A and B coefficient due to Linazation formule

            #region Summ(X[i]*Y[i]) and Summ(SampleMean(X[0-n])^2)

            double summOfKnownArguments = 0;
            double summOfSquareSampleMeanOfX = 0;

            var xRef = data.XArgumentsFromRegressionAnalyze;
            var yRef = data.YArgumentsFromRegressionAnalyze;
            for (int i = 0; i < n; i++)
            {
                summOfKnownArguments += xRef[i] * yRef[i];             //due to optimization this operations
                summOfSquareSampleMeanOfX += Math.Pow(xRef[i], 2); //could be pocessed in the same cycle
            }

            #endregion Summ(X[i]*Y[i]) and Summ(SampleMean(X[0-n])^2)

            #region n * SampleMeaning(X[0-n]) * SmapleMeaning(Y[0-n])

            double multiplicationOfMeanSampleOfKnownArgs = n * xSampleMean * ySampleMean;

            #endregion n * SampleMeaning(X[0-n]) * SmapleMeaning(Y[0-n])

            #region n*(SampleMean(X[0-n])^2)

            double nMultiplicationOnSquareSampleMeanX = n * Math.Pow(xSampleMean, 2);

            #endregion n*(SampleMean(X[0-n])^2)

            #region A Coefficient in regression model

            var a = (summOfKnownArguments - multiplicationOfMeanSampleOfKnownArgs) / (summOfSquareSampleMeanOfX - nMultiplicationOnSquareSampleMeanX);

            #endregion A Coefficient in regression model

            #region B Coefficient in regression model

            var b = ySampleMean - a * xSampleMean;

            #endregion B Coefficient in regression model

            data.ACoefficientForX = a;
            data.BFreeCoefficient = b;

            return data;
        }
    }
}