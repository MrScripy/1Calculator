using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    class ExpressionConverter
    {
        public string ConvertToPostfix()
        {

            return " ";
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
