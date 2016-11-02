using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.Validation;

namespace CatchUp.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			Mvx.RegisterType<IValidator, Validator>();

			RegisterAppStart<ViewModels.HomeViewModel>();

        }
    }
}
