#nullable disable

using System;

namespace MatrixCalculator
{
    // Свое исключение
    class MatrixException : Exception
    {
        public MatrixException(string message)
            : base(message)
        {

        }
    }

    // Класс матрицы
    class Matrix : ICloneable, IComparable
    {
        private int[,] data;
        private int size;

        // Конструктор случайной матрицы
        public Matrix(int size)
        {
            this.size = size;
            data = new int[size, size];
            Random rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    data[i, j] = rnd.Next(1, 10);
                }
            }
        }

        // Конструктор из массива
        public Matrix(int[,] array)
        {
            size = array.GetLength(0);
            data = (int[,])array.Clone();
        }

        // Индексатор
        public int this[int i, int j]
        {
            get { return data[i, j]; }
            set { data[i, j] = value; }
        }

        // Сложение
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.size != b.size)
            {
                throw new MatrixException("Матрицы разных размеров");
            }

            Matrix result = new Matrix(a.size);

            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            return result;
        }

        // Умножение
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.size != b.size)
            {
                throw new MatrixException("Матрицы разных размеров");
            }

            Matrix result = new Matrix(a.size);

            for (int i = 0; i < a.size; i++)
            {
                for (int j = 0; j < a.size; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < a.size; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        // Детерминант только для 2x2
        public int Determinant()
        {
            if (size != 2)
            {
                throw new MatrixException(
                    "Детерминант реализован только для 2x2");
            }

            return data[0, 0] * data[1, 1]
                 - data[0, 1] * data[1, 0];
        }

        // Обратная матрица 2x2
        public Matrix Inverse()
        {
            int det = Determinant();

            if (det == 0)
            {
                throw new MatrixException(
                    "Обратной матрицы не существует");
            }

            int[,] arr = new int[2, 2];

            arr[0, 0] = data[1, 1];
            arr[0, 1] = -data[0, 1];
            arr[1, 0] = -data[1, 0];
            arr[1, 1] = data[0, 0];

            return new Matrix(arr);
        }

        // Сравнение по детерминанту
        public static bool operator >(Matrix a, Matrix b)
        {
            return a.Determinant() > b.Determinant();
        }

        public static bool operator <(Matrix a, Matrix b)
        {
            return a.Determinant() < b.Determinant();
        }

        public static bool operator >=(Matrix a, Matrix b)
        {
            return a.Determinant() >= b.Determinant();
        }

        public static bool operator <=(Matrix a, Matrix b)
        {
            return a.Determinant() <= b.Determinant();
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            return a.Determinant() == b.Determinant();
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            return a.Determinant() != b.Determinant();
        }

        // true / false
        public static bool operator true(Matrix m)
        {
            return m.Determinant() != 0;
        }

        public static bool operator false(Matrix m)
        {
            return m.Determinant() == 0;
        }

        // Приведение к int
        public static explicit operator int(Matrix m)
        {
            return m.Determinant();
        }

        // ToString
        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    s += data[i, j] + "\t";
                }

                s += "\n";
            }

            return s;
        }

        // CompareTo
        public int CompareTo(object obj)
        {
            Matrix other = (Matrix)obj;

            return this.Determinant()
                .CompareTo(other.Determinant());
        }

        // Equals
        public override bool Equals(object obj)
        {
            Matrix other = (Matrix)obj;

            return this.Determinant() ==
                   other.Determinant();
        }

        // GetHashCode
        public override int GetHashCode()
        {
            return Determinant().GetHashCode();
        }

        // Глубокое копирование
        public object Clone()
        {
            int[,] copy = (int[,])data.Clone();

            return new Matrix(copy);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Matrix a = new Matrix(2);
                Matrix b = new Matrix(2);

                Console.WriteLine("Матрица A:");
                Console.WriteLine(a);

                Console.WriteLine("Матрица B:");
                Console.WriteLine(b);

                // Сложение
                Matrix sum = a + b;

                Console.WriteLine("A + B:");
                Console.WriteLine(sum);

                // Умножение
                Matrix mult = a * b;

                Console.WriteLine("A * B:");
                Console.WriteLine(mult);

                // Детерминант
                Console.WriteLine(
                    "Детерминант A = " + a.Determinant());

                // Сравнение
                if (a > b)
                {
                    Console.WriteLine("A > B");
                }
                else
                {
                    Console.WriteLine("A <= B");
                }

                // true false
                if (a)
                {
                    Console.WriteLine(
                        "Матрица A имеет обратную");
                }

                // Копирование
                Matrix copy = (Matrix)a.Clone();

                Console.WriteLine("Копия матрицы A:");
                Console.WriteLine(copy);

                // Приведение типов
                int det = (int)a;

                Console.WriteLine(
                    "Матрица A как int = " + det);

                // Обратная матрица
                Console.WriteLine(
                    "Обратная матрица A:");

                Console.WriteLine(a.Inverse());
            }

            catch (MatrixException ex)
            {
                Console.WriteLine(
                    "Ошибка: " + ex.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine(
                    "Общая ошибка: " + ex.Message);
            }
        }
    }
}
