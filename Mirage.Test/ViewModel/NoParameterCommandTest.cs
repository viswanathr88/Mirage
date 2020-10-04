using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mirage.ViewModel.Commands;

namespace Mirage_Test.ViewModel
{
    class NoParameterCommand : Command
    {
        protected override bool CanExecute()
        {
            return true;
        }

        protected override void Run()
        {
            
        }
    }

    [TestClass]
    public class NoParameterCommandTest
    {
        [TestMethod]
        public void CanExecute_NullObjectParameter_EnsureTrue()
        {
            object param = null;
            NoParameterCommand command = new NoParameterCommand();
            Assert.IsTrue(command.CanExecute(param));
        }

        [TestMethod]
        public void CanExecute_NullParameter_EnsureTrue()
        {
            NoParameterCommand command = new NoParameterCommand();
            Assert.IsTrue(command.CanExecute(null));
        }
    }
}
