Friend Class Character
    Sub New(creatureIndex As Integer)
        Me.CreatureIndex = creatureIndex
    End Sub
    Friend Property XP As Integer = 0
    Friend Property XPGoal As Integer = 10
    Friend ReadOnly Property Inventory As New List(Of Integer)
    Friend ReadOnly Property ItemStacks As New Dictionary(Of ItemTypeIdentifier, Integer)
    Friend ReadOnly Property CreatureIndex As Integer
    Friend ReadOnly Property Creature As Creature
        Get
            Return Worlds.world.GetCreature(CreatureIndex)
        End Get
    End Property

    Public ReadOnly Property UsableItemTypes As IEnumerable(Of ItemType)
        Get
            Return ItemStacks.Select(Function(x) AllItemTypes(x.Key)).Where(Function(x) x.IsUsable)
        End Get
    End Property

    Friend Function Move(d As DirectionIdentifier) As MoveResult
        Return Creature.Move(d)
    End Function
    Friend Function GetEnemy(direction As DirectionIdentifier) As Integer
        Return Worlds.world.
            GetRoom(Creature.MazeColumn, Creature.MazeRow).Map.
            GetCell(direction.StepX(Creature.RoomColumn), direction.StepY(Creature.RoomRow)).
            Creature.CreatureIndex
    End Function
    Friend Sub AddXP(XP As Integer)
        XP += XP
    End Sub
    Friend Function GetPickUp(D As DirectionIdentifier) As Integer
        Dim I = CreatureIndex
        Dim X = Worlds.world.GetCreature(I).RoomColumn
        Dim Y = Worlds.world.GetCreature(I).RoomRow
        Dim NX = D.StepX(X)
        Dim NY = D.StepY(Y)
        Dim MX = Worlds.world.GetCreature(I).MazeColumn
        Dim MY = Worlds.world.GetCreature(I).MazeRow
        Return Worlds.world.GetRoom(MX, MY).Map.GetCell(NX, NY).ItemIndex.Value
    End Function
    Friend Sub TakeItem(II As Integer)
        Worlds.world.GetItem(II).ClearRoom()
        Dim IT = Worlds.world.GetItem(II).ItemType
        Dim ic As Integer
        If If(IT?.Stacks, False) Then
            If Not ItemStacks.ContainsKey(IT.Identifier) Then
                ic = 1
            Else
                ic = ItemStacks(IT.Identifier) + 1
            End If
            ItemStacks(IT.Identifier) = ic
            Worlds.world.GetItem(II).Destroy()
        Else
            If Not Inventory.Contains(II) Then
                Inventory.Add(II)
            End If
        End If
    End Sub

    Friend Function CombatUseItemType(itemType As ItemType) As String()
        If Not itemType.IsUsable Then
            Return New String() {$"{Creature.Name} cannot use {itemType.Name}."}
        End If
        If Not ItemStacks.ContainsKey(itemType.Identifier) OrElse ItemStacks(itemType.Identifier) < 1 Then
            Return New String() {$"{Creature.Name} doesn't have any {itemType.Name}."}
        End If
        ItemStacks(itemType.Identifier) -= 1
        Select Case itemType.Identifier
            Case ItemTypeIdentifier.Köttbulle
                Creature.AddWounds(-1)
                Return New String() {
                    $"{Creature.Name} eats {itemType.Name}.",
                    $"{Creature.Name} now has {Creature.Health} HP."
                }
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
