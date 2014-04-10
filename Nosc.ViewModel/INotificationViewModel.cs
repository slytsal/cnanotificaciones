using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Nosc.ViewModel
{
    public interface INotificationViewModel
    {
        ObservableCollection<INotificationViewModel> Childrens {get;}
    }
}
