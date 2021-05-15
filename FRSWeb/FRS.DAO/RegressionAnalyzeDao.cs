using FRS.DAL.Interfaces;
using FRS.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FRS.DAO
{
    public class RegressionAnalyzeDao : BaseDao, IRegressionAnalyzeDao
    {
        public int AddNewRegressionData(RegressionData data)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.AddRegressionData");
                AddParameter(GetParameter("@RegressionOwnerId", data.UserId, DbType.Int32), command);
                AddParameter(GetParameter("@ACoeff", data.ACoefficientForX, DbType.Decimal), command);
                AddParameter(GetParameter("@BCoeff", data.BFreeCoefficient, DbType.Decimal), command);
                connection.Open();
                var dataId = command.ExecuteScalar();
                return (int)dataId;
            }
        }

        public RegressionData GetRegressionDataById(int dataId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.GetRegressionDataById");
                AddParameter(GetParameter("@DataId", dataId, DbType.Int32), command);
                connection.Open();
                RegressionData regressionData = null;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    regressionData = new RegressionData()
                    {
                        ACoefficientForX = (double)reader["ACoeff"],
                        BFreeCoefficient = (double)reader["BCoeff"],
                        UserId = (int)reader["RegressionOwnerId"],
                        RegressionDataId = (int)reader["Id"],
                        PrecisionError = (int)reader["PrecisionError"]
                    };
                }
                return regressionData;
            }
        }

        public IEnumerable<RegressionData> GetRegressionDataByUserId(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.GetRegressionDataByUserId");
                AddParameter(GetParameter("@UserId", userId, DbType.Int32), command);
                connection.Open();
                List<RegressionData> regressionDataList = null;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    regressionDataList.Add(new RegressionData()
                    {
                        ACoefficientForX = (double)reader["ACoeff"],
                        BFreeCoefficient = (double)reader["BCoeff"],
                        UserId = (int)reader["RegressionOwnerId"],
                        RegressionDataId = (int)reader["Id"],
                        PrecisionError = (int)reader["PrecisionError"]
                    });
                }
                return regressionDataList;
            }
        }

        public RegressionData GetRegressionFromFile(int dataId)
        {
            byte[] fileByteArray = null;
            string fileName = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.GetRegressionDataFile");
                AddParameter(GetParameter("@RegressionDataId", dataId, DbType.Int32), command);
                connection.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fileByteArray = (byte[])reader["FileData"];
                    fileName = reader["FileName"] as string;
                }
                File.WriteAllBytes(fileName, fileByteArray);
            }

            RegressionData regressionData = null;
            regressionData.RegressionDataId = dataId;
            var xRef = regressionData.XArgumentsFromRegressionAnalyze = new List<double>();
            var yRef = regressionData.YArgumentsFromRegressionAnalyze = new List<double>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string row = sr.ReadLine();
                    string[] data = row.Split(' ');
                    xRef.Add(double.Parse(data[0]));
                    yRef.Add(double.Parse(data[1]));
                }
            }
            return regressionData;
        }

        public void UpdateRegressionData(RegressionData data) // +
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.UpdateRegressionData");
                AddParameter(GetParameter("@DataId", data.RegressionDataId, DbType.Int32), command);
                AddParameter(GetParameter("@ACoeff", data.ACoefficientForX, DbType.Decimal), command);
                AddParameter(GetParameter("@BCoeff", data.BFreeCoefficient, DbType.Decimal), command);
                AddParameter(GetParameter("@PrecisionError", data.PrecisionError, DbType.Decimal), command);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UploadRegressionInFile(FileInfo fileInfo, int dataId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(connection, "dbo.UploadRegressionFile");
                AddParameter(GetParameter("@RegressionDataId", dataId, DbType.Int32), command);
                AddParameter(GetParameter("@FileName", fileInfo.Name, DbType.String), command);
                byte[] fileByte = File.ReadAllBytes(fileInfo.Name);
                AddParameter(GetParameter("@FileData", fileInfo, DbType.Binary), command);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}