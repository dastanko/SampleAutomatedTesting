using System;
using System.Diagnostics;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;

namespace SampleAutomatedTesting
{
    [Binding]
    public class CalculatorSteps
    {
        private const string ExeSourceFile = @"C:\Windows\system32\calc.exe";
        private const string ResultLabelId = "150";
        private Window _mainWindow;
        private Application _application;

        [BeforeScenario()]
        public void BeforeScenario()
        {
            var processStartInfo = new ProcessStartInfo(ExeSourceFile);
            _application = Application.AttachOrLaunch(processStartInfo);
            _mainWindow = _application.GetWindow("Calculator", InitializeOption.NoCache);
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            _application.Close();
        }

        [When(@"слагаю (.*) и (.*)")]
        public void ЕслиСлагаюИ(string value1, string value2)
        {
            Add(value1, value2);
        }

        private void Add(string value1, string value2)
        {
            InputValue(value1);
            _mainWindow.Get<Button>(SearchCriteria.ByText("+")).Click();
            InputValue(value2);
            _mainWindow.Get<Button>(SearchCriteria.ByText("=")).Click();
        }

        [When(@"отнимаю (.*) из (.*)")]
        public void ЕслиОтнимаюИз(string value2, string value1)
        {
            InputValue(value1);
            _mainWindow.Get<Button>(SearchCriteria.ByText("-")).Click();
            InputValue(value2);
            _mainWindow.Get<Button>(SearchCriteria.ByText("=")).Click();
        }

        [When(@"(.*) умножаю на (.*)")]
        public void ЕслиУмножаюНа(string value1, string value2)
        {
            InputValue(value1);
            _mainWindow.Get<Button>(SearchCriteria.ByText("*")).Click();
            InputValue(value2);
            _mainWindow.Get<Button>(SearchCriteria.ByText("=")).Click();
        }

        [Then(@"получаю (.*)")]
        public void ТоПолучаю(string result)
        {
            var label = _mainWindow.Get<Label>(SearchCriteria.ByAutomationId(ResultLabelId));
            Assert.AreEqual(result, label.Text);
        }

        private void InputValue(string value1)
        {
            var array = value1.ToCharArray();
            foreach (var value in array)
            {
                _mainWindow.Get<Button>(value.ToString()).Click();
            }
        }
    }
}