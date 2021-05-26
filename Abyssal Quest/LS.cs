using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Abyssal_Quest
{
    class LS
    {
        public Ship load(string shipName)
        {
            int tonnage = 0;
            int speed = 0;
            double experience = 0;
            double shellPenetration = 0;

            List<int> armour = new List<int>();

            string mainGunName = "";
            int mainGunCaliber = 0;
            double mainGunQuality = 0;
            int mainGunROF = 0;
            List<int> mainGuns = new List<int>();

            string secondaryGunName = "";
            int secondaryGunCaliber = 0;
            double secondaryGunQuality = 0;
            int secondaryGunROF = 0;
            List<int> secondaryGuns = new List<int>();

            string smallAAName = "";
            double smallAACaliber = 0;
            double smallAAQuality = 0;
            int smallAARateOfFire = 0;
            List<int> smallAA = new List<int>();

            string mediumAAName = "";
            double mediumAACaliber = 0;
            double mediumAAQuality = 0;
            int mediumAARateOfFire = 0;
            List<int> mediumAA = new List<int>();

            string largeAAName = "";
            double largeAACaliber = 0;
            double largeAAQuality = 0;
            int largeAARateOfFire = 0;
            List<int> largeAA = new List<int>();

            string torpedoName = "";
            int torpedoSize = 0;
            double torpedoWarhead = 0;
            int torpedoGuidance = 0;
            int torpedoSpeed = 0;
            List<int> torpedoes = new List<int>();

            Dictionary<string, int> superstructure = new Dictionary<string, int>();
            Dictionary<string, double> research = new Dictionary<string, double>();

            string line;
            int counter = 0;

            // Read the file and display it line by line.  
            StreamReader shipReader = new StreamReader(@"Ship Designs\" + shipName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                if (line.Contains(":"))
                {
                    string match = Regex.Replace(line, @".*?:", "");
                    match = match.Substring(1);
                    //textBox1.Text += match + Environment.NewLine;

                    switch (counter)
                    {
                        case 0:
                            tonnage = Convert.ToInt32(match);
                            break;
                        case 1:
                            speed = Convert.ToInt32(match);
                            break;
                        case 2:
                            experience = Convert.ToDouble(match);
                            break;
                        case 3:
                            mainGunName = match;
                            break;
                        case 4:
                            mainGuns.Add(Convert.ToInt32(match));
                            break;
                        case 5:
                            mainGuns.Add(Convert.ToInt32(match));
                            break;
                        case 6:
                            mainGuns.Add(Convert.ToInt32(match));
                            break;
                        case 7:
                            secondaryGunName = match;
                            break;
                        case 8:
                            secondaryGuns.Add(Convert.ToInt32(match));
                            break;
                        case 9:
                            secondaryGuns.Add(Convert.ToInt32(match));
                            break;
                        case 10:
                            secondaryGuns.Add(Convert.ToInt32(match));
                            break;
                        case 11:
                            smallAAName = match;
                            break;
                        case 12:
                            smallAA.Add(Convert.ToInt32(match));
                            break;
                        case 13:
                            smallAA.Add(Convert.ToInt32(match));
                            break;
                        case 14:
                            mediumAAName = match;
                            break;
                        case 15:
                            mediumAA.Add(Convert.ToInt32(match));
                            break;
                        case 16:
                            mediumAA.Add(Convert.ToInt32(match));
                            break;
                        case 17:
                            largeAAName = match;
                            break;
                        case 18:
                            largeAA.Add(Convert.ToInt32(match));
                            break;
                        case 19:
                            largeAA.Add(Convert.ToInt32(match));
                            break;
                        case 20:
                            torpedoName = match;
                            break;
                        case 21:
                            torpedoes.Add(Convert.ToInt32(match));
                            break;
                        case 22:
                            torpedoes.Add(Convert.ToInt32(match));
                            break;
                        case 23:
                            superstructure.Add("Shipwide Gun Director", Convert.ToInt32(match));
                            break;
                        case 24:
                            superstructure.Add("Shipwide AA Director", Convert.ToInt32(match));
                            break;
                        case 25:
                            superstructure.Add("Radio", Convert.ToInt32(match));
                            break;
                        case 26:
                            superstructure.Add("Radar", Convert.ToInt32(match));
                            break;
                        case 27:
                            armour.Add(Convert.ToInt32(match));
                            break;
                        case 28:
                            armour.Add(Convert.ToInt32(match));
                            break;
                        case 29:
                            armour.Add(Convert.ToInt32(match));
                            break;
                        case 30:
                            shellPenetration = Convert.ToInt32(match);
                            break;
                        case 31:
                            research.Add("Ship Design Skill", Convert.ToInt32(match));
                            break;
                        case 32:
                            research.Add("Oil Efficiency", Convert.ToDouble(match));
                            break;
                        case 33:
                            research.Add("Engine Design", Convert.ToInt32(match));
                            break;
                        case 34:
                            research.Add("Turret Armour", Convert.ToInt32(match));
                            break;
                        case 35:
                            research.Add("Magazine Armour", Convert.ToInt32(match));
                            break;
                        case 36:
                            research.Add("Belt Armour", Convert.ToDouble(match));
                            break;
                        case 37:
                            research.Add("Deck Armour", Convert.ToDouble(match));
                            break;
                        case 38:
                            research.Add("Superstructure Armour", Convert.ToDouble(match));
                            break;
                    }
                    counter++;
                }

            }
            counter = 0;
            shipReader.Close();

            shipReader = new StreamReader(@"Guns\" + mainGunName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                string match = Regex.Replace(line, @".*?:", "");
                match = match.Substring(1);

                switch (counter)
                {
                    case 0:
                        mainGunCaliber = Convert.ToInt32(match);
                        break;
                    case 1:
                        mainGunQuality = Convert.ToDouble(match);
                        break;
                    case 2:
                        mainGunROF = Convert.ToInt32(match);
                        break;
                }
                counter++;
            }
            counter = 0;
            shipReader.Close();
            Guns mainGun = new Guns(mainGunCaliber, mainGunQuality, shellPenetration, mainGunROF);

            shipReader = new StreamReader(@"Guns\" + secondaryGunName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                string match = Regex.Replace(line, @".*?:", "");
                match = match.Substring(1);

                switch (counter)
                {
                    case 0:
                        secondaryGunCaliber = Convert.ToInt32(match);
                        break;
                    case 1:
                        secondaryGunQuality = Convert.ToDouble(match);
                        break;
                    case 2:
                        secondaryGunROF = Convert.ToInt32(match);
                        break;
                }
                counter++;
            }
            counter = 0;
            shipReader.Close();
            Guns secondaryGun = new Guns(secondaryGunCaliber, secondaryGunQuality, shellPenetration, secondaryGunROF);

            shipReader = new StreamReader(@"AA\" + smallAAName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                string match = Regex.Replace(line, @".*?:", "");
                match = match.Substring(1);

                switch (counter)
                {
                    case 0:
                        smallAACaliber = Convert.ToDouble(match);
                        break;
                    case 1:
                        smallAAQuality = Convert.ToDouble(match);
                        break;
                    case 2:
                        smallAARateOfFire = Convert.ToInt32(match);
                        break;
                }
                counter++;
            }
            counter = 0;
            shipReader.Close();
            AntiAir smallAAGun = new AntiAir(smallAACaliber, smallAAQuality, smallAARateOfFire);

            shipReader = new StreamReader(@"AA\" + mediumAAName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                string match = Regex.Replace(line, @".*?:", "");
                match = match.Substring(1);

                switch (counter)
                {
                    case 0:
                        mediumAACaliber = Convert.ToDouble(match);
                        break;
                    case 1:
                        mediumAAQuality = Convert.ToDouble(match);
                        break;
                    case 2:
                        mediumAARateOfFire = Convert.ToInt32(match);
                        break;
                }
                counter++;
            }
            counter = 0;
            shipReader.Close();
            AntiAir mediumAAGun = new AntiAir(mediumAACaliber, mediumAAQuality, mediumAARateOfFire);

            shipReader = new StreamReader(@"AA\" + largeAAName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                string match = Regex.Replace(line, @".*?:", "");
                match = match.Substring(1);

                switch (counter)
                {
                    case 0:
                        largeAACaliber = Convert.ToDouble(match);
                        break;
                    case 1:
                        largeAAQuality = Convert.ToDouble(match);
                        break;
                    case 2:
                        largeAARateOfFire = Convert.ToInt32(match);
                        break;
                }
                counter++;
            }
            counter = 0;
            shipReader.Close();
            AntiAir largeAAGun = new AntiAir(largeAACaliber, largeAAQuality, largeAARateOfFire);

            shipReader = new StreamReader(@"Torpedoes\" + torpedoName + ".txt");
            while ((line = shipReader.ReadLine()) != null)
            {
                string match = Regex.Replace(line, @".*?:", "");
                match = match.Substring(1);

                switch (counter)
                {
                    case 0:
                        torpedoSize = Convert.ToInt32(match);
                        break;
                    case 1:
                        torpedoWarhead = Convert.ToDouble(match);
                        break;
                    case 2:
                        torpedoGuidance = Convert.ToInt32(match);
                        break;
                    case 3:
                        torpedoSpeed = Convert.ToInt32(match);
                        break;
                }
                counter++;
            }
            counter = 0;
            shipReader.Close();
            Torpedo torpedo = new Torpedo(torpedoSize, torpedoWarhead, torpedoGuidance, torpedoSpeed);

            Dictionary<string, AntiAir> antiAirWeapons = new Dictionary<string, AntiAir>();
            antiAirWeapons.Add("Small AA", smallAAGun);
            antiAirWeapons.Add("Medium AA", mediumAAGun);
            antiAirWeapons.Add("Large AA", largeAAGun);

            Dictionary<string, List<int>> aaList = new Dictionary<string, List<int>>();
            aaList.Add("Small AA", smallAA);
            aaList.Add("Medium AA", mediumAA);
            aaList.Add("Large AA", largeAA);

            Dictionary<string, Guns> guns = new Dictionary<string, Guns>();
            guns.Add("Main Gun", mainGun);
            guns.Add("Secondary Gun", secondaryGun);

            return new Ship(tonnage, speed, experience, armour, research, superstructure, aaList, antiAirWeapons, guns, mainGuns, secondaryGuns, torpedoes, torpedo);
        }
    }
}
