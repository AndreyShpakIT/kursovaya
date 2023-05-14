using System;

class MainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите количество точек для аппроксимации:");
        int pointCount = int.Parse(Console.ReadLine());

        double[] x = new double[pointCount];
        double[] y = new double[pointCount];

        Console.WriteLine("Введите координаты точек (через пробел):");
        for (int i = 0; i < pointCount; i++)
        {
            string[] coords = Console.ReadLine().Split();
            x[i] = double.Parse(coords[0]);
            y[i] = double.Parse(coords[1]);
        }

        double[] coefs = CalculateLeastSquaresGauss(x, y);

        Console.WriteLine($"Аппроксимирующее уравнение: y = {coefs[0]} + {coefs[1]}t");
    }

    static double[] CalculateLeastSquaresGauss(double[] x, double[] y)
    {
        double sumX = 0;
        double sumY = 0;
        double sumXY = 0;
        double sumX2 = 0;

        for (int i = 0; i < x.Length; i++)
        {
            sumX += x[i];
            sumY += y[i];
            sumXY += x[i] * y[i];
            sumX2 += x[i] * x[i];
        }

        double[,] matrix = { { x.Length, sumX }, { sumX, sumX2 } };
        double[] yVector = { sumY, sumXY };
        double[] coeffs = Gauss(matrix, yVector);

     
        double a = coeffs[0];
        double b = coeffs[1];

        return new double[] { a, b };
    }

    static double[] Gauss(double[,] matrix, double[] vector)
    {
        

        int n = vector.Length;
        double[] x = new double[n];

        for (int k = 0; k < n - 1; k++)
        {
            for (int i = k + 1; i < n; i++)
            {
                double factor = matrix[i, k] / matrix[k, k];
                for (int j = k + 1; j < n; j++)
                {
                    matrix[i, j] -= factor * matrix[k, j];
                }
                vector[i] -= factor * vector[k];
            }
        }

       
        x[n - 1] = vector[n - 1] / matrix[n - 1, n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            double sum = vector[i];
            for (int j = i + 1; j < n; j++)
            {
                sum -= matrix[i, j] * x[j];
            }
            x[i] = sum / matrix[i, i];
        }

        return x;
    }
}
