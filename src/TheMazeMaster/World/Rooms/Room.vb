Friend Class Room
    Sub New(map As Map, roomType As RoomType, mazeColumn As Integer?, mazeRow As Integer?)
        Me.Map = map
        Me.RoomType = roomType
        Me.MazeColumn = mazeColumn
        Me.MazeRow = mazeRow
    End Sub
    Friend ReadOnly Property Map As Map
    Friend ReadOnly Property RoomType As RoomType
    Public ReadOnly Property MazeColumn As Integer?
    Public ReadOnly Property MazeRow As Integer?
End Class
