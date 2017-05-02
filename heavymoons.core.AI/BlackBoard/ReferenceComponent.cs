namespace heavymoons.core.AI
{
    /// <summary>
    /// 他のDataStoreの登録データを参照するコンポーネント
    /// </summary>
    internal class ReferenceComponent : Component
    {
        private readonly DataStore _blackboard;
        private readonly string _name;

        public override object Value
        {
            get { return _blackboard.GetValue(_name); }
            set { _blackboard.SetValue(_name, value); }
        }

        public override T GetValue<T>()
        {
            return _blackboard.GetValue<T>(_name);
        }

        public override void SetValue(object value)
        {
            _blackboard.SetValue(_name, value);
        }

        public ReferenceComponent(DataStore blackboard, string name) : base()
        {
            _blackboard = blackboard;
            _name = name;
        }
    }
}