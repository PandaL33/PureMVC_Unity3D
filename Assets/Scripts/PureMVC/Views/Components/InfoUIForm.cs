/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-19
    * 描述：       
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Unitech.Net.UnityFramework;
using PureMVC.Patterns;

namespace Unitech.Net.UnityDemo
{
    public class InfoUIForm : BaseUIForm
    {
        public System.Action Btn_Info;
        public void Awake()
        {
            CurrentUIType.UIForm_Type = UIFormType.Fixed;
            CurrentUIType.UIForm_ShowMode = UIFormShowMode.Normal;

            Facade.Instance.RegisterCommand(ProConst.PRO_MVC_INFO_BTNINFO, typeof(InfoCommand));
            Facade.Instance.RegisterMediator(new InfoMediator(this));
            RigisterButtonObjectEvent("btnInfo", p => btnInfoClick());
        }

        void btnInfoClick()
        {
            if (Btn_Info != null)
            {
                Btn_Info();
            }
        }
    }
}
