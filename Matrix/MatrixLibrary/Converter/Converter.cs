using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matrix;

namespace Matrix.Converter
{
    public class Converter
    {
        public Matrix<float> DoubleToFloat(Matrix<double> input)
        {
            return ConverterHelper<double, float>.Convert(input);
        }

        public Matrix<double> FloatToDouble(Matrix<float> input)
        {
            return ConverterHelper<float, double>.Convert(input);
        }

        public Matrix<float> IntToFloat(Matrix<int> input)
        {
            return ConverterHelper<int, float>.Convert(input);
        }

        public Matrix<int> FloatToInt(Matrix<float> input)
        {
            return ConverterHelper<float, int>.Convert(input);
        }

        public Matrix<double> IntToDouble(Matrix<int> input)
        {
            return ConverterHelper<int, double>.Convert(input);
        }
        
        public Matrix<int> DoubleToInt(Matrix<double> input)
        {
            return ConverterHelper<double, int>.Convert(input);
        }
    }
}
