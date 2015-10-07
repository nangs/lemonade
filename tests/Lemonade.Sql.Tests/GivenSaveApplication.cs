﻿using Lemonade.Builders;
using Lemonade.Core.Events;
using Lemonade.Core.Exceptions;
using Lemonade.Sql.Commands;
using Lemonade.Sql.Migrations;
using Lemonade.Sql.Queries;
using NUnit.Framework;

namespace Lemonade.Sql.Tests
{
    public class GivenSaveApplication : IDomainEventDispatcher
    {
        [SetUp]
        public void SetUp()
        {
            DomainEvent.Dispatcher = this;
            Runner.SqlCompact("Lemonade").Down();
            Runner.SqlCompact("Lemonade").Up();
        }

        [Test]
        public void WhenISaveAnApplication_ThenICanRetrieveIt()
        {
            var saveApplication = new CreateApplication();
            var getApplicationByName = new GetApplicationByName();
            saveApplication.Execute(new ApplicationBuilder()
                .WithName("Test12345")
                .Build());

            var application = getApplicationByName.Execute("Test12345");
            Assert.That(application, Is.Not.Null);
            Assert.That(application.Name, Is.EqualTo("Test12345"));
        }

        [Test]
        public void WhenISaveAnApplication_ThenApplicationSavedEventIsRaisedWithCorrectDetails()
        {
            var saveApplication = new CreateApplication();
            var getApplicationByName = new GetApplicationByName();
            saveApplication.Execute(new ApplicationBuilder()
                .WithName("Test12345")
                .Build());

            var application = getApplicationByName.Execute("Test12345");

            Assert.That(_createdApplication.ApplicationId, Is.EqualTo(application.ApplicationId));
            Assert.That(_createdApplication.Name, Is.EqualTo(application.Name));
        }

        [Test]
        public void WhenITryToSaveADuplicateApplication_ThenSaveApplicationExceptionIsThrown()
        {
            var saveApplication = new CreateApplication();
            var application = new ApplicationBuilder()
                .WithName("Test12345")
                .Build();

            saveApplication.Execute(application);

            Assert.Throws<CreateApplicationException>(() => saveApplication.Execute(application));
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            if (@event is ApplicationHasBeenCreated) _createdApplication = @event as ApplicationHasBeenCreated;
        }

        private ApplicationHasBeenCreated _createdApplication;
    }
}