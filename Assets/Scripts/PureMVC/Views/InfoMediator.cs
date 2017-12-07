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

namespace Unitech.Net.UnityDemo
{
	public class InfoMediator : Mediator {
        public new const string NAME = "InfoMediator";

        private InfoUIForm View
        {
            get { return (InfoUIForm)ViewComponent; }
        }

        public InfoMediator(InfoUIForm viewComponent)
            : base(NAME, viewComponent)
        {
            View.Btn_Info += BtnInfo_Click;
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        void BtnInfo_Click()
        {
            SendNotification(ProConst.PRO_MVC_INFO_BTNINFO, View);
        }

	}
}
