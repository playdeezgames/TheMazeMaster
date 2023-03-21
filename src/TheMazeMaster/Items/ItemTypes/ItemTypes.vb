Friend Module ItemTypes
    Friend AllItemTypes As IReadOnlyDictionary(Of ItemTypeIdentifier, ItemType) =
        New List(Of ItemType) From
        {
                New ItemType(
                    ItemTypeIdentifier.Fist,
                    "Fist",
                    attackValue:=1,
                    attackMaximum:=1),
                New ItemType(
                    ItemTypeIdentifier.Bite,
                    "Bite",
                    attackValue:=1,
                    attackMaximum:=1),
                New ItemType(
                    ItemTypeIdentifier.RatTail,
                    "Rat Tail",
                    stacks:=True,
                    tileIndex:=TILE_RATTAIL),
                New ItemType(
                    ItemTypeIdentifier.Köttbulle,
                    "Köttbulle",
                    tileIndex:=tile_köttbulle,
                    stacks:=True,
                    spawnCount:=128,
                    isCombatUsable:=True,
                    isNoncombatUsable:=True),
                New ItemType(
                    ItemTypeIdentifier.Penny,
                    "Penny",
                    tileIndex:=TILE_PENNY,
                    stacks:=True,
                    spawnCount:=256),
                New ItemType(
                    ItemTypeIdentifier.Key,
                    "Key",
                    tileIndex:=TILE_KEY,
                    stacks:=True,
                    minimumExitCount:=2),
                New ItemType(
                    ItemTypeIdentifier.MacGuffin,
                    "MacGuffin",
                    tileIndex:=TILE_MACGUFFIN,
                    spawnCount:=1,
                    maximumExitCount:=1,
                    stacks:=True),
                New ItemType(
                    ItemTypeIdentifier.RedHerring,
                    "Red Herring",
                    tileIndex:=TILE_MACGUFFIN,
                    spawnCount:=15,
                    maximumExitCount:=1,
                    stacks:=True,
                    isNoncombatUsable:=True)
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module

