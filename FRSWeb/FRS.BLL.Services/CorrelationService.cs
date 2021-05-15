using FRS.BLL.Interfaces;
using FRS.Entities;
using System;
using System.Collections.Generic;

namespace FRS.BLL.Services
{
    public class CorrelationService : ICorrelationService
    {
        public CorrellationCoefficient CountCorrelationCoefficient(List<double> x, List<double> y)
        {
            double xSampleMean = SampleMeanCount(x);
            double ySampleMean = SampleMeanCount(y);
            double upperNumerator = 0;
            double xSampleVariance = 0;
            double ySampleVariance = 0;
            int CountOfDigitsInData = x.Count; // like n

            for (int i = 0; i < CountOfDigitsInData; i++)
            {
                upperNumerator += (x[i] - xSampleMean) * (y[i] - ySampleMean);
                xSampleVariance += Math.Pow((x[i] - xSampleMean), 2);
                ySampleVariance += Math.Pow((y[i] - ySampleMean), 2);
            }
            upperNumerator /= CountOfDigitsInData;
            xSampleVariance /= CountOfDigitsInData;
            ySampleVariance /= CountOfDigitsInData;

            double r = upperNumerator / (Math.Sqrt(xSampleVariance) - Math.Sqrt(ySampleVariance));

            return new CorrellationCoefficient()
            {
                R = r
            };
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
    }
}