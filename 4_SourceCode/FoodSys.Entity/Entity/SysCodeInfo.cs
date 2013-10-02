using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Project.Common;

namespace FoodSys.Entity
{
    /// <summary>
    /// 代码组信息表
    /// </summary>
    public class SysCodeInfo_Validation
    {
        /// <summary>
        /// ID
        /// Length :
        /// </summary>
        public virtual System.Guid ID { get; set; }

        /// <summary>
        /// 代码组类型
        /// Length : 100
        /// </summary>
        public virtual System.String Type { get; set; }

        /// <summary>
        /// 名称
        /// Length : 100
        /// </summary>
        [Required(ErrorMessage = "请输入名称！")]
        [Remote("RemoteCheckSysCodeInfoName", "SysCodeInfo", "ModuleSys", AdditionalFields = "ID,SysCodeInfoCode", ErrorMessage = "此名称已经存在！")]
        public virtual System.String Name { get; set; }

        /// <summary>
        /// 代码
        /// Length : 20
        /// </summary>
        [Required(ErrorMessage = "请输入代码！")]
        [Remote("RemoteCheckSysCodeInfoCode", "SysCodeInfo", "ModuleSys", AdditionalFields = "ID,SysCodeInfoCode,Type", ErrorMessage = "此代码已经存在！")]
        public virtual System.String Code { get; set; }

        /// <summary>
        ///
        /// Length : 20
        /// </summary>
        public virtual System.String ParentCode { get; set; }

        /// <summary>
        /// 顺序
        /// Length :
        /// </summary>
        [Required(ErrorMessage = "请输入排序！")]
        [Range(1, int.MaxValue, ErrorMessage = "请输入大于等于1的数")]
        [RegularExpression(RegexHelper.Int, ErrorMessage = "请输入数字！")]
        public virtual System.Nullable<Int32> SortOrder { get; set; }

        /// <summary>
        /// 是否有效
        /// Length :
        /// </summary>
        public virtual System.Nullable<Boolean> IsActive { get; set; }

        /// <summary>
        /// 备注
        /// Length : 500
        /// </summary>
        public virtual System.String Memo { get; set; }
    }
}