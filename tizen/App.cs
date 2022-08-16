using ElmSharp;
using Tizen.Applications;
using Tizen.Flutter.Embedding;

namespace Runner
{
    class App : CoreUIApplication
    {
        ElmFlutterView flutterView;

        protected override void OnCreate()
        {
            base.OnCreate();

            Initialize();
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();

            if (flutterView.IsRunning)
            {
                flutterView.Destroy();
            }
        }

        void Initialize()
        {
            Window window = new Window("ElmSharpApp")
            {
                AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180 | DisplayRotation.Degree_270 | DisplayRotation.Degree_90
            };
            window.BackButtonPressed += (s, e) =>
            {
                Exit();
            };
            window.Show();

            var box = new Box(window);
            box.Resize(720, 1000);
            box.SetSizeHintAspect(AspectControl.Both, 720, 1000);
            box.Show();

            var bg = new Background(window)
            {
                Color = Color.White
            };
            bg.SetContent(box);

            var conformant = new Conformant(window);
            conformant.Show();
            conformant.SetContent(bg);

            var button = new Button(window)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = -1,
                Text = "Resize",
            };
            button.Show();
            box.PackEnd(button);

            flutterView = new ElmFlutterView(box);
            if (flutterView.RunEngine())
            {
                GeneratedPluginRegistrant.RegisterPlugins(flutterView.Engine);

                var evasObject = flutterView.EvasObject;
                box.PackEnd(evasObject);

                button.Clicked += (s, e) =>
                {
                    if (flutterView.Width > 400)
                    {
                        flutterView.Resize(400, 600);
                    }
                    else
                    {
                        flutterView.Resize(600, 800);
                    }
                };
            }
        }

        static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            App app = new App();
            app.Run(args);
        }
    }
}
