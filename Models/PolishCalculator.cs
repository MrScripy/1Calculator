using System;
using System.Collections.Generic;

namespace Calculator.Models
{
    internal static class PolishCalculator
    {
        /// <summary>
        /// take postfix expression and comes back the result of the expression
        /// </summary>
        /// <param name="postfixExpr"></param>
        /// <returns></returns>
        public static double Calculate(string postfixExpr)
        {
            Stack<double> numbersStack = new();

            for (int i = 0; i < postfixExpr.Length; i++)
            {
                char curChar = postfixExpr[i];

                if (Char.IsDigit(curChar))
                {
                    string number = ExpressionConverter.ReceiveStringNumber(postfixExpr, ref i);
                    numbersStack.Push(Double.Parse(number));
                }
                else if (ExpressionConverter.OperationPriority.ContainsKey(curChar))
                {
                    if (curChar == '~')
                    {
                        // check if stack is empty. if it isn't take number
                        double last = numbersStack.Count > 0 ? numbersStack.Pop() : 0;
                        // do unary operation
                        numbersStack.Push(DoOperation('-', 0, last));                        
                        continue;
                    }

                    double second = numbersStack.Count > 0 ? numbersStack.Pop() : 0;
                    double first = numbersStack.Count > 0 ? numbersStack.Pop() : 0;

                    numbersStack.Push(DoOperation(curChar, first, second));
                }
            }
            return numbersStack.Pop();
        }

        /// <summary>
        /// does arithmetic operations
        /// </summary>
        /// <param name="op"></param>
        /// <param name="firstNum"></param>
        /// <param name="secondNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                    if (secondNum.Equals(0))
                        throw new Exception("Division by zero is not possible");
                    return firstNum / secondNum;
                case '^':
                    return Math.Pow(firstNum, secondNum);                
                default:
                    return 0;
            }
        }
    }
}
