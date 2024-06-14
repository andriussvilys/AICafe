using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CafeGame;
using UnityEngine;

public class NPCSpawner : Spawner<NPC>
{
    protected async override Task<bool> LoadSpawnerState(SaveSystem.SaveState saveState)
    {
        List<SaveSystem.NPC> data = saveState?.waitingLine?.list;
        foreach (SaveSystem.NPC npcData in data)
        {
            await CreateInstance(npcData.styleVector);
        }
        return true;
    }

    protected override Task UpdateSpawnerState(SaveSystem.SaveState state)
    {
        List<SaveSystem.NPC> npcData = GetChildren().Select(npc => new SaveSystem.NPC(npc)).ToList();
        state.waitingLine = new SaveSystem.Spawner<SaveSystem.NPC>(npcData);
        return null;
    }
}
