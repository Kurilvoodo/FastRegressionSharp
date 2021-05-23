﻿using FRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FRSWebApp.Models.Regression
{
    public class ForeCastModel
    {
        public double ACoefficientForX { get; set; }
        public double BFreeCoefficient { get; set; }
        public double XRegressiontCounting { get; set; }
        public double YRegressionResult { get; set; }
        public List<double> MultipleRegressionAnswers { get; set; }
        public int UserId { get; set; }
        public int RegressionDataId { get; set; }
        public CorrellationCoefficient correllation { get; set; }
        
    }
}