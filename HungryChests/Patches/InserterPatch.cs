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
                int maxIndex = inventory.numSlots - 1;
                if (!inventory.myStacks[maxIndex].isEmpty && getVoidableItems().Contains(inventory.myStacks[maxIndex].info.displayName)) {
                    inventory.RemoveResourcesFromSlot(maxIndex, inventory.myStacks[maxIndex].count);
                }
            }
        }

        private static List<string> getVoidableItems() {
            return HungryChestsPlugin.VoidableItems.Value.Split(',').ToList();
        }
    }
}
