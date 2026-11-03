# Unity 컨벤션

> AGENT가 Unity 에서 구현할때 참고해야할 기본 규칙

## 입력

- 입력 시스템은 [프로젝트 값](<../../Project Values.md>)을 따른다. 프로젝트 세팅의 `Active Input Handler` 가 이와 다를 경우 사용자에게 보고하고, 패키지·설정을 임의로 변경하지 않는다.
- `New Input System` 으로 입력 구현시 AGENT 는 `InputAction` 혹은 `InputActionReference` 를 **적극 사용**한다.

## 구현 규칙

- Unity 에서 제공하는 [Unity 매뉴얼](https://docs.unity3d.com/kr/current/Manual/scripting.html) 을 기본으로 참고한다.
- 사용자가 구현 방법을 명시했더라도 Unity 기본 기능·기존 에셋을 활용하는 더 적합한 방법을 찾으면 기존 요청과 대안의 동작·장단점·영향을 비교해 역제안하고, 사용자 선택을 받는다.
- 사용자가 구현 방법을 명시하지 않은 경우에도 관련 Unity 기본 기능·기존 에셋의 활용 가능성을 검토하고, 요구사항에 적합하면 사용 방식·선택 이유·필요한 패키지와 설정·영향 파일을 브리핑에서 제안한다.
- Unity 생명주기를 주의하며 참조 순서를 설정한다.
- 순수한 데이터나 단순 계산은 가능하면 일반 C# 클래스로 둔다.
- 설정·스킬·적 패턴 등과 같은 반복 데이터는 필요에 따라 `ScriptableObject`를 검토한다. 데이터 구조는 구현 전 승인한다.
- 이벤트 함수 위치, `Update()` 작성 방식, `CacheComponent()`·`Init()` 분리는 [코드 서식 컨벤션](<Formatting Convention.md>)의 `기본 서식`·`멤버 순서`를 따른다.


## 에셋 사용

- Shader·HLSL·Shader Graph·Material·Texture·Sprite 작업은 [그래픽 공통 컨벤션](<../Graphics/Common Convention.md>)과 해당 분야 문서를 함께 따른다.
- 스크립트 구현만으로 해결하지 않고 유니티 자체적으로 제공하는 에셋을 **적극 사용**한다.
    - 아래 목록은 검토 대상의 예시이며 선택 범위를 제한하지 않는다. 사용자가 언급하지 않았거나 목록에 없는 Unity 기능·에셋도 요구사항에 적합하면 제안한다. 실제 사용 가능 여부와 필요한 패키지·설정은 프로젝트에서 확인한다.
    - 예시:
        ```
        Input Action Asset
        Audio Mixer
        Visual Tree Asset
        Timeline
        Behavior Tree
        State Graph
        ...
        ```
- 프로젝트에 에셋 사용을 위한 패키지가 설치되어 있지 않다면 임의로 설치를 진행하지 않고 사용자에게 보고후 진행한다.

## 참고 사항
- [참조 관리](<Reference Convention.md>)
- [이벤트 수명](<Event Convention.md>)
- [비동기 처리](<Async Convention.md>)
- [환경](<../Work/Environment Convention.md>)
