using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
 
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("file.txt"))
            {
                Console.WriteLine("file not exists");
                return;
            }
            string my_text = "";
            string[] text = System.IO.File.ReadAllLines("file.txt");
            for (int i = 0; i < text.Length; i++)
            {
                my_text = my_text + text[i] + " ";
            }

            Lexem l;
            Tables t = new Tables();
            List<Lexem> Token = new List<Lexem>();
            LexAnalyzer Lexer = new LexAnalyzer(my_text);
            Parser Parsic = new Parser(Token);
            try
            {
                l = (Lexem)Lexer.Analyze();
                while (l.type != type_of_lex.LEX_FIN)
                {
                    Token.Add(l);
                    l = (Lexem)Lexer.Analyze();
                }
                int i = 0;
                Console.ReadLine();
                while (i < Token.Count())
                {
                    Console.WriteLine((Token[i].type).ToString());
                    i++;
                }
                Console.ReadLine();
                Parsic.Analyze();
                Console.ReadLine();
                Executer ex = new Executer(Parsic.Poliz);
                ex.Execute();
                Console.WriteLine("Interpretation:");
                for (int j = 0; j < Tables.TID.Count(); j++)
                {
                    Console.Write(Tables.TID[j] + " = ");
                    Console.WriteLine(Tables.Values[j]);
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                Console.ReadLine();
                Console.ReadLine();
            }
        }
    }
}
