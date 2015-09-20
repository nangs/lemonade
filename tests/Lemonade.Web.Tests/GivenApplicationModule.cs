﻿using System.Linq;
using Lemonade.Sql.Migrations;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using SelfishHttp;

namespace Lemonade.Web.Tests
{
    public class GivenApplicationModule
    {
        [SetUp]
        public void SetUp()
        {
            _server = new Server(64978);
            Runner.Sqlite(ConnectionString).Down();
            Runner.Sqlite(ConnectionString).Up();

            _browser = new Browser(new FakeLemonadeBootstrapper());
        }

        [TearDown]
        public void Teardown()
        {
            _server.Dispose();
        }

        [Test]
        public void WhenIPostANewApplication_ThenTheResponseIsOK()
        {
            var postResponse = _browser.Post("/application/", (with) =>
            {
                with.HttpRequest();
                with.FormValue("name", "TestApplication1");
            });

            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void WhenIPostANewApplication_ThenItIsRendered()
        {
            _browser.Post("/application/", (with) => {
                with.HttpRequest();
                with.FormValue("name", "TestApplication1");
            });

            var response = _browser.Get("/feature");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Body[".application"].First().InnerText.Contains("TestApplication1"));
        }

        private Server _server;
        private Browser _browser;
        private const string ConnectionString = "Lemonade";
    }
}