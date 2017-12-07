/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述： 通用配置管理器接口   
    * 功能：基于“键值对”配置文件的通用解析      
    * 修改时间：
    * 修改人：
******************************************************************************/
using System.Collections.Generic;
using System;

namespace Unitech.Net.UnityFramework
{
    public interface IConfigManager
    {

        /// <summary>
        /// 只读属性： 应用设置
        /// 功能： 得到键值对集合数据
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// 得到配置文件（AppSeting）最大的数量
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();
    }

    [Serializable]
    internal class KeyValuesInfo
    {
        //配置信息
        public List<KeyValuesNode> ConfigInfo = null;
    }

    [Serializable]
    internal class KeyValuesNode
    {
        //键
        public string Key = null;
        //值
        public string Value = null;
    }
}