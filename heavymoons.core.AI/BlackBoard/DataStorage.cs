using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace heavymoons.core.AI.BlackBoard
{
    /// <summary>
    /// 共有パラメーター置き場
    /// ステートマシン、ビヘイビアツリー内で共有するパラメータを保持する
    /// </summary>
    public class DataStorage
    {
        /// <summary>
        /// パラメータ保持用ディクショナリ
        /// </summary>
        private readonly Dictionary<string, IComponent> _components = new Dictionary<string, IComponent>();

        public ReadOnlyCollection<string> Names => _components.Keys.ToList().AsReadOnly();

        /// <summary>
        /// パラメータをインデックスプロパティとして取得
        /// </summary>
        /// <param name="name"></param>
        public object this[string name]
        {
            get { return GetValue(name);}
            set { SetValue(name, value);}
        }

        ~DataStorage()
        {
            Reset();
        }

        public void Reset()
        {
            _components.Clear();
        }

        /// <summary>
        /// 他のDataStorageに保存されている値を参照する
        /// </summary>
        /// <param name="dataStorage"></param>
        /// <param name="name"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ReferTo(DataStorage dataStorage, string name)
        {
            if (_components.ContainsKey(name)) throw new ArgumentException($"already registered name: {name}");

            _components[name] = new ReferenceComponent(dataStorage, name);
        }

        /// <summary>
        /// パラメータを更新する
        /// 登録済みのパラメータの名前、型と一致している必要がある
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private void SetValue(string name, object value)
        {
            if (_components.ContainsKey(name))
            {
                var component = _components[name];
                component.Value = value;
            }
            else
            {
                _components[name] = new Component(value);
            }
        }

        /// <summary>
        /// 登録されているパラメータ値をそのまま取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private object GetValue(string name)
        {
            if (_components.ContainsKey(name)) return _components[name].GetValue<object>();
            throw new ArgumentException($"name not registered: {name}");
        }

        /// <summary>
        /// 登録されているパラメータ値を型指定（＆チェック）して取得
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>(string name)
        {
            if (_components.ContainsKey(name)) return _components[name].GetValue<T>();
            throw new ArgumentException($"name not registered: {name}");
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
    }
}