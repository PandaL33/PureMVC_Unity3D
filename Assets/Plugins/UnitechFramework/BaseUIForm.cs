/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述： UI窗体的父类     
    * 功能： 定义所有UI窗体的父类
    *           定义四个生命周期
    *           1、Display 显示状态
    *           2、Hiding  隐藏状态
    *           3、RePlay  再显示状态
    *           4、Freeze  冻结状态
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;

namespace Unitech.Net.UnityFramework
{
    public class BaseUIForm : MonoBehaviour
    {

        /// <summary>
        /// 
        /// </summary>
        private UIType _currentUIType;

        /// <summary>
        /// 当前UI窗体类型
        /// </summary>
        public UIType CurrentUIType
        {
            get
            {
                return _currentUIType;
            }

            set
            {
                _currentUIType = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseUIForm()
        {
            _currentUIType = new UIType();
        }

        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_currentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _currentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
        public virtual void Hiding()
        {
            gameObject.SetActive(false);
            //取消模态窗体调用
            if (_currentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }
        }

        /// <summary>
        /// 再显示状态
        /// </summary>
        public virtual void ReDisplay()
        {
            gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_currentUIType.UIForm_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _currentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
        public virtual void Freeze()
        {
            gameObject.SetActive(true);
        }

        #region 封装子类常用的方法

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
        protected void RigisterButtonObjectEvent(string buttonName, EventTriggerListener.VoidDelegate delHandle)
        {
            GameObject goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName).gameObject;
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void OpenUIForm(string uiFormName)
        {
            UIManager.Instance.ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
        public void CloseUIForm()
        {
            string strUIFromName = string.Empty;            //处理后的UIFrom 名称
            //int intPosition = -1;

            strUIFromName = GetType().ToString();             //命名空间+类名、
            string[] strName = strUIFromName.Split('.');

            if (strName.Length > 0)
            {
                strUIFromName = strName[strName.Length - 1];
            }

            UIManager.Instance.CloseUIForms(strUIFromName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
        public void SendMessage(string msgType, string msgName, object msgContent)
        {
            KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
            MessageCenter.SendMessage(msgType, kvs);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="messagType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public void ReceiveMessage(string messagType, MessageCenter.DelMessageDelivery handler)
        {
            MessageCenter.AddMsgListener(messagType, handler);
        }

        #endregion
    }
}
