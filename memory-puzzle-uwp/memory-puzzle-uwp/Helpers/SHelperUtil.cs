using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace memory_puzzle_uwp.Helpers
{
    /// <summary>
    /// Holds a collection of helper methods
    /// </summary>
    public class SHelperUtil
    {

        /// <summary>
        /// Convinient method to get value from local storage
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetLocalStorageValue(string key) {
            var localStorage = ApplicationData.Current.LocalSettings;
            if (localStorage.Values.ContainsKey(key))
                return localStorage.Values[key];
            else
                return null;
        }

        /// <summary>
        /// Set key value in localstorage dict
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetLocalStorageValue(string key, object value)
        {
            var localStorage = ApplicationData.Current.LocalSettings;
            localStorage.Values[key] = value;
        }
    }
}
