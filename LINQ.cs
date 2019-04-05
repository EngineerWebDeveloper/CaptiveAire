using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptiveAire
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] csvlines = File.ReadAllLines(@"C:\xampp\htdocs\New_York_State_Mathematics_Exam.csv");
            List<Data> cvs = new List<Data>();
            foreach (string s in csvlines)
            {
                Data d = new Data();
                d.level2 = s.Split(',')[7];
                d.year = s.Split(',')[1];
                d.category = s.Split(',')[2];
                d.grade = s.Split(',')[0];
                cvs.Add(d);
            }
            var Averages = (from t in cvs.Skip(1)
                            where t.grade != "All Grades"
                            group t by new { t.year, t.category }
             into grp
                            select new
                            {
                                grp.Key.year,
                                grp.Key.category,
                                Average = grp.Average(t => Convert.ToInt32(t.level2))
                            }).OrderByDescending(x => x.Average);

            foreach (var s in Averages)
            {
                Console.WriteLine(s.Average);
            }

            Console.ReadKey();
        }

        class Data
        {
            public string level2 { get; set; }
            public string year { get; set; }
            public string category { get; set; }
            public string grade { get; set; }
        }
    }



}
