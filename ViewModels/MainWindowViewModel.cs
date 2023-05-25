using System;
using System.Windows;
using System.Windows.Input;
using Calculator.Infrastructure.Commands;
using Calculator.Models;
using Calculator.ViewModels.Base;

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
            try
            {
                PosfixExpr = Models.ExpressionConverter.ConvertToPostfix(infixExp);
                ResultExpr = PolishCalculator.Calculate(PosfixExpr).ToString();
            }
            catch (Exception ex)
            {
                PosfixExpr = " ";
                MessageBox.Show(ex.Message, "caption", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
