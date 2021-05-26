using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Abyssal_Quest
{
    public partial class LoadShip : Form
    {
        Ship ship;
        LS loadShip = new LS();
        string shipName = "";
        string output = "";

        public LoadShip()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            string filepath = @"Ship Designs\";
            DirectoryInfo d = new DirectoryInfo(filepath);
            foreach (var file in d.GetFiles("*"))
            {
                string output = file.ToString();
                output = output.Substring(0, output.Length - 4);
                shipNameDisplay.Items.Add(output);
            }
        }

        private void printStats(Ship ship)
        {
            outputTextBox.Text = "Tonnage: " + ship.getTonnage() + Environment.NewLine;
            outputTextBox.Text += "Flotation: " + ship.getFlotation() + Environment.NewLine;
            outputTextBox.Text += "Superstructure: " + ship.getSuperstructure().ToString() + Environment.NewLine;
            outputTextBox.Text += "Maintenance: " + ship.getMaintenance() + Environment.NewLine;
            outputTextBox.Text += "Experience: " + ship.getExperience() + Environment.NewLine;
            outputTextBox.Text += "Cost: " + ship.getSteel() + Environment.NewLine + Environment.NewLine;

            outputTextBox.Text += "Superstructure" + Environment.NewLine;
            outputTextBox.Text += "Shipwide Gun Director: " + ship.getShipwideGunDirector() + Environment.NewLine;
            outputTextBox.Text += "Shipwide AA Director: " + ship.getShipwideAADirector() + Environment.NewLine;
            outputTextBox.Text += "Radio: " + ship.getRadio() + Environment.NewLine + "Radar: " + ship.getRadar() + Environment.NewLine + Environment.NewLine;

            if (ship.getNumMainGuns() == 0)
            {
                outputTextBox.Text += "Main Guns" + Environment.NewLine;
                outputTextBox.Text += "Main Guns: " + "N/A" + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                outputTextBox.Text += "Main Guns" + Environment.NewLine;
                outputTextBox.Text += "Main Guns: " + ship.getNumMainGuns() + " x " + ship.getMainGunCaliber() + "mm" + Environment.NewLine;
                outputTextBox.Text += "Main Gun Firepower: " + ship.getMainGunFirepower()[0] + " | " + ship.getMainGunFirepower()[1] + " (APHE | HE)" + Environment.NewLine;
                outputTextBox.Text += "Rate of Fire: " + ship.getMainGunReloadSpeed() + " Turns to Reload" + Environment.NewLine;
                outputTextBox.Text += "Main Gun Belt Penetration: " + ship.getMainGunBeltPen("Melee Range") + "mm" + "/" + ship.getMainGunBeltPen("Close Range") + "mm" + "/" + ship.getMainGunBeltPen("Medium Range") + "mm" + "/";
                outputTextBox.Text += ship.getMainGunBeltPen("Long Range") + "mm" + " | " + ship.getMainGunBeltPen("HE") + "mm" + " (APHE | HE)" + Environment.NewLine;
                outputTextBox.Text += "Main Gun Plunging Penetration: " + ship.getMainGunPlungingPen("Melee Range") + "mm" + "/" + ship.getMainGunPlungingPen("Close Range") + "mm" + "/" + ship.getMainGunPlungingPen("Medium Range") + "mm" + "/";
                outputTextBox.Text += ship.getMainGunPlungingPen("Long Range") + "mm" + " | " + ship.getMainGunPlungingPen("HE") + "mm" + " (APHE | HE)" + Environment.NewLine;
                outputTextBox.Text += "Main Gun Accuracy: " + ship.getMainGunAccuracy("Melee Range") + "%" + "/" + ship.getMainGunAccuracy("Close Range") + "%" + "/" + ship.getMainGunAccuracy("Medium Range") + "%" + "/";
                outputTextBox.Text += ship.getMainGunAccuracy("Long Range") + "%" + Environment.NewLine + Environment.NewLine;
            }

            if (ship.getNumSecondaryGuns() == 0)
            {
                outputTextBox.Text += "Secondary Guns" + Environment.NewLine;
                outputTextBox.Text += "Secondary Guns: " + "N/A" + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                outputTextBox.Text += "Secondary Guns" + Environment.NewLine;
                outputTextBox.Text += "Secondary Guns: " + ship.getNumSecondaryGuns() + " x " + ship.getSecondaryGunCaliber() + "mm" + Environment.NewLine;
                outputTextBox.Text += "Secondary Gun Firepower: " + ship.getSecondaryGunFirepower()[0] + " | " + ship.getSecondaryGunFirepower()[1] + " (APHE | HE)" + Environment.NewLine;
                outputTextBox.Text += "Rate of Fire: " + ship.getSecondaryGunReloadSpeed() + " Shots per Turn" + Environment.NewLine;
                outputTextBox.Text += "Secondary Gun Belt Penetration: " + ship.getSecondaryGunBeltPen("Melee Range") + "mm" + "/" + ship.getSecondaryGunBeltPen("Close Range") + "mm" + "/" + ship.getSecondaryGunBeltPen("Medium Range") + "mm" + "/";
                outputTextBox.Text += ship.getSecondaryGunBeltPen("Long Range") + "mm" + " | " + ship.getSecondaryGunBeltPen("HE") + "mm" + " (APHE | HE)" + Environment.NewLine;
                outputTextBox.Text += "Secondary Gun Plunging Penetration: " + ship.getSecondaryGunPlungingPen("Melee Range") + "mm" + "/" + ship.getSecondaryGunPlungingPen("Close Range") + "mm" + "/" + ship.getSecondaryGunPlungingPen("Medium Range") + "mm" + "/";
                outputTextBox.Text += ship.getSecondaryGunPlungingPen("Long Range") + "mm" + " | " + ship.getSecondaryGunPlungingPen("HE") + "mm" + " (APHE | HE)" + Environment.NewLine;
                outputTextBox.Text += "Secondary Gun Accuracy: " + ship.getSecondaryGunAccuracy("Melee Range") + "%" + "/" + ship.getSecondaryGunAccuracy("Close Range") + "%" + "/" + ship.getSecondaryGunAccuracy("Medium Range") + "%" + "/";
                outputTextBox.Text += ship.getSecondaryGunAccuracy("Long Range") + "%" + Environment.NewLine + Environment.NewLine;
            }

            if (ship.getTotalTorpedoes() == 0)
            {
                outputTextBox.Text += "Torpedoes" + Environment.NewLine;
                outputTextBox.Text += "Torpedoes: N/A" + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                outputTextBox.Text += "Torpedoes" + Environment.NewLine;
                outputTextBox.Text += "Torpedoes: " + ship.getTotalTorpedoes() + " x " + ship.getTorpedoSize() + "mm" + Environment.NewLine;
                outputTextBox.Text += "Torpedoes Firepower: " + ship.getTorpedoFirepower()[0] + Environment.NewLine + Environment.NewLine;
            }

            if (ship.getTotalSmallAA() == 0)
            {
                outputTextBox.Text += "Anti Aircraft" + Environment.NewLine;
                outputTextBox.Text += "Small Anti Air: N/A" + Environment.NewLine;
            }
            else
            {
                outputTextBox.Text += "Anti Aircraft" + Environment.NewLine;
                outputTextBox.Text += "Small AA: " + ship.getTotalSmallAA() + " x " + ship.getSmallAACaliber() + "mm" + Environment.NewLine;
                outputTextBox.Text += "Small AA Firepower: " + ship.getSmallAAFirepower()[0] + " (AA Fuze)" + Environment.NewLine;
                outputTextBox.Text += "Small AA Rate of Fire: " + ship.getSmallAAROF() + " Shots per Turn" + Environment.NewLine;
                outputTextBox.Text += "Small AA Accuracy: " + ship.getSmallAAAccuracy("Melee Range") + "%" + "/" + ship.getSmallAAAccuracy("Close Range") + "%" + "/" + ship.getSmallAAAccuracy("Medium Range") + "%" + "/";
                outputTextBox.Text += ship.getSmallAAAccuracy("Long Range") + "%" + Environment.NewLine + Environment.NewLine;
            }

            if (ship.getTotalMediumAA() == 0) { outputTextBox.Text += "Medium Anti Air: N/A" + Environment.NewLine; }
            else
            {
                outputTextBox.Text += "Medium AA: " + ship.getTotalMediumAA() + " x " + ship.getMediumAACaliber() + "mm" + Environment.NewLine;
                outputTextBox.Text += "Medium AA Firepower: " + ship.getMediumAAFirepower()[0] + " (AA Fuze)" + Environment.NewLine;
                outputTextBox.Text += "Medium AA Rate of Fire: " + ship.getMediumAAROF() + " Shots per Turn" + Environment.NewLine;
                outputTextBox.Text += "Medium AA Accuracy: " + ship.getMediumAAAccuracy("Melee Range") + "%" + "/" + ship.getMediumAAAccuracy("Close Range") + "%" + "/" + ship.getMediumAAAccuracy("Medium Range") + "%" + "/";
                outputTextBox.Text += ship.getMediumAAAccuracy("Long Range") + "%" + Environment.NewLine + Environment.NewLine;
            }

            if (ship.getTotalLargeAA() == 0) { outputTextBox.Text += "Large Anti Air: N/A" + Environment.NewLine + Environment.NewLine; }
            else
            {
                outputTextBox.Text += "Large AA: " + ship.getTotalLargeAA() + " x " + ship.getLargeAACaliber() + "mm" + Environment.NewLine;
                outputTextBox.Text += "Large AA Firepower: " + ship.getLargeAAFirepower()[0] + " (AA Fuze)" + Environment.NewLine;
                outputTextBox.Text += "Large AA Rate of Fire: " + ship.getLargeAAROF() + " Shots per Turn" + Environment.NewLine;
                outputTextBox.Text += "Large AA Accuracy: " + ship.getLargeAAAccuracy("Melee Range") + "%" + "/" + ship.getLargeAAAccuracy("Close Range") + "%" + "/" + ship.getLargeAAAccuracy("Medium Range") + "%" + "/";
                outputTextBox.Text += ship.getLargeAAAccuracy("Long Range") + "%" + Environment.NewLine + Environment.NewLine;
            }

            outputTextBox.Text += "Armour" + Environment.NewLine;
            outputTextBox.Text += "Belt Armour: " + ship.getBeltArmour() + "mm" + Environment.NewLine + "Deck Armour: " + ship.getDeckArmour() + "mm" + Environment.NewLine;
            outputTextBox.Text += "Superstructure Armour: " + ship.getSuperstructureArmour() + "mm" + Environment.NewLine;
            outputTextBox.Text += "Main Gun Turret Armour: " + ship.getMainTurretArmour() + "mm" + Environment.NewLine + "Secondary Gun Turret Armour: " + ship.getSecondaryTurretArmour() + "mm" + Environment.NewLine + Environment.NewLine;

            outputTextBox.Text += "Engine" + Environment.NewLine;
            outputTextBox.Text += "Speed: " + ship.getSpeed() + Environment.NewLine;
        }

        public void displayShotsFired(string name, int shotsFired, int shotsHit, string targetName, int mGunAmmo, int sGunAmmo)
        {
            output += name + " - Shots Fired at " + targetName + ": " + shotsFired + Environment.NewLine;
            output += name + " - Shots Hit: " + shotsHit + Environment.NewLine;
            output += name + " - Main Gun Ammo: " + mGunAmmo + Environment.NewLine;
            output += name + " - Secondary Gun Ammo:  " + sGunAmmo + Environment.NewLine + Environment.NewLine;
        }

        public void displayCombatDamage(double flotation, double superstructure, string name, int mShotsLanded, int sShotsLanded, int mNonPens, int sNonPens)
        {
            output += name + " - Superstructure: " + superstructure + Environment.NewLine;
            output += name + " - Flotation: " + flotation + Environment.NewLine;
            if (mShotsLanded > 0) { output += "Main Gun Hits Taken: " + mShotsLanded + Environment.NewLine; }
            if (mNonPens > 0) { output += "Main Gun Nonpens Taken: " + mNonPens + Environment.NewLine; }
            if (sShotsLanded > 0) { output += "Secondary Gun Hits Taken: " + sShotsLanded + Environment.NewLine; }
            if (sNonPens > 0) { output += "Secondary Gun Nonpens Taken: " + sNonPens + Environment.NewLine; }
        }

        public void displayStats(bool sunk, Dictionary<string, int> team, string teamName)
        {
            output += teamName + Environment.NewLine;
            foreach (KeyValuePair<string, int> entry in team) { output += entry.Key + " Hits: " + entry.Value + Environment.NewLine; }
            if (sunk) { output += "Sunk" + Environment.NewLine; }
            output += Environment.NewLine;
        }

        public void shipEscaped(string shipName) { output += shipName + " Escaped" + Environment.NewLine; }
        public void rangeChanged(string range) { output += "Ships Enered: " + range + Environment.NewLine; }

        public void turnsTaken(int turns) { output += Environment.NewLine + "Turns Expired: " + turns + Environment.NewLine; outputTextBox.Text = output; }

        private void startButton_Click(object sender, EventArgs e)
        {
            shipName = shipNameDisplay.Text;
            if (File.Exists(@"Ship Designs\" + shipName + ".txt"))
            {
                ship = loadShip.load(shipName);
                printStats(ship);
            }
            else { outputTextBox.Text = "Invalid Ship Name"; }
        }

        private void combatButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Clear();

            Combat test = new Combat(this, shipName);
        }
    }
}
