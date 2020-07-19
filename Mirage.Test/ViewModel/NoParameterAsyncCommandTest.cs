using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mirage.ViewModel.Commands;
using System.Threading.Tasks;

namespace Mirage_Test.ViewModel
{
    class NoParameterAsyncCommand : AsyncCommand, IAsyncCommand
    {
        protected override bool CanExecute()
        {
            return true;
        }

        protected override Task RunAsync()
        {
            return Task.CompletedTask;
        }
    }

    [TestClass]
    public class NoParameterAsyncCommandTest
    {
        [TestMethod]
        public void CanExecute_NullParameter_EnsureTrue()
        {
            object param = null;
            NoParameterAsyncCommand command = new NoParameterAsyncCommand();
            command.CanExecute(param);
        }
    }
}
