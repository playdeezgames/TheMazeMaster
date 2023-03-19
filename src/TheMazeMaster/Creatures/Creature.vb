﻿Friend Class Creature
    Sub New(
           creatureIndex As Integer,
           creatureTypeIdentifier As CreatureTypeIdentifier,
           mazeColumn As Integer,
           mazeRow As Integer,
           roomColumn As Integer,
           roomRow As Integer,
           Optional alive As Boolean = True)
        Me.CreatureIndex = creatureIndex
        Me.Alive = alive
        Me.MazeColumn = mazeColumn
        Me.MazeRow = mazeRow
        Me.RoomColumn = roomColumn
        Me.RoomRow = roomRow
        Me.CreatureTypeIdentifier = creatureTypeIdentifier
        Me.MaximumHitPoints = CreatureType.MaximumHitPoints
        Me.Wounds = 0
        Me.Weapon = AllItemTypes(CreatureType.DefaultWeaponType).Create
    End Sub
    ReadOnly Property CreatureIndex As Integer
    Private Property CreatureTypeIdentifier As CreatureTypeIdentifier
    ReadOnly Property CreatureType As CreatureType
        Get
            Return AllCreatureTypes(CreatureTypeIdentifier)
        End Get
    End Property
    Property Alive As Boolean
    Property MazeColumn As Integer?
    Property MazeRow As Integer?
    Property RoomColumn As Integer
    Property RoomRow As Integer
    Property MaximumHitPoints As Integer
    Property Wounds As Integer
    Property Weapon As Integer?
    Sub Place()
        Worlds.world.GetRoom(MazeColumn, MazeRow).Map.GetCell(RoomColumn, RoomRow).Creature = Me
    End Sub
    ReadOnly Property Name As String
        Get
            Return CreatureType.Name
        End Get
    End Property
    ReadOnly Property Health As Integer
        Get
            Return MaximumHitPoints - Wounds
        End Get
    End Property
    ReadOnly Property XP As Integer
        Get
            Return CreatureType.XP
        End Get
    End Property
    Function RollAttack() As Integer
        If Weapon.HasValue Then
            Dim W = Weapon.Value
            Return Worlds.world.GetItem(W).RollAttack
        End If
        Return 0
    End Function
    Function RollDefend() As Integer
        'ARMOR
        Return 0
    End Function
    Sub AddWounds(d As Integer)
        If Alive Then
            Wounds = Math.Clamp(Wounds + d, 0, MaximumHitPoints)
            Alive = Wounds < MaximumHitPoints
        End If
    End Sub
    Sub Drop()
        Dim CT = CreatureType
        'TODO: CHANCE OF NOT DROPPING ITEM?
        'TODO: WEIGHTED GENERATOR FOR WHAT ITEM GETS DROPPED?
        Dim IT = CT.Drop
        If IT = ItemTypeIdentifier.None Then
            Return
        End If
        Dim II = AllItemTypes(IT).CreateInRoom(MazeColumn, MazeRow, RoomColumn, RoomRow)
        Worlds.world.GetItem(II).Place()
    End Sub
    Sub Remove()
        Worlds.world.GetRoom(MazeColumn, MazeRow).Map.GetCell(RoomColumn, RoomRow).Creature = Nothing
    End Sub
    Function Move(d As DirectionIdentifier) As MoveResult
        Dim R = MoveResult.Blocked
        If Alive Then
            Remove()
            Dim X = RoomColumn
            Dim Y = RoomRow
            Dim NX = d.StepX(X)
            Dim NY = d.StepY(Y)
            Dim MX = MazeColumn
            Dim M_Y = MazeRow
            Dim room = Worlds.world.GetRoom(MX, M_Y)
            If NX < 0 OrElse NY < 0 OrElse NX >= room.Map.Columns OrElse NY >= room.Map.Rows Then
                MazeColumn = d.StepX(MX)
                MazeRow = d.StepY(M_Y)
                If NX < 0 Then
                    RoomColumn = NX + room.Map.Columns
                ElseIf NX >= room.Map.COLUMNS Then
                    RoomColumn = NX - room.Map.Columns
                Else
                    RoomColumn = NX
                End If
                If NY < 0 Then
                    RoomRow = NY + room.Map.Rows
                ElseIf NY >= room.Map.ROWS Then
                    RoomRow = NY - room.Map.Rows
                Else
                    RoomRow = NY
                End If
            Else
                Dim terrain = AllTerrains(Worlds.world.GetRoom(MX, M_Y).Map.GetCell(NX, NY).Terrain)
                If terrain.CanWalk Then
                    Dim creature = Worlds.world.GetRoom(MX, M_Y).Map.GetCell(NX, NY).Creature
                    If creature IsNot Nothing Then
                        R = MoveResult.Fight
                    Else
                        Dim itemIndex = Worlds.world.GetRoom(MX, M_Y).Map.GetCell(NX, NY).ItemIndex
                        If itemIndex.HasValue Then
                            R = MoveResult.PickUp
                        Else
                            Dim feature = Worlds.world.GetRoom(MX, M_Y).Map.GetCell(NX, NY).Feature
                            If feature IsNot Nothing Then
                                R = InteractWithFeature(feature)
                            Else
                                RoomColumn = NX
                                RoomRow = NY
                                R = MoveResult.Success
                            End If
                        End If
                    End If
                End If
            End If
            Place()
        End If
        Return R
    End Function

    Private Function InteractWithFeature(feature As Feature) As MoveResult
        Select Case feature.FeatureTypeIdentifier
            Case FeatureTypeIdentifier.StairsDown
                MoveToFeature(FeatureTypeIdentifier.StairsUp)
                Return MoveResult.Success
            Case FeatureTypeIdentifier.StairsUp
                MoveToFeature(FeatureTypeIdentifier.StairsDown)
                Return MoveResult.Success
            Case FeatureTypeIdentifier.Knacker
                Return MoveResult.Shoppe
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Friend Sub MoveToFeature(featureTypeIdentifier As FeatureTypeIdentifier)
        Dim feature = Worlds.world.GetFeatureOfType(featureTypeIdentifier)
        MazeColumn = feature.MazeColumn
        MazeRow = feature.MazeRow
        RoomColumn = feature.RoomColumn
        RoomRow = feature.RoomRow
    End Sub
End Class
