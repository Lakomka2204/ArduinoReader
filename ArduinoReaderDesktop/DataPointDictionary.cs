using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReaderDesktop
{
    public class DataPointDictionary : Dictionary<int, DataPointCollection>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public new void Add(int key, DataPointCollection value)
        {
            base.Add(key, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value as object, key));
        }
        public new bool Remove(int key)
        {
            if (TryGetValue(key, out DataPointCollection? value) && base.Remove(key))
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value));
                return true;
            }
            return false;
        }
        public new DataPointCollection this[int key]
        {
            get => base[key];
            set
            {
                if (TryGetValue(key, out DataPointCollection? oldValue))
                {
                    base[key] = value;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<int, DataPointCollection>(key, value), new KeyValuePair<int, DataPointCollection>(key, oldValue)));
                }
                else
                {
                    base[key] = value;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<int, DataPointCollection>(key, value)));
                }
            }
        }
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
    }
}
