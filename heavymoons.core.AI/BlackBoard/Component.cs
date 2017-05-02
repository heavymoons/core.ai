namespace heavymoons.core.AI
{
    /// <summary>
    /// スカラー型以外のパラメータを管理する内部クラス
    /// </summary>
    internal class Component
    {
        /// <summary>
        /// 保持する値
        /// </summary>
        public virtual object Value { get; set; }

        public Component()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public Component(object value)
        {
            Value = value;
        }

        /// <summary>
        /// 値を型指定して取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetValue<T>()
        {
            return (T) Value;
        }

        public virtual void SetValue(object value)
        {
            Value = value;
        }
    }
}