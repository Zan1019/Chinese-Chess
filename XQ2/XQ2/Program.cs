using System;

namespace XQ2
{
    class Program
    {


        //初始化建立所有棋子       
        //车：Rook 马：Knight 相/象：Minister 炮：Cannon 兵/卒：Pawn 将/帅：King 士/仕：Guard
        //黑 Black
        static Rook b_Rook31 = new Rook(-31);
        static Rook b_Rook32 = new Rook(-32);

        static Knight b_Knight41 = new Knight(-41);
        static Knight b_Knight42 = new Knight(-42);

        static Minister b_Minister51 = new Minister(-51);
        static Minister b_Minister52 = new Minister(-52);

        static Guard b_Guard61 = new Guard(-61);
        static Guard b_Guard62 = new Guard(-62);

        static King b_King7 = new King(-7);

        static Cannon b_Cannon21 = new Cannon(-21);
        static Cannon b_Cannon22 = new Cannon(-22);

        static Pawn b_Pawn11 = new Pawn(-11);
        static Pawn b_Pawn12 = new Pawn(-12);
        static Pawn b_Pawn13 = new Pawn(-13);
        static Pawn b_Pawn14 = new Pawn(-14);
        static Pawn b_Pawn15 = new Pawn(-15);


        //红 Red
        static Rook r_Rook31 = new Rook(31);
        static Rook r_Rook32 = new Rook(32);

        static Knight r_Knight41 = new Knight(41);
        static Knight r_Knight42 = new Knight(42);

        static Minister r_Minister51 = new Minister(51);
        static Minister r_Minister52 = new Minister(52);

        static Guard r_Guard61 = new Guard(61);
        static Guard r_Guard62 = new Guard(62);

        static King r_King7 = new King(7);

        static Cannon r_Cannon21 = new Cannon(21);
        static Cannon r_Cannon22 = new Cannon(22);

        static Pawn r_Pawn11 = new Pawn(11);
        static Pawn r_Pawn12 = new Pawn(12);
        static Pawn r_Pawn13 = new Pawn(13);
        static Pawn r_Pawn14 = new Pawn(14);
        static Pawn r_Pawn15 = new Pawn(15);

        public static Piece[] pie = {   //16*2=32         
            b_Rook31, b_Rook32,b_Knight41,b_Knight42,b_Minister51,b_Minister52, //6
            b_Guard61,b_Guard62,b_King7,b_Cannon21,b_Cannon22,b_Pawn11,b_Pawn12, //7
            b_Pawn13,b_Pawn14,b_Pawn15, //3
            r_Rook31, r_Rook32,r_Knight41,r_Knight42,r_Minister51,r_Minister52,
            r_Guard61,r_Guard62,r_King7,r_Cannon21,r_Cannon22,r_Pawn11,r_Pawn12,
            r_Pawn13,r_Pawn14,r_Pawn15
        };
        //转换position给pie
        static public int ChangeTopie(int position)
        {
            if (position == -31)
                return 0;
            else if (position == -32)
                return 1;
            else if (position == -41)
                return 2;
            else if (position == -42)
                return 3;
            else if (position == -51)
                return 4;
            else if (position == -52)
                return 5;
            else if (position == -61)
                return 6;
            else if (position == -62)
                return 7;
            else if (position == -7)//b_King7
                return 8;
            else if (position == -21)
                return 9;
            else if (position == -22)
                return 10;
            else if (position == -11)
                return 11;
            else if (position == -12)
                return 12;
            else if (position == -13)
                return 13;
            else if (position == -14)
                return 14;
            else if (position == -15)
                return 15;
            else if (position == 31)//////////////////////////
                return 16;
            else if (position == 32)
                return 17;
            else if (position == 41)
                return 18;
            else if (position == 42)
                return 19;
            else if (position == 51)
                return 20;
            else if (position == 52)
                return 21;
            else if (position == 61)
                return 22;
            else if (position == 62)
                return 23;
            else if (position == 7)//b_King7
                return 24;
            else if (position == 21)
                return 25;
            else if (position == 22)
                return 26;
            else if (position == 11)
                return 27;
            else if (position == 12)
                return 28;
            else if (position == 13)
                return 29;
            else if (position == 14)
                return 30;
            else if (position == 15)
                return 31;



            return 0;
        }

        static Boolean Turn = true;

        //转换回合
        static public void TurnBlackandRed()
        {
            if (Turn)
                Turn = false;
            else
                Turn = true;
        }

        //确认所选定的棋子
        static public void Choise(Point destination)
        {
            int position = GameBoard.gameboard[destination.getX(), destination.getY()];
            int a = ChangeTopie(position);

            

            //展示位置
            Console.WriteLine("Please input the destination.");
            Point distination = (GetInputPoint());

            //判断是否错误，错误的话要求重新输入
            while (!pie[a].ValidMoves(distination))
            {
                Console.WriteLine("Please input right destination.");
                distination = (GetInputPoint());
                position = GameBoard.gameboard[destination.getX(), destination.getY()];

            }

            //输入正确            
            if (pie[a].ValidMoves(distination))
            {
                int b = GameBoard.gameboard[distination.getX(), distination.getY()];
                //如果有对方棋子
                if (b != 0)
                {
                    pie[ChangeTopie(b)].Eaten();
                }
                pie[a].setPosition(distination);                
            }
            //转换回合
            TurnBlackandRed();

        }

