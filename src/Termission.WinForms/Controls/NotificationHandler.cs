using Eto.Drawing;
using Eto.Forms;
using System;
using swf = System.Windows.Forms;
using sd = System.Drawing;
using Eto;
using Eto.WinForms;
using Eto.WinForms.Forms;

namespace Juniansoft.Termission.WinForms.Controls
{
	public class NotificationHandler : WidgetHandler<swf.Control, Notification, Notification.ICallback>, Notification.IHandler
	{
		public string Message { get; set; }

		public bool RequiresTrayIndicator
		{
			get { return true; }
		}

		public string Title { get; set; }

		public string UserData { get; set; }

		public Image ContentImage { get; set; }

		static TrayIndicator s_sharedIndicator;

		TrayIndicator GetSharedIndicator()
		{
			if (s_sharedIndicator == null)
			{
				s_sharedIndicator = new TrayIndicator
				{
					Image = Application.Instance?.MainForm?.Icon ?? sd.SystemIcons.Application.ToEto()
				};
			}
			s_sharedIndicator.Show();
			return s_sharedIndicator;
		}

		static readonly object NotificationHandler_Key = new object();

		public void Show(TrayIndicator indicator = null)
		{
			indicator = indicator ?? GetSharedIndicator();
			var notifyIcon = TrayIndicatorHandler.GetControl(indicator);

			var currentNotification = indicator.Properties.Get<NotificationHandler>(NotificationHandler_Key);
			if (currentNotification != null)
			{
				currentNotification.Unhook(notifyIcon);
				indicator.Properties.Remove(NotificationHandler_Key);
			}

			notifyIcon.ShowBalloonTip(3000, Title, Message, swf.ToolTipIcon.None);
			notifyIcon.BalloonTipClicked += Tray_BalloonTipClicked;
			notifyIcon.BalloonTipClosed += Tray_BalloonTipClosed;
			indicator.Properties.Set(NotificationHandler_Key, this);
		}

		void Unhook(swf.NotifyIcon notifyIcon)
		{
			if (notifyIcon == null)
				return;
			notifyIcon.BalloonTipClicked -= Tray_BalloonTipClicked;
			notifyIcon.BalloonTipClosed -= Tray_BalloonTipClosed;
			notifyIcon = null;
		}

		void Tray_BalloonTipClosed(object sender, EventArgs e)
		{
			Unhook(sender as swf.NotifyIcon);
			s_sharedIndicator?.Hide();
		}

		void Tray_BalloonTipClicked(object sender, EventArgs e)
		{
			Unhook(sender as swf.NotifyIcon);
			s_sharedIndicator?.Hide();

			var app = ApplicationHandler.Instance;
			app?.Callback.OnNotificationActivated(app.Widget, new NotificationEventArgs(ID, UserData));
		}
	}
}
