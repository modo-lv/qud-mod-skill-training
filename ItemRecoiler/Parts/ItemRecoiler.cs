﻿using System;
using System.Linq;
using ModoMods.Core.Utils;
using ModoMods.ItemRecoiler;
using ModoMods.ItemRecoiler.Data;
using XRL.UI;

// ReSharper disable once CheckNamespace
namespace XRL.World.Parts {
  /// <summary>Recoiler item functionality</summary>
  [Serializable] public class ItemRecoiler : ProgrammableRecoiler {
    public Boolean IsImprinted;

    /// <summary>Adds/updates the item receiver chest.</summary>
    public override void ProgrammedForLocation(Zone zone, Cell cell) {
      base.ProgrammedForLocation(zone, cell);
      if (!this.IsImprinted) {
        cell.AddObject(
          GameObject.CreateUnmodified(IrBlueprintNames.Receiver)
        );
        this.IsImprinted = true;
      } else {
        var chest = cell.FindObject(IrBlueprintNames.Receiver);
        if (chest == null)
          return;
        
        var total = 0;
        chest.Inventory.Objects.ToList().ForEach(item => {
          cell.AddObject(item);
          total += item.Count;
        });
        if (total > 0) {
          Output.DebugLog($"{total} item(s) removed from the chest before moving it to the new location.");
        }
        chest.ZoneTeleport(zone.ZoneID, cell.X, cell.Y);
      }
    }
  }
}