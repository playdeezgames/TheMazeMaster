﻿Friend Class Feature
    ReadOnly Property FeatureIndex As Integer
    ReadOnly Property FeatureTypeIdentifier As FeatureTypeIdentifier
    ReadOnly Property MazeColumn As Integer?
    ReadOnly Property MazeRow As Integer?
    ReadOnly Property RoomColumn As Integer
    ReadOnly Property RoomRow As Integer
    ReadOnly Property FeatureType As FeatureType
        Get
            Return AllFeatureTypes(FeatureTypeIdentifier)
        End Get
    End Property
    ReadOnly Property ShoppeType As ShoppeTypeIdentifier?
        Get
            Return FeatureType.ShoppeType
        End Get
    End Property
    Sub New(
           featureIndex As Integer,
           featureTypeIdentifier As FeatureTypeIdentifier,
           mazeColumn As Integer?,
           mazeRow As Integer?,
           roomColumn As Integer,
           roomRow As Integer)
        Me.FeatureIndex = featureIndex
        Me.FeatureTypeIdentifier = featureTypeIdentifier
        Me.MazeColumn = mazeColumn
        Me.MazeRow = mazeRow
        Me.RoomColumn = roomColumn
        Me.RoomRow = roomRow
    End Sub
    Friend Sub Remove(world As World)
        Dim cell = world.GetRoom(MazeColumn, MazeRow).Map.GetCell(RoomColumn, RoomRow)
        cell.FeatureIndex = Nothing
    End Sub
End Class
