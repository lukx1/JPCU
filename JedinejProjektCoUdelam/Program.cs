using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JedinejProjektCoUdelam
{
    class Program
    {

        private static bool IsSorted(List<int> list)
        {
            var prev = list[0];
            foreach (var item in list)
            {
                if (prev > item)
                    return false;
                prev = item;
            }
            return true;
        }
        private static long BogoSort(List<int> list, long maxTries = 1000000)
        {
            var rnd = new Random();
            long tries = 0;
            while (!IsSorted(list))
            {
                if (tries > maxTries && maxTries != -1)
                    return -1;
                int randomA = rnd.Next(0, list.Count);
                int randomB = rnd.Next(0, list.Count);
                var temp = list[randomA];
                list[randomA] = list[randomB];
                list[randomB] = temp;
                tries++;
            }
            return tries;
        }

        private static void BogoTest()
        {
            var times = new List<double>(20);
            var sv = new Stopwatch();
            var rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                List<int> randomNums = new List<int>(i+1);
                for (int y = 0; y < randomNums.Capacity; y++)
                {
                    randomNums.Add(rnd.Next(0, 100));
                }
                sv.Restart();
                var res = BogoSort(randomNums,1000000000);
                if (res == -1)
                    goto end;
                sv.Stop();
                times.Add(res == -1.0 ? -1.0 : sv.ElapsedTicks);
            }
            end:
            Console.WriteLine("Results");
            int z = 0;
            Console.WriteLine("N;Time");
            using (StreamWriter sw = new StreamWriter(@"C:\Users\joskalukas\Desktop\out.csv"))
            {
                foreach (var time in times)
                {
                    sw.WriteLine(z++ + ";" + time);
                }
                sw.Close();
            }
        }
        private static void TreeTest()
        {
            /*TreeExplorer exp = new TreeExplorer();
            var tree = exp.LoadTree(@"X:\PGR\tree.csv");
            TreeExplorer.PrintChildren(tree.Nodes);*/
            BogoTest();
            return;
            List<int> bogo = new List<int>();

            bogo.Add(1);
            bogo.Add(7);
            bogo.Add(5);
            bogo.Add(2);
            bogo.Add(5);
            bogo.Add(0);
            bogo.Add(2);
            bogo.Add(5);
            /*bogo.Add(9);
            bogo.Add(2);
            bogo.Add(3);
            bogo.Add(1);
            bogo.Add(7);
            bogo.Add(5);
            bogo.Add(2);
            bogo.Add(5);
            bogo.Add(0);
            bogo.Add(2);
            bogo.Add(5);
            bogo.Add(9);
            bogo.Add(2);
            bogo.Add(3);*/
            BogoSort(bogo);
            foreach (var item in bogo)
            {
                Console.WriteLine(item);
            }
            //TreeExplorer.PrintChildren(tree.GetChildren(tree.Nodes[0]));
        }


        public static void Main(string[] args)
        {
            TreeTest();
            Console.ReadLine();
        }
    }
}
