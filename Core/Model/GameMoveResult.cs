namespace Core.Model
{
    public sealed record GameMoveResult(Board UpdatedBoard, Ship? HitShip);
}