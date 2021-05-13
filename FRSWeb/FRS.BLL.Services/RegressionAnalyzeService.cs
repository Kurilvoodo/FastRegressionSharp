using FRS.BLL.Interfaces;
using FRS.DAL.Interfaces;
using FRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.BLL.Services
{
    public class RegressionAnalyzeService : IRegressionAnalyzeService
    {
        private IRegressionAnalyzeDao _regressionAnalyzeDao;
        public RegressionAnalyzeService(IRegressionAnalyzeDao regressionAnalyzeDao)
        {
            _regressionAnalyzeDao = regressionAnalyzeDao;
        }
        public int AddNewRegressionData(RegressionData data)
        {
            return _regressionAnalyzeDao.AddNewRegressionData(data);
        }

        public RegressionModel CountForecast(double xArg, RegressionData data)
        {
            throw new NotImplementedException();
        }

        public RegressionModel CountMultipleForecast(List<double> xArgs, RegressionData data)
        {
            throw new NotImplementedException();
        }

        public RegressionData GetRegressionDataById(int dataId)
        {
            return _regressionAnalyzeDao.GetRegressionDataById(dataId);
        }

        public RegressionData GetRegressionDataByUserId(int userId)
        {
            return _regressionAnalyzeDao.GetRegressionDataByUserId(userId);
        }

        public void UpdateRegressionData(RegressionData data)
        {
            _regressionAnalyzeDao.UpdateRegressionData(data);
        }
    }
}
