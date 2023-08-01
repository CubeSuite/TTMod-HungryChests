using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace HungryChests.Patches
{
    public class InserterPatch
    {
        [HarmonyPatch(typeof(InserterInstance), "Give")]
        [HarmonyPrefix]
        static void deleteLastChestSlot(InserterInstance __instance) {
            if(__instance.giveResourceContainer.typeIndex == MachineTypeEnum.Chest) {
                Inventory inventory = __instance.giveResourceContainer.GetInventory(0);
                if (!inventory.myStacks[55].isEmpty && getVoidableItems().Contains(inventory.myStacks[55].info.displayName)) {
                    inventory.RemoveResourcesFromSlot(inventory.numSlots - 1, inventory.myStacks[55].count);
                }
            }
        }

        private static List<string> getVoidableItems() {
            return HungryChestsPlugin.VoidableItems.Value.Split(',').ToList();
        }
    }
}
