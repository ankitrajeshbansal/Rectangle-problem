using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleProblem
{

    /// <summary>
    /// Ankit Bansal
    /// 03-04-2017
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0, x = 0, y=0;
            List<Rectangle> inputRec = new List<Rectangle>();
            List<Rectangle> outputRec = new List<Rectangle>();
            while (true)
            {
                Console.Write("H W");
                string value = Console.ReadLine();
                if (!string.IsNullOrEmpty(value) && value.Split(' ').Count() == 2)
                {
                    Rectangle rec = new Rectangle
                    {
                        sequence = i++,
                        height = Convert.ToInt32(value.Split(' ')[0]),
                        width = Convert.ToInt32(value.Split(' ')[1]),
                        X = x,
                        Y = y
                    };
                    inputRec.Add(rec);
                    x = x + rec.width;
                }
                else {
                    Console.Write("start process the rotate rectangle");
                    break;
                }
            }

            if (inputRec.Count > 0)
            {
                if (inputRec.Count == 1)
                {
                    Console.Write("Only one rectangle no need to rotate");

                }
                else {
                    CallRecursive(inputRec, outputRec);

                }
            }


        }

        static void CallRecursive(List<Rectangle> inputRec, List<Rectangle> outputRec)
        {
            
            Rectangle rect = new Rectangle { height = inputRec.Min(r => r.height), width = inputRec.Sum(r => r.width), X= inputRec.Min(r=>r.X), Y= inputRec.Min(r=>r.Y) };
            outputRec.Add(rect);
            foreach (var item in inputRec)
            {
                item.height = item.height - rect.height;
                item.Y = item.Y + rect.height;
            }
            inputRec = inputRec.Where(r => r.height > 0).ToList();

            if (inputRec.Count > 0)
            {
                var result = inputRec.GroupBy(r => inputRec.Where(inRec => inRec.sequence >= r.sequence)
                                                .OrderBy(inRec => inRec.sequence)
                                                .TakeWhile((inRec, index) => inRec.sequence == r.sequence + index)
                                                .Last())
                           .Select(seq => seq.OrderBy(r => r.sequence)).ToList();
                foreach (var item in result)
                {
                    CallRecursive(item.ToList(), outputRec);
                }
            }
        }

    }

    class Rectangle {
        public int sequence { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        
        public int X { set; get; }
        public int Y { set; get; }
    }

    
}
