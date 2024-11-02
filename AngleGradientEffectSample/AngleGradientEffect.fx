//GradientStops를 이용한 선형 그래디언트 샘플
sampler1D colors : register(S1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    //0.5, 0.5 좌표를 중심으로 각도 구하기
    float angle = atan2(uv.y - 0.5, uv.x - 0.5);
    
    //0 ~ 2𝝿 범위의 각도를 0.0 ~ 1.0 범위로 변환하기
    //단, 12시 방향이 기준이 되도록 1.25 만큼 오프셋 한 후 mod 연산 수행
    float x = fmod(angle / (3.14159265359 * 2) + 1.25, 1);
    
    //AngleGradientEffect에 입력된 GradientStops로부터 색상 가져오기
    float4 color = tex1D(colors, x);

    return color;
}