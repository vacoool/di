using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();
                container.Bind(x => x
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .BindAllInterfaces());

                // start here
                container.Rebind<ImageSettings>().ToSelf().InSingletonScope();
                container.Rebind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();
                container.Rebind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Rebind<IBlobStorage>().To<FileBlobStorage>();
                container.Rebind<AppSettings, IImageDirectoryProvider>()
                    .ToMethod(context => container.Get<SettingsManager>().Load());


                container.Rebind<Palette>().ToSelf().InSingletonScope();
                container.Rebind<IDragonPainterFactory>().ToFactory();


                // container.Bind<KochPainter>().ToSelf().InSingletonScope();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                throw;
            }
        }
    }
}