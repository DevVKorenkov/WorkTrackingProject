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
using System.Windows.Controls.Primitives;
using WorkTrackingLib.Models;

namespace NewWorkTracking
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string toolTipText;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LeftMenuButton_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = sender as Button;

            GetButtonName(button);

            popupUC.Placement = PlacementMode.Right;
            popupUC.IsOpen = true;
            Header.PopupText.Text = toolTipText;
        }

        private void LeftMenuButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popupUC.Visibility = Visibility.Collapsed;
            popupUC.IsOpen = false;
        }

        private void GetButtonName(Button button)
        {
            switch (button.Name)
            {
                case "UserWorksButton":
                    toolTipText = "Заявки пользователя";
                    popupUC.PlacementTarget = UserWorksButton;
                    break;
                case "AllOrdersButton":
                    toolTipText = "Все заявки";
                    popupUC.PlacementTarget = AllOrdersButton;
                    break;
                case "AllRepairsButton":
                    toolTipText = "Все ремонты";
                    popupUC.PlacementTarget = AllRepairsButton;
                    break;
                case "DevicesButton":
                    toolTipText = "Устройства";
                    popupUC.PlacementTarget = DevicesButton;
                    break;
                case "AdministrateButton":
                    toolTipText = "Администрирование";
                    popupUC.PlacementTarget = AdministrateButton;
                    break;
                case "ChangeServerButton":
                    toolTipText = "Сменить сервер";
                    popupUC.PlacementTarget = ChangeServerButton;
                    break;
            }
        }      
    }
}
