/******************************************************************************  
    * 作者： 李国煌
	* 时间： 2017-07-02
    * 描述：       
    * 修改时间：
    * 修改人：
******************************************************************************/
using UnityEngine;
using System.Collections;
using System;
namespace Unitech.Net.UnityFramework
{
    public class JsonAnlysisException : Exception
    {
        public JsonAnlysisException() : base() { }

        public JsonAnlysisException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
