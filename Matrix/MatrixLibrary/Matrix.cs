using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{
    public class Matrix<T>
    {
        private T[,] matrix;
        public static readonly Type[] supportedTypes = new Type[] { typeof(double), typeof(int), typeof(float) };
        public enum Dimensions
        {
            Row,
            Column
        }
        public Matrix(int x, int y)
        {
            matrix = new T[x, y];
            typeCheck();
        }

        private void typeCheck()
        {
            Type typeOfT = typeof(T);
            if (!supportedTypes.Contains(typeOfT)) throw new Exception(typeOfT.ToString() + " is not a supported type! Check Matrix.supportedTypes for all the supported types.");
        }

        public T this[int x, int y]
        {
            get => matrix[x, y];
            set => matrix[x, y] = value;
        }

        public int Width { get => matrix.GetLength(0); }

        public int Height { get => matrix.GetLength(1); }

        public bool HasSameSizeAs(Matrix<T> b)
        {
            if (this.Width == b.Width && this.Height == b.Height) return true;
            else return false;
        }

        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
        {
            if (!a.HasSameSizeAs(b)) throw new Exception("The dimensions must be the same!");
            Matrix<T> newMatrix = new Matrix<T>(a.Width, a.Height);
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    newMatrix[x, y] = (dynamic)a[x, y] + b[x, y];
                }
            }
            return newMatrix;
        }

        public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
        {
            if (!a.HasSameSizeAs(b)) throw new Exception("The dimensions must be the same!");
            Matrix<T> newMatrix = new Matrix<T>(a.Width, a.Height);
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    newMatrix[x, y] = (dynamic)a[x, y] - b[x, y];
                }
            }
            return newMatrix;
        }

        public static Matrix<T> operator *(int a, Matrix<T> b)
        {
            Matrix<T> newMatrix = new Matrix<T>(b.Width, b.Height);
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    newMatrix[x, y] = a * (dynamic)b[x, y];
                }
            }
            return newMatrix;
        }

        public static Matrix<T> operator *(Matrix<T> a, int b)
        {
            return b * a;
        }

        public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
        {
            if (a.Width != b.Height)
            {
                if (a.Height == b.Width)
                {
                    Matrix<T> helper = a;
                    a = b;
                    b = helper;
                }
                else throw new Exception("The number of columns in the first matrix must be equal to the number of rows in the second matrix");
            }
            Matrix<T> newMatrix = new Matrix<T>(b.Width, a.Height);
            for (int ay = 0; ay < a.Height; ay++)
            {
                for (int bx = 0; bx < b.Width; bx++)
                {
                    for (int axby = 0; axby < a.Width; axby++)
                    {
                        newMatrix[bx, ay] += (dynamic)a[axby, ay] * b[bx, axby];
                    }
                }
            }
            return newMatrix;
        }

        public T[] MatrixToArray(Dimensions dimension)
        {
            if (dimension == Dimensions.Row)
            {
                if (Height != 1) throw new Exception("In case of row to array, the height of the matrix must be 1!");
                return RowToArray();
            }
            else
            {
                if (Width != 1) throw new Exception("In case of column to array, the width of the matrix must be 1!");
                return ColumnToArray();
            }
        }

        private T[] RowToArray()
        {
            T[] retval = new T[this.Width];
            for (int x = 0; x < this.Width; x++)
            {
                retval[x] = this[x, 0];
            }
            return retval;
        }

        private T[] ColumnToArray()
        {
            T[] retval = new T[this.Height];
            for (int y = 0; y < this.Height; y++)
            {
                retval[y] = this[0, y];
            }
            return retval;
        }

        public static Matrix<T> ArrayToMatrix(Dimensions dimension, T[] array)
        {
            Matrix<T> newMatrix;
            if (dimension == Dimensions.Row)
            {
                newMatrix = new Matrix<T>(array.Length, 1);
                for (int i = 0; i < array.Length; i++)
                {
                    newMatrix[i, 0] = array[i];
                }
            }
            else
            {
                newMatrix = new Matrix<T>(1, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    newMatrix[0, i] = array[i];
                }
            }
            return newMatrix;
        }

        public Matrix<T> ApplyFunctoinForAllEntries(Func<T, T> function)
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this[x, y] = function(this[x, y]);
                }
            }
            return this;
        }

        public Matrix<T> AddRows()
        {
            Matrix<T> newMatrix = new Matrix<T>(this.Width, 1);
            for (int x = 0; x < this.Width; x++)
            {
                T sum = (dynamic)0;
                for (int y = 0; y < this.Height; y++)
                {
                    sum += (dynamic)this[x, y];
                }
                newMatrix[x, 0] = sum;
            }
            return newMatrix;
        }

        public Matrix<T> AddColumns()
        {
            Matrix<T> newMatrix = new Matrix<T>(1, this.Height);
            for (int y = 0; y < this.Height; y++)
            {
                T sum = (dynamic)0;
                for (int x = 0; x < this.Width; x++)
                {
                    sum += (dynamic)this[x, y];
                }
                newMatrix[0, y] = sum;
            }
            return newMatrix;
        }

        public Matrix<T> Power(double n)
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this[x, y] = (T)Math.Pow((dynamic)this[x, y], n);
                }
            }
            return this;
        }

        public override string ToString()
        {
            string retval = "[";
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width - 1; x++)
                {
                    retval += this[x, y] + ", ";
                }
                retval += this[this.Width - 1, y];
                if (y != this.Height - 1) retval += "\n ";
            }
            retval += "]";
            return retval;
        }
    }
}
