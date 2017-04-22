using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EBNFParser
{
    class Program
    {
        List<string> outputLog = new List<string>();
        
        public int depth = 0;

        static void Main(string[] args)
        {
            Program _parser = new Program();
            string s = "";
            Console.WriteLine("Enter first line of code ");
            s = Console.ReadLine();
            _parser.ParseStatementList(s);
            foreach(string line in _parser.outputLog)
            {
                Console.WriteLine(line);
            }
        }

        public void ParseStatementList(string s)
        {
            switch (s)
            {
                case "begin.":
                    {
                        printDepth();
                        Console.WriteLine("<statement_list>");
                        outputLog.Add("<statement_list >");
                        depth++;
                        printDepth();
                        Console.WriteLine(s);
                        outputLog.Add(s);
                        depth--;
                        parseStatement();
                       
                        break;
                    }
                default:
                    {
                        Console.WriteLine("not a valid instruction, moving on");
                        break;
                    }
            }
        }

        public void parseStatement()
        {
            depth++;
            string s = Console.ReadLine();
            switch (s)
            {
                case "end.":
                    {
                        printDepth();
                        Console.WriteLine(s);
                        outputLog.Add(s);
                        parseStatement();
                        depth--;
                        break;
                    }
                case "else:":
                    {
                        s = Console.ReadLine();
                        ParseStatementList(s);
                        depth--;
                        break;
                    }
                default:
                    {
                        printDepth();
                        Console.WriteLine("<statement>");
                        outputLog.Add("<statement>");
                        depth--;

                        string[] seperatedStatement = s.Split(' ');

                        switch (seperatedStatement[0])
                        {
                            case "if":
                                {
                                    parseIf(s);
                                    parseStatement();
                                    break;
                                }
                            case "while":
                                {
                                    parseWhile(s);
                                    parseStatement();
                                    break;
                                }
                            case "print":
                                {
                                    parsePrint(s);
                                    parseStatement();
                                    break;
                                }
                            default:
                                {
                                    if(stringIsIdentifier(seperatedStatement[0]))
                                    {
                                        parseAssignment(s);
                                    } else
                                    {
                                        Console.WriteLine(s + " is not a valid line, moving on");
                                    }
                                    parseStatement();
                                    break;
                                }
                                
                        }
                        break;
                    }
            }
        }

        public void parseIf(string s)
        {
            string[] seperatedStatement = s.Split(' ');
            depth++;
            printDepth();
            Console.WriteLine("<if_statement>");
            outputLog.Add("<if_statement>");
            depth++;
            printDepth();
            Console.WriteLine("if");
            outputLog.Add("if");
            parseCondition(seperatedStatement[1], seperatedStatement[2], seperatedStatement[3]);
            s = Console.ReadLine();
            ParseStatementList(s);
            depth--;
            depth--;


        }

        public void parseCondition(string idNum1, string comparison, string idNum2)
        {
            depth++;
            printDepth();
            Console.WriteLine("<condition>");
            outputLog.Add("<condition>");
            if(stringIsIdentifier(idNum1))
            {
                parseIdintifier(idNum1);
            } else
            {
                if (stringIsNumber(idNum1))
                {
                    parseNumber(idNum1);
                }
            }
            parseComparison(comparison);
            if (stringIsIdentifier(idNum2))
            {
                parseIdintifier(idNum2);
            }
            else
            {
                if (stringIsNumber(idNum2))
                {
                    parseNumber(idNum2);
                }
            }
            depth--;

        }
        public void parseIdintifier(string s)
        {
            depth++;
            printDepth();
            Console.WriteLine("<identifier>");
            outputLog.Add("<identifier>");
            depth++;
            printDepth();
            Console.WriteLine(s);
            depth--;
            depth--;
        }

        public void parseNumber(string s)
        {
            depth++;
            printDepth();
            Console.WriteLine("<number>");
            outputLog.Add("<number>");
            depth++;
            printDepth();
            Console.WriteLine(s);
            depth--;
            depth--;
        }

        public void parseComparison(string s)
        {
            printDepth();
            Console.WriteLine(s);
        }


        public void parseWhile(string s)
        {
            Console.WriteLine("<while_loop>");
            outputLog.Add("<while_loop>");
            depth++;
            printDepth();
            Console.WriteLine("while");
            outputLog.Add("while");
         }

        public void parseAssignment(string s)
        {
            depth++;
            printDepth();
            Console.WriteLine("<assignment>");
            outputLog.Add("<assignment>");
            string[] seperatedStatement = s.Split(' ');
            parseIdintifier(seperatedStatement[0]);
            printDepth();
            Console.WriteLine("=");
            outputLog.Add("=");
            if (stringIsIdentifier(seperatedStatement[2]))
            {
                parseIdintifier(seperatedStatement[2]);
            } else
            {
                if (stringIsNumber(seperatedStatement[2]))
                {
                    parseNumber(seperatedStatement[2]);
                }
            }
            depth--;
        }

        public void parsePrint(string s)
        {
            depth++;
            printDepth();
            Console.WriteLine("<print>");
            outputLog.Add("<print>");
            if (stringIsIdentifier(s))
            {
                parseIdintifier(s);

            } else
            {
                if (stringIsNumber(s))
                {
                    parseNumber(s);
                }
            }
            depth--;
        }

        public bool stringIsIdentifier(string s)
        {
            bool id = false;
            if (s == "A" || s == "B" || s == "C" || s == "D" || s == "E" || s == "F" || s == "G" || s == "H")
            {
                id = true;
            }
            return id;
        }

        public bool stringIsNumber(string s)
        {
            bool num = false;
            if (s == "1" || s == "2" || s == "3" || s == "4" || s == "5" || s == "6" || s == "7" || s == "8" || s == "9" || s == "0")
            {
                num = true;
            }
            return num;
        }

        public void printDepth()
        {
            string s = "";
            for(int i = 0; i < depth; i++)
            {
                Console.Write("|");
                s += "|";

            }
            outputLog.Add(s);
        }
    }
}
