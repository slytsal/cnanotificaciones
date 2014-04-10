using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nosc.ViewModel;
using System.Collections.ObjectModel;
using Nosc.Model;
using System.Windows;
using Nosc.Repository;

namespace Nosc.UI
{
    public class Recurrence : IRecurrence
    {
        public void GetRecurrence(ObservableCollection<NotificacionModel> notifs)
        {
            
            NotificationRepository _NotificationRepository = new NotificationRepository();
            long fechaSystema = long.Parse(String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", DateTime.Now).Replace(":", ""));

            if (notifs.Count() !=0)
            {
                notifs.ToList().ForEach(p =>
                {
                    if (this.GetFUMRecurrencia(p) <= fechaSystema)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.GetParetWindows().GetScreenActive();
                            //actualizar la fecha por la del sistema
                            _NotificationRepository.UpdateNotificationRecurrencia(
                                new Model.NotificacionModel() 
                                {
                                    IdNotificacion = p.IdNotificacion,
                                    Recurrencia =p.Recurrencia,
                                    FechaUltimaMuestra = new UNID().getNewUNID()
                                });
                        }));
                    }
                });

            }
        }

        public MainNotif GetParetWindows()
        {
            MainNotif res = null;
            try
            {
                object query = Application.Current.Windows[0];
                res = query as MainNotif;
            }
            catch (Exception)
            {
                ;
            }
            return res;
        }

        public long GetFUMRecurrencia(NotificacionModel nof)
        {
            long result = 0;
            long fechaUM = nof.FechaUltimaMuestra;
            if (fechaUM !=0)
            {
                string convertStrig = null;
                string YYYY = null;
                string MM = null;
                string dd = null;
                string HH = null;
                string mm = null;
                string ss = null;
                string fff = null;
                string convertDateTime = null;
                //yyyy:MM:dd:HH:mm:ss:fff
                convertStrig = fechaUM.ToString();

                YYYY = convertStrig.Substring(0, 4);//YYYY
                MM = convertStrig.Substring(4, 2);//MM
                dd = convertStrig.Substring(6, 2);//dd
                HH = convertStrig.Substring(8, 2);//HH
                mm = convertStrig.Substring(10, 2);//mm
                ss = convertStrig.Substring(12, 2);//ss
                fff = convertStrig.Substring(14, 3);//fff

                convertDateTime = YYYY + "-" + MM + "-" + dd + " " + HH + ":" + mm + ":" + ss + "." + fff;

                DateTime convertTime = Convert.ToDateTime(convertDateTime);

                convertTime = convertTime.AddMinutes(nof.Recurrencia);

                result = long.Parse(String.Format("{0:yyyy:MM:dd:HH:mm:ss:fff}", convertTime).Replace(":", ""));
            }
            return result;
        }

        public void GetRefresh(ObservableCollection<NotificacionModel> notifs)
        {
            ObservableCollection<NotificacionModel> notifsMostradas = new ObservableCollection<NotificacionModel>();
            NotificationRepository _NotificationRepository = new NotificationRepository();

            if (notifs.Count() != 0)
            {
                int cont = 0;
                notifs.ToList().ForEach(p =>
                {
                    if (p.NotificacionActiva.Count() ==0)
                    {
                        cont++;
                        notifsMostradas.Add(p);
                    }

                });

                if (cont!=0)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.GetParetWindows().GetScreenActive();
                        }));

                    if (notifsMostradas.Count != 0)
                        _NotificationRepository.UpdateNotificationActiva(notifsMostradas);
                }

            }
        }

    }
}
