using FRS.Entities;
using System.Collections.Generic;

namespace FRS.BLL.Interfaces
{
    public interface IRegressionAnalyzeService
    {
        RegressionModel CountForecast(double xArg, RegressionData data);

        RegressionModel CountMultipleForecast(List<double> xArgs, RegressionData data);

        IEnumerable<RegressionData> GetRegressionDataByUserId(int userId);

        RegressionData GetRegressionDataById(int dataId);

        int AddNewRegressionData(RegressionData data);

        void UpdateRegressionData(RegressionData data);

        RegressionData GetRegressionFromFile(int dataId);

        void UploadRegressionInFile(RegressionData data);

        RegressionData CalculateRegression(RegressionData data);
    }
}