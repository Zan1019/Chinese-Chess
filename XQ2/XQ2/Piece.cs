using System;
using System.Collections.Generic;
using System.Text;

namespace XQ2


{
    //车：Rook 马：Knight 相/象：Minister 炮：Cannon 兵/卒：Pawn 将/帅：King 士/仕：Guard

    abstract class Piece
    {
        protected int x, y, position;
        protected Point p;
        //无参构造器，防止编译报错
        public Piece()
        {

        }

        //构造器获得坐标以及转换行和列
        public Piece(int position)
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (GameBoard.gameboard[i,j] == position)
                    {
                        x = j;
                        y = i;
                        this.p = new Point(i, j);
                        this.position = position;
                    }
                }
            }
        }

        //返回棋子坐标
        public Point getPosition()
        {
            return this.p;
        }

        //判断棋子目的地是不是错的
        protected Boolean judge(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();

            //判断目的地是不是在棋盘外
            if (pdx > 8 || pdx < 0 || pdy < 0 || pdy > 9)
            {
                // Console.WriteLine("在棋盘外");
                return false;
            }

            //判断目的地有没有己方棋子
            if (position > 0 && GameBoard.gameboard[destination.getX(), destination.getY()] > 0)
            {
                return false;
            }
            if (position < 0 && GameBoard.gameboard[destination.getX(), destination.getY()] < 0)
            {
                return false;
            }

            //判断是不是没有移动
            if (pdx == x && pdy == y)
            {
                return false;
            }

            //判断移动后将是不是会相对
            
            //当两个将在同一列时
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //这有BUG
            if (Program.pie[Program.ChangeTopie(7)].getPosition().getY() == Program.pie[Program.ChangeTopie(-7)].getPosition().getY())
            {
                int Line = Program.pie[Program.ChangeTopie(7)].getPosition().getY();

                int count = 0;
                //黑将的y坐标
                int i = Program.pie[Program.ChangeTopie(-7)].getPosition().getX()+1;
                //红帅的y坐标
                for (; i < Program.pie[Program.ChangeTopie(7)].getPosition().getX()-1; i++)
                {
                    //如果棋子移动后的地方在两个将之间
                    if (i == destination.getX() && destination.getY() == Line)
                    {
                        count++;
                    }

                    if (GameBoard.gameboard[i, Line] != 0 && i != y)
                    {
                        count++;
                    }

                }
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    count = 0;
                }

            }
            

            return true;

            

        }
        
       //返回棋子的名字
        public override String ToString()
        {
           
            return "";
        }

        //判断棋子能不能走到目的地，能返回true。
        public abstract Boolean ValidMoves(Point destination);

        //返回所有能走的位置的坐标
        public abstract Point[] GetMove();
        //获得position
        public int GetGameBoardposition()
        {
            return position;
        }      

        //设置棋子坐标
        public void setPosition(Point p)
        {
            GameBoard.gameboard[this.p.getX(), this.p.getY()] = 0;
            this.p = p;
            x = p.getY();
            y = p.getX();
            GameBoard.gameboard[p.getX(), p.getY()] = position;
        }

        //被吃
        public void Eaten()
        {
            this.position = 0;
            Console.WriteLine(ToString()+" was eaten.");
        }



    }

    class Knight : Piece
    {
        //构造器获得马的坐标和把数组行和列换成坐标Point
        public Knight(int position):base(position)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }
        //以原点为坐标判断马可能走的终点
        private static Point Point1 = new Point(1, 2);
        private static Point Point2 = new Point(2, 1);
        private static Point Point3 = new Point(1, -2);
        private static Point Point4 = new Point(-2, 1);
        private static Point Point5 = new Point(-1, -2);
        private static Point Point6 = new Point(-2, -1);
        private static Point Point7 = new Point(-1, 2);
        private static Point Point8 = new Point(2, -1);
        private Point[] Move = { Point1, Point2, Point3, Point4, Point5, Point6, Point7, Point8 };

        public override String ToString()
        {
            //是不是红方
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "馬";
            }
            else//是不是黑方
            {
                Console.ForegroundColor = ConsoleColor.Black;
                return "马";
            }
            
            
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }

            for (int i = 0; i < Move.Length; i++)
            { 
                    //判断是不是走日
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        //Console.WriteLine("是走日");
                        //判断有没有被蹩脚马
                        if (Move[i].getX() == 2 || Move[i].getX() == -2)
                        {
                            if (GameBoard.gameboard[y, x + Move[i].getX() / 2] == 0)
                            {
                                return true;
                            }
                        }
                        else if (GameBoard.gameboard[y - Move[i].getY() / 2,x] == 0)
                        {
                            return true;
                        }
                        //Console.WriteLine("被蹩脚马");

                    }
                    //Console.WriteLine("不是走日");
            }
            //Console.WriteLine("x={0},y={1}",x,y);
            
            
            return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[Move.Length];
            Point movep;
            for (int i = 0; i < Move.Length; i++)
            {
                movep = new Point(y - Move[i].getY(), x + Move[i].getX());
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }

    class King : Piece
    {
        //构造器获得兵的坐标和把数组行和列换成坐标Point
        public King(int position) : base(position)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }
        //以原点为坐标判断象可能走的终点
        private static Point Point1 = new Point(1, 0);
        private static Point Point2 = new Point(-1, 0);
        private static Point Point3 = new Point(0, 1);
        private static Point Point4 = new Point(0, -1);

        private readonly Point[] Move = { Point1, Point2, Point3, Point4 };

        public override String ToString()
        {
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "帅";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                return "将";
            }
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }

            //红,目的地在九宫格内
            if (position > 0 && pdx >= 3 && pdx <= 5 && pdy >= 7 && pdy <= 9)
            {

                for (int i = 0; i < Move.Length; i++)
                {
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;
                    }
                }
            }//黑,目的地在九宫格内
            else if (position < 0 && pdx >= 3 && pdx <= 5 && pdy >= 0 && pdy <= 2)
            {
                for (int i = 0; i < Move.Length; i++)
                {
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[Move.Length];
            Point movep;
            for (int i = 0; i < Move.Length; i++)
            {
                movep = new Point(y - Move[i].getY(), x + Move[i].getX());
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }

    class Guard : Piece
    {
        //构造器获得兵的坐标和把数组行和列换成坐标Point
        public Guard(int position) : base(position)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }
        //以原点为坐标判断象可能走的终点
        private static Point Point1 = new Point(1, 1);
        private static Point Point2 = new Point(-1, 1);
        private static Point Point3 = new Point(1, -1);
        private static Point Point4 = new Point(-1, -1);

        private readonly Point[] Move = { Point1, Point2, Point3, Point4 };

        public override String ToString()
        {
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "士";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                return "仕";
            }
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }

            //红,目的地在九宫格内
            if (position > 0 && pdx >= 3 && pdx <= 5 && pdy >= 7 && pdy <= 9)
            {

                for (int i = 0; i < Move.Length; i++)
                {
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;
                    }
                }
            }//黑,目的地在九宫格内
            else if (position < 0 && pdx >= 3 && pdx <= 5 && pdy >= 0 && pdy <= 2)
            {
                for (int i = 0; i < Move.Length; i++)
                {
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[Move.Length];
            Point movep;
            for (int i = 0; i < Move.Length; i++)
            {
                movep = new Point(y - Move[i].getY(), x + Move[i].getX());
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }

    class Minister : Piece
    {
        //构造器获得象的坐标和把数组行和列换成坐标Point
        public Minister(int position) : base(position)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }
        //以原点为坐标判断象可能走的终点
        private static Point Point1 = new Point(2, 2);
        private static Point Point2 = new Point(2, -2);
        private static Point Point3 = new Point(-2, 2);
        private static Point Point4 = new Point(-2, -2);
        private readonly Point[] Move = { Point1, Point2, Point3, Point4 };

        public override String ToString()
        {
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "相";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            return "象";
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }
            //判断过河
            if (position > 0&& destination.getX() <= 4)
            {
                return false;
            }
            else if (position < 0 && destination.getX() > 4)
            {
                return false;
            }



                for (int i = 0; i < Move.Length; i++)
            { 
                    //判断是不是走田
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;

                    }
                
            }




            return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[Move.Length];
            Point movep;
            for (int i = 0; i < Move.Length; i++)
            {
                movep = new Point(y - Move[i].getY(), x + Move[i].getX());
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }

    class Pawn : Piece
    {
        //构造器获得兵的坐标和把数组行和列换成坐标Point
        public Pawn(int position) : base(position)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }
        //以原点为坐标判断象可能走的终点
        private static Point Point1 = new Point(1, 0);
        private static Point Point2 = new Point(-1, 0);
        private static Point Point3 = new Point(0, 1);
        private static Point Point4 = new Point(0, -1);

        private readonly Point[] Move = { Point1, Point2, Point3, Point4 };

        public override String ToString()
        {
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "兵";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                return "卒";
            }
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }

            //判断有没有过河和是红还是黑
            if (position > 0&& pdy > 4)
            {
                //没有过河，红
                //Console.WriteLine("没有过河");
                if (0 == (pdx - x) && 1 == (y - pdy))
                {
                    return true;
                }
            }//没有过河，黑
            else if(position < 0 && pdy < 5)
            {
                if (0 == (pdx - x) && -1 == (y - pdy))
                {
                    return true;
                }
            }//过河，红
            else if(position > 0 && pdy <= 4)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;
                    }
                    else if (0 == (pdx - x) && 1 == (y - pdy))
                    {
                        return true;
                    }
                }

            }//过河，黑
            else if (position < 0 && pdy >= 5)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Move[i].getX() == (pdx - x) && Move[i].getY() == (y - pdy))
                    {
                        return true;
                    }
                    else if (0 == (pdx - x) && -1 == (y - pdy))
                    {
                        return true;
                    }
                }
            }


                return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[Move.Length];
            Point movep;
            for (int i = 0; i < Move.Length; i++)
            {
                movep = new Point(y - Move[i].getY(), x + Move[i].getX());
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }
    
    class Rook : Piece
    {
        //构造器获得兵的坐标和把数组行和列换成坐标Point
        public Rook(int position) : base(position)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }
        
        public override String ToString()
        {
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "車";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                return "车";
            }
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }

            //竖着走,向下
            if (pdx==x && pdy > y)
            {
                //目的地和坐标间有无其它棋子
                for(int i = y+1; i < pdy; i++)
                {
                    if (GameBoard.gameboard[i, x] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }//竖着走,向上
            else if (pdx == x && pdy < y)
            {
                //目的地和坐标间有无其它棋子
                for (int i = pdy + 1; i < y; i++)
                {
                    if (GameBoard.gameboard[i, x] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }

            //横着走,向下
            if (pdy == y && pdx > x)
            {
                //目的地和坐标间有无其它棋子
                for (int i = x + 1; i < pdx; i++)
                {
                    if (GameBoard.gameboard[y, i] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }//竖着走,向上
            else if (pdy == y && pdx < x)
            {
                //目的地和坐标间有无其它棋子
                for (int i = pdx + 1; i < x; i++)
                {
                    if (GameBoard.gameboard[y, i] != 0)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[20];
            Point movep;
            //走行
            for (int i = 0; i < 8; i++)
            {
                movep = new Point(y, i);
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //走列
            for (int j = 0; j < 9; j++)
            {
                movep = new Point(j, x);
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }

            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }

    class Cannon : Piece
    {
        //构造器获得炮的坐标和把数组行和列换成坐标Point
        public Cannon(int position) : base(position)
        {
            GameBoard gb = new GameBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard.gameboard[i, j] == position)
                    {
                        x = j;
                        y = i;
                        Point p = new Point(x, y);
                    }
                }
            }
        }

        public override String ToString()
        {
            if (position > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "砲";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                return "炮";
            }
        }

        public override Boolean ValidMoves(Point destination)
        {
            //转换行列到坐标
            int pdx = destination.getY();
            int pdy = destination.getX();
            //简单判断目的地有没有问题
            if (!judge(destination))
            {
                return false;
            }

            //竖着走,向下
            if (pdx == x && pdy > y)
            {
                int num = 0;
                //目的地和坐标间有无其它棋子
                for (int i = y + 1; i <= pdy; i++)
                {
                    if (GameBoard.gameboard[i, x] != 0)
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    return true;
                }//可以吃子
                else if (num == 2 && GameBoard.gameboard[pdy, pdx] != 0)
                {
                    return true;
                }
            }//竖着走,向上
            else if (pdx == x && pdy < y)
            {
                int num = 0;
                //目的地和坐标间有无其它棋子
                for (int i = pdy; i < y; i++)
                {

                    if (GameBoard.gameboard[i, x] != 0)
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    return true;
                }//可以吃子
                else if (num == 2 && GameBoard.gameboard[pdy, pdx] != 0)
                {
                    return true;
                }
            }

            //横着走,向下
            if (pdy == y && pdx > x)
            {
                int num = 0;
                //目的地和坐标间有无其它棋子
                for (int i = x + 1; i <= pdx; i++)
                {
                    if (GameBoard.gameboard[y, i] != 0)
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    return true;
                }//可以吃子
                else if (num == 2 && GameBoard.gameboard[pdy, pdx] != 0)
                {
                    return true;
                }
            }//竖着走,向上
            else if (pdy == y && pdx < x)
            {
                int num = 0;
                //目的地和坐标间有无其它棋子
                for (int i = pdx; i < x; i++)
                {
                    if (GameBoard.gameboard[y, i] != 0)
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    return true;
                }//可以吃子
                else if (num == 2 && GameBoard.gameboard[pdy, pdx] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public override Point[] GetMove()
        {
            int a = 0;
            Point[] getmove = new Point[20];
            Point movep;
            //走行
            for (int i = 0; i < 9; i++)
            {
                movep = new Point(y, i);
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }
            //走列
            for (int j = 0; j < 10; j++)
            {
                movep = new Point(j, x);
                if (ValidMoves(movep))
                {
                    getmove[a] = movep;
                    a++;
                }
            }

            //删除多余数组
            Point[] r = new Point[a];
            for (int i = 0; i < a; i++)
            {
                r[i] = getmove[i];
            }
            return r;
        }

    }

}
