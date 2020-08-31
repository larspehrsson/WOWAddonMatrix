using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using WOW_Addon_Matrix;

namespace WOW_Addon_manager
{
    public partial class Form1 : Form
    {
        private const int offset = 3;

        private const string userRoot = "HKEY_CURRENT_USER";
        private const string subkey = @"Software\Wow AddOn Manager";
        private const string keyName = userRoot + "\\" + subkey;
        public List<AddOnData> SortAddOnCollection = new List<AddOnData>();
        public List<spiller> Spillere = new List<spiller>();

        private string wowpath = "";

        public Form1()
        {
            InitializeComponent();
        }

        // Load formular. Loader spilleroversigt
        private void Form1_Load(object sender, EventArgs e)
        {
            wowpath = (string)Registry.GetValue(keyName, "WOW Home Folder", "");
            if (wowpath == "" || !Directory.Exists(wowpath + "\\WTF\\"))
                while (true)
                {
                    var browser = new FolderBrowserDialog
                    {
                        Description = "Mark home folder for WOW (where WTF and Interface is located)"
                    };
                    browser.ShowDialog();
                    wowpath = browser.SelectedPath;
                    if (!Directory.Exists(wowpath + "\\WTF\\"))
                    {
                        MessageBox.Show("The folder WTF was not found in this folder " + wowpath, "Error in input",
                            MessageBoxButtons.OK);
                    }
                    else
                    {
                        Registry.SetValue(keyName, "WOW Home Folder", wowpath);
                        break;
                    }
                }

            var account = (string)Registry.GetValue(keyName, "Account", "");
            var fileEntries = Directory.GetDirectories(wowpath + @"\WTF\Account\", "*");
            foreach (var fileName in fileEntries)
            {
                var parts = fileName.Split('\\');
                var spillernavn = parts[parts.Count() - 1];

                AccountComboBox.Items.Add(spillernavn);
            }

            AccountComboBox.SelectedItem = account;
            AccountComboBox.Focus();
        }

        private void ScanAccount(string account, string realm)
        {
            Cursor = Cursors.WaitCursor;
            Spillere.Clear();
            SortAddOnCollection.Clear();
            dataGridView1.ClearSelection();

            ScanAddons();
            ScanTemplate(wowpath + @"\WTF\account\addontemplate.txt");
            ScanPlayers(wowpath + @"\WTF\account\" + account + @"\" + realm, 0);

            versiondb.DataSource = SortAddOnCollection.Select(s => s.xinterface).OrderBy(s => s).Distinct().ToList();
            dataGridView1.DataSource = SortAddOnCollection.Select(s => s).OrderBy(s => s.navn).ToList();
            ;

            dataGridView1.AutoSize = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;

            dataGridView1.Columns[0].Visible = false; // indeks
            dataGridView1.Columns[1].MinimumWidth = 150; // addon navn
            dataGridView1.Columns[2].MinimumWidth = 50; // version
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].HeaderText = "Version";

            //ImageList imglist = new ImageList();
            //imglist.Images.Add("plus", Image.FromFile("plus.png"));
            //imglist.Images.Add("minus", Image.FromFile("minus.png"));
            //imglist.Images.Add("question-mark", Image.FromFile("question-mark.png"));
            // Skjul kolonner uden spillere
            for (var i = 0; i < 12; i++)
                if (i >= Spillere.Count)
                {
                    dataGridView1.Columns[i + offset].Visible = false;
                }
                else
                {
                    dataGridView1.Columns[i + offset].MinimumWidth = 70;
                    dataGridView1.Columns[i + offset].HeaderText = Spillere[i].name;
                    dataGridView1.Columns[i + offset].HeaderCell.Style.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Columns[i + offset].Visible = true;

                    //dataGridView1.Columns[i + offset].CellTemplate = new DataGridViewThreeStateCheckBoxCell(imglist, false, false);
                }

            dataGridView1.Columns[15].HeaderText = "Dependencies";
            dataGridView1.Columns[15].MinimumWidth = 100;

            //DataGridViewThreeStateCheckBoxColumn column = new DataGridViewThreeStateCheckBoxColumn(imglist, false);
            //dataGridView1.Columns.Add(column);
            //dataGridView1.Columns[16].DataPropertyName = "enabled11";

            Cursor = Cursors.Default;
        }

        // Der er klikket på et felt. Gem det og kør "CellValueChange" eventen
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        //
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var enabled = (bool)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                var række = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;

                if (SortAddOnCollection[række].dependencies != null && enabled)
                {
                    CheckDependencies2(række, e.ColumnIndex - offset);
                    dataGridView1.Refresh();
                }
            }
        }

