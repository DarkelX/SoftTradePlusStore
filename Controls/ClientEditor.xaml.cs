using System;
using SoftTradePlusStore.Models;
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
using SoftTradePlusStore.Data;

namespace SoftTradePlusStore.Controls
{
    /// <summary>
    /// Interaction logic for ClientEditor.xaml
    /// </summary>
    public partial class ClientEditor : UserControl
    {
        public ClientEditor()
        {
            InitializeComponent();
            Loaded += ClientEditor_Loaded;
        }

        private void ClientEditor_Loaded(object sender, RoutedEventArgs e)
        {
            var dataManager = DataManager.GetInstance();

            StatusComboBox.ItemsSource = GetStatuses();
            StatusComboBox.SelectedIndex = 0;

            ManagerComboBox.ItemsSource = dataManager.GetManagers();
            if(ManagerComboBox.Items.Count != 0)
                ManagerComboBox.SelectedIndex = 0;

            IndividualComboBox.ItemsSource = dataManager.GetIndividuals();
            if (IndividualComboBox.Items.Count != 0)
                IndividualComboBox.SelectedIndex = 0;
        }

        private static List<Client.ClientStatus> GetStatuses()
        {
            return Enum.GetValues<Client.ClientStatus>().ToList();
        }
    }
}
