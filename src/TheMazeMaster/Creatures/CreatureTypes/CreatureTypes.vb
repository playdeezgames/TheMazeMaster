Friend Module CreatureTypes
    Friend ReadOnly AllCreatureTypes As IReadOnlyDictionary(Of CreatureTypeIdentifier, CreatureType) =
        New List(Of CreatureType) From
        {
                New CreatureType(
                    CreatureTypeIdentifier.Dude,
                    "Dude",
                    TILE_DUDE,
                    3,
                    minimumExitCount:=2,
                    defaultWeaponType:=ItemTypeIdentifier.Fist),
                New CreatureType(
                    CreatureTypeIdentifier.Rat,
                    "Rat",
                    TILE_RAT,
                    1,
                    spawnCount:=192,
                    defaultWeaponType:=ItemTypeIdentifier.Bite,
                    xp:=1,
                    drop:=ItemTypeIdentifier.RatTail)
        }.ToDictionary(Function(x) x.Identifier, Function(x) x)
End Module