        // Gem addons + template
        private void OpdaterBtn_Click(object sender, EventArgs e)
        {
            foreach (var s in Spillere)
                using (var writer = new StreamWriter(s.filename))
                {
                    string line;
                    foreach (var f in SortAddOnCollection.Where(a => a.fundet))
                    {
                        var enabled = false;
                        switch (s.index)
                        {
                            case 0:
                                enabled = f.enabled0;
                                break;

                            case 1:
                                enabled = f.enabled1;
                                break;

                            case 2:
                                enabled = f.enabled2;
                                break;

                            case 3:
                                enabled = f.enabled3;
                                break;

                            case 4:
                                enabled = f.enabled4;
                                break;

                            case 5:
                                enabled = f.enabled5;
                                break;

                            case 6:
                                enabled = f.enabled6;
                                break;

                            case 7:
                                enabled = f.enabled7;
                                break;

                            case 8:
                                enabled = f.enabled8;
                                break;

                            case 9:
                                enabled = f.enabled9;
                                break;

                            case 10:
                                enabled = f.enabled10;
                                break;

                            case 11:
                                enabled = f.enabled11;
                                break;
                        }

                        line = f.navn.Replace(" ", "") + ": " + (enabled ? "enabled" : "disabled");
                        writer.WriteLine(line);
                    }

                    writer.Close();
                }
        }

        // Fjerner alle markeringer bortset fra template kolonnen
        private void clearbtn_Click(object sender, EventArgs e)
        {
            foreach (var f in SortAddOnCollection)
            {
                f.enabled1 = false;
                f.enabled2 = false;
                f.enabled3 = false;
                f.enabled4 = false;
                f.enabled5 = false;
                f.enabled6 = false;
                f.enabled7 = false;
                f.enabled8 = false;
                f.enabled9 = false;
                f.enabled10 = false;
                f.enabled11 = false;
            }

            dataGridView1.Refresh();
        }

        // Klik på "tjek dependencies" knappen
        private void dependbtn_Click(object sender, EventArgs e)
        {
            for (var i = 0; i <= 11; i++) CheckDependencies2(spiller: i);
            dataGridView1.Refresh();
        }

        // Tjekker alle dependencies og enabler addons som andre er afhængige af.
        private void SetDependenciesRecursive(int række, int spiller)
        {
            switch (spiller)
            {
                case 0:
                    SortAddOnCollection[række].enabled0 = true;
                    break;

                case 1:
                    SortAddOnCollection[række].enabled1 = true;
                    break;

                case 2:
                    SortAddOnCollection[række].enabled2 = true;
                    break;

                case 3:
                    SortAddOnCollection[række].enabled3 = true;
                    break;

                case 4:
                    SortAddOnCollection[række].enabled4 = true;
                    break;

                case 5:
                    SortAddOnCollection[række].enabled5 = true;
                    break;

                case 6:
                    SortAddOnCollection[række].enabled6 = true;
                    break;

                case 7:
                    SortAddOnCollection[række].enabled7 = true;
                    break;

                case 8:
                    SortAddOnCollection[række].enabled8 = true;
                    break;

                case 9:
                    SortAddOnCollection[række].enabled9 = true;
                    break;

                case 10:
                    SortAddOnCollection[række].enabled10 = true;
                    break;

                case 11:
                    SortAddOnCollection[række].enabled11 = true;
                    break;
            }

            if (SortAddOnCollection[række].dependencies != null)
                foreach (var depend in SortAddOnCollection[række].dependencies.Split(','))
                    foreach (var s in SortAddOnCollection.Where(a => a.navn == depend))
                        SetDependenciesRecursive(s.indeks, spiller);
        }

