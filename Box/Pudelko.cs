using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Box
{
    public enum UnitOfMeasure
    {
        milimeter,
        centimeter,
        meter
    }
    public sealed class Pudelko : IEquatable<Pudelko>, IFormattable, IEnumerable, IComparable<Pudelko>
    {
        private UnitOfMeasure Unit { get; set; }
        public readonly double A;
        public readonly double B;
        public readonly double C;
        public double Objetosc => Math.Round(A * B * C, 9);
        public double Pole => Math.Round((A * B * 2) + (A * C * 2) + (B * C * 2), 6);

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.A = a == null ? 0.1 : ConvertToMeter((double)a, unit);
            this.B = b == null ? 0.1 : ConvertToMeter((double)b, unit);
            this.C = c == null ? 0.1 : ConvertToMeter((double)c, unit);
            this.Unit = unit;
            if (A < 0.001 || B < 0.001 || C < 0.001)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (IsProperWithGuidelines() == false)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private static double ConvertToMeter(double value, UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.milimeter:
                    return value / 1000;
                case UnitOfMeasure.centimeter:
                    return value / 100;
                default:
                    return value;
            }
        }

        private bool IsProperWithGuidelines()
        {
            if (A <= 0 || B <= 0 || C <= 0 || A > 10 || B > 10 || C > 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return $"{A:0.000} m \u00D7 {B:0.000} m \u00D7 {C:0.000} m";
        }
        public string ToString(string format, IFormatProvider formatProvider = null)
        {
            if (formatProvider == null)
            {
                _ = CultureInfo.CurrentCulture;
            }
            if (format == null)
            {
                format = "m";
            }
            switch (format)
            {
                case ("m"):
                    return ToString();
                case ("cm"):
                    return string.Format($"{(A * 100):0.0} cm \u00D7 {(B * 100):0.0} cm \u00D7 {(C * 100):0.0} cm");
                case ("mm"):
                    return string.Format($"{(A * 1000):0} mm \u00D7 {(B * 1000):0} mm \u00D7 {(C * 1000):0} mm");
                default:
                    throw new FormatException();

            }
        }
        public bool Equals(Pudelko other)
        {
            if (other == null) return false;
            try
            {
                if (this.A == other.A || this.A == other.B || this.A == other.C)
                    if (this.B == other.A || this.B == other.B || this.B == other.C)
                        if (this.C == other.A || this.C == other.B || this.C == other.C)
                            return true;
                return false;
            }
            catch (NullReferenceException e)
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return A.GetHashCode() + B.GetHashCode() + C.GetHashCode() + Unit.GetHashCode();
        }
        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            if (((object)p1) == null || ((object)p2) == null)
            {
                return Object.Equals(p1, p2);
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !p1.Equals(p2);
        }

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double minSize = 10000;
            double edgeA = 0;
            double edgeB = 0;
            double edgeC = 0;
            double[,] p1Edges = new double[6, 3] { {p1.A, p1.B,p1.C},{p1.A, p1.C,p1.B},{p1.B, p1.A,p1.C},{p1.B, p1.C,p1.A},{p1.C, p1.A,p1.B},{p1.C, p1.B,p1.A} };
            double[,] p2Edges = new double[6, 3] { {p2.A, p2.B,p2.C}, {p2.A, p2.C,p2.B}, {p2.B, p2.A,p2.C}, {p2.B, p2.C,p2.A}, {p2.C, p2.A,p2.B}, {p2.C, p2.B,p2.A} };

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    double tempEdgeA = p1Edges[i, 0] + p2Edges[j, 0];
                    double tempEdgeB = Math.Max(p1Edges[i, 1], p2Edges[j, 1]);
                    double tempEdgeC = Math.Max(p1Edges[i, 2], p2Edges[j, 2]);
                    Pudelko testPudelko = new Pudelko(tempEdgeA, tempEdgeB, tempEdgeC);
                    if (testPudelko.Objetosc < minSize)
                    {
                        minSize = testPudelko.Objetosc;
                        edgeA = tempEdgeA;
                        edgeB = tempEdgeB;
                        edgeC = tempEdgeC;
                    }
                }
            }
            return new Pudelko(edgeA, edgeB, edgeC);
        }
        public double[] ConvertToArray(Pudelko p)
        {
            double[] result = new double[3] { p.A, p.B, p.C };
            return result;
        }
        public IEnumerator<double> GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> a)
        {
            Pudelko p = new Pudelko(a.Item1, a.Item2, a.Item3, UnitOfMeasure.milimeter);
            return p;
        }

        public static Pudelko Parse(string str)
        {
            double[] edges = new double[3];
            string[] rawEdges = str.Split("\u00D7");
            for (int i = 0; i < 3; i++)
            {
                string trimmeditem = rawEdges[i].Trim();
                string[] aa = trimmeditem.Replace(".", ",").Split(" ");
                double number = Convert.ToDouble(aa[0]);
                UnitOfMeasure unit;
                if (aa[1] == "mm") { unit = UnitOfMeasure.milimeter; }
                else if (aa[1] == "cm") { unit = UnitOfMeasure.centimeter; }
                else { unit = UnitOfMeasure.meter; }
                edges[i] = ConvertToMeter(number, unit);
            }
            return new Pudelko(edges[0], edges[1], edges[2]);
        }

        public int CompareTo(Pudelko other)
        {
            return compareByVolume(other);
        }
        public int compareByVolume(Pudelko other)
        {
            if (this.Objetosc == other.Objetosc)
            {
                return compareByArea(other);
            }
            else
            {
                return this.Objetosc > other.Objetosc ? 1 : -1;
            }
        }
        private int compareByArea(Pudelko other)
        {
            if (this.Pole == other.Pole)
            {
                return compareBySides(other);
            }
            else
            {
                return this.Pole > other.Pole ? 1 : -1;
            }
        }
        private int compareBySides(Pudelko other)
        {
            if (this.A + this.B + this.C > other.A + other.B + other.C)
            {
                return 0;
            }
            else
            {
                return this.A + this.B + this.C > other.A + other.B + other.C ? 1 : -1;
            }
        }
    }
}