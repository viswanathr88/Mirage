using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Mirage.ViewModel.Commands;
using System;

namespace Mirage_Test.ViewModel
{
    [TestClass]
    public sealed class RelayCommandTest
    {
        private bool fFlag = false;
        private int fParam = 5;

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

            command.Execute(null);
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

            command.Execute(null);
            Assert.IsTrue(actionExecuted);
        }

        [TestMethod]
        public void Validate_Relay_Lambda()
        {
            bool lambdaRan = false;
            var command = new RelayCommand(() =>
            {
                lambdaRan = true;
            });

            command.Execute(null);

            Assert.IsTrue(lambdaRan);
        }

        [TestMethod]
        public void Validate_Relay_Function()
        {
            var command = new RelayCommand(Relay_Function);
            Assert.IsFalse(fFlag);

            command.Execute(null);

            Assert.IsTrue(fFlag);
        }

        [TestMethod]
        public void Validate_Relay_Lambda_With_Parameter()
        {
            bool lambdaRan = false;
            int param = 5;
            var command = new RelayCommand<int>((int x) =>
            {
                Assert.AreEqual(param, x);
                lambdaRan = true;
            });

            command.Execute(5);

            Assert.IsTrue(lambdaRan);
        }

        [TestMethod]
        public void Validate_Relay_Function_With_Parameter()
        {
            var command = new RelayCommand<int>((int x) =>
            {
                Relay_Function_With_Parameter(x);
            });
            Assert.IsFalse(fFlag);

            command.Execute(fParam);

            Assert.IsTrue(fFlag);
        }

        [TestMethod]
        public void Validate_Executing_Raised()
        {
            bool fExecuted = false;
            bool fExecuting = false;
            var command = new RelayCommand(() => fExecuted = true);
            command.Executing += (sender, args) =>
            {
                Assert.IsFalse(args.Cancel);
                Assert.IsTrue(command.IsExecuting);
                fExecuting = true;
            };

            command.Execute(null);
            Assert.IsTrue(fExecuted);
            Assert.IsTrue(fExecuting);
        }

        [TestMethod]
        public void Validate_Executed_Raised()
        {
            bool fExecuted = false;
            var command = new RelayCommand(() =>
            {
                // Do nothing
            });

            command.Executed += (sender, args) =>
            {
                Assert.AreEqual(args.State, CommandExecutionState.Success);
                fExecuted = true;
            };

            command.Execute(null);

            Assert.IsTrue(fExecuted);
            Assert.IsFalse(command.IsExecuting);
        }

        [TestMethod]
        public void Validate_Error_Property()
        {
            bool fExecuted = false;
            var error = new ArgumentNullException();
            var command = new RelayCommand(() =>
            {
                throw error;
            });

            command.Executed += (sender, args) =>
            {
                Assert.AreEqual(args.State, CommandExecutionState.Failure);
                fExecuted = true;
            };

            command.Execute(null);
            Assert.AreEqual(command.Error, error);
            Assert.IsTrue(fExecuted);
        }

        [TestMethod]
        public void Validate_Invalid_Parameter()
        {
            bool fExecuting = false;
            var command = new RelayCommand<int>((int x) => Assert.AreEqual(fParam, x));

            command.Executing += (sender, args) =>
            {
                fExecuting = true;
            };
            command.Execute(null);

            // Ensure command never executed
            Assert.AreEqual(fExecuting, false);
        }

        [TestMethod]
        public void Validate_Command_Cancel()
        {
            bool fExecuted = false;
            var command = new RelayCommand(() => new NotImplementedException());
            
            // Cancel the command when the command asks for it
            command.Executing += (sender, args) => args.Cancel = true;

            command.Executed += (sender, args) =>
            {
                Assert.AreEqual(args.State, CommandExecutionState.Cancelled);
                fExecuted = true;
            };

            command.Execute(null);

            Assert.IsTrue(fExecuted);
        }

        private void Relay_Function()
        {
            fFlag = true;
        }

        private void Relay_Function_With_Parameter(int x)
        {
            Assert.AreEqual(x, fParam);
            fFlag = true;
        }
    }
}
