//using System.Collections.Generic;
//using System.Drawing;
//using System.Windows.Forms;

//namespace WOW_Addon_manager
//{
//    public class DataGridViewThreeStateCheckBoxColumn : DataGridViewImageColumn
//    {
//        public DataGridViewThreeStateCheckBoxColumn(ImageList images, bool isThreeState)
//        {
//            this.CellTemplate = new DataGridViewThreeStateCheckBoxCell(images, isThreeState, true);
//        }
//    }

//    public enum eState { Unknown = 0, Positive = 1, Negative = 2 }
//    public class DataGridViewThreeStateCheckBoxCell : DataGridViewImageCell
//    {
//        private Dictionary<eState, Image> StateToImage { get; set; }

//        private bool IsReadOnly { get; set; }

//        private eState cellState;
//        public eState CellState
//        {
//            get
//            {
//                return cellState;
//            }
//            set
//            {
//                cellState = value;
//                this.Value = StateToImage[cellState];
//            }
//        }

//        public DataGridViewThreeStateCheckBoxCell()
//        {
//        }

//        public DataGridViewThreeStateCheckBoxCell(ImageList images, bool isThreeState, bool isReadOnly)
//        {
//            StateToImage = new Dictionary<eState, Image> { { eState.Unknown, images.Images["question-mark"] }, { eState.Positive, images.Images["plus"] } };
//            if (isThreeState)
//                StateToImage[eState.Negative] = images.Images["minus"];
//            CellState = eState.Unknown;
//            IsReadOnly = isReadOnly;
//        }

//        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
//        {
//            if (IsReadOnly)
//                return;

//            // toggle the cell state
//            CellState = ((eState)(((int)CellState + 1) % StateToImage.Count));

//            base.OnMouseClick(e);
//        }
//    }
//}

