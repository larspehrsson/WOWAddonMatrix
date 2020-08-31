using System;
using System.IO;
using System.Linq;
using WOW_Addon_Matrix;

namespace WOW_Addon_manager
{
    partial class Form1
    {
        // Find template eller opret en hvis der ikke er en i forvejen
        public void ScanTemplate(string addontemplate)
        {
            if (File.Exists(addontemplate))
            {
                scanaddons(addontemplate);
                Spillere[0].name = "Template";
            }
            else
            {
                var chr = new spiller();
                chr.name = "Template";
                chr.filename = addontemplate;
                chr.index = 0;
                Spillere.Add(chr);
            }
        }

        // Scan addon katalog for addons
        private void ScanAddons()
        {
            string[] fileEntries = Directory.GetDirectories(wowpath + @"\Interface\Addons\", "*");
            foreach (string fileName in fileEntries)
            {
                string[] parts = fileName.Split(new[] {'\\'});
                string addonnavn = parts[parts.Count() - 1];

                bool opdatering;
                AddOnData add;
                TilføjAddon(addonnavn, out opdatering, out add);

                if (!opdatering && add.fundet)
                {
                    add.indeks = SortAddOnCollection.Count();
                    SortAddOnCollection.Add(add);
                }
            }
        }

        public void ScanPlayers(string sourceDir, int recursionLvl)
        {
            if (recursionLvl <= 3)
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(sourceDir, "AddOns.txt");
                foreach (string fileName in fileEntries)
                {
                    scanaddons(fileName);
                }

                // Recurse into subdirectories of this directory.
                string[] subdirEntries = Directory.GetDirectories(sourceDir);
                foreach (string subdir in subdirEntries)

                    // Do not iterate through reparse points
                    if ((File.GetAttributes(subdir) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                        ScanPlayers(subdir, recursionLvl + 1);
            }
        }

        private void addaddon(string filename, string line)
        {
            string addonname = line.Split(new[] {':'})[0].Trim();
            string toggled = line.Split(new[] {':'})[1].Trim();
            string[] parts = filename.Split(new[] {'\\'});
            string spillernavn = parts[parts.Count() - 2];

            // Har vi allerede spillernavnet på listen? I så fald find indekset
            int i = -1;
            foreach (spiller sp in Spillere)
                if (sp.name == spillernavn)
                {
                    i = Spillere.IndexOf(sp);
                    break;
                }

            // Opret spilleren på listen og returner indekset.
            if (i == -1)
            {
                i = Spillere.Count();
                var chr = new spiller();
                chr.name = spillernavn;
                chr.filename = filename;
                chr.index = i;
                Spillere.Add(chr);
            }

            bool opdatering;
            AddOnData add;
            TilføjAddon(addonname, out opdatering, out add);

            switch (i)
            {
                case 0:
                    add.enabled0 = (toggled == "enabled");
                    break;
                case 1:
                    add.enabled1 = (toggled == "enabled");
                    break;
                case 2:
                    add.enabled2 = (toggled == "enabled");
                    break;
                case 3:
                    add.enabled3 = (toggled == "enabled");
                    break;
                case 4:
                    add.enabled4 = (toggled == "enabled");
                    break;
                case 5:
                    add.enabled5 = (toggled == "enabled");
                    break;
                case 6:
                    add.enabled6 = (toggled == "enabled");
                    break;
                case 7:
                    add.enabled7 = (toggled == "enabled");
                    break;
                case 8:
                    add.enabled8 = (toggled == "enabled");
                    break;
                case 9:
                    add.enabled9 = (toggled == "enabled");
                    break;
                case 10:
                    add.enabled10 = (toggled == "enabled");
                    break;
                case 11:
                    add.enabled11 = (toggled == "enabled");
                    break;
                default:
                    break;
            }

            if (!opdatering)
            {
                add.indeks = SortAddOnCollection.Count();
                SortAddOnCollection.Add(add);
            }
        }

        private void TilføjAddon(string addonname, out bool opdatering, out AddOnData add)
        {
            // Har vi allerede AddOn'en i collectionen?
            opdatering = false;
            add = new AddOnData();
            foreach (AddOnData a in SortAddOnCollection)
            {
                if (a.navn == addonname)
                {
                    add = a;
                    opdatering = true;
                    break;
                }
            }

            // Lav en ny addon hvis den ikke allerede findes
            if (!opdatering)
            {
                add = new AddOnData();
                add.navn = addonname.Replace(" ", "");
                addonname = addonname.Replace(" ", "");
                string tocname = wowpath + @"\interface\AddOns\" + addonname + @"\\" + addonname + ".toc";
                if (File.Exists(tocname))
                {
                    add.fundet = true;
                    using (var reader = new StreamReader(tocname))
                    {
                        string indhold;
                        while ((indhold = reader.ReadLine()) != null)
                        {
                            if (indhold.StartsWith("## Interface:"))
                                add.xinterface = Convert.ToInt32(indhold.Substring(14));
                            if (indhold.StartsWith("## Notes:"))
                                add.notes = indhold.Substring(10);
                            if (indhold.StartsWith("## RequiredDeps: "))
                                add.dependencies = indhold.Substring(17);
                            if (indhold.StartsWith("## Dependencies: "))
                                add.dependencies = indhold.Substring(17);
                        }
                    }
                }
                else
                    add.fundet = false;
            }
        }

        private bool scanaddons(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    addaddon(filename, line);
                }
            }
            return true;
        }
    }
}