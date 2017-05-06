namespace heavymoons.core.AI.BlackBoard
{
    /// <summary>
    /// 他のDataStoreの登録データを参照するコンポーネント
    /// </summary>
    internal class ReferenceComponent : IComponent
    {
        private readonly DataStorage _dataStorage;
        private readonly string _name;

        public object Value
        {
            get { return GetValue(); }
            set { SetValue(value); }
        }

        public object GetValue()
        {
            return _dataStorage[_name];
        }

        public T GetValue<T>()
        {
            return _dataStorage.GetValue<T>(_name);
        }

        public void SetValue(object value)
        {
            _dataStorage[_name] = value;
        }

        public ReferenceComponent(DataStorage dataStorage, string name)
        {
            _dataStorage = dataStorage;
            _name = name;
        }
    }
}