using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Chess
    {
        public string fen { get; private set; }
        Board board;
        Moves moves;
        List<FigureMoving> allMoves;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1") //начальная позиция шахмат
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
        }

        Chess(Board board)  //создание "новой" доски
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);
        }

        public Chess Move (string move)  //ход фигуры
        {
            FigureMoving fm = new FigureMoving(move);  //генерация хода
            if(!moves.CanMove(fm))
                return this;
            Board nextBoard = board.Move(fm); //сделали на новой доске ход
            Chess nextChess = new Chess(nextBoard);  //создание нового объекта шахмат после хода
            return nextChess;
        }

        public char GetFigureAt(int x, int y)  //посмотреть где находится фигура
        {
           Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);  //получить фигуру, которая находится на этой клетке
            return f == Figure.none ? '.' : (char)f;  //если "ненормальная клетка то вернется "." , если "нормальная", то вернется соответствующая фигура
        }

        void FindAllMoves()         //как найти все ходы
        {
            allMoves = new List<FigureMoving>();
            foreach(FigureOnSquare fs in board.YieldFigures())  //перебор всех фигур на доске, того цвета фигур, который сейчас ходит
                foreach(Square to in Square.YieldSquare())      //перебор всех клеток на доске, куда мы можем пойти
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if(moves.CanMove(fm))
                        allMoves.Add(fm);   
                }                                               

        }

        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach(FigureMoving fm in allMoves)
            list.Add(fm.ToString());
            return list;
        }
    }
}