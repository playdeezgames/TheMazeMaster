Friend Module CreatureTypes
    Friend ReadOnly AllCreatureTypes As IReadOnlyDictionary(Of CreatureTypeIdentifier, CreatureType) =
        New Dictionary(Of CreatureTypeIdentifier, CreatureType) From
        {
            {
                CreatureTypeIdentifier.Dude,
                New CreatureType(
                    "Dude",
                    TILE_DUDE,
                    3,
                    minimumExitCount:=2,
                    defaultWeaponType:=ItemTypeIdentifier.Fist)
            },
            {
                CreatureTypeIdentifier.Rat,
                New CreatureType(
                    "Rat",
                    TILE_RAT,
                    1,
                    spawnCount:=192,
                    defaultWeaponType:=ItemTypeIdentifier.Bite,
                    xp:=1,
                    drop:=ItemTypeIdentifier.RatTail)
            }
        }
End Module

