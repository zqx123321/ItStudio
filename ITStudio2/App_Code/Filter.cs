using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Filter 的摘要说明
/// </summary>
public class Filter
{
	public Filter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static bool IsNumeric(string testedStr)//此函数来自14级学长
    {
        if (testedStr == "")
        {
            return false;
        }
        int strLength = testedStr.Length;
        for (int index = 0; index < strLength; index++)
        {
            //改为较大位数的类型。Unicode是16位。
            UInt16 charByte = Convert.ToUInt16(testedStr[index]);
            if (charByte < '0' || charByte > '9')  //ASCII:: ord('0')==48;ord('9')==57
            {
                return false;
            }
        }
        return true;
    }
}