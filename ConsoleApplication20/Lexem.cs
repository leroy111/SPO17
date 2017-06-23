using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Lexem
    {
        public type_of_lex type;
        public int number_of_lexem_in_table;
        public Lexem(type_of_lex t = type_of_lex.LEX_NULL, int v = 0)
        {
            type = t;
            number_of_lexem_in_table = v;
        }
    }
}
