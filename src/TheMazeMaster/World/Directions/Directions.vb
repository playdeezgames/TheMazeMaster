Friend Module Directions
    Friend ReadOnly AllDirections As IReadOnlyList(Of DirectionIdentifier) =
        New List(Of DirectionIdentifier) From
        {
            DirectionIdentifier.North,
            DirectionIdentifier.East,
            DirectionIdentifier.South,
            DirectionIdentifier.West
        }
    Friend Const DIRECTION_COUNT = 4
End Module
