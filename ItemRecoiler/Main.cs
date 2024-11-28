﻿using System;
using ModoMods.Core.Utils;
using ModoMods.ItemRecoiler.Data;
using ModoMods.ItemRecoiler.Parts;
using XRL;
using XRL.UI;
using XRL.Wish;
using XRL.World;

namespace ModoMods.ItemRecoiler {
  [HasCallAfterGameLoaded][PlayerMutator][HasWishCommand]
  public class Main : IPlayerMutator {
    public static GameObject Player =>
      The.Player ?? throw new NullReferenceException("[The.Player] is null.");

    public static void Init(GameObject player) {
      if (!player.Inventory.HasObject(IrBlueprintNames.Recoiler)) {
        Output.DebugLog(
          $"[{player}] does not appear to own a [{IrBlueprintNames.Recoiler}], placing in inventory..."
        );
        player.Inventory.AddObject(IrBlueprintNames.Recoiler);
      }
      player.RequirePart<ActivationCommand>();
    }

    /// <summary>New game.</summary>
    public void mutate(GameObject player) { Init(player); }

    /// <summary>Load game.</summary>
    [CallAfterGameLoaded] public static void OnGameLoaded() { Init(Player); }
  }
}