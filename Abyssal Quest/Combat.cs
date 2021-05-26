using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyssal_Quest
{
    class Combat
    {
        Random rnd = new Random();
        LoadShip loadShip;
        LS shipLoader = new LS();

        List<Ship> team1 = new List<Ship>();
        List<Ship> team2 = new List<Ship>();

        bool team1InCombat = false;
        bool team2InCombat = false;

        int battleDuration = 0;
        string range = "Melee Range";
        int encounterStartDistance = 550;
        int distance = 0;

        Dictionary<string, WeightedRandom<string>> rangeHitProbability = new Dictionary<string, WeightedRandom<string>>();

        public Combat(LoadShip lS, string shipName)
        {
            distance = encounterStartDistance;
            if (distance < 500) { range = "Melee Range"; }
            else if (distance < 1000) { range = "Close Range"; }
            else if (distance < 1500) { range = "Medium Range"; }
            else { range = "Long Range"; }

            team1InCombat = true;
            team2InCombat = true;

            loadShip = lS;

            for (int i = 0; i < 1; i++) { team1.Add(shipLoader.load(shipName)); }
            for (int i = 0; i < 1; i++) { team2.Add(shipLoader.load(shipName)); }

            for (int i = 0; i < team1.Count(); i++) { team1[i].setName("Kanmusu " + i); }
            for (int i = 0; i < team2.Count(); i++) { team2[i].setName("Abyssal " + i); }

            rangeHitProbability.Add("Melee Range", fillTmpProbability(90, 5, 4, 1));
            rangeHitProbability.Add("Close Range", fillTmpProbability(75, 10, 10, 5));
            rangeHitProbability.Add("Medium Range", fillTmpProbability(50, 25, 15, 10));
            rangeHitProbability.Add("Long Range", fillTmpProbability(20, 45, 20, 15));

            battle();
        }

        private WeightedRandom<string> fillTmpProbability(int belt, int sup, int bS, int deck)
        {
            WeightedRandom<string> tmpProbability = new WeightedRandom<string>();
            tmpProbability.AddEntry("Belt", belt);
            tmpProbability.AddEntry("Superstructure", sup);
            tmpProbability.AddEntry("Bow/Stern", bS);
            tmpProbability.AddEntry("Deck", deck);
            return tmpProbability;
        }

        private void battle()
        {
            while (team1InCombat)
            {
                for (int i = 0; i < team1.Count(); i++)
                {
                    if (!team1[i].getEscaping() && !team1[i].getSunk() && !team1[i].getDisabled()) { if (team1[i].getMainGunAmmo() < 1 && team1[i].getSecondaryGunAmmo() < 1) { team1[i].setEscaping(); } }
                    if (!team1[i].getEscaping() && !team1[i].getSunk() && !team1[i].getDisabled()) { if (team1[i].getBattleFlotation() < (team1[i].getFlotation() * 0.3)) { team1[i].setEscaping(); } }

                    if (team1[i].getEscaping() && !team1[i].getSunk() && !team1[i].getDisabled())
                    {
                        team1[i].setEscapingCount();
                        if (team1[i].getEscapingCount() > 2500) { team1[i].setEscaped(); shipEscaped(team1[i].getName()); }
                    }
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    if (!team2[i].getEscaping() && !team2[i].getSunk() && !team2[i].getDisabled()) { if (team2[i].getMainGunAmmo() < 1 && team2[i].getSecondaryGunAmmo() < 1) { team2[i].setEscaping(); } }
                    if (!team2[i].getEscaping() && !team2[i].getSunk() && !team2[i].getDisabled()) { if (team2[i].getBattleFlotation() < (team2[i].getFlotation() * 0.3)) { team2[i].setEscaping(); } }

                    if (team2[i].getEscaping() && !team2[i].getSunk() && !team2[i].getDisabled())
                    {
                        team2[i].setEscapingCount();
                        if (team2[i].getEscapingCount() > 2500) { team2[i].setEscaped(); shipEscaped(team2[i].getName()); }
                    }
                }

                for (int i = 0; i < team1.Count(); i++)
                {
                    double a = rnd.NextDouble();
                    if (a < 0.5) { if (!team1[i].getSunk() && !team1[i].getEscaped() && !team1[i].getDisabled()) { distance += team1[i].getSpeed(); } }
                    else { if (!team1[i].getSunk() && !team1[i].getEscaped() && !team1[i].getDisabled()) { distance -= team1[i].getSpeed(); } }
                    i = team1.Count();
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    double a = rnd.NextDouble();
                    if (a < 0.5) { if (!team2[i].getSunk() && !team2[i].getEscaped() && !team2[i].getDisabled()) { distance += team2[i].getSpeed(); } }
                    else { if (!team2[i].getSunk() && !team2[i].getEscaped() && !team2[i].getDisabled()) { distance -= team2[i].getSpeed(); } }
                    i = team2.Count();
                }

                for (int i = 0; i < team1.Count(); i++)
                {
                    string nRange = "";
                    string oRange = team1[i].getMRange();

                    if (distance < 500) { nRange = "Melee Range"; }
                    else if (distance < 1000) { nRange = "Close Range"; }
                    else if (distance < 1500) { nRange = "Medium Range"; }
                    else { nRange = "Long Range"; }

                    if (oRange != nRange)
                    {
                        team1[i].setMRange(nRange); team1[i].setSRange(nRange);
                        if (i == 0) { rangeChange(range); }
                    }
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    string nRange = "";
                    string oRange = team2[i].getMRange();

                    if (distance < 500) { nRange = "Melee Range"; }
                    else if (distance < 1000) { nRange = "Close Range"; }
                    else if (distance < 1500) { nRange = "Medium Range"; }
                    else { nRange = "Long Range"; }

                    if (oRange != nRange) { team2[i].setMRange(nRange); team2[i].setSRange(nRange); }
                }

                for (int i = 0; i < team1.Count(); i++)
                {
                    if (!team1[i].getSunk() && !team1[i].getEscaped() && !team1[i].getDisabled())
                    {
                        if (team2[team1[i].getTargetIndex()].getSunk())
                        {
                            team1[i].setTarget("");
                            team1[i].setTargetIndex(0);
                        }

                        if (team1[i].getTarget() == "")
                        {
                            while (team1[i].getTarget() == "")
                            {
                                int x = rnd.Next(team2.Count());
                                if (!team2[x].getSunk()) { team1[i].setTarget(team2[x].getName()); team1[i].setTargetIndex(x); }
                            }
                        }
                    }
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    if (!team2[i].getSunk() && !team2[i].getEscaped() && !team2[i].getDisabled())
                    {
                        if (team1[team2[i].getTargetIndex()].getSunk())
                        {
                            team2[i].setTarget("");
                        }

                        if (team2[i].getTarget() == "")
                        {
                            while (team2[i].getTarget() == "")
                            {
                                int x = rnd.Next(team1.Count());
                                if (!team1[x].getSunk()) { team2[i].setTarget(team1[x].getName()); team2[i].setTargetIndex(x); }
                            }
                        }
                    }
                }

                for (int i = 0; i < team1.Count(); i++)
                {
                    if (!team1[i].getSunk() && !team1[i].getEscaped() && !team1[i].getDisabled())
                    {
                        if (team1[i].getMainGunAmmo() > 0)
                        {
                            mGunAttack(range, team1[i], team2[team1[i].getTargetIndex()]);
                        }
                        if (team1[i].getSecondaryGunAmmo() > 0)
                        {
                            sGunAttack(range, team1[i], team2[team1[i].getTargetIndex()]);
                        }
                    }
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    if (!team2[i].getSunk() && !team2[i].getEscaped() && !team2[i].getDisabled())
                    {
                        if (team2[i].getMainGunAmmo() > 0)
                        {
                            mGunAttack(range, team2[i], team1[team2[i].getTargetIndex()]);
                        }
                        if (team2[i].getSecondaryGunAmmo() > 0)
                        {
                            sGunAttack(range, team2[i], team1[team2[i].getTargetIndex()]);
                        }
                    }
                }

                for (int i = 0; i < team1.Count(); i++)
                {
                    if (!team1[i].getSunk() && !team1[i].getEscaped() && !team1[i].getDisabled())
                    {
                        Ship target = team2[team1[i].getTargetIndex()];
                        inflictDamage(target, team1[i]);
                    }
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    if (!team2[i].getSunk() && !team2[i].getEscaped() && !team2[i].getDisabled())
                    {
                        Ship target = team1[team2[i].getTargetIndex()];
                        inflictDamage(target, team2[i]);
                    }
                }

                for (int i = 0; i < team1.Count(); i++)
                {
                    if (!team1[i].getSunk() && !team1[i].getEscaped() && !team2[i].getDisabled())
                    {
                        displayCombatDamage(team1[i]);
                        displayShotsFired(team1[i]);
                    }
                }

                for (int i = 0; i < team2.Count(); i++)
                {
                    if (!team2[i].getSunk() && !team2[i].getEscaped() && !team2[i].getDisabled())
                    {
                        displayCombatDamage(team2[i]);
                        displayShotsFired(team2[i]);
                    }
                }

                nextTurn();
            }

            for (int i = 0; i < team1.Count(); i++) { displayStats(team1[i].getSunk(), team1[i].getHitsTaken(), "Kanmusu " + i + " : "); }
            for (int i = 0; i < team2.Count(); i++) { displayStats(team2[i].getSunk(), team2[i].getHitsTaken(), "Abyssal " + i + " : "); }
            loadShip.turnsTaken(battleDuration);

        }

        private void mGunAttack(string range, Ship ship, Ship target)
        {
            int mFirepower = 0;
            if (ship.getMainGunBeltPen("HE") > target.getBeltArmour())
            {
                ship.setMRange(range);
                mFirepower = ship.getMainGunFirepower()[1];
            }
            else
            {
                ship.setMRange(range);
                mFirepower = ship.getMainGunFirepower()[0];
            }

            double superstuctureAccuracy = ship.getBattleSuperstructure();
            if (superstuctureAccuracy < 0) { superstuctureAccuracy = 0; }
            double mAccuracy = (ship.getMainGunAccuracy(range) * superstuctureAccuracy) / 100;

            List<int> shotsHit = new List<int>();

            if (ship.getGunsLoaded() >= ship.getMainGunReloadSpeed())
            {
                for (int i = 0; i < ship.getNumMainGuns(); i++)
                {
                    ship.addShotFired();
                    ship.setMainGunAmmo(1);

                    if ((rnd.NextDouble() * 100) < mAccuracy)
                    {
                        shotsHit.Add(mFirepower);
                        target.setMShotsTaken();
                    }
                }
                ship.fireMainGuns();
            }
            else { ship.incrementMainGunReload(); }



            ship.setMGunHits(shotsHit);
        }

        private void sGunAttack(string range, Ship ship, Ship target)
        {
            int sFirepower = 0;
            if (ship.getSecondaryGunBeltPen("HE") > target.getBeltArmour())
            {
                ship.setSRange("HE");
                sFirepower = ship.getSecondaryGunFirepower()[1];
            }
            else
            {
                ship.setSRange(range);
                sFirepower = ship.getSecondaryGunFirepower()[0];
            }

            double superstuctureAccuracy = ship.getBattleSuperstructure();
            if (superstuctureAccuracy < 0) { superstuctureAccuracy = 0; }
            double sAccuracy = (ship.getSecondaryGunAccuracy(range) * superstuctureAccuracy) / 100;

            List<int> shotsHit = new List<int>();

            for (int i = 0; i < ship.getNumSecondaryGuns(); i++)
            {
                ship.addShotFired();
                ship.setSecondaryGunAmmo(1);

                if ((rnd.NextDouble() * 100) < sAccuracy)
                {
                    shotsHit.Add(sFirepower);
                    target.setSShotsTaken();
                }
            }

            ship.setSGunHits(shotsHit);
        }

        private void inflictDamage(Ship target, Ship attacker)
        {
            WeightedRandom<string> tmpProbability = new WeightedRandom<string>();

            string hitLocation = "";

            for (int i = 0; i < attacker.getMGunHits().Count(); i++)
            {
                rangeHitProbability.TryGetValue(range, out tmpProbability);
                hitLocation = tmpProbability.GetRandom();
                if (hitLocation == "Superstructure")
                {
                    if (attacker.getMainGunBeltPen(attacker.getMRange()) > target.getSuperstructureArmour())
                    {
                        target.setBattleSuperstructure(target.getBattleSuperstructure() - attacker.getMGunHits()[i]);
                        target.setHitsTaken(hitLocation);
                    }
                    else { target.setNonPenHitsTaken(hitLocation); target.setNonPenMShotsTaken(); }
                }
                else if (hitLocation == "Belt")
                {
                    if (attacker.getMainGunBeltPen(attacker.getMRange()) > target.getBeltArmour())
                    {
                        target.setBattleFlotation(target.getBattleFlotation() - attacker.getMGunHits()[i]);
                        target.setHitsTaken(hitLocation);
                    }
                    else { target.setNonPenHitsTaken(hitLocation); target.setNonPenMShotsTaken(); }
                }
                else if (hitLocation == "Deck")
                {
                    if (attacker.getMainGunBeltPen(attacker.getMRange()) > target.getDeckArmour())
                    {
                        target.setBattleFlotation(target.getBattleFlotation() - attacker.getMGunHits()[i]);
                        target.setHitsTaken(hitLocation);
                    }
                    else { target.setNonPenHitsTaken(hitLocation); target.setNonPenMShotsTaken(); }
                }
                else
                {
                    target.setBattleFlotation(target.getBattleFlotation() - attacker.getMGunHits()[i]);
                    target.setHitsTaken(hitLocation);
                }
            }

            for (int i = 0; i < attacker.getSGunHits().Count(); i++)
            {
                rangeHitProbability.TryGetValue(range, out tmpProbability);
                hitLocation = tmpProbability.GetRandom();
                if (hitLocation == "Superstructure")
                {
                    if (attacker.getSecondaryGunBeltPen(attacker.getSRange()) > target.getSuperstructureArmour())
                    {
                        target.setBattleSuperstructure(target.getBattleSuperstructure() - attacker.getSGunHits()[i]);
                        target.setHitsTaken(hitLocation);
                    }
                    else { target.setNonPenHitsTaken(hitLocation); target.setNonPenSShotsTaken(); }
                }
                else if (hitLocation == "Belt")
                {
                    if (attacker.getSecondaryGunBeltPen(attacker.getSRange()) > target.getBeltArmour())
                    {
                        target.setBattleFlotation(target.getBattleFlotation() - attacker.getSGunHits()[i]);
                        target.setHitsTaken(hitLocation);
                    }
                    else { target.setNonPenHitsTaken(hitLocation); target.setNonPenSShotsTaken(); }
                }
                else if (hitLocation == "Deck")
                {
                    if (attacker.getSecondaryGunBeltPen(attacker.getSRange()) > target.getDeckArmour())
                    {
                        target.setBattleFlotation(target.getBattleFlotation() - attacker.getSGunHits()[i]);
                        target.setHitsTaken(hitLocation);
                    }
                    else { target.setNonPenHitsTaken(hitLocation); target.setNonPenSShotsTaken(); }
                }
                else
                {
                    target.setBattleFlotation(target.getBattleFlotation() - attacker.getSGunHits()[i]);
                    target.setHitsTaken(hitLocation);
                }
            }
        }

        private void displayShotsFired(Ship ship)
        {
            int numGuns = ship.getShotsFired();
            int shotsHit = ship.getMGunHits().Count() + ship.getSGunHits().Count();
            loadShip.displayShotsFired(ship.getName(), numGuns, shotsHit, ship.getTarget(), ship.getMainGunAmmo(), ship.getSecondaryGunAmmo());
        }

        private void displayCombatDamage(Ship ship)
        {
            double flotation = ship.getBattleFlotation();
            double superstructure = ship.getBattleSuperstructure();

            loadShip.displayCombatDamage(flotation, superstructure, ship.getName(), ship.getMShotsTaken(), ship.getSShotsTaken(), ship.getNonPenMShotsTaken(), ship.getNonPenSShotsTaken());
        }

        private void displayStats(bool sunk, Dictionary<string, int> team, string teamName) { loadShip.displayStats(sunk, team, teamName); }

        private void shipEscaped(string shipname) { loadShip.shipEscaped(shipname); }
        private void rangeChange(string range){ loadShip.rangeChanged(range); }

        private void nextTurn()
        {
            int t1SunkCounter = 0;
            int t2SunkCounter = 0;
            for (int i = 0; i < team1.Count(); i++)
            {
                team1[i].clearShotsTaken();
                team1[i].clearShotsHit();
                if (team1[i].getBattleFlotation() < 0) { team1[i].setSunk(); t1SunkCounter++; }
                else if (team1[i].getBattleSuperstructure() < 0) { team1[i].setDisabled(); t1SunkCounter++; }
                else if (team1[i].getEscaped()) { t1SunkCounter++; }
            }

            for (int i = 0; i < team2.Count(); i++)
            {
                team2[i].clearShotsTaken();
                team2[i].clearShotsHit();
                if (team2[i].getBattleFlotation() < 0) { team2[i].setSunk(); t2SunkCounter++; }
                else if (team2[i].getBattleSuperstructure() < 0) { team2[i].setDisabled(); t2SunkCounter++; }
                else if(team2[i].getEscaped()) { t2SunkCounter++; }
            }

            if (t1SunkCounter == team1.Count()) { team1InCombat = false; team2InCombat = false; }
            if (t2SunkCounter == team2.Count()) { team1InCombat = false; team2InCombat = false; }
            battleDuration++;
        }
    }
}
