using TopCalendar.Utility.UI;

namespace TopCalendar.Utility.Tests.UI
{
	public abstract class observations_for_presentation_model_with_stubbed_view<TPresentationModel, TViewType>
		: observations_for_auto_created_sut_of_type<TPresentationModel>
		where TPresentationModel : PresentationModelFor<TViewType>
		where TViewType : IViewForModel<TViewType,TPresentationModel>
	{
		protected override void EstablishContext()
		{
			ProvideImpelentationOf(Stub<TViewType>());
			base.EstablishContext();
		}
	}
}