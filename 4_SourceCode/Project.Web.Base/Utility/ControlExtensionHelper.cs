using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.Diagnostics.CodeAnalysis;
using System.Web.Routing;
using System.Collections;
using System.Globalization;

namespace Project.Web.Base.Utility
{
	public enum ButtonType
	{
		删除,
		删除_不可用,
		新增,
		新增_不可用,
		编辑,
		编辑_不可用,
		导出XLS,
		导出XLS_不可用,
		导出PDF,
		导出WORD,
		条件查询,
		条件查询_不可用,
		锁定,
		锁定_不可用,
		重置密码,
		重置密码_不可用,
		保存,
		保存_不可用,
		取消,
		取消_不可用,
		确认,
		确认_不可用,
		批量删除,
		批量删除_不可用,
		指定室户型,
		指定室户型_不可用,
		确认选择,
		确认选择_不可用,
		新增报废动资产,
		新增报废动资产_不可用,
		报废,
		报废_不可用,
		替换,
		替换_不可用,
		红冲,
		红冲_不可用,
		显示全部,
		显示全部_不可用,
		打印,
		打印_不可用,
        扫描签章,
        扫描签章_不可用

	}

	/// <summary>
	/// 按钮扩展方法
	/// </summary>
	public static class ControlExtensionHelper
	{
		private const string createIconName = "add.png";
		private const string createDisableIconName = "add-disable.png";
		private const string deleteIconName = "delete.gif";
		private const string deleteDisableIconName = "delete-disable.gif";
		private const string editIconName = "edit.png";
		private const string editDisableIconName = "edit-disable.png";
		private const string exportXLSIconName = "exportXLS.gif";
		private const string exportXLSDisableIconName = "exportXLS-disable.gif";
		private const string exportPDFIconName = "exportPDF.png";
		private const string exportWORDIconName = "exportWORD.png";
		private const string scanIconName = "scan.gif";
		private const string scanDisableIconName = "scan-disable.gif";
		private const string lockIconName = "lock.gif";
		private const string lockDisableIconName = "lock-disable.gif";
		private const string resetPwdIconName = "edit.png";
		private const string resetPwdDisableIconName = "edit-disable.png";
		private const string saveIconName = "save.png";
		private const string saveDisableIconName = "save-disable.png";
		private const string cancelIconName = "cancel.png";
		private const string cancelDisableIconName = "cancel-disable.png";
		private const string confirmIconName = "confirm.png";
		private const string confirmDisableIconName = "confirm-disable.png";
		private const string assignRoomTypeIconName = "set.gif";
		private const string assignRoomTypeDisableIconName = "set-disable.gif";
		private const string confirmChooseIconName = "choose.gif";
		private const string confirmChooseDisableIconName = "choose-disable.gif";
		private const string ScrapIconName = "add.png";
		private const string ScrapDisableIconName = "add-disable.png";
		private const string replaceIconName = "replace.png";
		private const string replaceDisableIconName = "replace-disable.png";
		private const string redFlushIconName = "multiSP.gif";
		private const string redFlushDisableIconName = "multiSP-disable.gif";
		private const string showAllIconName = "folderModule.gif";
		private const string showAllDisableIconName = "folderModule-disable.gif";
		private const string printIconName = "print.gif";
		private const string printDisableIconName = "print-disable.gif";
		private const string scanningSignatureIconName = "codebar.png";
		private const string scanningSignatureDisableIconName = "codebar-disable.png";



		/// <summary>
		/// 按钮
		/// </summary>
		/// <param name="htmlHelper"></param>
		/// <param name="onclick"></param>
		/// <param name="buttonType">按钮类型</param>
		/// <returns></returns>
		public static MvcHtmlString Button(this HtmlHelper htmlHelper, ButtonType buttonType, string onclick)
		{
			return ButtonHelper(htmlHelper, buttonType, onclick, null, null);
		}

