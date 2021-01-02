using System;

namespace Lr3
{
    class Program
    {
        static void Main(string[] args)
        {
            var o1 = new BoolVector();
            BoolVector.PrintInfo(o1);
            BoolVector.PrintInfo(o1);
            var o2 = new BoolVector();
            BoolVector.PrintInfo(o2);
            Console.Write("o2.Length = ");
            Console.WriteLine(o2.Length);
            Console.WriteLine("o1 = {0}", o1);
            Console.WriteLine(o1.GetHashCode());
            Console.WriteLine(o2.GetHashCode());
            Console.WriteLine("o2 = {0}", o2);
            Console.WriteLine(o1.Equals(o2));
            
            if (BoolVector.TryParse("10101010", out BoolVector o3))
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failure!");
            }

            Console.WriteLine(o3.Equals(o1));
            Console.WriteLine(o3.GetType());
            Console.WriteLine("o3 = {0}", o3);
            var o4 = new BoolVector(new bool[]
            { BoolVector.One, BoolVector.Zero, BoolVector.One, BoolVector.Zero,
              BoolVector.One, BoolVector.Zero, BoolVector.One, BoolVector.One });

            Console.WriteLine("o4 = {0}", o4);
            o4.Negate();
            Console.WriteLine("o4.Negate()");
            Console.WriteLine("o4 = {0}", o4);
            o4.And(o4);
            Console.WriteLine("o4.And(o4)");
            Console.WriteLine("o4 = {0}", o4);
            o4.Or(o3);
            Console.WriteLine("o4.Or(o3)");
            Console.WriteLine("o4 = {0}", o4);
            Console.WriteLine("o1.And(o3)");
            o1.And(o3);
            Console.WriteLine("o1 = {0}", o1);
            Console.WriteLine("o2.Or(o3)");
            o2.Or(o3);
            Console.WriteLine("o2 = {0}", o2);

            
            var strArray = new string[]
            {
                "1010",
                "110000",
                "000001;1",
                "111111",
                "1010",
                "10101010;data",
                "0000000",
                "1",
                "0;zero",
                "1010",
                "111011101",
                "000001",
            };
            var array = new BoolVector[strArray.Length];
            Console.WriteLine("{0, 20} {1, 20} {2, -10}", "Input", "Output", "Rest");
            for (int i = 0; i < strArray.Length; i++)
            {
                var input = strArray[i];
                BoolVector.Consume(ref strArray[i], out array[i]);
                Console.WriteLine("{0, 20} {1, 20} {2, -10}", input, array[i], strArray[i]);
            }

            const int countZero = 4;
            const int countOnes = 2;

            Console.WriteLine("Элементы с числом единиц = {0}:", countOnes);
            foreach (var bv in array)
            {
                if (bv.CountOnes() == countOnes)
                {
                    Console.WriteLine(bv);
                }
            }

            Console.WriteLine("Элементы с числом нулей = {0}:", countZero);
            foreach (var bv in array)
            {
                if (bv.CountZeros() == countZero)
                {
                    Console.WriteLine(bv);
                }
            }

            var watchedIndexes = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0 && Array.Exists(watchedIndexes, item => item == i))
                {
                    continue;
                }
                var duplicates = new int[0];
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i].Equals(array[j]))
                    {
                        Array.Resize(ref duplicates, duplicates.Length + 1);
                        duplicates[duplicates.Length - 1] = j;
                        watchedIndexes[j] = j;
                    }
                }

                if (duplicates.Length != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Вектора равные вектору {0}", array[i]);
                    foreach (var ind in duplicates)
                    {
                        Console.WriteLine(array[ind]);
                    }
                    Console.WriteLine();
                }
            }

            var anType = new
            {
                BoolVec = o1,
                Length = o1.Length
            };

            Console.WriteLine("Анонимный тип:");
            Console.WriteLine(anType);

            Console.ReadKey(true);
        }
    }
}
