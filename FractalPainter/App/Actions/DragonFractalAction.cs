using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly Func<DragonSettings, DragonPainter> dragonPainterFactory;

        public DragonFractalAction(Func<DragonSettings, DragonPainter> dragonPainterFactory)
        {
            this.dragonPainterFactory = dragonPainterFactory;
        }
        //private readonly IDragonPainterFactory dragonPainterFactory;

        // public DragonFractalAction(IDragonPainterFactory dragonPainterFactory)
        // {
        //     this.dragonPainterFactory = dragonPainterFactory;
        // }


        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";
        public int Order => 2;

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();

            dragonPainterFactory(dragonSettings).Paint();
        }
        private static DragonSettings CreateRandomSettings()
        {
            return new DragonSettingsGenerator(new Random()).Generate();
        }
    }
}