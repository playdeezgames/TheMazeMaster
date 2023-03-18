Friend Class Room
    Sub New(map As Map, roomType As RoomType)
        Me.Map = map
        Me.RoomType = roomType
    End Sub
    Friend ReadOnly Property Map As Map
    Friend ReadOnly Property RoomType As RoomType
End Class
