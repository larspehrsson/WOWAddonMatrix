namespace WOW_Addon_Matrix
{
    public class AddOnData //: INotifyPropertyChanged
    {
        //public ObservableCollection<spiller> spillere;

        //public event PropertyChangedEventHandler PropertyChanged;

        //public bool all { get; set; }
        public bool fundet = false;
        public string notes; // { get; set; }

        public string spiller0; // { get; set; } // template
        public string spiller1; // { get; set; }
        public string spiller10; // { get; set; }
        public string spiller11; // { get; set; }
        public string spiller2; // { get; set; }
        public string spiller3; // { get; set; }
        public string spiller4; // { get; set; }
        public string spiller5; // { get; set; }
        public string spiller6; // { get; set; }
        public string spiller7; // { get; set; }
        public string spiller8; // { get; set; }
        public string spiller9; // { get; set; }
        public int indeks { get; set; }
        public string navn { get; set; }
        public int xinterface { get; set; }

        public bool enabled0 { get; set; } // template
        public bool enabled1 { get; set; }
        public bool enabled2 { get; set; }
        public bool enabled3 { get; set; }
        public bool enabled4 { get; set; }
        public bool enabled5 { get; set; }
        public bool enabled6 { get; set; }
        public bool enabled7 { get; set; }
        public bool enabled8 { get; set; }
        public bool enabled9 { get; set; }
        public bool enabled10 { get; set; }
        public bool enabled11 { get; set; }

        public string dependencies { get; set; }
    }
}