        private void CheckDependencies2(int række = -1, int spiller = -1)
        {
            foreach (var f in SortAddOnCollection.Where(s => s.indeks == række || række == -1))
            {
                var enabled = false;
                switch (spiller)
                {
                    case 0:
                        enabled = f.enabled0;
                        break;

                    case 1:
                        enabled = f.enabled1;
                        break;

                    case 2:
                        enabled = f.enabled2;
                        break;

                    case 3:
                        enabled = f.enabled3;
                        break;

                    case 4:
                        enabled = f.enabled4;
                        break;

                    case 5:
                        enabled = f.enabled5;
                        break;

                    case 6:
                        enabled = f.enabled6;
                        break;

                    case 7:
                        enabled = f.enabled7;
                        break;

                    case 8:
                        enabled = f.enabled8;
                        break;

                    case 9:
                        enabled = f.enabled9;
                        break;

                    case 10:
                        enabled = f.enabled10;
                        break;

                    case 11:
                        enabled = f.enabled11;
                        break;
                }

                if (enabled)
                    SetDependenciesRecursive(f.indeks, spiller);
            }
        }

        // Resetter alle til templaten
        private void TemplateResetBtn_Click(object sender, EventArgs e)
        {
            foreach (var f in SortAddOnCollection)
            {
                f.enabled1 = f.enabled0;
                f.enabled2 = f.enabled0;
                f.enabled3 = f.enabled0;
                f.enabled4 = f.enabled0;
                f.enabled5 = f.enabled0;
                f.enabled6 = f.enabled0;
                f.enabled7 = f.enabled0;
                f.enabled8 = f.enabled0;
                f.enabled9 = f.enabled0;
                f.enabled10 = f.enabled0;
                f.enabled11 = f.enabled0;
            }

            dataGridView1.Refresh();
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 2)
            {
                dataGridView1.DataSource = SortAddOnCollection.Select(s => s).OrderBy(s => s.xinterface).ToList();
                ;
                dataGridView1.Refresh();
            }

            if (e.RowIndex == -1 && e.ColumnIndex == 1)
            {
                dataGridView1.DataSource = SortAddOnCollection.Select(s => s).OrderBy(s => s.navn).ToList();
                ;
                dataGridView1.Refresh();
            }
        }

        // doublick i margen sætter alle kolonner til template
        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == -1)
            {
                var række = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                SortAddOnCollection[række].enabled1 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled2 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled3 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled4 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled5 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled6 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled7 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled8 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled9 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled10 = SortAddOnCollection[række].enabled0;
                SortAddOnCollection[række].enabled11 = SortAddOnCollection[række].enabled0;

                for (var i = 0; i <= 11; i++) CheckDependencies2(række, i);
                dataGridView1.Refresh();
            }
        }

        // Valg af spillerkonto
        private void AccountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Registry.SetValue(keyName, "Account", AccountComboBox.Text);

            RealmComboBox.Items.Clear();
            var fileEntries = Directory.GetDirectories(wowpath + @"\WTF\Account\" + AccountComboBox.Text, "*");
            foreach (var fileName in fileEntries)
            {
                var parts = fileName.Split('\\');
                var realmnavn = parts[parts.Count() - 1];
                if (realmnavn != "SavedVariables")
                    RealmComboBox.Items.Add(realmnavn);
            }

            var realm = (string)Registry.GetValue(keyName, "Realm", "");

            RealmComboBox.SelectedItem = realm;
        }

        // Valg af REALM
        private void RealmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Registry.SetValue(keyName, "Realm", RealmComboBox.Text);

            ScanAccount(AccountComboBox.Text, RealmComboBox.Text);
            AccountComboBox.SelectedText = "";
            dataGridView1.Focus();
        }

        // Valg af version
        private void versiondb_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource =
                SortAddOnCollection.Select(s => s)
                    .Where(s => s.xinterface >= Convert.ToInt32(versiondb.Text))
                    .OrderBy(s => s.navn)
                    .ToList();
            dataGridView1.Focus();
        }

        // Mouseover for at vise beskrivelse
        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex > -1)
                e.ToolTipText += string.Format("{0}",
                    SortAddOnCollection[(int)dataGridView1.Rows[e.RowIndex].Cells[0].Value]
                        .notes);
        }
    }
}