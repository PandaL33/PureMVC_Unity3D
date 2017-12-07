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
    public class MainUIForm : BaseUIForm
    {

        public System.Action Btn_Info;
        public System.Action Btn_Skill;

        public void Awake()
        {
            CurrentUIType.UIForm_ShowMode = UIFormShowMode.HideOther;
            Facade.Instance.RegisterCommand(ProConst.PRO_MVC_MAIN_BTNINFO, typeof(MainCommand));
            Facade.Instance.RegisterCommand(ProConst.PRO_MVC_MAIN_BTNSKILL, typeof(MainCommand));
            Facade.Instance.RegisterMediator(new MainMediator(this));
            RigisterButtonObjectEvent("btnInfo", p => btnInfoClick());
            RigisterButtonObjectEvent("btnSkill", p => btnSkillClick());
        }

        public void btnInfoClick()
        {
            if (Btn_Info != null)
            {
                Btn_Info();
            }
        }

        public void btnSkillClick()
        {
            if (Btn_Skill != null)
            {
                Btn_Skill();
            }
        }
    }
}
