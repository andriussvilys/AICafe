GameStateManager : object that holds game state. Manages loading save file, deserializing, storing, updating state;
Persistable : implements UpdateState and LoadState methods. Subscribes to Load/Save event channels.
When Persistable instance complete save/load operations, it raises and event on save/load completeChannel. 
When all subscribed instances complete their operations, Saver/Loader concludes the overall operation.

Saver / Loader

SaveData
Each new class must include empty constructor or initialize starting values, otherwise QuickSave will fail to deserialize