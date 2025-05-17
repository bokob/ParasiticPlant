using System;

public class Util
{
    #region Enum 변환
    // string -> enum
    public static T StringToEnum<T>(string str)
    {
        return (T)Enum.Parse(typeof(T), str);
    }

    // int -> enum
    public static T IntToEnum<T>(int i)
    {
        return (T)(object)i;    // 모든 자료형은 object를 상속받으므로
    }
    #endregion

    #region 현재 transform에서 Find
    #endregion
}