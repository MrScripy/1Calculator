using Calculator.Infrastructure.Commands;
using Calculator.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Properties
        #region Fields
        private string infixExpr;
        private string exprResult;
        private string postfixExpr;
        #endregion
        public string ExprResult
        {
            get => exprResult;
            set => Set(ref exprResult, value);
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
