using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Board //работа с доской
    {
        public string fen { get; private set; }
        Figure[,] figures;
        public Color moveColor { get; private set; } //чей ход?
        public int moveNumber { get; private set; }  //номер хода


        public Board(string fen)
        {
            this.fen = fen;
            figures = new Figure[8, 8];
            Init();
        }

        void Init() //инициализация фигур
        {
            //rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
            string[] parts = fen.Split();
            if (parts.Length != 6) return;
            InitFigures(parts[0]);
            moveColor = (parts[1] == "b") ? Color.black : Color.white;
            moveNumber = int.Parse(parts[5]);
        }

        void InitFigures(string data)
        {
            for (int j = 8; j >= 2; j--)
                data = data.Replace(j.ToString(), (j-1).ToString() + "1");
            data = data.Replace("1", ".");
            string[] lines = data.Split('/'); 
            for(int y = 7; y >= 0; y--)
                for(int x =0; x <8; x++)
                    figures[x, y] = lines[7-y][x]== '.' ? Figure.none : (Figure)lines[7-y][x];
        }

        void GenerateFEN()
        {
            fen =  FenFigures() + " " +
                (moveColor ==Color.white ? "w" : "b") +
                " - - 0 " + moveNumber.ToString();
        }

        string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                if (y > 0)
                    sb.Append('/');
            }
            string eight = "11111111";
            for(int j = 8; j >=2; j--)
                sb.Replace(eight.Substring(0,j), j.ToString());
            return sb.ToString();
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach(Square square in Square.YieldSquare())
                if ( GetFigureAt(square).GetColor() == moveColor)  //если на квадрете находится фигур того цвета, чей сейчас ход, то мы ее возвращаем
                    yield return new FigureOnSquare(GetFigureAt(square), square);
        }

        public Figure GetFigureAt(Square square)  //получение фигур "какая на какой клетке?"
        {
            if(square.OnBoard())
                return figures[square.x, square.y];
            return Figure.none;
        } 

        void SetFigureAt(Square square, Figure figure)  //для установки фигур
        {
            if (square.OnBoard())
                figures[square.x, square.y] = figure;
        }

        public Board Move(FigureMoving fm) //движение
        {
            Board next = new Board(fen);  //отличие только в перемещении 1-2 фигур
            next.SetFigureAt(fm.from, Figure.none);  //откуда пошла фигура, на той клетке будет пусто
            next.SetFigureAt(fm.to, fm.promotion == Figure.none ? fm.figure : fm.promotion);  //куда она пошла, то там появится фигура
            if (moveColor == Color.black) //увеличиваем ход
                next.moveNumber++;
            next.moveColor = moveColor.FlipColor();  //меняем цвет фигуры для хода другого игрока
            next.GenerateFEN();
            return next;
        }
    }
}
