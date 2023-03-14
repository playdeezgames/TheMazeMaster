﻿Friend Class Item
    Friend Sub New(
                  itemIndex As Integer,
                  itemTypeIdentifier As ItemTypeIdentifier,
                  Optional mazeColumn As Integer? = Nothing,
                  Optional mazeRow As Integer? = Nothing,
                  Optional roomColumn As Integer? = Nothing,
                  Optional roomRow As Integer? = Nothing)
        Me.ItemTypeIdentifier = itemTypeIdentifier
        Me.MazeColumn = mazeColumn
        Me.MazeRow = mazeRow
        Me.RoomColumn = roomColumn
        Me.RoomRow = roomRow
        Me.ItemIndex = itemIndex
    End Sub
    Friend ReadOnly Property ItemIndex As Integer
    Private Property ItemTypeIdentifier As ItemTypeIdentifier
    Friend Property MazeColumn As Integer?
    Friend Property MazeRow As Integer?
    Friend Property RoomColumn As Integer?
    Friend Property RoomRow As Integer?
    Friend ReadOnly Property ItemType As ItemType
        Get
            If AllItemTypes.ContainsKey(ItemTypeIdentifier) Then
                Return AllItemTypes(ItemTypeIdentifier)
            End If
            Return Nothing
        End Get
    End Property
    Friend Sub SetItemType(itemTypeIdentifier As ItemTypeIdentifier)
        Me.ItemTypeIdentifier = itemTypeIdentifier
    End Sub
    Friend Function RollAttack() As Integer
        Return ItemType.RollAttack
    End Function
    Friend Sub Place()
        If MazeColumn.HasValue Then
            Dim IT = ItemType
            Dim MX = MazeColumn.Value
            Dim MY = MazeRow.Value
            Dim X = RoomColumn.Value
            Dim Y = RoomRow.Value
            Dim TI = IT.TileIndex
            Dim RM = GET_ROOM_MAP_ASSET(MX, MY)
            MSET(RM, 2, X, Y, TI)
            GetRoomMap(MX, MY).GetCell(X, Y).Item = ItemIndex
        End If
    End Sub
    Friend Sub Remove()
        If MazeColumn.HasValue Then
            Dim IT = ItemType
            Dim MX = MazeColumn.Value
            Dim MY = MazeRow.Value
            Dim X = RoomColumn.Value
            Dim Y = RoomRow.Value
            Dim TI = TILE_EMPTY
            Dim RM = GET_ROOM_MAP_ASSET(MX, MY)
            MSET(RM, 2, X, Y, TI)
            GetRoomMap(MX, MY).GetCell(X, Y).Item = Nothing
        End If
    End Sub
    Friend Sub ClearRoom()
        MazeColumn = Nothing
        MazeRow = Nothing
        RoomColumn = Nothing
        RoomRow = Nothing
    End Sub
    Friend Sub Destroy()
        ClearRoom()
        SetItemType(ItemTypeIdentifier.None)
    End Sub
End Class
