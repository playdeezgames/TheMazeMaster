Friend Module Directions
    Friend ReadOnly AllDirections As IReadOnlyList(Of DirectionIdentifier) =
        New List(Of DirectionIdentifier) From
        {
            DirectionIdentifier.North,
            DirectionIdentifier.East,
            DirectionIdentifier.South,
            DirectionIdentifier.West
        }
    Friend Const DIRECTION_NORTH = 0
    Friend Const DIRECTION_EAST = 1
    Friend Const DIRECTION_SOUTH = 2
    Friend Const DIRECTION_WEST = 3
    Friend Const DIRECTION_FIRST = DIRECTION_NORTH
    Friend Const DIRECTION_LAST = DIRECTION_WEST
    Friend Const DIRECTION_COUNT = 4
    Friend Function OPPOSITE_DIRECTION(d As Integer) As Integer
        If d = DIRECTION_NORTH Then
            Return DIRECTION_SOUTH
        ElseIf d = DIRECTION_EAST Then
            Return DIRECTION_WEST
        ElseIf d = DIRECTION_SOUTH Then
            Return DIRECTION_NORTH
        ElseIf d = DIRECTION_WEST Then
            Return DIRECTION_EAST
        Else
            Throw New NotImplementedException
        End If
    End Function
    Function STEP_X(d As Integer, x As Integer, y As Integer) As Integer
        If d = DIRECTION_NORTH Then
            Return x
        ElseIf d = DIRECTION_EAST Then
            Return x + 1
        ElseIf d = DIRECTION_SOUTH Then
            Return x
        ElseIf d = DIRECTION_WEST Then
            Return x - 1
        Else
            Throw New NotImplementedException()
        End If
    End Function
    Function STEP_Y(d As Integer, x As Integer, y As Integer) As Integer
        If d = DIRECTION_NORTH Then
            Return y - 1
        ElseIf d = DIRECTION_EAST Then
            Return y
        ElseIf d = DIRECTION_SOUTH Then
            Return y + 1
        ElseIf d = DIRECTION_WEST Then
            Return y
        Else
            Throw New NotImplementedException()
        End If
    End Function
End Module
