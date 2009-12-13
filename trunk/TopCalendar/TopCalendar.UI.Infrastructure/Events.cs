﻿using System;
using Microsoft.Practices.Composite.Presentation.Events;
using TopCalendar.Client.DataModel;
using TopCalendar.Utility.UI;
using Microsoft.Practices.Composite.Modularity;

namespace TopCalendar.UI.Infrastructure
{
	/// <summary>
	/// Event publikowany po wybraniu "Zakończ" z menu
	/// W Shellu musi być subskrybent, który go obsłuży i zamknie aplikację.
	/// W parametrze nic :)
	/// </summary>
    public class CloseAppEvent : CompositePresentationEvent<object>
    {
    }

	/// <summary>
	/// Event publikowany przez moduly, ktore chca, zeby je wyladowac z regionu
	/// W parametrze obiekt widoku do wywalenia
	/// </summary>
	public class UnloadViewEvent : CompositePresentationEvent<IView>
	{
	}

	/// <summary>
	/// Event publikowany przez moduly, ktore chca, zeby je calkiem wywalic z pamieci
	/// W parametrze obiekt modulu do wywalenia
	/// </summary>
	public class UnloadModuleEvent : CompositePresentationEvent<IModule>
	{
	}

	/// <summary>
	/// Event publikowany po pomyslnym zakonczeniu rejestracji.
	/// W parametrze Login
	/// </summary>
    public class RegistrationCompletedEvent : CompositePresentationEvent<string>
    {
    }

    /// <summary>
    /// Event publikowany gdy ma pojawić się okno nowego zadania
    /// W parametrze data początkowa zadania
    /// </summary>
    public class ShowAddNewTaskViewEvent : CompositePresentationEvent<DateTime?>
    {
    }

    /// <summary>
    /// Event is Published if new task is added to repository
    /// </summary>
    public class NewTaskAddedEvent : CompositePresentationEvent<Task>
	{
	}

	/// <summary>
	/// Event publikowany po wybraniu Managera pluginow z menu
	/// W parametrze nic :)
	/// </summary>
	public class ShowPluginsEvent : CompositePresentationEvent<object>
	{ 
	}
}