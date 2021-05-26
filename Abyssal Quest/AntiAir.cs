using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyssal_Quest
{
    class AntiAir
    {
        double caliber = 0;
        double gunQuality = 0;
        int rateOfFire = 0;

        Dictionary<string, double> shellTypes = new Dictionary<string, double>();

        public AntiAir(double c, double gQ, int rOF)
        {
            caliber = c;
            gunQuality = gQ;
            rateOfFire = rOF;

            shellTypes.Add("AA Fuze", 1);
        }
        public double getCaliber() { return caliber; }
        public double getGunQuality() { return gunQuality; }
        public double getShellType(string str) { double value; shellTypes.TryGetValue(str, out value); return value; }
        public int getRateOfFire() { return rateOfFire; }
    }
}
