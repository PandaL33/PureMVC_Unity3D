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
    public class MainMediator : Mediator
    {

   public new const string NAME = "MainMediator";

        private MainUIForm View
        {
            get { return (MainUIForm)ViewComponent; }
        }

        public MainMediator(MainUIForm viewComponent)
            : base(NAME, viewComponent)
        {
            View.Btn_Info += BtnInfo_Click;
            View.Btn_Skill += BtnSkill_Click;
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        void BtnInfo_Click()
        {
            SendNotification(ProConst.PRO_MVC_MAIN_BTNINFO,View);
        }

        void BtnSkill_Click()
        {
            SendNotification(ProConst.PRO_MVC_MAIN_BTNSKILL, View);
        }
	}
}
