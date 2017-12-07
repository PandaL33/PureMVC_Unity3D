/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-19
    * 描述：       
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unitech.Net.UnityFramework;
using PureMVC.Patterns;

namespace Unitech.Net.UnityDemo
{
    public class MarketUIForm : BaseUIForm
    {

        public Text Title;

        public Text Name;
        public Text Age;
        public Text Sex;

        public void Awake()
        {
            CurrentUIType.UIForm_Type = UIFormType.PopUp;
            CurrentUIType.UIForm_ShowMode = UIFormShowMode.ReverseChange;
            CurrentUIType.UIForm_LucencyType = UIFormLucencyType.ImPenetrable;
            Facade.Instance.RegisterCommand(ProConst.PRO_MVC_MARKET_INFO, typeof(MarketCommand));
            Facade.Instance.RegisterCommand(ProConst.PRO_MVC_MARKET_MAIN, typeof(MarketCommand));
            Facade.Instance.RegisterProxy(new MarketProxy());
            Facade.Instance.RegisterMediator(new MarketMediator(this));
            RigisterButtonObjectEvent("btnCommit", p => CloseUIForm());
        }

        public void ShowData(MarketData marketData)
        {
            Name.text = marketData.Name;
            Age.text = marketData.Age;
            Sex.text = marketData.Sex;
        }
    }
}
