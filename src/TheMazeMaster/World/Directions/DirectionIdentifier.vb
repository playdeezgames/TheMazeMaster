Imports System.Runtime.CompilerServices

Public Enum DirectionIdentifier
    North
    East
    South
    West
End Enum
Friend Module DirectionIdentifierExtensions
    Private ReadOnly nextDirections As IReadOnlyDictionary(Of DirectionIdentifier, DirectionIdentifier) =
        New Dictionary(Of DirectionIdentifier, DirectionIdentifier) From
        {
            {DirectionIdentifier.North, DirectionIdentifier.East},
            {DirectionIdentifier.East, DirectionIdentifier.South},
            {DirectionIdentifier.South, DirectionIdentifier.West},
            {DirectionIdentifier.West, DirectionIdentifier.North}
        }
    <Extension>
    Friend Function NextDirection(directionIdentifier As DirectionIdentifier) As DirectionIdentifier
        Return nextDirections(directionIdentifier)
    End Function
    Private ReadOnly opposites As IReadOnlyDictionary(Of DirectionIdentifier, DirectionIdentifier) =
        New Dictionary(Of DirectionIdentifier, DirectionIdentifier) From
        {
            {DirectionIdentifier.North, DirectionIdentifier.South},
            {DirectionIdentifier.East, DirectionIdentifier.West},
            {DirectionIdentifier.South, DirectionIdentifier.North},
            {DirectionIdentifier.West, DirectionIdentifier.East}
        }
    <Extension>
    Friend Function Opposite(directionIdentifier As DirectionIdentifier) As DirectionIdentifier
        Return opposites(directionIdentifier)
    End Function
    Private ReadOnly xSteps As IReadOnlyDictionary(Of DirectionIdentifier, Integer) =
        New Dictionary(Of DirectionIdentifier, Integer) From
        {
            {DirectionIdentifier.North, 0},
            {DirectionIdentifier.East, 1},
            {DirectionIdentifier.South, 0},
            {DirectionIdentifier.West, -1}
        }
    <Extension>
    Friend Function StepX(directionIdentifier As DirectionIdentifier, x As Integer?) As Integer
        Return x.Value + xSteps(directionIdentifier)
    End Function
    Private ReadOnly ySteps As IReadOnlyDictionary(Of DirectionIdentifier, Integer) =
        New Dictionary(Of DirectionIdentifier, Integer) From
        {
            {DirectionIdentifier.North, -1},
            {DirectionIdentifier.East, 0},
            {DirectionIdentifier.South, 1},
            {DirectionIdentifier.West, 0}
        }
    <Extension>
    Friend Function StepY(directionIdentifier As DirectionIdentifier, y As Integer?) As Integer
        Return y.Value + ySteps(directionIdentifier)
    End Function
End Module