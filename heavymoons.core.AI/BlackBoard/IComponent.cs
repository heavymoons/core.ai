namespace heavymoons.core.AI.BlackBoard
{
    public interface IComponent
    {
        object Value { get; set; }
        T GetValue<T>();
        object GetValue();
        void SetValue(object value);

    }
}