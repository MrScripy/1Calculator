using System;
using System.Collections.Generic;


namespace Calculator.Models
{
    internal static class ExpressionConverter
    {
        public static readonly Dictionary<char, int> OperationPriority = new() {
        {'(', 0},
        {'+', 1},
        {'-', 1},
        {'*', 2},
        {'/', 2},
        {'^', 3},
        {'~', 4}	//	unary minus
                };

        /// <summary>
        /// method converts the string infix expression to postfix expression
        /// </summary>
        /// <param name="infixExpr"></param>
        /// <returns></returns>
        public static string ConvertToPostfix(string infixExpr)
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
                    // put in output string all till opening (
                    while (operatorsStack.Count > 0 && operatorsStack.Peek() != '(')
                        postfixExpr += operatorsStack.Pop();
                    operatorsStack.Pop();
                }
                else if (OperationPriority.ContainsKey(exprChar))
                {
                    //checking if the operator is unary minus
                    char operationChar = exprChar;
                    if (operationChar == '-' && (i == 0 || (i > 1 && OperationPriority.ContainsKey(infixExpr[i - 1]))))
                        operationChar = '~';

                    // put all the operators from stack in output string if they have higher priority
                    while (operatorsStack.Count > 0 && (OperationPriority[operatorsStack.Peek()] >= OperationPriority[operationChar]))
                        postfixExpr += operatorsStack.Pop();
                    operatorsStack.Push(operationChar);
                }
            }
            foreach (char op in operatorsStack)
                postfixExpr += op;

            return postfixExpr;
        }

        /// <summary>
        /// Check and return numbers
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string ReceiveStringNumber(string expression, ref int position)
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
                // checking for non-integers
                else if (posChar.Equals('.') || posChar.Equals(','))
                {
                    strNumber += ',';
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
