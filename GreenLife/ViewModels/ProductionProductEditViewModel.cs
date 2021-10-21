using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenLife.ViewModels
{
    public class ProductionProductEditViewModel:ProductionProductViewModel
    {
        private int _ExistingID;
        private string _ExistingPhoto;
        
        public int ExistingID
        {

            get { return _ExistingID; }
            set { this._ExistingID = value; }
        }
        public string? ExistingPhtopathEdit
        {

            get { return _ExistingPhoto; }
            set { this._ExistingPhoto = value; }
        }
    }
}
