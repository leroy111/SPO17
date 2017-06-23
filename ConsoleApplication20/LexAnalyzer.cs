using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    enum State { H, Num, Ident, Neq, Delim, Ale }
    class LexAnalyzer
    {
        State CS;
        char c;
        int d;
        string new_lex;
        string s;
        int ind = -1;
        char get_symbol()
        {
            ind++;
            return (char)(s[ind]);
        }
        public LexAnalyzer(string t)
        {
            CS = State.H;
            s = t;
            c = get_symbol();
        }
        public Lexem Analyze()
        {
            CS = State.H;
            while (true)
            {
                switch (CS)
                {
                    case State.H:
                        if (c == ' ' || c == '\n' || c == '\t') //пробел, перенос, табуляция
                            c = get_symbol();
                        else if (c >= '0' && c <= '9')
                        {
                            d = c - '0';
                            CS = State.Num;
                            c = get_symbol();
                        }
                        else if (c == '$')
                            return new ConsoleApplication20.Lexem(type_of_lex.LEX_FIN);
                        else if (c >= 'a' && c <= 'z')
                        {
                            new_lex = "";
                            new_lex += c;
                            c = get_symbol();
                            CS = State.Ident;
                        }
                        else if (c == '>' || c == '<' || c == '=')
                        {
                            new_lex = "";
                            new_lex += c;
                            c = get_symbol();
                            CS = State.Ale;
                        }
                        else if (c == '!')
                        {
                            new_lex = "!";
                            c = get_symbol();
                            CS = State.Neq;
                        }
                        else
                            CS = State.Delim;
                        break;
                    case State.Ident:
                        if (c >= 'a' && c <= 'z' || c >= '0' && c <= '9')
                        {
                            new_lex += c;
                            c = get_symbol();
                        }
                        else if (Tables.TW.Contains(new_lex))
                        {
                            return new Lexem(Tables.words[Tables.TW.IndexOf(new_lex)], Tables.TW.IndexOf(new_lex));
                        }
                        else if (Tables.TID.Contains(new_lex))
                        {
                            CS = State.H;
                            return new Lexem(type_of_lex.LEX_TID, Tables.TID.IndexOf(new_lex));
                        }
                        else
                        {
                            CS = State.H;
                            Tables.TID.Add(new_lex);
                            int j = Tables.TID.Count() - 1;
                            return new Lexem(type_of_lex.LEX_TID, j);
                        }
                        break;
                    case State.Num:
                        if (c >= '0' && c <= '9')
                        {
                            d = 10 * d + c - '0';
                            c = get_symbol();
                        }
                        else
                            return new Lexem(type_of_lex.LEX_NUM, d);
                        break;
                    case State.Ale:
                        if (c == '=')
                        {
                            new_lex += c;
                            CS = State.H;
                            c = get_symbol();
                            return new Lexem(Tables.dlmns[Tables.TD.IndexOf(new_lex)], Tables.TD.IndexOf(new_lex));
                        }
                        else
                        {
                            CS = State.H;
                            c = get_symbol();
                            return new Lexem(Tables.dlmns[Tables.TD.IndexOf(new_lex)], Tables.TD.IndexOf(new_lex));
                        }
                        break;
                    case State.Neq:
                        if (c == '=')
                        {
                            new_lex += c;
                            CS = State.H;
                            c = get_symbol();
                            return new Lexem(Tables.dlmns[Tables.TD.IndexOf(new_lex)], Tables.TD.IndexOf(new_lex));
                        }
                        else
                            throw new Exception("!");
                        break;
                    case State.Delim:
                        new_lex = "";
                        if (c != '\n')
                        {
                            new_lex += c;
                            if (Tables.TD.Contains(new_lex))
                            {
                                c = get_symbol();
                                return new Lexem(Tables.dlmns[Tables.TD.IndexOf(new_lex)], Tables.TD.IndexOf(new_lex));
                            }
                            else
                            {
                                Console.WriteLine(c);
                                Console.WriteLine(new_lex);
                                throw new Exception("Unknown lexem" + new_lex );
                            }
                        }
                        else
                            c = get_symbol();
                        break;
                }
            }
        }
    }
}
