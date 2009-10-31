using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Presentation.Commands;
using Ninject;
using TopCalendar.Utility.BasicExtensions;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.Registration
{
	public class RegistrationPresentationModel 
		: PresentationModelFor<IRegistrationView>, IRegistrationPresentationModel
    {
    	private readonly IEventAggregator _eventAggregator;
    	private DelegateCommand<object> _registerCommand;

		[Inject]
		public ILoggerFacade Log { get; set; }

        public ICommand RegisterCommand
        {
            get { return _registerCommand; }
            private set
            {
                _registerCommand = value as DelegateCommand<object>;
                OnPropertyChanged("RegisterCommand");
            }
        }

    	public RegistrationPresentationModel(IRegistrationView view,IEventAggregator eventAggregator)
			:base(view)
    	{
    		_eventAggregator = eventAggregator;
    		_registerCommand = new DelegateCommand<object>(Register, CanRegister);
    		_cancelCommand = new DelegateCommand<object>(Cancel);
    		_view.ViewModel = this;
    	}

		private void Cancel(object obj)
		{
			_eventAggregator.GetEvent<ViewShouldDie<IRegistrationView>>()
				.Publish(View);
		}

		private DelegateCommand<object> _cancelCommand;

		public ICommand CancelCommand
		{
			get { return _cancelCommand; }
			set { _cancelCommand = (DelegateCommand<object>)value; }
		}

		private string _login;
    	public string Login
    	{
			get { return _login; }
			set { 
				_login = value;
				OnPropertyChanged("Login");
				_registerCommand.RaiseCanExecuteChanged();
			}
    	}

    	private string _password;
    	public string Password
    	{
    		get
    		{
    			return _password;
    		}
    		set { 
				_password = value; 
				OnPropertyChanged("Password");
				_registerCommand.RaiseCanExecuteChanged();
			}
    	}

    	private bool CanRegister(object arg)
    	{
    		return _login.IsNotEmpty() && _password.IsNotEmpty();
    	}

    	private void Register(object obj)
    	{
    		_eventAggregator.GetEvent<ViewShouldDie<IRegistrationView>>().Publish(View);
    		Log.Log(string.Format("{0}, {1} - zarejestrwonay", Login, Password), Category.Info, Priority.None);
    	}
    }
}