using System.Collections.Generic;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using Project.Entity.Base;
using System.Web.Mvc;
using Project.Common;
namespace FoodSys.Entity
{

	/// <summary>
	/// 企业基本信息
	/// </summary>
	public class UTOrgCompany_Validation
	{


		/// <summary>
		/// ID
		/// Length : 
		/// </summary>
		public virtual System.Guid ID { get; set; }

		/// <summary>
		/// 企业中文名称
		/// Length : 100
		/// </summary>
		[Required(ErrorMessage = "请输入企业全称！")]
		//[Remote("RemoteCheckOrgCompanyName", "OrgCompany", "ModuleSys", AdditionalFields = "ID,IsNeedCompanyNameValidate", ErrorMessage = "此企业名称已经存在！")]
		[Export(DisplayName = "企业全称")]
		public virtual System.String Name { get; set; }

		/// <summary>
		/// 地址
		/// Length : 100
		/// </summary>
		[Required(ErrorMessage = "请输入企业地址！")]
		[Export(DisplayName = "企业地址")]
		public virtual System.String Address { get; set; }


		/// <summary>
		/// 法人登记证号
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "请输入法人登记证号！")]
		[Export(DisplayName = "法人登记证号")]
		public virtual System.String LegalPersonRegNO { get; set; }


		/// <summary>
		/// 组织机构代码
		/// Length : 9
		/// </summary>
		[Required(ErrorMessage = "请输入组织机构代码！")]
		[Export(DisplayName = "组织机构代码")]
		//[Remote("RemoteCheckCompanyCode", "OrgCompany", "ModuleSys", AdditionalFields = "ID,IsNeedCompanyCodeValidate", ErrorMessage = "此组织机构代码已经存在！")]
		public virtual System.String OrganizationCode { get; set; }


		/// <summary>
		/// 税务登记证号
		/// Length : 50
		/// </summary>
		[Export(DisplayName = "税务登记证号")]
		[Required(ErrorMessage = "请输入税务登记证号！")]
		//[Remote("RemoteCheckTaxRegisterNo", "OrgCompany", "ModuleSys", AdditionalFields = "ID,IsNeedTaxRegisterNoValidate", ErrorMessage = "此税务登记证号已经存在！")]
		public virtual System.String TaxRegNO { get; set; }



		/// <summary>
		/// 营业执照号码
		/// Length : 50
		/// </summary>
		[Export(DisplayName = "营业执照代码")]
		[Required(ErrorMessage = "请输入营业执照号码！")]
		//[Remote("RemoteCheckLicenseNo", "OrgCompany", "ModuleSys", AdditionalFields = "ID,IsNeedLicenseNoValidate", ErrorMessage = "此营业执照代码已经存在！")]
		public virtual System.String LicenseNo { get; set; }


		/// <summary>
		/// 社会保险登记证号
		/// Length : 50
		/// </summary>
		[Export(DisplayName = "社会保险登记证号")]
		[Required(ErrorMessage = "请输入社会保险登记证号！")]
		public virtual System.String SocialInsuranceRegNO { get; set; }


		/// <summary>
		/// 住房公积金账户
		/// Length : 50
		/// </summary>
		[Export(DisplayName = "住房公积金账户")]
		[Required(ErrorMessage = "请输入住房公积金账户！")]
		public virtual System.String HousingFundNO { get; set; }


		/// <summary>
		/// 联系人
		/// Length : 50
		/// </summary>
		[Export(DisplayName = "联系人")]
		[Required(ErrorMessage = "请输入企业联系人")]
		public virtual System.String LinkMan { get; set; }


		/// <summary>
		/// 手机
		/// Length : 20
		/// </summary>
		[Required(ErrorMessage = "请输入企业联系电话！")]
		[RegularExpression(RegexHelper.Mobile, ErrorMessage = "请输入正确的电话号码！")]
		[Export(DisplayName = "联系电话")]
		public virtual System.String Mobile { get; set; }


		/// <summary>
		/// 座机
		/// Length : 20
		/// </summary>
		[RegularExpression(RegexHelper.Telephone, ErrorMessage = "请输入正确的电话号码！")]
		[Required(ErrorMessage = "请输入企业联系电话")]
		public virtual System.String Tel { get; set; }


		/// <summary>
		/// 传真
		/// Length : 20
		/// </summary>
		[RegularExpression(RegexHelper.Telephone, ErrorMessage = "请输入正确的传真号码！")]
		[Required(ErrorMessage = "请输入传真!")]
		public virtual System.String Fax { get; set; }


		/// <summary>
		/// Email
		/// Length : 50
		/// </summary>
		[Required(ErrorMessage = "请输入邮箱！")]
		[RegularExpression(RegexHelper.Email,
			ErrorMessage = "请输入正确的Email格式")]
		[Export(DisplayName = "邮箱")]
		public virtual System.String Email { get; set; }


		/// <summary>
		/// 版本号
		/// Length : 
		/// </summary>
		public virtual System.Byte[] Version { get; set; }

	}
}

