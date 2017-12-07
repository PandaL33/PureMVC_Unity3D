/**********************************
*	作者：李国煌
*  时间：2017-07-19
*  描述：
*  修改人：
*  修改时间：
**********************************/
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using System.Collections.Generic;
using PureMVC.Interfaces;

namespace Unitech.Net.UnityDemo
{
    public class LoginMediator : Mediator
    {
        public new const string NAME = "LoginMediator";

        private LoginUIForm View
        {
            get { return (LoginUIForm)ViewComponent; }
        }

        public LoginMediator(LoginUIForm viewComponent)
            : base(NAME, viewComponent)
        {
            View.Btn_Comfirm += btnComfirm;
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        void btnComfirm()
        {
            SendNotification(ProConst.PRO_MVC_BTNLOGIN, null);
        }
    }
}
