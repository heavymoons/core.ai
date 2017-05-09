using System;
using System.Linq;
using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.BehaviourTree
{
    public abstract class BaseNode : INode
    {
        public DataStorage DataStorage { get; private set; } = new DataStorage();
        private INode _parentNode;
        public INode ParentNode => _parentNode;

        public void ReplaceDataStorage(DataStorage dataStorage)
        {
            if (DataStorage.Names.Count > 0)
                throw new InvalidOperationException($"DataStorage already registered some values: {string.Join(", ", DataStorage.Names.ToArray())}");
            DataStorage = dataStorage;
        }

        public NodeEvent OnExecuteEvent;

        public virtual bool Execute(BehaviourMachine machine, INode parentNode)
        {
            _parentNode = parentNode;
            OnExecute(machine);
            return false;
        }

        public void OnExecute(BehaviourMachine machine)
        {
            OnExecuteEvent?.Invoke(machine, this);
        }
    }
}