using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Converter
{
    public class ConverterHelper<T, Q>
    {
        public enum ConversionType
        {
            DoubleToFloat,
            FloatToDouble,
            IntToFloat,
            FloatToInt,
            IntToDouble,
            DoubleToInt
        }

        public static  Matrix<Q> Convert(Matrix<T> input)
        {
            Matrix<Q> output = new Matrix<Q>(input.Width, input.Height);
            ConversionType conversion = TypeCheck();
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    if (conversion == ConversionType.DoubleToFloat ||
                        conversion == ConversionType.IntToFloat ||
                        conversion == ConversionType.FloatToInt ||
                        conversion == ConversionType.DoubleToInt) output[x, y] = (Q)(dynamic)input[x, y];
                    else if (conversion == ConversionType.FloatToDouble ||
                             conversion == ConversionType.IntToDouble) output[x, y] = System.Convert.ToDouble((dynamic)input[x, y]);
                }
            }
            return output;
        }
        private static ConversionType TypeCheck()
        {
            if (typeof(T) == typeof(double) && typeof(Q) == typeof(float)) return ConversionType.DoubleToFloat;
            if (typeof(T) == typeof(float) && typeof(Q) == typeof(double)) return ConversionType.FloatToDouble;
            if (typeof(T) == typeof(int) && typeof(Q) == typeof(float)) return ConversionType.IntToDouble;
            if (typeof(T) == typeof(float) && typeof(Q) == typeof(int)) return ConversionType.FloatToInt;
            if (typeof(T) == typeof(int) && typeof(Q) == typeof(double)) return ConversionType.IntToDouble;
            if (typeof(T) == typeof(double) && typeof(Q) == typeof(int)) return ConversionType.DoubleToInt;
            else throw new Exception("Your are using an unsupported type!");
        }
    }
}
