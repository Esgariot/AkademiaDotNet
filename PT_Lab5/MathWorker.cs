using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PT_Lab5.Annotations;

namespace PT_Lab5 {
    public class MathWorker {
        private Random random;

        public MathWorker() {
            random = new Random();
        }

        public ulong PartialFactorial(int upper, int lower = 1) {
            if (lower > upper)
                throw new ArgumentOutOfRangeException("K must be smaller than N");
            ulong value = 1;
            for (var i = lower; i <= upper; i++) {
                value *= (ulong) i;
            }
            return value;
        }

       

        public int NextRand() {
            return random.Next();
        }
    }
}