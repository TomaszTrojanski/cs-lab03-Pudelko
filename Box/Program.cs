using System;
using System.Collections.Generic;

namespace Box
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pudelko p1 = new Pudelko(unit: UnitOfMeasure.milimeter);
            Pudelko p2 = new Pudelko(200, 100, 150, unit: UnitOfMeasure.milimeter);
            Console.WriteLine("A: " + p1.A);
            Console.WriteLine("B: " + p1.B);
            Console.WriteLine("C: " + p1.C);
            Console.WriteLine(p1.ToString());
            Console.WriteLine(p1.ToString("cm"));
            Console.WriteLine(p1.ToString("mm"));
            Console.WriteLine($"Objestosc: {p1.Objetosc}");
            Console.WriteLine($"Pole: {p1.Pole}");
            Pudelko p3 = new Pudelko(1, 3.05, 2.1);
            Pudelko p4 = new Pudelko(2100, 1000, 3050, unit: UnitOfMeasure.milimeter);
            Console.WriteLine($"Equals: {p3.Equals(p4)}");
            Console.WriteLine($"HashCode: {p1.GetHashCode()}");
            Console.WriteLine($"Wymiary pudelka na pudelko nr 1 i pudelko nr 2:  {(p1 + p2).ToString()}");
            double[] result = p1.ConvertToArray(p1);
            Pudelko a = Pudelko.Parse("3 m × 3 m × 3 m");
            Console.WriteLine(a.ToString());
            Pudelko p5 = new Pudelko(2, 2, 2, UnitOfMeasure.centimeter);
            Pudelko p6 = new Pudelko(2, 2, 2, UnitOfMeasure.centimeter);
            Console.WriteLine("łączone pudełko "+ (p5 + p6).ToString());
            Console.WriteLine(p2.Kompresuj().ToString());

            List<Pudelko> listaPudelek = new List<Pudelko>();
            listaPudelek.Add(new Pudelko());
            listaPudelek.Add(new Pudelko(10, 10, 10, UnitOfMeasure.milimeter));
            listaPudelek.Add(new Pudelko(10, 10, 10, UnitOfMeasure.meter));
            listaPudelek.Add(new Pudelko(10, 10, 10, UnitOfMeasure.centimeter));
            listaPudelek.Add(new Pudelko(2000, 2000, 3100, UnitOfMeasure.milimeter));
            listaPudelek.Sort();
            Console.WriteLine("Posortowane Pudełka");
            foreach (Pudelko item in listaPudelek)
            {
                Console.WriteLine($"{item.ToString()} Objętość: {item.Objetosc} Powierzchnia {item.Pole}");
            }
        }
    }
}