/**********************************
*	作者：李国煌
*  时间：2017-07-03
*  描述：PureMVC框架入口Facade
*  修改人：
*  修改时间：
**********************************/
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;

namespace Unitech.Net.UnityFramework
{
    public class ApplicationFacade : Facade
    {

        /// <summary>
        /// 单例外观
        /// </summary>
        public new static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new ApplicationFacade();
                        }
                    }
                }
                return m_instance;
            }
        }

        /// <summary>
        /// 启动通知
        /// </summary>
        /// <param name="startUIForm">启动UIForm名称</param>
        public void StartUp(string startUIForm)
        {
            SendNotification("STARTUP", startUIForm);
        }

        /// <summary>
        /// 初始化控制器命令
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand("STARTUP", typeof(StartUpCommand));
        }
    }
}
