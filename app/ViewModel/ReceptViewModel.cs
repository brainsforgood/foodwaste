using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Foodwaste.ViewModel
{
    public class ReceptViewModel : INotifyPropertyChanged
    {
        readonly IList<Product> source;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Product> Products { get; private set; }

        public ReceptViewModel() {
           source = new List<Product>();
           InitCollection();
        }

        private void InitCollection()
        {
            source.Add(new Product { Name = "Sperzie bonen", Expiry = DateTime.Now, ImageUrl = "https://static.ah.nl/dam/product/AHI_43545239383833393433?revLabel=1&rendition=200x200_JPG_Q85&fileType=binary" });
            source.Add(new Product { Name = "Prij", Expiry = DateTime.Now, ImageUrl = "https://static.ah.nl/dam/product/AHI_434d50303133303736?revLabel=3&rendition=200x200_JPG_Q85&fileType=binary" });
            Products = new ObservableCollection<Product>(source);
            
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
