Friend Module Rooms
    Friend Const ROOM_ROWS = 15
    Friend Const ROOM_COLUMNS = 15


    Friend Function GetRoomMap(COLUMN As Integer, ROW As Integer) As Map
        Return GetRoom(COLUMN, ROW).Map
    End Function
    Friend Function GetRoom(COLUMN As Integer, ROW As Integer) As Room
        Return Worlds.world.GetRoom(COLUMN, ROW)
    End Function

    Function IS_ROOM_CHAMBER(MX As Integer, M_Y As Integer) As Boolean
        Return GetRoom(MX, M_Y).IsChamber
    End Function
End Module
