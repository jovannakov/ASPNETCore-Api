using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Exceptions
{
    public class RequiredFieldsException : Exception
    {
        private const string MESSAGE = "";
        public RequiredFieldsException(string Message = "Required fields exception") : base(Message) { }
    }
}
