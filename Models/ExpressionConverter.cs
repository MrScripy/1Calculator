using System;
using System.Collections.Generic;


namespace Calculator.Models
{
    class ExpressionConverter
    {
        private Dictionary<char, int> operationPriority = new() {
        {'(', 0},
        {'+', 1},
        {'-', 1},
        {'*', 2},
        {'/', 2},
        {'^', 3},
        {'~', 4}	//	unary minus
                };

        public string ConvertToPostfix(string infixExpr)
        {
            string postfixExpr = "";
            Stack<char> operatorsStack = new Stack<char>();

            for (int i = 0; i < infixExpr.Length; i++)
            {
                char exprChar = infixExpr[i];

                if (Char.IsDigit(exprChar))
                {
                    postfixExpr += ReceiveStringNumber(infixExpr, ref i) + " ";
                }
                else if (exprChar.Equals('('))
                {
                    operatorsStack.Push(exprChar);
                }
                else if (exprChar.Equals(')'))
                {
                    while (operatorsStack.Count > 0 && operatorsStack.Peek() != '(')
                        postfixExpr += operatorsStack.Pop();
                    operatorsStack.Pop();
                }
                else if (operationPriority.ContainsKey(exprChar))
                {
                    char operationChar = exprChar;
                    if (operationChar == '-' && (i == 0 || (i > 1 && operationPriority.ContainsKey(infixExpr[i - 1]))))
                    {
                        operationChar = '~';
                    }
                    while (operatorsStack.Count > 0 && (operationPriority[operatorsStack.Peek()] >= operationPriority[operationChar]))
                        postfixExpr += operatorsStack.Pop();
                    operatorsStack.Push(operationChar);
                }
            }
            foreach (char op in operatorsStack)
                postfixExpr += op;

            return postfixExpr;
        }

        /// <summary>
        /// Check and return simple numbers
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private string ReceiveStringNumber(string expression, ref int position)
        {
            string strNumber = "";

            while (position < expression.Length)
            {
                char posChar = expression[position];
                if (Char.IsDigit(posChar))
                {
                    strNumber += posChar;
                    position++;
                }
                else
                {
                    position--;
                    break;
                }
            }
            return strNumber;
        }
    }
}
