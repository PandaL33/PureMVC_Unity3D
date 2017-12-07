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
    public class MarketProxy : Proxy
    {

        public new const string NAME = "MarketProxy";

        public MarketData MarketData
        {
            get { return (MarketData)base.Data; }
        }

        public MarketProxy()
            : base(NAME, new MarketData())
        {
        }

        public void UpdateData(MarketData market)
        {
            MarketData.Name = market.Name;
            MarketData.Age = market.Age;
            MarketData.Sex = market.Sex;
        }
    }
}
