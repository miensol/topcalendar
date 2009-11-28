﻿using System;
using Microsoft.Practices.Composite.Events;
using Ninject;
using NUnit.Framework;
using Rhino.Mocks;
using TopCalendar.Client.Connector;
using TopCalendar.Client.DataModel;
using TopCalendar.UI.Infrastructure;
using TopCalendar.UI.PluginManager;
using TopCalendar.Utility.Tests;
using TopCalendar.Utility.UI;

namespace TopCalendar.UI.Modules.TaskViewer.Tests
{
    public abstract class TaskViewPresentationModelTestsBase : observations_for_auto_created_sut_of_type<TaskPresentationModel>
    {
        protected UnloadModuleEvent _unloadModuleEvent;
        protected ITaskView _taskView;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _unloadModuleEvent = MockRepository.GenerateMock<UnloadModuleEvent>();
            Dependency<IEventAggregator>().Stub(aggregator => aggregator.GetEvent<UnloadModuleEvent>()).Return(
                _unloadModuleEvent);
            _taskView = Dependency<ITaskView>();
        }
    }

    [TestFixture]
    public class TaskViewPresentationModel_when_CancelCommand_is_executed : TaskViewPresentationModelTestsBase
    {

        protected override void Because()
        {
            Sut.CancelCommand.Execute(null);
        }

        [Test]
        public void View_should_be_unloaded()
        {

            var viewToUnload = Dependency<ITaskView>();

            _unloadModuleEvent.AssertWasCalled(x => x.Publish(viewToUnload));
        }
    }

    [TestFixture]
    public class TaskViewPresentationModel_when_UpdateCommand_is_executed : TaskViewPresentationModelTestsBase
    {

        protected override void Because()
        {
            Sut.UpdateCommand.Execute(null);
        }

        [Test]
        public void UpdateTask_method_in_TaskRepository_should_be_called()
        {
            Dependency<ITaskRepository>().AssertWasCalled(x => x.UpdateTask(Arg<Task>.Is.Anything));
        }
    }

    [TestFixture]
    public class TaskViewPresentationModel_when_UpdateCommand_is_executed_and_UpdateTask_ended_successfull
        : TaskViewPresentationModelTestsBase
    {

        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(repo => repo.UpdateTask(null))
                .IgnoreArguments().Return(true);
        }

        protected override void Because()
        {
            Sut.UpdateCommand.Execute(null);
        }
        [Test]
        public void TaskView_should_be_unregistered()
        {
            _unloadModuleEvent.AssertWasCalled(x => x.Publish(_taskView));
        }
    }

    [TestFixture]
    public class TaskViewPresentationModel_when_UpdateCommand_is_executed_and_UpdateTask_fail
        : TaskViewPresentationModelTestsBase
    {

        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(repo => repo.UpdateTask(null))
                .IgnoreArguments().Return(false);
        }

        protected override void Because()
        {
            Sut.UpdateCommand.Execute(null);
        }
        [Test]
        public void TaskView_should_not_be_unregistered()
        {
            _unloadModuleEvent.AssertWasNotCalled(x => x.Publish(_taskView));
        }
    }

    [TestFixture]
    public class TaskPresentationModel_when_AddCommand_is_executed : TaskViewPresentationModelTestsBase
    {
        protected override void Because()
        {
            Sut.AddCommand.Execute(null);
        }

        [Test]
        public void AddTask_in_TaskRepository_should_be_called ()
        {
            Dependency<ITaskRepository>().AssertWasCalled(x => x.AddTask(Arg<Task>.Is.Anything));
        }
    }

    [TestFixture]
    public class TaskPresentationModel_when_AddCommand_is_executed_and_AddTask_success : TaskViewPresentationModelTestsBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(x => x.AddTask(null)).IgnoreArguments().Return(true);
        }

        protected override void Because()
        {
            Sut.AddCommand.Execute(null);
        }

        [Test]
        public void TaskView_should_be_unregistered()
        {
            _unloadModuleEvent.AssertWasCalled(x => x.Publish(_taskView));
        }
    }

    [TestFixture]
    public class TaskPresentationModel_when_AddCommand_is_executed_and_AddTask_fail : TaskViewPresentationModelTestsBase
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            Dependency<ITaskRepository>().Stub(x => x.AddTask(null)).IgnoreArguments().Return(false);
        }

        protected override void Because()
        {
            Sut.AddCommand.Execute(null);
        }

        [Test]
        public void TaskView_should_not_be_unregistered()
        {
            _unloadModuleEvent.AssertWasNotCalled(x => x.Publish(_taskView));
        }
    }



}
