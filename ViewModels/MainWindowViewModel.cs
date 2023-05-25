using Calculator.Infrastructure.Commands;
using Calculator.Models;
using Calculator.ViewModels.Base;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties
        #region Fields
        private string infixExpr;
        private string resultExpr;
        private string postfixExpr;
        #endregion
        public string ResultExpr
        {
            get => resultExpr;
            set => Set(ref resultExpr, value);
        }
        public string PosfixExpr
        {
            get => postfixExpr;
            set => Set(ref postfixExpr, value);
        }

        #endregion

        #region Commands
        public ICommand CountCommand { get; }

        private void OnCountCommandExecuted(object p)
        {
            string? infixExp = p as string;
            PosfixExpr = ExpressionConverter.ConvertToPostfix(infixExp);
            ResultExpr = PolishCalculator.Calculate(PosfixExpr).ToString();
        }

        private bool CanCountCommandExecute(object p)
        {
            if (p as string != string.Empty) return true;
            return false;
        }

        #endregion


        public MainWindowViewModel()
        {
            CountCommand = new LambdaCommand(OnCountCommandExecuted, CanCountCommandExecute);            
        }
    }
}
