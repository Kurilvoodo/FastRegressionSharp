using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRS.Entities
{
    public enum FunctionType
    {
        SimpleLine = 0, //Line
        Exponential = 1, //e
        Logarithmic = 2, // ln(x)
        Sedate = 3 // x^a
    }
}