		public static MvcHtmlString Button(this HtmlHelper htmlHelper, ButtonType buttonType, string onclick, string buttonText)
		{
			return ButtonHelper(htmlHelper, buttonType, onclick, buttonText, null);
		}

		public static MvcHtmlString Button(this HtmlHelper htmlHelper, ButtonType buttonType, string onclick, object htmlAttributes)
		{
			return ButtonHelper(htmlHelper, buttonType, onclick, null, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static MvcHtmlString Button(this HtmlHelper htmlHelper, ButtonType buttonType, string onclick, string buttonText, object htmlAttributes)
		{
			return ButtonHelper(htmlHelper, buttonType, onclick, buttonText, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		private static MvcHtmlString ButtonHelper(HtmlHelper htmlHelper, ButtonType buttonType, string onclick, string buttonText, IDictionary<string, object> htmlAttributes)
		{
			TagBuilder aBuilder = new TagBuilder("a");
			TagBuilder imgBuilder = new TagBuilder("img");
			TagBuilder spanBuilder = new TagBuilder("span");
			if (htmlAttributes != null)
				aBuilder.MergeAttributes(htmlAttributes);
			aBuilder.MergeAttribute("href", string.Format("javascript:{0}", onclick));
			aBuilder.MergeAttribute("style", "color:black;text-decoration: none;");
			aBuilder.MergeAttribute("class", "toolbar-button");
			switch (buttonType)
			{
				case ButtonType.删除:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + deleteIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.删除.ToString());
					break;
				case ButtonType.删除_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + deleteDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.删除_不可用.ToString());
					break;
				case ButtonType.新增:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + createIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.新增.ToString());
					break;
				case ButtonType.新增_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + createDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.新增_不可用.ToString());
					break;
				case ButtonType.编辑:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + editIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.编辑.ToString());
					break;
				case ButtonType.编辑_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + editDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.编辑_不可用.ToString());
					break;
				case ButtonType.导出XLS:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + exportXLSIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.导出XLS.ToString());
					break;
				case ButtonType.导出XLS_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + exportXLSDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.导出XLS_不可用.ToString());
					break;
				case ButtonType.导出PDF:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + exportPDFIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.导出PDF.ToString());
					break;
				case ButtonType.导出WORD:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + exportWORDIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.导出WORD.ToString());
					break;
				case ButtonType.条件查询:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + scanIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.条件查询.ToString());
					break;
				case ButtonType.条件查询_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + scanDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.条件查询_不可用.ToString());
					break;
				case ButtonType.锁定:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + lockIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.锁定.ToString());
					break;
				case ButtonType.锁定_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + lockDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.锁定_不可用.ToString());
					break;
				case ButtonType.重置密码:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + resetPwdIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.重置密码.ToString());
					break;
				case ButtonType.重置密码_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + resetPwdDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.重置密码_不可用.ToString());
					break;
				case ButtonType.保存:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + saveIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.保存.ToString());
					break;
				case ButtonType.保存_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + saveDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.保存_不可用.ToString());
					break;
				case ButtonType.取消:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + cancelIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.取消.ToString());
					break;
				case ButtonType.取消_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + cancelDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.取消_不可用.ToString());
					break;
				case ButtonType.确认:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + confirmIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.确认.ToString());
					break;
				case ButtonType.确认_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + confirmDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.确认_不可用.ToString());
					break;
				case ButtonType.批量删除:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + deleteIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.批量删除.ToString());
					break;
				case ButtonType.批量删除_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + deleteDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.批量删除_不可用.ToString());
					break;
				case ButtonType.指定室户型:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + assignRoomTypeIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.指定室户型.ToString());
					break;
				case ButtonType.指定室户型_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + assignRoomTypeDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.指定室户型_不可用.ToString());
					break;
				case ButtonType.确认选择:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + confirmChooseIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.确认选择.ToString());
					break;
				case ButtonType.确认选择_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + confirmChooseDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.确认选择_不可用.ToString());
					break;
				case ButtonType.新增报废动资产:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + ScrapIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.新增报废动资产.ToString());
					break;
				case ButtonType.新增报废动资产_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + ScrapDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.新增报废动资产_不可用.ToString());
					break;
				case ButtonType.报废:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + ScrapIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.报废.ToString());
					break;
				case ButtonType.报废_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + ScrapDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.报废_不可用.ToString());
					break;
				case ButtonType.替换:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + replaceIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.替换.ToString());
					break;
				case ButtonType.替换_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + replaceDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.替换_不可用.ToString());
					break;
				case ButtonType.红冲:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + redFlushIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.红冲.ToString());
					break;
				case ButtonType.红冲_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + redFlushDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.红冲_不可用.ToString());
					break;
				case ButtonType.显示全部:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + showAllIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.显示全部.ToString());
					break;
				case ButtonType.显示全部_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + showAllDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.显示全部_不可用.ToString());
					break;
				case ButtonType.打印:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + printIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.打印.ToString());
					break;
				case ButtonType.打印_不可用:
					imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + printDisableIconName);
					imgBuilder.MergeAttribute("alt", ButtonType.打印_不可用.ToString());
					break;
                case ButtonType.扫描签章:
                    imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + scanningSignatureIconName);
                    imgBuilder.MergeAttribute("alt", ButtonType.扫描签章.ToString());
                    break;
                case ButtonType.扫描签章_不可用:
                    imgBuilder.MergeAttribute("src", "/content/images/tool-bar/" + scanningSignatureDisableIconName);
                    imgBuilder.MergeAttribute("alt", ButtonType.扫描签章_不可用.ToString());
                    break;
				default:
					break;
			}

			imgBuilder.MergeAttribute("alt", buttonText == null ? buttonType.ToString() : buttonText, true);
			spanBuilder.InnerHtml = string.Format("{0}{1}", imgBuilder.ToString(), buttonText == null ? "&nbsp;" + buttonType.ToString() : "&nbsp;" + buttonText);

			aBuilder.InnerHtml = spanBuilder.ToString();
			return new MvcHtmlString(aBuilder.ToString());
		}

		public static MvcHtmlString ThOrderHelper<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> propertyExpression, string headerText, object thAttributes = null, object aAttributes = null)
		{
			var dictionaryThAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(thAttributes);
			var dictionaryaAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(aAttributes);
			MemberExpression memberExpr = null;
			if (propertyExpression.Body.NodeType == ExpressionType.Convert)
				memberExpr = ((UnaryExpression)propertyExpression.Body).Operand as MemberExpression;
			else if (propertyExpression.Body.NodeType == ExpressionType.MemberAccess)
				memberExpr = propertyExpression.Body as MemberExpression;
			TagBuilder thTagBuilder = new TagBuilder("th");
			TagBuilder aTagBuilder = new TagBuilder("a");
			if (thAttributes != null)
				thTagBuilder.MergeAttributes(dictionaryThAttributes);
			if (aAttributes != null)
				aTagBuilder.MergeAttributes(dictionaryaAttributes);
			aTagBuilder.MergeAttribute("href", string.Format("javascript:defaultTable.fieldOrder('{0}');", memberExpr.Member.Name));
			aTagBuilder.SetInnerText(headerText);
			thTagBuilder.MergeAttribute("data-order-field", memberExpr.Member.Name);
			thTagBuilder.InnerHtml = aTagBuilder.ToString();
			return new MvcHtmlString(thTagBuilder.ToString());
		}

	}

	#region DropdownListFor 复杂model无法工作的替换

	/// <summary>
	/// 复杂model无法工作的替换
	/// </summary>
	public static class BugFixDropDownListFor
	{
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString DropDownListForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
		{
			return DropDownListForFix(htmlHelper, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString DropDownListForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return DropDownListForFix(htmlHelper, expression, selectList, null /* optionLabel */, new RouteValueDictionary(htmlAttributes));
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString DropDownListForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return DropDownListForFix(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString DropDownListForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
		{
			return DropDownListForFix(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString DropDownListForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
		{
			return DropDownListForFix(htmlHelper, expression, selectList, optionLabel, new RouteValueDictionary(htmlAttributes));
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString DropDownListForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

			IDictionary<string, object> validationAttributes = htmlHelper
				.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

			if (htmlAttributes == null)
				htmlAttributes = validationAttributes;
			else
				htmlAttributes = htmlAttributes.Concat(validationAttributes).ToDictionary(k => k.Key, v => v.Value);

			return SelectExtensions.DropDownListFor(htmlHelper, expression, selectList, optionLabel, htmlAttributes);
		}
	}
	#endregion

	#region TextareaFor 复杂model无法工作的替换

	/// <summary>
	/// 复杂model无法工作的替换
	/// </summary>
	public static class BugFixTextareaFor
	{
		public static MvcHtmlString TextAreaForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			return TextAreaForFix<TModel, TProperty>(htmlHelper, expression, 2, 20, null);
		}

		public static MvcHtmlString TextAreaForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
		{
			return TextAreaForFix<TModel, TProperty>(htmlHelper, expression,2,20, htmlAttributes);
		}

		public static MvcHtmlString TextAreaForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
		{
			return TextAreaForFix<TModel, TProperty>(htmlHelper, expression, 2, 20, htmlAttributes);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString TextAreaForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, object htmlAttributes)
		{
			return TextAreaForFix<TModel, TProperty>(htmlHelper, expression, rows, columns, new RouteValueDictionary(htmlAttributes));
		}

		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString TextAreaForFix<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, int columns, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

			IDictionary<string, object> validationAttributes = htmlHelper
				.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

			if (htmlAttributes == null)
				htmlAttributes = validationAttributes;
			else
				htmlAttributes = htmlAttributes.Concat(validationAttributes).ToDictionary(k => k.Key, v => v.Value);

			return TextAreaExtensions.TextAreaFor(htmlHelper, expression, rows, columns, htmlAttributes);
		}
	}
	#endregion

	#region LabelFor 无法添加 htmlAttribute
	public static class LabelExtensions
	{
		public static MvcHtmlString LabelFor<TModel, TValue>(
			this HtmlHelper<TModel> html,
			Expression<Func<TModel, TValue>> expression,
			object htmlAttributes
		)
		{
			return LabelHelper(
				html,
				ModelMetadata.FromLambdaExpression(expression, html.ViewData),
				ExpressionHelper.GetExpressionText(expression),
				htmlAttributes
			);
		}

		private static MvcHtmlString LabelHelper(
			HtmlHelper html,
			ModelMetadata metadata,
			string htmlFieldName,
			object htmlAttributes
		)
		{
			string resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
			if (string.IsNullOrEmpty(resolvedLabelText))
			{
				return MvcHtmlString.Empty;
			}

			TagBuilder tag = new TagBuilder("label");
			tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName)));
			tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			tag.SetInnerText(resolvedLabelText);
			return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
		}
	}
	#endregion

	#region RadioButtonList 自定义
	public static class RadioButtonListExtensions
	{
		/// <summary>
		/// DropDownListForCustom
		/// </summary>
		#region RadioButtonListFor
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
		{
			return RadioButtonListFor(htmlHelper, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return RadioButtonListFor(htmlHelper, expression, selectList, null /* optionLabel */, new RouteValueDictionary(htmlAttributes));
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return RadioButtonListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
		{
			return RadioButtonListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
		}

		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
		{
			return RadioButtonListFor(htmlHelper, expression, selectList, optionLabel, new RouteValueDictionary(htmlAttributes));
		}
		static internal object GetModelStateValue(HtmlHelper htmlHelper, string key, Type destinationType)
		{
			ModelState modelState;
			if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
			{
				if (modelState.Value != null)
				{
					return modelState.Value.ConvertTo(destinationType, null /* culture */);
				}
			}
			return null;
		}
		private static SelectListItem GetDefaultSelectedItem(HtmlHelper htmlHelper, IEnumerable<SelectListItem> selectList, string fullName, string optionLabel)
		{
			object defaultValue = GetModelStateValue(htmlHelper, fullName, typeof(string));

			// If we haven't already used ViewData to get the entire list of items then we need to
			// use the ViewData-supplied value before using the parameter-supplied value.
			if (defaultValue == null)
			{
				defaultValue = htmlHelper.ViewData.Eval(fullName);
			}

			SelectListItem defaultValueSelectedItem = new SelectListItem();
			if (defaultValue != null)
			{
				IEnumerable defaultValues = new[] { defaultValue };
				IEnumerable<string> values = from object value in defaultValues select Convert.ToString(value, CultureInfo.CurrentCulture);
				HashSet<string> selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
				if (selectList != null)
					defaultValueSelectedItem = selectList.FirstOrDefault(x => selectedValues.Contains(x.Value));
			}
			if (defaultValueSelectedItem == null && !string.IsNullOrEmpty(optionLabel))
			{
				defaultValueSelectedItem = new SelectListItem()
				{
					Value = "",
					Text = optionLabel,
					Selected = true
				};
			}
			return defaultValueSelectedItem;
		}
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
		public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}

			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

			IDictionary<string, object> validationAttributes = htmlHelper
				.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

			if (htmlAttributes == null)
				htmlAttributes = validationAttributes;
			else
				htmlAttributes = htmlAttributes.Concat(validationAttributes).ToDictionary(k => k.Key, v => v.Value);

			TagBuilder tableTag = new TagBuilder("table");
			if (htmlAttributes != null)
			{
				tableTag.MergeAttributes(htmlAttributes);
			}

			var trTag = new TagBuilder("tr");
			string fullId = FullHtmlFieldId<TModel, TProperty>(htmlHelper, expression);
			string fullName = FullHtmlFieldName<TModel, TProperty>(htmlHelper, expression);

			SelectListItem defaultValueSelectedItem = GetDefaultSelectedItem(htmlHelper, selectList, fullName, optionLabel);
			var tdTag = new TagBuilder("td");
			if (selectList != null)
			{
				foreach (var item in selectList)
				{
					var divTag = new TagBuilder("div");
					divTag.MergeAttribute("style", "float:left;");
					if (defaultValueSelectedItem.Value == item.Value)
					{
						item.Selected = true;
					}
					else
					{
						item.Selected = false;
					}

					var rbValue = item.Value ?? item.Text;
					var rbText = item.Text ?? item.Value;
					var rbName = fullName + "_" + rbValue;
					MvcHtmlString radioTag = htmlHelper.RadioButton(fullName, rbValue, item.Selected, validationAttributes);;
					var labelTag = new TagBuilder("label");
					labelTag.MergeAttribute("style", "padding-right:5px;");
					labelTag.MergeAttribute("for", rbName);
					labelTag.MergeAttribute("id", rbName + "_label");
					labelTag.InnerHtml = rbText;
					divTag.InnerHtml = radioTag.ToString() + labelTag.ToString();
					tdTag.InnerHtml += divTag.ToString();
				}
			}
			trTag.InnerHtml += tdTag.ToString();
			tableTag.InnerHtml = trTag.ToString();
			return new MvcHtmlString(tableTag.ToString());
		}

		public static string FullHtmlFieldName<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
			return fullName;
		}
		public static string FullHtmlFieldId<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
		{
			string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
			if (!string.IsNullOrEmpty(fullName))
			{
				fullName = fullName.Replace('.', '_');
			}
			return fullName;
		}

		#endregion
	}
	#endregion

}
