using FRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.BLL.Interfaces
{
    public interface IRegressionAnalyzeService
    {
        RegressionModel CountForecast(double xArg, RegressionData data);
        RegressionModel CountMultipleForecast(List<double> xArgs, RegressionData data);
        RegressionData GetRegressionDataByUserId(int userId);
        RegressionData GetRegressionDataById(int dataId);
        int AddNewRegressionData(RegressionData data);
        void UpdateRegressionData(RegressionData data);
    }
}
