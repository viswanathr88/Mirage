using Mirage.ViewModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Mirage.View
{
    public class DataPage<TNav> : Page
    {
        public DataPage()
        {

        }

        public TNav Parameter
        {
            get;
            private set;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Ensure DataContext has already been set
            if (this.DataContext == null)
            {
                return;
            }

            // Ensure DataContext derives from IDataViewModel
            IDataViewModel vm = this.DataContext as IDataViewModel;

            if (vm == null)
            {
                return;
            }

            // Validate input Parameter
            vm.LoadAsync(e.Parameter, false);
        }
    }
}
