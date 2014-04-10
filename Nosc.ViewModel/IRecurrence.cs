using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Nosc.Model;

namespace Nosc.ViewModel
{
    public interface IRecurrence
    {
        void GetRefresh(ObservableCollection<NotificacionModel> notifs);

        void GetRecurrence(ObservableCollection<NotificacionModel> notifs);
    }
}
