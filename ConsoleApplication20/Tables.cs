using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    enum type_of_lex
    {
        LEX_NULL,
        LEX_BEGIN,
        LEX_END,
        LEX_IF,
        LEX_ELSE,
        LEX_WHILE,
        LEX_THEN,
        LEX_TRUE,
        LEX_FOR,
        LEX_INT,
        LEX_FALSE,

        LEX_SEMICOLON,//;
        LEX_COLON, //TD.Add(",");2
        LEX_Q, //TD.Add("=");3
        LEX_EQ,//TD.Add("==");4
        LEX_NEQ,//TD.Add("!=");5
        LEX_LSS,//TD.Add("<");6
        LEX_GRT,//TD.Add(">");7
        LEX_EQ_LSS,//TD.Add("<=");//8
        LEX_EQ_GRT,//TD.Add(">=");//9
        LEX_PLUS,//TD.Add("+");//10
        LEX_MINUS,//TD.Add("-");//11
        LEX_MLT,//TD.Add("*");//12
        LEX_SLASH,//TD.Add("/");//13
        LEX_LPAREN,//TD.Add("(");//14
        LEX_PPAREN,//TD.Add(")");//15
        LEX_SEM,
        LEX_DO,
        LEX_JUMP,
        LEX_NOT_JUMP,

        LEX_TID,
        LEX_NUM,
        LEX_FIN,
        LEX_VAR,
        POLIZ_ADDRESS,
        POLIZ_LABEL,
    };


    class Tables
    {
        public static List<string> TW = new List<string>();//служебные слова
        public static List<string> TD = new List<string>();//ограничителя языка
        public static List<string> TID = new List<string>();//наши личные идентификаторы
        public static List<int> Values = new List<int>();// значения переменных
        public static type_of_lex[] words =
        {
            type_of_lex.LEX_NULL,
            type_of_lex.LEX_BEGIN,
            type_of_lex.LEX_END,
            type_of_lex.LEX_IF,
            type_of_lex.LEX_ELSE,
            type_of_lex.LEX_WHILE,
            type_of_lex.LEX_THEN,
            type_of_lex.LEX_TRUE,
            type_of_lex.LEX_FOR,
            type_of_lex.LEX_INT,
            type_of_lex.LEX_FALSE,
            type_of_lex.LEX_FIN,
            type_of_lex.LEX_VAR,
            type_of_lex.LEX_DO,
            type_of_lex.LEX_NUM
        };
        public static type_of_lex[] dlmns =
        {
            type_of_lex.LEX_SEMICOLON,//;
            type_of_lex.LEX_COLON, //TD.Add(",");2
            type_of_lex.LEX_Q, //TD.Add("=");3
            type_of_lex.LEX_EQ,//TD.Add("==");4
            type_of_lex.LEX_NEQ,//TD.Add("!=");5
            type_of_lex.LEX_LSS,//TD.Add("<");6
            type_of_lex.LEX_GRT,//TD.Add(">");7
            type_of_lex.LEX_EQ_LSS,//TD.Add("<=");//8
            type_of_lex.LEX_EQ_GRT,//TD.Add(">=");//9
            type_of_lex.LEX_PLUS,//TD.Add("+");//10
            type_of_lex.LEX_MINUS,//TD.Add("-");//11
            type_of_lex.LEX_MLT,//TD.Add("*");//12
            type_of_lex.LEX_SLASH,//TD.Add("/");//13
            type_of_lex.LEX_LPAREN,//TD.Add("(");//14
            type_of_lex.LEX_PPAREN,//TD.Add(")");//15
            type_of_lex.LEX_SEM,
            type_of_lex.LEX_NOT_JUMP,
            type_of_lex.LEX_JUMP
        };

        public Tables()
        {
            TW.Add("");
            TW.Add("begin");//1
            TW.Add("end");//2
            TW.Add("if");//3
            TW.Add("else");//4
            TW.Add("while");//5
            TW.Add("then");//6
            TW.Add("true");//7
            TW.Add("for");//8
            TW.Add("integer");//9
            TW.Add("false");//10
            TW.Add("$");//11
            TW.Add("var");//12
            TW.Add("do");//13

            TD.Add(";");//1
            TD.Add(",");//2
            TD.Add("=");//3
            TD.Add("==");//4
            TD.Add("!=");//5
            TD.Add("<");//6
            TD.Add(">");//7
            TD.Add("<=");//8
            TD.Add(">=");//9
            TD.Add("+");//10
            TD.Add("-");//11
            TD.Add("*");//12
            TD.Add("/");//13
            TD.Add("(");//14
            TD.Add(")");//15
            TD.Add(":");
            TD.Add("!F");
            TD.Add("!");
        }
        public static List<Lexem> Identificators = new List<Lexem>();
    }
}
