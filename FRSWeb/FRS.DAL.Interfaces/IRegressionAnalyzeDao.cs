using FRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.DAL.Interfaces
{
    public interface IRegressionAnalyzeDao
    {
        RegressionData GetRegressionDataByUserId(int userId);
        RegressionData GetRegressionDataById(int dataId);
        int AddNewRegressionData(RegressionData data);
        void UpdateRegressionData(RegressionData data);
    }
}
