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
    public class LoginUIForm : BaseUIForm
    {
        /// <summary>
        /// 场景预览
        /// </summary>
        public System.Action Btn_Comfirm;


        public void Awake()
        {
            //窗体性质
            CurrentUIType.UIForm_ShowMode = UIFormShowMode.HideOther;
            Facade.Instance.RegisterCommand(ProConst.PRO_MVC_BTNLOGIN, typeof(LoginCommand));
            Facade.Instance.RegisterMediator(new LoginMediator(this));
            //事件注册
            RigisterButtonObjectEvent("Btn_Comfirm", p => BtnComfirmClick());
        }

        private void BtnComfirmClick()
        {
            if (Btn_Comfirm != null)
                Btn_Comfirm();
        }
    }
}
