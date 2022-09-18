using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    public class Calc
    {
        private readonly Resources Resources;

        public Calc(Resources resources)
        {
            Resources = resources;
        }

        public void Run()
        {
            Console.WriteLine(Resources.GetEnterNumberMessage());
        }
    }
}