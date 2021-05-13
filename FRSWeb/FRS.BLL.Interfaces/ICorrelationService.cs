
using FRS.Entities;
using System.Collections.Generic;

namespace FRS.BLL.Interfaces
{
    public interface ICorrelationService
    {
        CorrellationCoefficient CountCorrelationCoefficient(List<double> x, List<double> y);
    }
}
