# Angle Gradient Effect Sample

예전부터, 도대체 왜 WPF에 AngleGradientBrush가 없는지 의문이었습니다.  
그래서 이번에 각도 기반 그라데이션 효과를 만들어 보게 되었습니다.

하지만 안타깝게도 WPF의 브러시는 재정의 구현이 불가능했습니다.  
그래서 [ShaderEffect](https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.media.effects.shadereffect?view=windowsdesktop-8.0)
클래스를 기반으로 AngleGradientEffect 클래스를 만들게 되었습니다.  
AngleGradientEffect 클래스에 대한 픽셀 셰이더는 AngleGradientEffect.fx 파일에 작성했습니다.  

샘플 프로그램을 만들면서, 로딩 인디케이터도 함께 만들어 봤습니다.  
혹시라도 이런 기능이 필요했던 분들께 도움이 되었으면 좋겠습니다.  
- 샘플 프로그램 화면

![AngleGradientEffectSample](https://github.com/user-attachments/assets/79f86fd7-c8f7-4ef3-9f75-9096bb6edb93)  



https://github.com/user-attachments/assets/3ef1a3ed-eb4a-4366-9239-0d70a465a854

