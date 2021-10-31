using System;
using System.Collections.Generic;
using System.Text;

namespace Recovery.Video.Enum
{
    /// <summary>
    /// 운영 체제에서 파일을 여는 방법을 지정합니다.
    /// </summary>
    public enum EFileMode
    {
        /// <summary>
        /// 새 데이터베이스 파일을 만들도록 지정합니다. 파일이 이미 있으면 IOException 예외가 throw됩니다. IOException.Data["FilePath"]으로 파일명이 전달됩니다.
        /// </summary>
        CreateNew = 1,
        /// <summary>
        /// 새 데이터베이스 파일을 만들도록 지정합니다. 파일이 이미 있으면 해당 파일을 덮어씁니다.
        /// </summary>
        Create = 2,
        /// <summary>
        /// 기존 데이터베이스 파일을 오픈합니다. 기존 데이터베이스 파일이 없으면 IOException 예외가 throw됩니다.
        /// </summary>
        Open = 3,
        /// <summary>
        /// 기존 데이터베이스 파일이 있으면 오픈하고 그렇지 않으면 새 데이터베이스 파일을 만들도록 지정합니다.
        /// </summary>
        OpenOrCreate = 4,
    }
}
