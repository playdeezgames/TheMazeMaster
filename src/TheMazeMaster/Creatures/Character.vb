Friend Class Character
    Sub New(creatureIndex As Integer)
        Me.CreatureIndex = creatureIndex
    End Sub
    Friend Property XP As Integer = 0
    Friend Property XPGoal As Integer = 10
    Friend ReadOnly Property Inventory As New List(Of Integer)
    Friend ReadOnly Property ItemStacks As New Dictionary(Of ItemTypeIdentifier, Integer)
    Friend ReadOnly Property CreatureIndex As Integer
    Friend ReadOnly Property EffectCounters As New Dictionary(Of CounterIdentifier, Integer)
    Friend ReadOnly Property Creature As Creature
        Get
            Return Worlds.world.GetCreature(CreatureIndex)
        End Get
    End Property

    Public ReadOnly Property CombatUsableItemTypes As IEnumerable(Of ItemType)
        Get
            Return ItemStacks.Select(Function(x) AllItemTypes(x.Key)).Where(Function(x) x.IsCombatUsable)
        End Get
    End Property
    Public ReadOnly Property NoncombatUsableItemTypes As IEnumerable(Of ItemType)
        Get
            Return ItemStacks.Select(Function(x) AllItemTypes(x.Key)).Where(Function(x) x.IsNoncombatUsable)
        End Get
    End Property
    Friend Function Move(d As DirectionIdentifier) As MoveResult
        If HasEffect(CounterIdentifier.Mislead) Then
            d = d.Opposite
            AddToCounter(CounterIdentifier.Mislead, -1)
        End If
        Return Creature.Move(d)
    End Function
    Private Function HasEffect(identifier As CounterIdentifier) As Boolean
        Return EffectCounters.ContainsKey(identifier) AndAlso EffectCounters(identifier) > 0
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
        If Not itemType.IsCombatUsable Then
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

    Friend Function NoncombatUseItemType(itemType As ItemType) As IEnumerable(Of String)
        If Not itemType.IsNoncombatUsable Then
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
            Case ItemTypeIdentifier.RedHerring
                AddToCounter(CounterIdentifier.Mislead, 10)
                Return New String() {
                    $"{Creature.Name} feels like they are being mislead."
                }
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Sub AddToCounter(identifier As CounterIdentifier, delta As Integer)
        EffectCounters(identifier) = If(EffectCounters.ContainsKey(identifier), EffectCounters(identifier), 0) + delta
    End Sub

    Friend Function GetShopType(direction As DirectionIdentifier) As ShoppeTypeIdentifier
        Return Worlds.
            world.
            GetRoom(Creature.MazeColumn, Creature.MazeRow).
            Map.
            GetCell(direction.StepX(Creature.RoomColumn), direction.StepY(Creature.RoomRow)).Feature.ShoppeType.Value
    End Function

    Friend Function CanTrade(trade As Trade) As Boolean
        Return GetItemCount(trade.Input.Item1) >= trade.Input.Item2
    End Function

    Private Function GetItemCount(itemTypeIdentifier As ItemTypeIdentifier) As Integer
        If Not ItemStacks.ContainsKey(itemTypeIdentifier) Then
            Return 0
        End If
        Return ItemStacks(itemTypeIdentifier)
    End Function

    Friend Sub MakeTrade(trade As Trade)
        If CanTrade(trade) Then
            RemoveItemCount(trade.Input.Item1, trade.Input.Item2)
            AddItemCount(trade.Output.Item1, trade.Output.Item2)
        End If
    End Sub

    Private Sub AddItemCount(item As ItemTypeIdentifier, delta As Integer)
        Dim newCount = GetItemCount(item) + delta
        If newCount <= 0 Then
            ItemStacks.Remove(item)
            Return
        End If
        ItemStacks(item) = newCount
    End Sub

    Private Sub RemoveItemCount(item As ItemTypeIdentifier, delta As Integer)
        AddItemCount(item, -delta)
    End Sub
End Class
