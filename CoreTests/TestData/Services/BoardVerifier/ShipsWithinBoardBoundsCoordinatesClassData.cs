using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Model;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.BoardVerifier
{
    public class ShipsWithinBoardBoundsCoordinatesClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Board(new BoardSize(10, 10),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(0, 0).Build()
                              }).Build(),
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(2, 1).Build()
                              }).Build()
                          }.ToImmutableList()),
                true
            };
            yield return new object[]
            {
                new Board(new BoardSize(100, 75),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(100, 75).Build()
                              }).Build()
                          }.ToImmutableList()),
                true
            };
            yield return new object[]
            {
                new Board(new BoardSize(75, 100),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(100, 75).Build()
                              }).Build()
                          }.ToImmutableList()),
                false
            };
            yield return new object[]
            {
                new Board(new BoardSize(10, 10),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(10, 10).Build()
                              }).Build(),
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(10, 10).Build()
                              }).Build()
                          }.ToImmutableList()),
                true
            };
            yield return new object[]
            {
                new Board(new BoardSize(10, 10),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(25, 25).Build()
                              }).Build(),
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(25, 25).Build()
                              }).Build()
                          }.ToImmutableList()),
                false
            };
            yield return new object[]
            {
                new Board(new BoardSize(25, 25),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(25, 25).Build()
                              }).Build(),
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(25, 25).Build()
                              }).Build()
                          }.ToImmutableList()),
                true
            };
            yield return new object[]
            {
                new Board(new BoardSize(10, 10),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(0, 0).Build()
                              }).Build(),
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(2, 11).Build()
                              }).Build()
                          }.ToImmutableList()),
                false
            };
            yield return new object[]
            {
                new Board(new BoardSize(10, 10),
                          new[]
                          {
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(100, 0).Build()
                              }).Build(),
                              new ShipBuilder().WithCells(new[]
                              {
                                  new CellBuilder().WithCoordinates(12, 8).Build()
                              }).Build()
                          }.ToImmutableList()),
                false
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}