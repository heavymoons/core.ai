using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace heavymoons.core.AI
{
    /// <summary>
    /// 共有パラメーター置き場
    /// ステートマシン、ビヘイビアツリー内で共有するパラメータを保持する
    /// </summary>
    public class BlackBoard
    {
        /// <summary>
        /// パラメータ保持用ディクショナリ
        /// </summary>
        private readonly Dictionary<string, Component> _components = new Dictionary<string, Component>();

        internal ReadOnlyDictionary<string, Component> Components => new ReadOnlyDictionary<string, Component>(
            _components);

        public bool HasRegistered => _components.Any();

        /// <summary>
        /// パラメータを名前、値、型を使って登録する
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Register(string name, object value, Type type = null)
        {
            if (_components.ContainsKey(name)) throw new ArgumentException($"already registered name: {name}");

            if (type == null) type = value.GetType();
            _components[name] = new Component(value);
        }

        /// <summary>
        /// パラメータを更新する
        /// 登録済みのパラメータの名前、型と一致している必要がある
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetValue(string name, object value)
        {
            if (_components.ContainsKey(name))
            {
                var component = _components[name];
                component.Value = value;
            }
            else throw new ArgumentException($"name:{name} not registered");
        }

        /// <summary>
        /// 登録されているパラメータ値をそのまま取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public object GetValue(string name)
        {
            if (_components.ContainsKey(name)) return _components[name].GetValue<object>();
            throw new ArgumentException($"name not registered: {name}");
        }

        /// <summary>
        /// 登録されているパラメータ値を型指定して取得
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>(string name)
        {
            var value = GetValue(name);
            return (T) value;
        }

        /// <summary>
        /// 同じ型が一つしか登録されていない場合、型指定だけで名前なしで値が取得できる
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T GetValue<T>()
        {
            var values = _components.Values.Where(component => component.Value is T).ToList();
            if (values.Count == 1)
            {
                return values.First().GetValue<T>();
            }
            throw new InvalidOperationException($"type has multiple or no parameter");
        }

        /// <summary>
        /// スカラー型以外のパラメータを管理する内部クラス
        /// </summary>
        internal class Component
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
            /// 値を型指定して取得
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public T GetValue<T>()
            {
                return (T) Value;
            }
        }
    }
}