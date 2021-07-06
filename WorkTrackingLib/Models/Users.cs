using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTrackingLib
{
    /// <summary>
    /// Contains fields and methods for getting the full name of the user authorized in the system 
    /// </summary>
    public class Users
    {
        #region Properties

        /// <summary>
        /// Username property
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Property ID of the selected order
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Message property
        /// </summary>
        public string Msg { get; set; } = string.Empty;

        #endregion

        #region Constructors

        public Users(string displayName, int orderId)
        {
            this.DisplayName = displayName;
            this.OrderId = orderId;
        }

        public Users()
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// Method for getting data on the user running the program
        /// </summary>
        /// <returns></returns>
        public static string GetAccaunt()
        {
            return UserPrincipal.Current.DisplayName;

            #region Getting a position (not used at the moment)

            //// получение данных о пользователе из AD
            //DirectoryEntry dirEntr = userName.GetUnderlyingObject() as DirectoryEntry;

            //// Условие совпадения должности
            //if (dirEntr.Properties.Contains("Title"))
            //{
            //    // ПРисвоение переменной значения должности пользователя
            //    var title = dirEntr.Properties["Title"].Value.ToString();

            //    // Условие, при котором дается доступ к модулю управления пользователю (Если должность содержится в списке доверенных должностей пользователь получит доступ)
            //    if (MainViewModel.AccauntList.Contains(title))
            //    {
            //        buttonVis = Visibility.Visible;
            //    }
            //    else
            //    {
            //        buttonVis = Visibility.Hidden;
            //    }

            //}
            //else
            //{
            //    buttonVis = Visibility.Hidden;
            //}

            #endregion

        }

        #endregion
    }
}
