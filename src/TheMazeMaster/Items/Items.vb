﻿Friend Module Items
    Friend AllItems As New List(Of Item)
    Friend Sub Clear()
        AllItems.Clear()
    End Sub
    Friend Function FIND_ITEM(MX As Integer, M_Y As Integer, X As Integer, Y As Integer) As Integer
        For ii = 0 To AllItems.Count - 1
            Dim CMX = AllItems(ii).MazeColumn
            If Not CMX.HasValue Then
                Continue For
            End If
            Dim CMY = AllItems(ii).MazeRow
            Dim CX = AllItems(ii).RoomColumn
            Dim CY = AllItems(ii).RoomRow
            If MX = CMX.Value AndAlso M_Y = CMY.Value AndAlso X = CX.Value AndAlso Y = CY.Value Then
                Return ii
            End If
        Next
        Return -1
    End Function
    'TODO: move to item
    Friend Sub ITEM_CLEAR_ROOM(II As Integer)
        AllItems(II).MazeColumn = Nothing
        AllItems(II).MazeRow = Nothing
        AllItems(II).RoomColumn = Nothing
        AllItems(II).RoomRow = Nothing
    End Sub
    'TODO: move to item
    Friend Sub ITEM_DESTROY(II As Integer)
        ITEM_CLEAR_ROOM(II)
        AllItems(II).SetItemType(ItemTypeIdentifier.None)
    End Sub
End Module
