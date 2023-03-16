Public Class MazeCell
    Public Property State As MazeCellState
    Private Doors As New HashSet(Of DirectionIdentifier)

    Friend Sub Clear()
        Doors.Clear()
        State = MazeCellState.Outside
    End Sub

    Friend Sub AddDoor(direction As DirectionIdentifier)
        Doors.Add(direction)
    End Sub

    Friend Function HasDoor(d As DirectionIdentifier) As Boolean
        Return Doors.Contains(d)
    End Function
    Friend ReadOnly Property ExitCount As Integer
        Get
            Return Doors.Count
        End Get
    End Property
End Class
