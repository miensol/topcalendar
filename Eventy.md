# Lista eventów #

Trzeba rzetelnie spisywać wszystkie Eventy, jakie dane moduły publikują i co z nich korzysta, bo nie połapiemy się w tym ;)

Wszystkie eventy powinny być zdefiniowane w `TopCalendar.UI.Infrastructure.Events` i muszą dziedziczyć po `CompositePresentationEvent<T>`.

Dodatkowo jesli chcemy aby jakiś przycisk, akcja publikowala event wystarczy użyć
[EventPublisherCommand](http://code.google.com/p/topcalendar/source/browse/trunk/TopCalendar/TopCalendar.UI.Infrastructure/CommonCommands/EventPublisherCommand.cs)

| **Event Name** | **Parametr** | **Opis** | **Moduł publikujący** | **Moduły subskrybujące** |
|:---------------|:-------------|:---------|:------------------------|:---------------------------|
| `CloseAppEvent` | - | Polecenie "Zakończ" z menu | Shell (menu) | Shell |
| `UnloadViewEvent` | _IView_ Obiekt widoku do wywalenia | Żądanie usunięcia widoku z UI | dowolny moduł | PluginManager |
| `UnloadModuleEvent` | _IModule_ Moduł do wywalenia | Żądanie usunięcia referencji do modułu | dowolny moduł | PluginManager |
| `RegistrationCompletedEvent` | _string_ Login nowego usera | Po rejestracji zakończonej sukcesem | Registration | MonthViewerModule |
| `ShowAddNewTaskViewEvent` | _DateTime_ data rozpoczęcia zadania  | Pokazuje w regionie MainContent formularz dodania nowego zadania  | Shell (menu) | TaskViewerModule |
| `ShowEditTaskViewEvent` | _Task_ zadanie do edycji  | Pokazuje w regionie MainContent formularz edytowania nowego zadania  | MonthViewerModule(Button edycji przy zadaniu) | TaskViewerModule |
| `NewTaskAddedEvent` | _Task_ dodane zadanie | Event jest publikowany po pomyślnym dodaniu zadania do repozytorium | tylko Repozytorium | MonthViewerModule |
| `ShowPluginsEvent` | - | Polecenie "Manager pluginów" z menu | Shell (menu) | PluginsModule |
| ... |   |  |  |  |

# Eventy w menu #

Eventy wpina się do menu w następujący sposób:

  1. pobieramy instancję `IMenuManager` z service locatora
  1. wywołujemy `AddTopLevelMenu("identyfikator", "tekst wyświetlany")` jeśli chemy menu głównego poziomu
  1. wywołujemy `AddItemToMenu<TypEventa>("identyfikator menu głównego poziomu", "identyfikator wpisu", "tekst wyświetlany")` - jeśli parametr ma być typu `object`, lub `AddItemToMenu<TypEventa, TypParametru>("identyfikator menu głównego poziomu", "identyfikator wpisu", "tekst wyświetlany")` - jeśli inny typ parametru
  1. menu automatycznie publikuje dany event po kliknięciu, wystarczy gdzieś go subskrybować

W celu zarządzania, czy dana pozycja w menu jest wyszarzona, czy nie, jako czwarty parametr `AddItemToMenu` można podać obiekt klasy `CommandCanExecuteHelper`, która ma wajchę `CanExecute` - przestawienie jej w dowolnym momencie na false wyszarza wpis, na true - uaktywnia.