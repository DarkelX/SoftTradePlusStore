using SoftTradePlusStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftTradePlusStore.Controls
{
    public partial class SaveAndCancelButtons : UserControl
    {
        public event RoutedEventHandler SaveButtonClicked;
        public SaveAndCancelButtons()
        {
            InitializeComponent();
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveButtonClicked?.Invoke(this, e);
            Disable();
        }

        public void SaveShanges()
        {
            var dataBase = DataManager.GetInstance();

            var viewModel = (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.ViewModel;
            var selectedItem = viewModel?.SelectedItem;

            if (viewModel == null || selectedItem == null) return;

            if (selectedItem.Id > 0)//TODO:Refactor
            {
                dataBase.SaveChanges();
            }
            else
            {
                dataBase.CreateItem(selectedItem);
                viewModel.Refresh();
            }

            SavedSuccessfullyText.Visibility = Visibility.Visible;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(App.Current.MainWindow) as MainWindow;
            var viewModel = mainWindow?.ViewModel;
            viewModel?.Reload();
            mainWindow?.UpdateEditor();
            Disable();
        }

        public void CheckForEnable(object sender)
        {
            if (sender is not Control control)
                return;

            if (control.IsFocused || control.IsMouseCaptured)
                IsEnabled = true;
        }

        private void Disable()
        {
            IsEnabled = false;
        }
    }
}
