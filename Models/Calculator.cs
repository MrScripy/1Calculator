using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    internal static class Calculator
    {
        public static double Calculate(string postfixExpr)
        {
            Stack<double> numbersStack = new Stack<double>();
            int counter = 0;

            for (int i = 0; i < postfixExpr.Length; i++)
            {
                char curChar = postfixExpr[i];
                if(Char.IsDigit(curChar))
                {
                    string number = ExpressionConverter.ReceiveStringNumber(postfixExpr, ref counter);
                    numbersStack.Push(Double.Parse(number));
                }
                else if (ExpressionConverter.OperationPriority.ContainsKey(curChar))
                {
                    counter++;
                    if(curChar.Equals('~'))
                    {
                        double last = numbersStack.Count>0 ? numbersStack.Pop() : 0;
                        numbersStack.Push(DoOperation('-', 0, last));
                        continue;
                    }

                    double second = numbersStack.Count > 0? numbersStack.Pop() : 0;
                    double first = numbersStack.Count > 0 ? numbersStack.Pop() : 0;

                    numbersStack.Push(DoOperation(curChar, first, second));
                }
            }

            return numbersStack.Pop();
        }
        private static double DoOperation(char op, double firstNum, double secondNum)
        {
            switch (op)
            {
                case '+':
                    return firstNum + secondNum;
                case '-':
                    return firstNum - secondNum;
                case '*':
                    return firstNum * secondNum;
                case '/':
                    return firstNum / secondNum;
                case '^':
                    return Math.Pow(firstNum, secondNum);
                default:
                    return 0;
            }
        }
    }
}
