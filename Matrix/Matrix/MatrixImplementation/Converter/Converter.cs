using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineLearningModel.Matrix
{
    class Converter
    {
        public Matrix<float> DoubleToFloat(Matrix<double> input)
        {
            Matrix<float> output = new Matrix<float>(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    output[x, y] = (float)input[x, y];
                }
            }
            return output;
        }

        public Matrix<double> FloatToDouble(Matrix<float> input)
        {
            Matrix<double> output = new Matrix<double>(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    output[x, y] = Convert.ToDouble(input[x, y]);
                }
            }
            return output;
        }
    }
}
