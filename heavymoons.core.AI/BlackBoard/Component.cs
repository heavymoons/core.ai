using System;

namespace heavymoons.core.AI.BlackBoard
{
    /// <summary>
    /// スカラー型以外のパラメータを管理する内部クラス
    /// </summary>
    internal class Component : IComponent
    {
        /// <summary>
        /// 保持する値
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public Component(object value)
        {
            Value = value;
        }

        /// <summary>
        /// 値をそのまま取得
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            return Value;
        }

        /// <summary>
        /// 値を型指定（＆チェック）して取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            if (!(Value is T))
            {
                throw new ArgumentException($"type not {typeof(T)}: {Value.GetType()}");
            }
            return (T) Value;
        }

        /// <summary>
        /// 値を更新
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            Value = value;
        }
    }
}