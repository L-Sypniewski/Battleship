using System.Collections.Generic;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.CellVerifier;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services
{
    public class CellVerifierTest
    {
        private readonly CellVerifier _sut;


        public CellVerifierTest()
        {
            _sut = new CellVerifier();
        }


        [Theory(DisplayName = "CellVerifier correctly detects when some Cells intersect")]
        [ClassData(typeof(IntersectingCellsClassData))]
        public void CellVerifier_correctly_detects_when_some_Cells_intersect(IEnumerable<Cell> cells)
        {
            _sut.CellsIntersect(cells).Should()
                .BeTrue("provided Cells share some of their coordinates");
        }


        [Theory(DisplayName = "CellVerifier correctly detects when Cells don't intersect")]
        [ClassData(typeof(NonIntersectingCellsClassData))]
        public void CellVerifier_correctly_detects_when_Cells_do_not_intersect(IEnumerable<Cell> cells)
        {
            _sut.CellsIntersect(cells).Should()
                .BeFalse("provided Cells do NOT share any of their coordinates");
        }
    }
}