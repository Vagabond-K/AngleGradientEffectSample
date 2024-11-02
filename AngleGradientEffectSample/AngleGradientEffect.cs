using System;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace AngleGradientEffectSample
{
    [ContentProperty("GradientStops")]
    public class AngleGradientEffect : ShaderEffect
    {
        static AngleGradientEffect()
        {
            pixelShader.UriSource = new Uri($"pack://application:,,,/{typeof(AngleGradientEffect).Assembly.GetName().Name};component/{nameof(AngleGradientEffect)}.ps");

            //아래의 코드는 AngleGradientEffect.ps 파일(AngleGradientEffect.fx를 컴파일한 결과)을 미리 Base64로 저장한 문자열로부터 읽어오는 코드입니다.
            //즉, 위의 UriSource 설정 부분을 지우고, 아래의 SetStreamSource 메서드를 실행하게 하면 현재의 AngleGradientEffect 클래스 소스코드만 복사해서 다른 프로젝트에 바로 사용할 수 있습니다.
            //단, AngleGradientEffect.fx 파일을 수정해야 한다면 아래의 코드는 쓸 수 없습니다.
            //pixelShader.SetStreamSource(new System.IO.MemoryStream(Convert.FromBase64String("AAL///7/HwBDVEFCHAAAAE8AAAAAAv//AQAAABwAAAAAAQAASAAAADAAAAADAAEAAQAGADgAAAAAAAAAY29sb3JzAKsEAAsAAQABAAEAAAAAAAAAcHNfMl8wAE1pY3Jvc29mdCAoUikgSExTTCBTaGFkZXIgQ29tcGlsZXIgMTAuMQCrUQAABQAAD6AAAAC/X66qPDZarr3idjg+UQAABQEAD6AEHam+OPd/PwAAAAAAAIA/UQAABQIAD6AAAADA2w/JPwAAAIDbD0nAUQAABQMAD6CD+SI+AACgPwAAAAAAAAAAHwAAAgAAAIAAAAOwHwAAAgAAAJABCA+gAgAAAwAAAoAAAACwAAAAoAIAAAMAAAiAAABVsAAAAKAjAAACAAABgAAA/4AjAAACAAAEgAAAVYALAAADAQAIgAAAAIAAAKqABgAAAgEAAYABAP+ACgAAAwEAAoAAAKqAAAAAgAIAAAMAAAGAAAAAgQAAqoBYAAAEAAABgAAAAIABAKqgAQD/oAUAAAMAAASAAQAAgAEAVYAFAAADAQABgAAAqoAAAKqABAAABAEAAoABAACAAABVoAAAqqAEAAAEAQACgAEAAIABAFWAAAD/oAQAAAQBAAKAAQAAgAEAVYABAACgBAAABAEAAYABAACAAQBVgAEAVaAFAAADAAAEgAAAqoABAACABAAABAEAAYAAAKqAAgAAoAIAVaAEAAAEAAABgAEAAIAAAACAAACqgFgAAAQAAASAAABVgAIAqqACAP+gAgAAAwAAAYAAAKqAAAAAgAIAAAMAAASAAAAAgAAAAIALAAADAQABgAAA/4AAAFWACgAAAwEAAoAAAFWAAAD/gFgAAAQAAAKAAQAAgAEA/6ABAKqgWAAABAAAAoABAFWAAQCqoAAAVYAEAAAEAAABgAAAVYAAAKqBAAAAgAQAAAQAAAGAAAAAgAMAAKADAFWgIwAAAgAAAoAAAACAEwAAAgAAAoAAAFWAWAAABAAAA4AAAACAAABVgAAAVYFCAAADAAAPgAAA5IABCOSgAQAAAgAID4AAAOSA//8AAA==")));
        }
        private readonly static PixelShader pixelShader = new PixelShader();

        public AngleGradientEffect()
        {
            if (GradientStopsPropertyMetadata.DefaultGradientStopFieldInfo == null)
                GradientStops = new GradientStopCollection();

            PixelShader = pixelShader;
            UpdateShaderValue(ColorsSamplerProperty);
        }

        private static readonly DependencyProperty ColorsSamplerProperty = RegisterPixelShaderSamplerProperty("ColorsSampler", typeof(AngleGradientEffect), 1);

        public GradientStopCollection GradientStops
        {
            get { return (GradientStopCollection)GetValue(GradientStopsProperty); }
            set { SetValue(GradientStopsProperty, value); }
        }

        public static readonly DependencyProperty GradientStopsProperty =
            DependencyProperty.Register("GradientStops", typeof(GradientStopCollection), typeof(AngleGradientEffect), new GradientStopsPropertyMetadata(OnGradientStopsChanged));

        private static void OnGradientStopsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngleGradientEffect effect)
            {
                effect.SetValue(ColorsSamplerProperty, new VisualBrush
                {
                    Visual = new Rectangle
                    {
                        Width = 1,
                        Height = 1,
                        Fill = new LinearGradientBrush(e.NewValue as GradientStopCollection, new Point(0, 0), new Point(1, 0))
                    }
                });
            }
        }

        // 굳이 PropertyMetadata를 재정의 한 이유는 Xaml 디자이너에서 GradientStop을 추가할 때 나오는 경고를 없에고 싶어서입니다.
        class GradientStopsPropertyMetadata : PropertyMetadata
        {
            public static FieldInfo DefaultGradientStopFieldInfo = typeof(PropertyMetadata).GetField("_defaultValue", BindingFlags.NonPublic | BindingFlags.Instance);
            public GradientStopsPropertyMetadata(PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback)
            {
                DefaultValue = DefaultGradientStopFieldInfo.GetValue(GradientBrush.GradientStopsProperty.GetMetadata(typeof(GradientBrush)));
            }
        }
    }
}
