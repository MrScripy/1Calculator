using System;
using System.Collections.Generic;

namespace Calculator.Models
{
    internal static class PolishCalculator
    {
        public static double Calculate(string postfixExpr)
        {
            Stack<double> numbersStack = new();
            int counter = 0;

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
                    counter += 1;
                    if (curChar == '~')
                    {
                        double last = numbersStack.Count > 0 ? numbersStack.Pop() : 0;

                        numbersStack.Push(DoOperation('-', 0, last));
                        Console.WriteLine($"{counter}) {curChar}{last} = {numbersStack.Peek()}");
                        continue;
                    }

                    double second = numbersStack.Count > 0 ? numbersStack.Pop() : 0,
                    first = numbersStack.Count > 0 ? numbersStack.Pop() : 0;

                    numbersStack.Push(DoOperation(curChar, first, second));
                    Console.WriteLine($"{counter}) {first} {curChar} {second} = {numbersStack.Peek()}");
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