        //判断输入是不是合适的坐标
        public static Boolean Judge(String str)
        {
            string[] nums = str.Split(new char[2] { ' ', ',' });
            //判断是不是两个字符
            if (nums.Length != 2)
            {
                return false;
            }
            //判断是不是数字
            for (int i = 0; i < nums.Length; i++)
            {
                try
                {
                    Convert.ToInt32(nums[i]);
                    //Console.Write("是数字");
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            if(Convert.ToInt32(nums[1])>9|| Convert.ToInt32(nums[1]) < 0)
            {
                return false;
            }
            if (Convert.ToInt32(nums[0]) > 8 || Convert.ToInt32(nums[0]) < 0)
            {
                return false;
            }

            return true;
        }

        //获取用户输入坐标
        public static Point GetInputPoint()
        {
            Console.WriteLine("Place input point(x,y):");
            String str = Console.ReadLine();
            //
            while (!Judge(str))
            {
                Console.WriteLine("Your input is wrong, place input point like 1,1 or 1 2.");
                Console.WriteLine("Place input point(x,y):");
                str = Console.ReadLine();
            }

            string[] nums = str.Split(new char[2] { ' ', ',' });
                              
            return new Point(Convert.ToInt32(nums[1]), Convert.ToInt32(nums[0]));
        }

        //判断还有没棋可以走，如果没有返回false
        public static Boolean JudgeLose()
        {
            int count = 0;
            int num = 0;
            if (Turn)
            {
                //红方
                for(int i = 16; i <= 31; i++)
                {
                    if(pie[i].GetGameBoardposition() != 0)
                    {
                        count++;
                    }
                    if (pie[i].GetGameBoardposition()!=0&&pie[i].GetMove() == null)
                    {
                        num++;
                    }
                }
                if (count == num)
                {
                    return false;
                }
            }
            else if (!Turn)
            {
                //黑方
                count = 0;
                num = 0;
                for (int i = 0; i <= 15; i++)
                {
                    if (pie[i].GetGameBoardposition() != 0)
                    {
                        count++;
                    }
                    if (pie[i].GetMove() == null)
                    {
                        num++;
                    }
                    if (count == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


            static void Main(string[] args)
        {

            Boolean JudgeTurn = true;
            while (pie[ChangeTopie(7)].GetGameBoardposition()!= 0 && pie[ChangeTopie(-7)].GetGameBoardposition() != 0)
            {
                
                GameDisplay display = new GameDisplay();
                Console.Clear();
                display.DisplayBoard();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                //判定是不是死棋，还能不能走
                if (!JudgeLose())
                {
                    if (Turn)
                    {
                        Console.WriteLine("There is no pieces you can move, you lose, and Black win!");
                        break;
                    }
                    else if (!Turn)
                    {
                        Console.WriteLine("There is no pieces you can move, you lose, and Red win!");
                        break;
                    }


                }
                //选中棋子
                if (Turn)
                {
                    Console.WriteLine("Now is Red side.");
                }
                else
                {
                    Console.WriteLine("Now is Black side.");
                }

                Console.WriteLine("Please choose point.");

                Point pa = GetInputPoint();
                int position = GameBoard.gameboard[pa.getX(), pa.getY()];
                //给JudgeTurn赋值
                if (position > 0)
                {
                    JudgeTurn = true;
                }
                else if (position < 0)
                {
                    JudgeTurn = false;
                }
                while (GameBoard.gameboard[pa.getX(), pa.getY()] == 0 || JudgeTurn != Turn)
                {
                    if (GameBoard.gameboard[pa.getX(), pa.getY()] == 0)
                    {
                        Console.WriteLine("Please choose pieces but not blank.");
                        pa = GetInputPoint();                        
                    }//判定是不是这一方走
                    else if (JudgeTurn != Turn)
                    {
                        Console.WriteLine("Please choose the right side.");
                        if (Turn)
                        {
                            Console.WriteLine("Now is Red side.");
                        }
                        else
                        {
                            Console.WriteLine("Now is Black side.");
                        }
                        pa = GetInputPoint();
                        position = GameBoard.gameboard[pa.getX(), pa.getY()];

                        if (position > 0)
                        {
                            JudgeTurn = true;
                        }
                        else if (position < 0)
                        {
                            JudgeTurn = false;
                        }
                    }

                }

                //判定成功后

                Console.Clear();
                GameDisplay.Show(pie[ChangeTopie(GameBoard.gameboard[pa.getX(), pa.getY()])].GetMove());
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                Choise(pa);

            }
            Console.WriteLine("");
            Console.WriteLine("Game over and put any key to close.");
            Console.ReadKey();
            
            
        
        }
    }
}