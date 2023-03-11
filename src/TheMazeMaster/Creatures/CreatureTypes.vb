Module CreatureTypes
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
                    defaultWeaponType:=ITEMTYPE_FIST)
            },
            {
                CreatureTypeIdentifier.Rat,
                New CreatureType(
                    "Rat",
                    TILE_RAT,
                    1,
                    spawnCount:=192,
                    defaultWeaponType:=ITEMTYPE_BITE,
                    xp:=1,
                    drop:=ITEMTYPE_RATTAIL)
            }
        }
End Module

