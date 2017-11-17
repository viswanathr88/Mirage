using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mirage.ViewModel.Commands;
using System;

namespace Mirage_Test.ViewModel
{
    [TestClass]
    public sealed class RelayCommandTest
    {
        private bool fFlag = false;

        [TestInitialize]
        public void Initialize()
        {
            fFlag = false;
        }

        [TestCleanup]
        public void Cleanup()
        {
            fFlag = false;
        }

        [TestMethod]
        public void Constructor_Null_Test()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new RelayCommand(null));

            var command = new RelayCommand(() => {; });
        }

        [TestMethod]
        public void Validate_Relay_Lambda()
        {
            bool lambdaRan = false;
            var command = new RelayCommand(() =>
            {
                lambdaRan = true;
            });

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }

            Assert.IsTrue(lambdaRan);
        }

        [TestMethod]
        public void Validate_Relay_Function()
        {
            var command = new RelayCommand(Relay_Function);
            Assert.IsFalse(fFlag);

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }

            Assert.IsTrue(fFlag);
        }

        [TestMethod]
        public void Validate_Can_Execute_False()
        {
            bool actionExecuted = false;
            var command = new RelayCommand(() =>
            {
                actionExecuted = true;
            }, () =>
            {
                return false;
            });

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
            Assert.IsFalse(actionExecuted);
        }

        [TestMethod]
        public void Validate_Can_Execute_True()
        {
            bool actionExecuted = false;
            var command = new RelayCommand(() =>
            {
                actionExecuted = true;
            }, () =>
            {
                return true;
            });

            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
            Assert.IsTrue(actionExecuted);
        }

        private void Relay_Function()
        {
            fFlag = true;
        }
    }
}
