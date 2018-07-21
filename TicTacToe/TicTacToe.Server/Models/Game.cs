using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Server.Models
{
    public class Game
    {
        private bool isFirstPlayersTurn;
        
        /// <param name="player1">The first player to join the game.</param>
        /// <param name="player2">The second player to join the game.</param>
        public Game(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            this.Id = Guid.NewGuid().ToString("d");
            this.Board = new Board();

            this.isFirstPlayersTurn = true;

            
            this.Player1.GameId = this.Id;
            this.Player2.GameId = this.Id;

           
            this.Player1.Piece = "X";
            this.Player2.Piece = "O";
        }

        
        public string Id { get; set; }

        public Player Player1 { get; set; }

       
        public Player Player2 { get; set; }

       
        public Board Board { get; set; }

       
        public Player WhoseTurn
        {
            get
            {
                return (this.isFirstPlayersTurn) ?
                    this.Player1 :
                    this.Player2;
            }
        }

        
        public bool IsOver
        {
            get
            {
                return this.IsTie || this.Board.IsThreeInRow;
            }
        }

      
        public bool IsTie
        {
            get
            {
                return !this.Board.AreSpacesLeft;
            }
        }

        
        /// <param name="row">The row where the piece will be placed.</param>
        /// <param name="col">The column where the piece will be placed.</param>
        public void PlacePiece(int row, int col)
        {
            string pieceToPlace = this.isFirstPlayersTurn ?
                this.Player1.Piece :
                this.Player2.Piece;
            this.Board.PlacePiece(row, col, pieceToPlace);
            
            this.isFirstPlayersTurn = !this.isFirstPlayersTurn;
        }

       
        /// <param name="row">The row position of the move.</param>
        /// <param name="col">The column position of the move.</param>
        /// <returns>true if the move is valid; otherwise false.</returns>
        public bool IsValidMove(int row, int col)
        {
            return 
                row < this.Board.Pieces.GetLength(0) &&
                col < this.Board.Pieces.GetLength(1) &&
                string.IsNullOrWhiteSpace(this.Board.Pieces[row, col]);
        }

        public override string ToString()
        {
            return String.Format("(Id={0}, Player1={1}, Player2={2}, Board={3})",
                this.Id, this.Player1, this.Player2, this.Board);
        }
    }
}
