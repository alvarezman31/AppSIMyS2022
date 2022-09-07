using AppSIMyS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppSIMyS.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}