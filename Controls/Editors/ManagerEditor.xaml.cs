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
    /// <summary>
    /// Interaction logic for ManagerEditor.xaml
    /// </summary>
    public partial class ManagerEditor : UserControl
    {
        public ManagerEditor()
        {
            InitializeComponent();
        }

        private void SaveAndCancelButtons_SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sender is not SaveAndCancelButtons saveAndCancelButtons)
                return;

            if (string.IsNullOrEmpty(NameField.Text))
                RequiredName.Show();
            else
            {
                RequiredName.Hide();
                saveAndCancelButtons.SaveShanges();
            }
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            RequiredName.Hide();
            SaveCancelButtons.HideSuccessfullyText();
        }
    }
}
