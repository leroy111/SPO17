using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Parser
    {
        Lexem current_lexem;
        List<Lexem> list_with_lexems;
        Stack<Lexem> stack = new Stack<Lexem>();
        public List<Lexem> Poliz = new List<Lexem>();
        int count = 0;
        public Parser(List<Lexem> lst)
        {
            list_with_lexems = lst;
        }
        public void get_lexem()
        {
            current_lexem = list_with_lexems[count];
            count++;
        }
        public void Analyze()
        {
            Console.WriteLine("Poliz:");
            get_lexem();
            if (current_lexem.type != type_of_lex.LEX_VAR)
                throw new Exception("Expected 'var'");
            For_Var();
            P();
            for (int i = 0; i < Poliz.Count(); i++)
            {
                if (Poliz[i].type == type_of_lex.POLIZ_ADDRESS)
                    Console.Write ("&" + Tables.TID[Poliz[i].number_of_lexem_in_table] + " ");
                else if (Poliz[i].type == type_of_lex.LEX_TID)
                    Console.Write(Tables.TID[Poliz[i].number_of_lexem_in_table] + " ");
                else if (Poliz[i].type == type_of_lex.LEX_NUM)
                    Console.Write(Poliz[i].number_of_lexem_in_table + " ");
                else if (Poliz[i].type == type_of_lex.POLIZ_LABEL)
                    Console.Write(Poliz[i].number_of_lexem_in_table + " ");
                else
                    Console.Write(Tables.TD[Poliz[i].number_of_lexem_in_table] + " ");
            }
            Console.WriteLine("oo eeeee///All ok!");
        }
        void P()
        {
            if (current_lexem.type != type_of_lex.LEX_BEGIN)
                throw new Exception("Expected 'begin'");
            get_lexem();
            D1();
            if (current_lexem.type != type_of_lex.LEX_END)
                throw new Exception("Expected 'end'");
        }
        void D1()
        {
            if (current_lexem.type == type_of_lex.LEX_END)
                return;
            if (current_lexem.type == type_of_lex.LEX_IF)
            {
                For_if();
                int count = Poliz.Count();
                Poliz.Add(new Lexem());
                Poliz.Add(new Lexem(Tables.dlmns[Tables.TD.IndexOf("!F")], Tables.TD.IndexOf("!F")));
                if (current_lexem.type != type_of_lex.LEX_THEN)
                    throw new Exception("Expected 'then'");
                For_then();
                int count_2 = Poliz.Count();
                Poliz.Add(new Lexem());
                Poliz.Add(new Lexem(Tables.dlmns[Tables.TD.IndexOf("!")], Tables.TD.IndexOf("!")));
                Poliz[count] = new Lexem(type_of_lex.POLIZ_LABEL, count_2 + 3);
                get_lexem();
                if (current_lexem.type == type_of_lex.LEX_ELSE)
                {
                    For_else();
                    get_lexem();
                }
                Poliz[count_2] = new Lexem(type_of_lex.POLIZ_LABEL, Poliz.Count() + 1);
            }
            else if (current_lexem.type == type_of_lex.LEX_WHILE)
            {
                int count = Poliz.Count();
                For_if();
                Poliz.Add(new Lexem());
                int count_2 = Poliz.Count();
                Poliz.Add(new Lexem(Tables.dlmns[Tables.TD.IndexOf("!F")], Tables.TD.IndexOf("!F")));
                if (current_lexem.type != type_of_lex.LEX_DO)
                    throw new Exception("Expected 'do'");
                For_then();
                Poliz.Add(new Lexem(type_of_lex.POLIZ_LABEL, count + 1));
                Poliz.Add(new Lexem(Tables.dlmns[Tables.TD.IndexOf("!")], Tables.TD.IndexOf("!")));
                Poliz[count_2] = new Lexem(type_of_lex.POLIZ_LABEL, Poliz.Count() + 1);
                get_lexem();
            }
            else
            {
                For_Operation();
                get_lexem();
            }
            D1();
        }
        void For_if()
        {
            get_lexem();
            if (current_lexem.type != type_of_lex.LEX_TID)
                throw new Exception("Expected identificator");
            Poliz.Add(current_lexem);
            if (current_lexem.type == type_of_lex.LEX_TID)
            {
                bool b = true;
                for (int i = 0; i < Tables.Identificators.Count(); i++)
                {
                    if (Tables.Identificators[i].type == current_lexem.type &&
                        Tables.TID[Tables.Identificators[i].number_of_lexem_in_table] == Tables.TID[current_lexem.number_of_lexem_in_table])
                        b = false;
                }
                if (b)
                    throw new Exception("Uninitialized variable used");
            }
            get_lexem();
            if (current_lexem.type != type_of_lex.LEX_EQ && current_lexem.type != type_of_lex.LEX_NEQ &&
                current_lexem.type != type_of_lex.LEX_EQ_GRT && current_lexem.type != type_of_lex.LEX_EQ_LSS &&
                current_lexem.type != type_of_lex.LEX_GRT && current_lexem.type != type_of_lex.LEX_LSS)
                throw new Exception("Expected operation");
            stack.Push(current_lexem);
            get_lexem();
            Operation();
            Poliz.Add(stack.Pop());
        }
        void For_then()
        {
            get_lexem();
            if (current_lexem.type == type_of_lex.LEX_BEGIN)
                P();
            else
                For_Operation();
        }
        void For_else()
        {
            get_lexem();
            if (current_lexem.type == type_of_lex.LEX_BEGIN)
                P();
            else
                For_Operation();
        }
        void For_reset()
        {
            if (current_lexem.type != type_of_lex.LEX_TID)
                throw new Exception("Expected identificator");
            for (int i = 0; i < Tables.Identificators.Count(); i++)
            {
                if (Tables.Identificators[i].type == current_lexem.type &&
                    Tables.TID[Tables.Identificators[i].number_of_lexem_in_table] == Tables.TID[current_lexem.number_of_lexem_in_table])
                    throw new Exception("Twice reset!");
            }
            Tables.Identificators.Add(current_lexem);
            get_lexem();
            while (current_lexem.type == type_of_lex.LEX_COLON)
            {
                get_lexem();
                if (current_lexem.type != type_of_lex.LEX_TID)
                    throw new Exception("Expected identificator");
                for (int i = 0; i < Tables.Identificators.Count(); i++)
                {
                    if (Tables.TID[Tables.Identificators[i].number_of_lexem_in_table] == Tables.TID[current_lexem.number_of_lexem_in_table])
                        throw new Exception("Twice reset!");
                }
                Tables.Identificators.Add(current_lexem);
                get_lexem();
            }
            if (current_lexem.type != type_of_lex.LEX_SEM)
                throw new Exception("Expected identificator");
            get_lexem();
            if (current_lexem.type != type_of_lex.LEX_INT)
                throw new Exception("Expected identificator");
            get_lexem();
            if (current_lexem.type != type_of_lex.LEX_SEMICOLON)
                throw new Exception("Expected ';'");
        }
        void For_Operation()
        {
            if (current_lexem.type != type_of_lex.LEX_TID)
                return;
            bool b = true;
            for (int i = 0; i < Tables.Identificators.Count(); i++)
            {
                if (Tables.Identificators[i].type == current_lexem.type &&
                    Tables.TID[Tables.Identificators[i].number_of_lexem_in_table] == Tables.TID[current_lexem.number_of_lexem_in_table])
                    b = false;
            }
            if (b)
                throw new Exception("Uninitialized variable used");
            Poliz.Add(new Lexem(type_of_lex.POLIZ_ADDRESS, current_lexem.number_of_lexem_in_table));
            get_lexem();
            if (current_lexem.type != type_of_lex.LEX_Q)
                throw new Exception("Expected operation");
            stack.Push(current_lexem);
            get_lexem();
            Operation();
            Poliz.Add(stack.Pop());
            if (current_lexem.type != type_of_lex.LEX_SEMICOLON)
                throw new Exception("Expected ';'");
            Poliz.Add(current_lexem);
        }
        void Operand_2()
        {
            bool b = true;
            get_lexem();
            if (current_lexem.type == type_of_lex.LEX_LPAREN)
            {
                stack.Push(current_lexem);
                get_lexem();
                Operation();
                if (current_lexem.type != type_of_lex.LEX_PPAREN)
                    throw new Exception("Expected ')'");
                Lexem l;
                l = stack.Pop();
                while (l.type != type_of_lex.LEX_LPAREN)
                {
                    Poliz.Add(l);
                    l = stack.Pop();
                }
            }
            else if (current_lexem.type != type_of_lex.LEX_TID && current_lexem.type != type_of_lex.LEX_NUM)
                throw new Exception("Expected operator");
            else
                Poliz.Add(current_lexem);
            if (current_lexem.type == type_of_lex.LEX_TID)
            {
                b = true;
                for (int i = 0; i < Tables.Identificators.Count(); i++)
                {
                    if (Tables.Identificators[i].type == current_lexem.type &&
                        Tables.TID[Tables.Identificators[i].number_of_lexem_in_table] == Tables.TID[current_lexem.number_of_lexem_in_table])
                        b = false;
                }
                if (b)
                    throw new Exception("Uninitialized variable used");
            }
            get_lexem();
        }
        void Operation()
        {
            bool b = true;
            if (current_lexem.type != type_of_lex.LEX_TID && current_lexem.type != type_of_lex.LEX_NUM && current_lexem.type != type_of_lex.LEX_LPAREN)
                throw new Exception("Expected operator");
            if (current_lexem.type == type_of_lex.LEX_TID)
            {
                b = true;
                for (int i = 0; i < Tables.Identificators.Count(); i++)
                {
                    if (Tables.Identificators[i].type == current_lexem.type &&
                        Tables.TID[Tables.Identificators[i].number_of_lexem_in_table] == Tables.TID[current_lexem.number_of_lexem_in_table])
                        b = false;
                }
                if (b)
                    throw new Exception("Uninitialized variable used");
            }
            if (current_lexem.type == type_of_lex.LEX_LPAREN)
            {
                stack.Push(current_lexem);
                get_lexem();
                Operation();
                if (current_lexem.type != type_of_lex.LEX_PPAREN)
                    throw new Exception("Expected ')'");
                Lexem l;
                l = stack.Pop();
                while (l.type != type_of_lex.LEX_LPAREN)
                {
                    Poliz.Add(l);
                    l = stack.Pop();
                }
                // достать все из стека
            }
            else
                Poliz.Add(current_lexem);
            get_lexem();
            while (current_lexem.type == type_of_lex.LEX_PLUS || current_lexem.type == type_of_lex.LEX_MINUS
                || current_lexem.type == type_of_lex.LEX_MLT || current_lexem.type == type_of_lex.LEX_SLASH)
            {
                stack.Push(current_lexem);
                Operand_2();
                Poliz.Add(stack.Pop());
            }
            
        } 
        void For_Var()
        {
            get_lexem();
            while (current_lexem.type != type_of_lex.LEX_BEGIN)
            { 
                For_reset();
                get_lexem();
            }
        }
    }
}
