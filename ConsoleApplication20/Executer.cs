using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Executer
    {
        List<Lexem> Poliz;
        public Executer(List<Lexem> Pol)
        {
            Poliz = Pol;
        }
        public void Execute()
        {
            Stack<int> stack = new Stack<int>();
            Tables.Values = new List<int>(Tables.TID.Count());
            for (int j = 0; j < Tables.TID.Count(); j++)
            {
                Tables.Values.Add(0);
            }
            for (int i = 0; i < Poliz.Count(); i++)
            {
                switch (Poliz[i].type)
                {
                    case type_of_lex.LEX_NUM:
                        stack.Push(Poliz[i].number_of_lexem_in_table);
                      //  Console.WriteLine("num");
                        break;
                    case type_of_lex.POLIZ_ADDRESS:
                        stack.Push(Poliz[i].number_of_lexem_in_table);
                      //  Console.WriteLine("&");
                        break;
                    case type_of_lex.POLIZ_LABEL:
                        stack.Push(Poliz[i].number_of_lexem_in_table);
                        break;
                    case type_of_lex.LEX_TID:
                        int f = Poliz[i].number_of_lexem_in_table;
                        stack.Push(Tables.Values[f]);
                        break;
                    case type_of_lex.LEX_NOT_JUMP:
                        int d = stack.Pop();
                        if (d == 0)
                            i = d - 1;
                        break;
                    case type_of_lex.LEX_Q:
                        d = stack.Pop();
                        Tables.Values[stack.Pop()] = d;
                     //   Console.WriteLine("q");
                        break;
                    case type_of_lex.LEX_JUMP:
                        i = stack.Pop() - 1;
                        break;

                    case type_of_lex.LEX_PLUS:
                        stack.Push(stack.Pop() + stack.Pop());
                        break;
                    case type_of_lex.LEX_MINUS:
                        stack.Push(stack.Pop() - stack.Pop());
                        break;
                    case type_of_lex.LEX_MLT:
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    case type_of_lex.LEX_SLASH:
                        stack.Push(stack.Pop() / stack.Pop());
                        break;
                    case type_of_lex.LEX_EQ:
                        if (stack.Pop() == stack.Pop())
                            stack.Push(1);
                        else
                            stack.Push(0);
                        break;
                    case type_of_lex.LEX_LSS:
                        int k = stack.Pop();
                        if (stack.Pop() < k)
                            stack.Push(1);
                        else
                            stack.Push(0);
                        break;
                    case type_of_lex.LEX_GRT:
                        k = stack.Pop();
                        if (stack.Pop() > k)
                            stack.Push(1);
                        else
                            stack.Push(0);
                        break;
                    case type_of_lex.LEX_EQ_LSS:
                        k = stack.Pop();
                        if (stack.Pop() <= k)
                            stack.Push(1);
                        else
                            stack.Push(0);
                        break;
                    case type_of_lex.LEX_EQ_GRT:
                        k = stack.Pop();
                        if (stack.Pop() >= k)
                            stack.Push(1);
                        else
                            stack.Push(0);
                        break;
                }
            }
        
        }
    }
}
