using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyssal_Quest
{
    class Guns
    {
        int caliber = 0;
        double gunQuality = 0;
        double shellPenetration = 0;
        int rateOfFire = 0;

        Dictionary<string, double> shellTypes = new Dictionary<string, double>();
        Dictionary<string, int> beltPenetration = new Dictionary<string, int>();
        Dictionary<string, int> plungingPenetration = new Dictionary<string, int>();

        public Guns(int c, double gQ, double sP, int rOF)
        {
            caliber = c;
            gunQuality = gQ;
            shellPenetration = sP;
            rateOfFire = rOF;

            shellTypes.Add("APHE Pen", 0.8);
            shellTypes.Add("APHE Blast", 0.5);
            shellTypes.Add("HE Pen", 0.2);
            shellTypes.Add("HE Blast", 0.8);

            calculateBeltPenetration();
            calculatePlungingPenetration();
        }

        private void calculateBeltPenetration()
        {
            int tmpVar = Convert.ToInt32(((caliber * gunQuality) * getShellType("APHE Pen")) * shellPenetration);
            beltPenetration.Add("Melee Range", tmpVar * 2);
            beltPenetration.Add("Close Range", tmpVar);
            beltPenetration.Add("Medium Range", Convert.ToInt32(tmpVar / 1.5));
            beltPenetration.Add("Long Range", Convert.ToInt32(tmpVar / 5));
            tmpVar = Convert.ToInt32(((caliber * gunQuality) * getShellType("HE Pen")) * shellPenetration);
            beltPenetration.Add("HE", Convert.ToInt32(tmpVar));
        }

        private void calculatePlungingPenetration()
        {
            int tmpVar = Convert.ToInt32(((caliber * gunQuality) * getShellType("APHE Pen")) * shellPenetration);
            plungingPenetration.Add("Melee Range", Convert.ToInt32(tmpVar / 100));
            plungingPenetration.Add("Close Range", Convert.ToInt32(tmpVar / 10));
            plungingPenetration.Add("Medium Range", Convert.ToInt32(tmpVar / 5));
            plungingPenetration.Add("Long Range", Convert.ToInt32(tmpVar / 1.5));
            tmpVar = Convert.ToInt32(((caliber * gunQuality) * getShellType("HE Pen")) * shellPenetration);
            plungingPenetration.Add("HE", Convert.ToInt32(tmpVar));
        }

        public int getCaliber() { return caliber; }
        public double getGunQuality() { return gunQuality; }
        public double getShellType(string str) { double value; shellTypes.TryGetValue(str, out value); return value; }
        public int getRateOfFire() { return rateOfFire; }
        public Dictionary<string, int> getBeltPen() { return beltPenetration;}
        public Dictionary<string, int> getPlungingPen() { return plungingPenetration; }
    }
}
