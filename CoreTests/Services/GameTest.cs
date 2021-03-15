using System;
using System.Collections.Generic;
using Core.Exceptions;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.Game;
using CoreTests.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services
{
    public class GameTest
    {
        private readonly Game _sut;
        private readonly Mock<IBoardInitializer> _boardInitializer = new();
        private readonly Mock<IBoardVerifier> _boardVerifier = new();


        public GameTest()
        {
            var boardSize = new BoardSize(5, 5);
            var shipConfiguration = new ShipConfigurationBuilder().Build();
            var shipConfigurations = new HashSet<ShipConfiguration>(new[] {shipConfiguration});
            _sut = CreateSut(boardSize, shipConfigurations);
            
            SetupBoardVerifierBoundsCheckToReturn(true);
        }


        private void SetupBoardVerifierBoundsCheckToReturn(bool result)
        {
            _boardVerifier.Setup(mock => mock.CellsAreWithinBounds(It.IsAny<BoardSize>(), It.IsAny<IEnumerable<Cell>>()))
                          .Returns(result);
        }


        private Game CreateSut(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations)
        {
            return new(boardSize, shipConfigurations, _boardInitializer.Object, _boardVerifier.Object);
        }


        [Fact(DisplayName = "Initialized Board is returned when new game starts")]
        public void Initialized_Board_is_returned_when_new_game_starts()
        {
            var expectedNewBoard = new BoardBuilder()
                                   .WithSize(new BoardSize(33, 33))
                                   .WithShips(new[]
                                   {
                                       new ShipBuilder().WithName("SuperShip").Build()
                                   }).Build();
            _boardInitializer.Setup(mock => mock.InitializedBoard(It.IsAny<BoardSize>(), It.IsAny<ISet<ShipConfiguration>>()))
                             .Returns(expectedNewBoard);


            var actualNewBoard = _sut.StartGame();


            actualNewBoard.Should().Be(expectedNewBoard,
                                       "Upon starting new game Board created by IBoardInitializer should be returned");
        }


        [Theory(DisplayName = "Game is finished when all Cells have been shot")]
        [ClassData(typeof(FinishedGameClassData))]
        public void Game_is_finished_when_all_Cells_have_been_shot(Board board)
        {
            var gameFinished = _sut.IsFinished(board);


            gameFinished.Should().BeTrue("all Cells on a Board have been shot");
        }


        [Theory(DisplayName = "Game is not finished if not all Cells have been shot")]
        [ClassData(typeof(NotFinishedGameClassData))]
        public void Game_is_not_finished_if_not_all_Cells_have_been_shot(Board board)
        {
            var gameFinished = _sut.IsFinished(board);


            gameFinished.Should().BeFalse("NOT all Cells on a Board have been shot");
        }


        [Theory(DisplayName = "Correct GameMoveResult is returned after shooting")]
        [ClassData(typeof(ShotAtClassData))]
        public void Correct_GameMoveResult_is_returned_after_shooting(Board initialBoard, Cell cellToShot,
                                                                      GameMoveResult expectedGameMoveResult)
        {
            var actualGameMoveResult = _sut.ShootAt(initialBoard, cellToShot);


            actualGameMoveResult.Should().Be(expectedGameMoveResult);
        }


        [Fact(DisplayName = "Exception is thrown if shot is made out of Board bounds")]
        public void Exception_is_thrown_if_shot_is_made_out_of_Board_bounds()
        {
            SetupBoardVerifierBoundsCheckToReturn(false);
            var initialBoard = new BoardBuilder().Build();
            var cellToShot = new CellBuilder().Build();
            
            
            Action act = () => _sut.ShootAt(initialBoard, cellToShot);


            act.Should().Throw<CannotMakeOutOfBoundsShotException>("it is illegal to shot outside a Board");
        }


        [Theory(DisplayName = "Exception is thrown if shot is made at already hit cell")]
        [ClassData(typeof(ShotAtAlreadyShotCellClassData))]
        public void Exception_is_thrown_if_shot_is_made_at_already_hit_cell(Board initialBoard, Cell cellToShot)
        {
            Action act = () => _sut.ShootAt(initialBoard, cellToShot);


            act.Should().Throw<CannotShotAlreadyShotCellException>("it is illegal to shot already shot Cell");
        }
    }
}