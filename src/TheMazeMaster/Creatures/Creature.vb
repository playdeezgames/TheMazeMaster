Friend Class Creature
    Sub New(
           mazeColumn As Integer,
           mazeRow As Integer,
           roomColumn As Integer,
           roomRow As Integer,
           Optional alive As Boolean = True)
        Me.Alive = alive
        Me.MazeColumn = mazeColumn
        Me.MazeRow = mazeRow
        Me.RoomColumn = roomColumn
        Me.RoomRow = roomRow
    End Sub
    Property Alive As Boolean
    Property MazeColumn As Integer
    Property MazeRow As Integer
    Property RoomColumn As Integer
    Property RoomRow As Integer
End Class
