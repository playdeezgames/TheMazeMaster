Module CreatureTypes
    Friend ReadOnly AllCreatureTypes As IReadOnlyDictionary(Of String, CreatureType) =
        New Dictionary(Of String, CreatureType) From
        {
            {
                CREATURETYPE_DUDE,
                New CreatureType(
                    TILE_DUDE,
                    3,
                    minimumExitCount:=2,
                    defaultWeaponType:=ITEMTYPE_FIST)
            },
            {
                CREATURETYPE_RAT,
                New CreatureType(
                    TILE_RAT,
                    1,
                    spawnCount:=192,
                    defaultWeaponType:=ITEMTYPE_BITE)
            }
        }
    Friend Const CREATURETYPE_DUDE = "DUDE"
    Friend Const CREATURETYPE_RAT = "RAT"
    Friend ReadOnly ALL_CREATURETYPES As IReadOnlyList(Of String) = New List(Of String) From {CREATURETYPE_DUDE, CREATURETYPE_RAT}
    Friend ReadOnly CREATURETYPE_MINIMUM_X As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 1}, {CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_MINIMUM_Y As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 1}, {CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_MAXIMUM_X As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 13}, {CREATURETYPE_RAT, 13}}
    Friend ReadOnly CREATURETYPE_MAXIMUM_Y As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_DUDE, 13}, {CREATURETYPE_RAT, 13}}
    Friend ReadOnly CREATURETYPE_NAME As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {{CREATURETYPE_DUDE, "DUDE"}, {CREATURETYPE_RAT, "RAT"}}
    Friend ReadOnly CREATURETYPE_XP As IReadOnlyDictionary(Of String, Integer) =
        New Dictionary(Of String, Integer) From
        {{CREATURETYPE_RAT, 1}}
    Friend ReadOnly CREATURETYPE_ITEMTYPE_DROP As IReadOnlyDictionary(Of String, String) =
        New Dictionary(Of String, String) From
        {{CREATURETYPE_RAT, ITEMTYPE_RATTAIL}}
End Module

