using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubicSpline_Attempt2
{
    public class triangular
    {
        static double[] a, b, c, uno, dos, tres, x, y;
        double piovertwo = Math.PI / 2.0;
        double twopi = Math.PI * 2;

        //heres where the magic begins
        public triangular(double[] xparam, double[] yparam)
        {
            y = yparam;
            x = xparam;

            // make our doubles
            a = new double[x.Length];
            b = new double[x.Length];
            c = new double[x.Length];

            // start setting up what we know will be included in the equations
            b[0] = 1;
            a[1] = x[1] - x[0];
            for (int i = 1, l = x.Length - 1; i < l; i++) // can all be done in a single for loop luckily
            {
                c[i] = a[i + 1] = x[i + 1] - x[i];
                b[i] = 2 * (a[i] + c[i]);
            }
            doTriangleThing();
        }

        public double sine(double e)
        {
            double answer;
            int idx;

            //this looks tricky but it's not
            e %= twopi;
            if (e < 0) e += twopi;
            int whatigot = (int)(e / piovertwo);
            e %= piovertwo;
            if (whatigot % 2 == 1) e = piovertwo - e;
            for (idx = 0; x[idx] < e && idx < x.Length; idx++) ; if (idx != 0) idx--; //find the correct spine
            answer = y[idx] + uno[idx] * (e - x[idx]) + dos[idx] * Math.Pow(e - x[idx], 2) + tres[idx] * Math.Pow(e - x[idx], 3);
            if (whatigot > 1) answer *= -1;
            return answer;
        }

        public double cosine(double e)
        {
            return sine(e + piovertwo);
        }

        public double tangent(double e)
        {
            double sin = sine(e), cos = cosine(e);
            if (cos == 0) // gotta watch for this behavior
            {
                Console.WriteLine("ASYMPTOTIC!");
            }
            double tan = sin / cos;
            return tan;
        }

        private void doTriangleThing()
        {
            int n = x.Length, l = n - 1;
            uno = new double[n];
            dos = new double[n];
            tres = new double[n];
            double[] v = new double[n];
            for (int i = 1; i < l; i++)
            {
                v[i] = (3 / a[i + 1]) * (y[i + 1] - y[i]) - (3 / a[i]) * (y[i] - y[i - 1]);
            }
            for (int i = 1; i < n; i++)
            {
                double m = a[i] / b[i - 1];
                b[i] = b[i] - m * c[i - 1];
                v[i] = v[i] - m * v[i - 1];
            }
            dos[n - 1] = v[n - 1] / b[n - 1];

            for (int i = n - 2; i >= 0; i--)
                dos[i] = (v[i] - c[i] * dos[i + 1]) / b[i];
            for (int i = 0; i < l; i++)
                uno[i] = (1 / a[i + 1]) * (y[i + 1] - y[i]) - (a[i + 1] / 3) * (dos[i + 1] + 2 * dos[i]);
            for (int i = 0; i < l; i++)
                tres[i] = (1 / (3 * a[i + 1])) * (dos[i + 1] - dos[i]);
        }

        static void wl(String s)
        {
            Console.WriteLine(s);
        }
        static void mikew(String s)
        {
            Console.Write(s);
        }


    }
}
