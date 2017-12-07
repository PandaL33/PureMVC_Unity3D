/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-17
    * 描述：消息（传递）中心
    * 功能：负责UI框架中，所有UI窗体中间的数据传值。       
    * 修改时间：
    * 修改人：
******************************************************************************/
using System.Collections.Generic;
namespace Unitech.Net.UnityFramework
{
    public class MessageCenter
    {

        /// <summary>
        /// 委托：消息传递
        /// </summary>
        /// <param name="kv">键值对更新</param>
        public delegate void DelMessageDelivery(KeyValuesUpdate kv);

        /// <summary>
        /// 消息中心缓存集合
        /// string : 数据大的分类，DelMessageDelivery 数据执行委托
        /// </summary>
        public static Dictionary<string, DelMessageDelivery> _dicMessages = new Dictionary<string, DelMessageDelivery>();

        /// <summary>
        /// 增加消息的监听。
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public static void AddMsgListener(string messageType, DelMessageDelivery handler)
        {
            if (!_dicMessages.ContainsKey(messageType))
            {
                _dicMessages.Add(messageType, null);
            }
            _dicMessages[messageType] += handler;
        }

        /// <summary>
        /// 取消消息的监听
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public static void RemoveMsgListener(string messageType, DelMessageDelivery handler)
        {
            if (_dicMessages.ContainsKey(messageType))
            {
                _dicMessages[messageType] -= handler;
            }
        }

        /// <summary>
        /// 取消所有指定消息的监听
        /// </summary>
        public static void ClearALLMsgListener()
        {
            if (_dicMessages != null)
            {
                _dicMessages.Clear();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息的分类</param>
        /// <param name="kv">键值对(对象)</param>
        public static void SendMessage(string messageType, KeyValuesUpdate kv)
        {
            DelMessageDelivery del;

            if (_dicMessages.TryGetValue(messageType, out del))
            {
                if (del != null)
                {
                    //调用委托
                    del(kv);
                }
            }
        }
    }

    /// <summary>
    /// 键值更新对
    /// 功能： 配合委托，实现委托数据传递
    /// </summary>
    public class KeyValuesUpdate
    {
        //键
        private string _Key;
        //值
        private object _Values;

        /*  只读属性  */
        public string Key
        {
            get { return _Key; }
        }
        public object Values
        {
            get { return _Values; }
        }

        public KeyValuesUpdate(string key, object valueObj)
        {
            _Key = key;
            _Values = valueObj;
        }
    }
}
