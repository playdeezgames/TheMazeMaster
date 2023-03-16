Friend Module Rooms
    Friend Const ROOM_ROWS = 15
    Friend Const ROOM_COLUMNS = 15
    Friend Function GetRoomMap(COLUMN As Integer, ROW As Integer) As Map
        Return Worlds.world.GetRoom(COLUMN, ROW).Map
    End Function
End Module
