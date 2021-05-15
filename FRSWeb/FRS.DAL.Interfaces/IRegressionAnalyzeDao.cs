using FRS.Entities;
using System.Collections.Generic;
using System.IO;

namespace FRS.DAL.Interfaces
{
    public interface IRegressionAnalyzeDao
    {
        IEnumerable<RegressionData> GetRegressionDataByUserId(int userId);

        RegressionData GetRegressionDataById(int dataId);

        int AddNewRegressionData(RegressionData data);

        void UpdateRegressionData(RegressionData data);

        RegressionData GetRegressionFromFile(int dataId);

        void UploadRegressionInFile(FileInfo fileInfo, int dataId);
    }
}