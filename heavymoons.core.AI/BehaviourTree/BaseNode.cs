using System;
using System.Linq;
using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.BehaviourTree
{
    public abstract class BaseNode : INode
    {
        public DataStorage DataStorage { get; private set; } = new DataStorage();

        public void ReplaceDataStorage(DataStorage dataStorage)
        {
            if (DataStorage.Names.Count > 0)
                throw new InvalidOperationException($"DataStorage already registered some values: {string.Join(", ", DataStorage.Names.ToArray())}");
            DataStorage = dataStorage;
        }

        public NodeEvent OnExecuteEvent;

        public abstract bool Execute(BehaviourMachine machine, INode parentNode);

        public void OnExecute(BehaviourMachine machine, INode parentNode)
        {
            OnExecuteEvent?.Invoke(machine, this, parentNode);
        }
    }
}