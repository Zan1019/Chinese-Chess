using System;
using System.Collections.Generic;
using System.Text;

namespace XQ2
{
    class GameDisplay
    {


        //画棋盘
        public void DisplayBoard()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            int line = 0;
            for (int i = 0; i < 10; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    
                    if (GameBoard.gameboard[i, j] == 0)
                    {
                        //画没棋子的空白位置
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("十");
                    }                        
                    else
                    {
                        //画棋子
                        Console.Write(Program.pie[Program.ChangeTopie(GameBoard.gameboard[i, j])].ToString());
                    }

                    if (j < 8)
                    {
                        //连接格子
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("一");
                    }
                       
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(" ");
                Console.Write(line);
                line++;
                if (i < 9)
                {

                    



                    if (i != 4)
                    {

                        Console.Write("\n");
                        for(int j = 0; j < 9; j++)
                        {
                            //画斜杠
                            if ((i == 0 || i == 7) && (j == 3 || j == 4))
                            {
                                if (j == 3)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("┃ ╲ ");
                                }
                                if (j == 4)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("┃ ╱ ");

                                }
                            }
                            else if ((i == 1 || i == 8) && (j == 3 || j == 4))
                            {
                                if (j == 3)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("┃ ╱ ");
                                }
                                if (j == 4)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("┃ ╲ ");

                                }
                            }
                            else
                            {
                                //连接行
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("┃   ");
                            }

                            
                        }
                    }
                    if (i == 4)
                    {
                        Console.Write("\n");
                        for (int j = 0; j < 9; j++)
                        {
                            //画河
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("    ");
                        }
                    }

                    Console.Write("\n");
                }

            }
            //画辅助坐标
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("0   1   2   3   4   5   6   7   8   ");

        }
        
        //判断上下左右有没有被选中点
        public static Boolean JudgeChoosedPoint(int i, int j, Point[] ps) 
        {
            for(int num = 0; num < ps.Length; num++)
            {
                
                /*if (i-1 == ps[num].getX()&& j == ps[num].getY())
                {
                    return false;
                }

                if (i+1 == ps[num].getX() && j == ps[num].getY())
                {
                    return false;
                }*/

                if (i == ps[num].getX() && j == ps[num].getY())
                {
                    return false;
                }

                if (i == ps[num].getX() && j+1 == ps[num].getY())
                {
                    return false;
                }
            }
            return true;
        }

        public static Boolean JCP2(int i, int j, Point[] ps)
        {
            for (int num = 0; num < ps.Length; num++)
            {
                //上方
                if (i == ps[num].getX() && j == ps[num].getY())
                {
                    return false;
                }
                //下方
                if (i+1 == ps[num].getX() && j == ps[num].getY())
                {
                    return false;
                }

              
            }
            return true;
        }

        //显示可走位置
        public static void Show(Point[] ps)
        {
            Console.BackgroundColor =ConsoleColor.DarkYellow;
            int line = 0;
                for (int i = 0; i < 10; i++)
                {

                    for (int j = 0; j < 9; j++)
                    {

                    if (GameBoard.gameboard[i, j] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("十");
                    }
                    else
                    {
                        Console.Write(Program.pie[Program.ChangeTopie(GameBoard.gameboard[i, j])].ToString());
                    }

                    if (j < 8 && JudgeChoosedPoint(i, j, ps))
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("一");
                    }//改动
                    else if (j < 8 && !JudgeChoosedPoint(i, j, ps))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("一");
                    }

                }


                Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(line);
                    line++;
                    if (i < 9)
                    {                    
                    if (i != 4)
                        {

                        Console.Write("\n");//改动
                        for (int j = 0; j < 9; j++)
                        {
                            //画斜杠
                            if ((i == 0 || i == 7) && (j == 3 || j == 4))
                            {
                                if (j == 3)
                                {
                                    if (JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("┃ ╲ ");
                                    }
                                    else if (!JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("┃ ");
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("╲ ");
                                    }
                                }
                                if (j == 4)
                                {
                                    if (JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("┃ ╱ ");
                                    }
                                    else if (!JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("┃ ");
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("╱ ");
                                    }


                                }
                            }
                            else if ((i == 1 || i == 8) && (j == 3 || j == 4))
                            {
                                if (j == 3)
                                {
                                    if (JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("┃ ");

                                        Console.Write("╱ ");
                                    }
                                    else if (!JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("┃ ");
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("╱ ");
                                    }

                                }
                                if (j == 4)
                                {
                                    if (JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("┃ ╲ ");
                                    }
                                    else if (!JCP2(i, j, ps))
                                    {
                                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("┃ ");
                                        Console.ForegroundColor = ConsoleColor.Black;
                                        Console.Write("╲ ");
                                    }

                                }
                            }
                            else
                            {
                                if (JCP2(i, j, ps))
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write("┃   ");
                                }
                                else if (!JCP2(i, j, ps))
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write("┃   ");
                                }
                            }
                            
                        }
                    }
                        if (i == 4)
                        {
                        Console.Write("\n");
                        for (int j = 0; j < 9; j++)
                        {

                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("    ");
                        }
                    }

                        Console.Write("\n");
                    }

                }
                Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("0   1   2   3   4   5   6   7   8   ");

            
        }
        
       

    }
}

        
